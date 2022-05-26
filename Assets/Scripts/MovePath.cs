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
    // ����� ���� � ������� �����, ����� ���� ����� ����� �� ����� ���������� ����� ��������� ������
    public void OnDrawGizmos()
    {
        // ���� ����� ����������� ��� �� ������ 2, �� �� ���� �� �����
        if(item.pathPoints == null || item.pathPoints.Count < 4)
        {
            return;
        }
        
        // ����� ���� ����� �������
        for (var i = 1; i < item.pathPoints.Count; i++)
        {
            //int sigmentsCount = 20;

            //for (int l = 0; l < sigmentsCount; l +=4)
            //{
            //    Vector3 firstPosition = item.pathPoints[i];
            //    float t = (float)i / sigmentsCount;
            //    Vector3 point = GetPoint(item.pathPoints[i], item.pathPoints[i + 1], item.pathPoints[i + 2], item.pathPoints[i + 3], t);
            //    Gizmos.DrawLine(firstPosition, point);

            //}

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
