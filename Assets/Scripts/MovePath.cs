using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MovePath : MonoBehaviour
{
    // Item в котором хранятся данные с json файла
    [System.Serializable]
    public class Item
    {
        public int time;
        public int loop;
        public List<Vector3> pathPoints = new List<Vector3>();
    }

    public Item item; // Экземпляр класса Item в котором хранятся данные с json файла
    public int movingTo = 0; // Индекс точки к которой буду двигаться

    // Рисую путь с помошью гизмо, чтобы было проще онять по какой траектории будет двигаться объект
    public void OnDrawGizmos()
    {
        // Если точки отсутствуют или их меньше 2, то ни чего не рисую
        if(item.pathPoints == null || item.pathPoints.Count < 2)
        {
            return;
        }
        // Рисую путь между точками
        for (var i = 1; i < item.pathPoints.Count; i++)
        {
            Gizmos.DrawLine(item.pathPoints[i-1], item.pathPoints[i]);
        }
        // Зацикливаю путь если loop = 1
        if (item.loop == 1)
        {
            Gizmos.DrawLine(item.pathPoints[0], item.pathPoints[item.pathPoints.Count - 1]);
        }

    }

    // Определение следуюшей точки
    public IEnumerator<Vector3> GetNextPoint()
    {
        // Если колличество точек меньше 1 выходим из цикла
        if (item.pathPoints.Count < 1)
        {
            yield break;
        }

        while (true)
        {
            yield return item.pathPoints[movingTo]; // Возвращает с текущим индексом movingTo

            // Если точка одна прерывает выполнение
            if (item.pathPoints.Count == 1)
            {
                continue;
            }

            if (item.loop == 0)
            {
                if (movingTo < item.pathPoints.Count - 1)
                {
                    movingTo++;
                }
                //else if (movingTo >= item.pathPoints.Count - 1)
                //{
                //    break;
                //}
            }

            

            // Если траектория зациклена, связываю последнюю точку с начальной
            if (item.loop == 1)
            {
                movingTo++;
                if (movingTo >= item.pathPoints.Count)
                {
                    movingTo = 0;
                }
            }
        }
    }

    // Беру данные из файла json
    private void Start()
    {
        //item = JsonUtility.FromJson<Item>(File.ReadAllText(Application.streamingAssetsPath + "/Json.json"));
    }

 

}
