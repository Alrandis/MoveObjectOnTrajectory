using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MovePath : MonoBehaviour
{
    // Item � ������� �������� ������ � json �����
    [System.Serializable]
    public class Item
    {
        public int time;
        public int loop;
        public List<Vector3> pathPoints = new List<Vector3>();
    }

    public Item item; // ��������� ������ Item � ������� �������� ������ � json �����
    public int movingTo = 0; // ������ ����� � ������� ���� ���������

    // ����� ���� � ������� �����, ����� ���� ����� ����� �� ����� ���������� ����� ��������� ������
    public void OnDrawGizmos()
    {
        // ���� ����� ����������� ��� �� ������ 2, �� �� ���� �� �����
        if(item.pathPoints == null || item.pathPoints.Count < 2)
        {
            return;
        }
        // ����� ���� ����� �������
        for (var i = 1; i < item.pathPoints.Count; i++)
        {
            Gizmos.DrawLine(item.pathPoints[i-1], item.pathPoints[i]);
        }
        // ���������� ���� ���� loop = 1
        if (item.loop == 1)
        {
            Gizmos.DrawLine(item.pathPoints[0], item.pathPoints[item.pathPoints.Count - 1]);
        }

    }

    // ����������� ��������� �����
    public IEnumerator<Vector3> GetNextPoint()
    {
        // ���� ����������� ����� ������ 1 ������� �� �����
        if (item.pathPoints.Count < 1)
        {
            yield break;
        }

        while (true)
        {
            yield return item.pathPoints[movingTo]; // ���������� � ������� �������� movingTo

            // ���� ����� ���� ��������� ����������
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

            

            // ���� ���������� ���������, �������� ��������� ����� � ���������
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

    // ���� ������ �� ����� json
    private void Start()
    {
        //item = JsonUtility.FromJson<Item>(File.ReadAllText(Application.streamingAssetsPath + "/Json.json"));
    }

 

}
