using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public MovePath movePath; // Путь передвижения объекта
    public float distanceToPoint = 0.1f; // Растояние на которое должен приблизиться объект до точки, чтобы понять что он в точке

    private IEnumerator<Vector3> pointInPath; // Точка в пути

    private void Start()
    {
        // Проверка существует ли путь для передвижения
        if (movePath == null)
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
        if (Input.GetKeyDown(KeyCode.Space)) // Запуск движения нажатием Space 
        {
            if (pointInPath == null || pointInPath.Current == null) // Если путь пустой или текущая точка пустая прерываю выполнение
            {
                return;
            }

            // Запускаю корутину для движения по ломанному пути
            StartCoroutine(DoMove(movePath.json.item.time, pointInPath.Current));
        }


    }

    private IEnumerator DoMove(float time, Vector3 targetPosition)
    {
        // Пока текущая точка существует(точка к торой стремиться мой обьект)
        while (pointInPath.Current != null)
        {
            // начальная позиция это позиция обьекта на кажной итерации
            Vector3 startPosition = transform.position;
            // время отсчитывается от начала запуска игры
            float startTime = Time.realtimeSinceStartup;
            // вместо параметра t служит для реализации Lerp
            float fraction = 0;
            while (fraction < 1f)
            {
                // Расчитваю переменную так, чтобы мой объект прошел весь путь за установленный в json временной отрезок
                // по сути от времени прошедшей с начала игры отнимаю время старта движения на данном отрезке
                // и делю это на время прохождения траектории, после умножаю на колличество точек в пути
                fraction = Mathf.Clamp01(((Time.realtimeSinceStartup - startTime) / time) * (movePath.json.item.pathPoints.Count));
               
                // передвижение объекта
                transform.position = Vector3.Lerp(startPosition, pointInPath.Current, fraction);
                // Поворачиваю обект по напровлению движения
                if(startPosition.y >= 4.32)
                {
                    transform.rotation = Quaternion.LookRotation(pointInPath.Current - startPosition);
                }
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
