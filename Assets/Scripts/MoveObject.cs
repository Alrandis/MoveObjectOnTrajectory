using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MoveObject : MonoBehaviour
{
    public MovePath movePath; // Путь передвижения объекта
    public float distanceToPoint = 0.1f; // Растояние на которое должен приблизиться объект до точки, чтобы понять что он в точке

    private IEnumerator<Vector3> pointInPath; // Точка в пути

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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter)) // Запуск движения нажатием либо Space либо Enter
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
        while (pointInPath.Current != null)
        {
            Vector3 startPosition = transform.position;
            float startTime = Time.realtimeSinceStartup;
            float fraction = 0;
            while (fraction < 1f)
            {
                fraction = Mathf.Clamp01(((Time.realtimeSinceStartup - startTime) / time) * (movePath.item.pathPoints.Count - 1));
                transform.position = Vector3.Lerp(startPosition, pointInPath.Current, fraction);
                yield return null;
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
