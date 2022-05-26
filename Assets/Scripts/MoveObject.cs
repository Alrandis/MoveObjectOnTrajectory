using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MoveObject : MonoBehaviour
{
    public MovePath movePath; // ���� ������������ �������
    public float distanceToPoint = 0.1f; // ��������� �� ������� ������ ������������ ������ �� �����, ����� ������ ��� �� � �����

    private IEnumerator<Vector3> pointInPath; // ����� � ����

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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter)) // ������ �������� �������� ���� Space ���� Enter
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
