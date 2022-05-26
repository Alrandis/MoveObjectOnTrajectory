using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public List<Transform> pathPoints;
    public int loop = 1;
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

    private void OnDrawGizmos()
    {
        int sigmentsCount = 20;
        Vector3 firstPosition = pathPoints[0].position;
        for (int i = 0; i < pathPoints.Count-1; i+=3)
        {
            for (int l = 0; l < sigmentsCount; l++)
            {

                float t = (float)l / sigmentsCount;
                Vector3 point = GetPoint(pathPoints[i].position, pathPoints[i + 1].position, pathPoints[i + 2].position, pathPoints[i + 3].position, t);
                Gizmos.DrawLine(firstPosition, point);
                firstPosition = point;
            }
        }
        if (loop == 1)
        {
            Gizmos.DrawLine(pathPoints[0].position, pathPoints[pathPoints.Count - 1].position);
        }
    }

}
