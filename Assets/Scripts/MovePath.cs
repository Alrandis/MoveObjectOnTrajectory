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

    public Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 p01 = Vector3.Lerp(p0, p1, t);
        Vector3 p12 = Vector3.Lerp(p1, p2, t);
        Vector3 p23 = Vector3.Lerp(p2, p3, t);

        Vector3 p012 = Vector3.Lerp(p01, p12, t);
        Vector3 p123 = Vector3.Lerp(p12, p23, t);

        Vector3 p0123 = Vector3.Lerp(p012, p123, t);

        return p0123;
    }

    // Рисую путь с помошью гизмо, чтобы было проще онять по какой траектории будет двигаться объект
    public void OnDrawGizmos()
    {
        // Если точки отсутствуют или их меньше 2, то ни чего не рисую
        if(item.pathPoints == null || item.pathPoints.Count < 4)
        {
            return;
        }
        int sigmentsCount = 20;
        Vector3 firstPosition = item.pathPoints[0];
        // Рисую путь между точками
        for (var i = 1; i < item.pathPoints.Count; i+=3)
        {
            for (int l = 0; l <= sigmentsCount; l++)
            {

                float t = (float)l / sigmentsCount;
                Vector3 point = GetPoint(item.pathPoints[i-1], item.pathPoints[i], item.pathPoints[i + 1], item.pathPoints[i + 2], t);
                Gizmos.DrawLine(firstPosition, point);
                firstPosition = point;
            }
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

    //[ContextMenu("Start")]
    //// Беру данные из файла json
    //private void JJJJ()
    //{
    //    item = JsonUtility.FromJson<Item>(File.ReadAllText(Application.persistentDataPath + "/Json.json"));
    //}

 

}
