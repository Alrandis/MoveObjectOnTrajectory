using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MoveObject : MonoBehaviour
{
    public MovePath movePath; // Путь передвижения объекта
    public float distanceToPoint = 0.1f; // Растояние на которое должен приблизиться объект до точки, чтобы понять что он в точке

    private IEnumerator<Vector3> pointInPath; // Точка в пути
    public int i;

    private void Start()
    {
        // Проверка существует ли путь для передвижения
        if(movePath == null) 
        {
            return;
        }

        pointInPath = movePath.GetNextPoint(); // Обращаюсь к ранее созданой корутине в MovePath - GetNextPoint
        pointInPath.MoveNext(); // Беру следующую точку

        if (pointInPath.Current == null) // Если следующая точка не существует, то прерываю выполнение
        {
            return;
        }

        transform.position = pointInPath.Current; // Делаю текущую позицию объекта равной стартовой точке пути 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Запуск движения нажатием либо Space либо Enter
        {
            if (pointInPath == null || pointInPath.Current == null) // Если путь пустой или текущая точка пустая прерываю выполнение
            {
                return;
            }

            StartCoroutine(DoMove(movePath.item.time, pointInPath.Current));
        }

        
    }

    private IEnumerator DoMove(float time, Vector3 targetPosition)
    {
       for( i = 1; i <= movePath.item.pathPoints.Count; i += 3)
        {
            
            if (movePath.item.loop == 1 && i >= movePath.item.pathPoints.Count)
            {
                float startTime = Time.realtimeSinceStartup;
                float fraction = 0;
                while (fraction < 1f)
                {
                    fraction = Mathf.Clamp01(((Time.realtimeSinceStartup - startTime) / time) * (movePath.item.pathPoints.Count));
                    transform.position = Vector3.Lerp(movePath.item.pathPoints[i-1], movePath.item.pathPoints[1], fraction);
                }
                i = 0;
            }

            if(i < movePath.item.pathPoints.Count)
            {
                float startTime = Time.realtimeSinceStartup;
                float fraction = 0;
                Vector3 startPosition = transform.position;
                
                while (fraction < 1f)
                {
                    fraction = Mathf.Clamp01(((Time.realtimeSinceStartup - startTime) / time) * (movePath.item.pathPoints.Count / 2.95f));
                    transform.position = movePath.GetPoint(movePath.item.pathPoints[i - 1], movePath.item.pathPoints[i], movePath.item.pathPoints[i + 1], movePath.item.pathPoints[i + 2], fraction);
                    yield return null;
                }
            }

            // Проверка достаточно ли близко объект подобрался к точке
            var distanceSqure = (transform.position - pointInPath.Current).sqrMagnitude;
            if (distanceSqure < distanceToPoint * distanceToPoint)
            {
                pointInPath.MoveNext(); // Сдвиг к следующей точке 
            }
            yield return null;
        }

    }
}
