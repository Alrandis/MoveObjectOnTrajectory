using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public MovePath movePath; // ���� ������������ �������
    public float distanceToPoint = 0.1f; // ��������� �� ������� ������ ������������ ������ �� �����, ����� ������ ��� �� � �����

    private IEnumerator<Vector3> pointInPath; // ����� � ����

    private void Start()
    {
        // �������� ���������� �� ���� ��� ������������
        if (movePath == null)
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
        if (Input.GetKeyDown(KeyCode.Space)) // ������ �������� �������� Space 
        {
            if (pointInPath == null || pointInPath.Current == null) // ���� ���� ������ ��� ������� ����� ������ �������� ����������
            {
                return;
            }

            // �������� �������� ��� �������� �� ��������� ����
            StartCoroutine(DoMove(movePath.json.item.time, pointInPath.Current));
        }


    }

    private IEnumerator DoMove(float time, Vector3 targetPosition)
    {
        // ���� ������� ����� ����������(����� � ����� ���������� ��� ������)
        while (pointInPath.Current != null)
        {
            // ��������� ������� ��� ������� ������� �� ������ ��������
            Vector3 startPosition = transform.position;
            // ����� ������������� �� ������ ������� ����
            float startTime = Time.realtimeSinceStartup;
            // ������ ��������� t ������ ��� ���������� Lerp
            float fraction = 0;
            while (fraction < 1f)
            {
                // ��������� ���������� ���, ����� ��� ������ ������ ���� ���� �� ������������� � json ��������� �������
                // �� ���� �� ������� ��������� � ������ ���� ������� ����� ������ �������� �� ������ �������
                // � ���� ��� �� ����� ����������� ����������, ����� ������� �� ����������� ����� � ����
                fraction = Mathf.Clamp01(((Time.realtimeSinceStartup - startTime) / time) * (movePath.json.item.pathPoints.Count));
               
                // ������������ �������
                transform.position = Vector3.Lerp(startPosition, pointInPath.Current, fraction);
                // ����������� ����� �� ����������� ��������
                if(startPosition.y >= 4.32)
                {
                    transform.rotation = Quaternion.LookRotation(pointInPath.Current - startPosition);
                }
                yield return null;
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
