using System.Collections.Generic;
using UnityEngine;


public class MovePath : MonoBehaviour
{

    public LoaderJson json; // Экземпляр класса LoaderJson в котором хранятся данные с json файла
    public int movingTo = 0; // Индекс точки к которой буду двигаться

    // Рисую путь с помошью гизмо, чтобы было проще онять по какой траектории будет двигаться объект
    public void OnDrawGizmos()
    {
        // Если точки отсутствуют или их меньше 2, то ни чего не рисую
        if (json.item.pathPoints == null || json.item.pathPoints.Count < 2)
        {
            return;
        }

        // Рисую путь между точками
        for (var i = 1; i < json.item.pathPoints.Count; i++)
        {
            Gizmos.DrawLine(json.item.pathPoints[i - 1], json.item.pathPoints[i]);
        }
        // Зацикливаю путь если loop = 1
        if (json.item.loop == true)
        {
            Gizmos.DrawLine(json.item.pathPoints[0], json.item.pathPoints[json.item.pathPoints.Count - 1]);
        }

    }

    // Определение следуюшей точки
    public IEnumerator<Vector3> GetNextPoint()
    {
        // Если колличество точек меньше 1 выходим из цикла
        if (json.item.pathPoints.Count < 1)
        {
            yield break;
        }

        while (true)
        {
            yield return json.item.pathPoints[movingTo]; // Возвращает с текущим индексом movingTo

            // Если точка одна прерывает выполнение
            if (json.item.pathPoints.Count == 1)
            {
                continue;
            }

            // Если траектория не зациклена, то просто двигаюсь
            if (json.item.loop == false)
            {
                if (movingTo < json.item.pathPoints.Count - 1)
                {
                    movingTo++;
                }
            }



            // Если траектория зациклена, связываю последнюю точку с начальной
            if (json.item.loop == true)
            {
                movingTo++;
                if (movingTo >= json.item.pathPoints.Count)
                {
                    movingTo = 0;
                }
            }
        }
    }
}

