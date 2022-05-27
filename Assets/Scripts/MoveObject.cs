using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MoveObject : MonoBehaviour
{
    public MovePath movePath; // ���� ������������ �������
    public float distanceToPoint = 0.1f; // ��������� �� ������� ������ ������������ ������ �� �����, ����� ������ ��� �� � �����

    private IEnumerator<Vector3> pointInPath; // ����� � ����
    public int i;

    private void Start()
    {
        // �������� ���������� �� ���� ��� ������������
        if(movePath == null) 
        {
            return;
        }

        pointInPath = movePath.GetNextPoint(); // ��������� � ����� �������� �������� � MovePath - GetNextPoint
        pointInPath.MoveNext(); // ���� ��������� �����

        if (pointInPath.Current == null) // ���� ��������� ����� �� ����������, �� �������� ����������
        {
            return;
        }

        transform.position = pointInPath.Current; // ����� ������� ������� ������� ������ ��������� ����� ���� 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // ������ �������� �������� ���� Space ���� Enter
        {
            if (pointInPath == null || pointInPath.Current == null) // ���� ���� ������ ��� ������� ����� ������ �������� ����������
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

            // �������� ���������� �� ������ ������ ���������� � �����
            var distanceSqure = (transform.position - pointInPath.Current).sqrMagnitude;
            if (distanceSqure < distanceToPoint * distanceToPoint)
            {
                pointInPath.MoveNext(); // ����� � ��������� ����� 
            }
            yield return null;
        }

    }
}
