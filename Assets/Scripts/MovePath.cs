using System.Collections.Generic;
using UnityEngine;


public class MovePath : MonoBehaviour
{

    public LoaderJson json; // ��������� ������ LoaderJson � ������� �������� ������ � json �����
    public int movingTo = 0; // ������ ����� � ������� ���� ���������

    // ����� ���� � ������� �����, ����� ���� ����� ����� �� ����� ���������� ����� ��������� ������
    public void OnDrawGizmos()
    {
        // ���� ����� ����������� ��� �� ������ 2, �� �� ���� �� �����
        if (json.item.pathPoints == null || json.item.pathPoints.Count < 2)
        {
            return;
        }

        // ����� ���� ����� �������
        for (var i = 1; i < json.item.pathPoints.Count; i++)
        {
            Gizmos.DrawLine(json.item.pathPoints[i - 1], json.item.pathPoints[i]);
        }
        // ���������� ���� ���� loop = 1
        if (json.item.loop == true)
        {
            Gizmos.DrawLine(json.item.pathPoints[0], json.item.pathPoints[json.item.pathPoints.Count - 1]);
        }

    }

    // ����������� ��������� �����
    public IEnumerator<Vector3> GetNextPoint()
    {
        // ���� ����������� ����� ������ 1 ������� �� �����
        if (json.item.pathPoints.Count < 1)
        {
            yield break;
        }

        while (true)
        {
            yield return json.item.pathPoints[movingTo]; // ���������� � ������� �������� movingTo

            // ���� ����� ���� ��������� ����������
            if (json.item.pathPoints.Count == 1)
            {
                continue;
            }

            // ���� ���������� �� ���������, �� ������ ��������
            if (json.item.loop == false)
            {
                if (movingTo < json.item.pathPoints.Count - 1)
                {
                    movingTo++;
                }
            }



            // ���� ���������� ���������, �������� ��������� ����� � ���������
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

