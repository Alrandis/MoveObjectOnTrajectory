using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bezie : MonoBehaviour
{
	// Это кривая на 3 точках
	//public GameObject start, middle, end;
	//public Color color = Color.white;
	//public float width = 0.2f;
	//public int numberOfPoints = 20;
	//LineRenderer lineRenderer;

	//void Start()
	//{
	//	lineRenderer = GetComponent<LineRenderer>();
	//	lineRenderer.useWorldSpace = true;
	//	lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
	//}

	//void Update()
	//{
	//	if (lineRenderer == null || start == null || middle == null || end == null)
	//	{
	//		return; // no points specified
	//	}

	//	// update line renderer
	//	lineRenderer.startColor = color;
	//	lineRenderer.endColor = color;
	//	lineRenderer.startWidth = width;
	//	lineRenderer.endWidth = width;

	//	if (numberOfPoints > 0)
	//	{
	//		lineRenderer.positionCount = numberOfPoints;
	//	}

	//	// set points of quadratic Bezier curve
	//	Vector3 p0 = start.transform.position;
	//	Vector3 p1 = middle.transform.position;
	//	Vector3 p2 = end.transform.position;
	//	float t;
	//	Vector3 position;
	//	for (int i = 0; i < numberOfPoints; i++)
	//	{
	//		t = i / (numberOfPoints - 1.0f);
	//		position = (1.0f - t) * (1.0f - t) * p0
	//		+ 2.0f * (1.0f - t) * t * p1 + t * t * p2;
	//		lineRenderer.SetPosition(i, position);
	//	}
	//}

	public List<GameObject> controlPoints = new List<GameObject>();
	public GameObject moveObject;
	public Color color = Color.white;
	public float width = 0.2f;
	public int numberOfPoints = 20;
	LineRenderer lineRenderer;
	public int time = 10;
	public int loop = 1;

	void Start()
	{
		moveObject.transform.position = controlPoints[0].transform.position;
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.useWorldSpace = true;
		lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
	}


	void Update()
	{
		if (null == lineRenderer || controlPoints == null || controlPoints.Count < 3)
		{
			return; // not enough points specified
		}
		// update line renderer
		lineRenderer.startColor = color;
		lineRenderer.endColor = color;
		lineRenderer.startWidth = width;
		lineRenderer.endWidth = width;

		if (numberOfPoints < 2)
		{
			numberOfPoints = 2;
		}
		lineRenderer.positionCount = numberOfPoints * (controlPoints.Count - 2);
	

		Vector3 p0, p1, p2;
		for (int j = 0; j < controlPoints.Count - 2; j++)
		{
			// check control points
			if (controlPoints[j] == null || controlPoints[j + 1] == null
			|| controlPoints[j + 2] == null)
			{
				return;
			}
			// determine control points of segment
			p0 = 0.5f * (controlPoints[j].transform.position
			+ controlPoints[j + 1].transform.position);
			p1 = controlPoints[j + 1].transform.position;
			p2 = 0.5f * (controlPoints[j + 1].transform.position
			+ controlPoints[j + 2].transform.position);


			// set points of quadratic Bezier curve
			Vector3 position;
			float t;
			float pointStep = 1.0f / numberOfPoints;
			if (j == controlPoints.Count - 3)
			{
				pointStep = 1.0f / (numberOfPoints - 1.0f);
				// last point of last segment should reach p2
			}
			for (int i = 0; i < numberOfPoints; i++)
			{
				t = i * pointStep;
				position = (1.0f - t) * (1.0f - t) * p0
				+ 2.0f * (1.0f - t) * t * p1 + t * t * p2;
				lineRenderer.SetPosition(i + j * numberOfPoints, position);
			}
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StartCoroutine(DoMove());
		}
	}
	private IEnumerator DoMove()
	{
		Vector3 p0, p1, p2;
		for (int j = 0; j < controlPoints.Count - 2; j++)
        {
			p0 = 0.5f * (controlPoints[j].transform.position
			+ controlPoints[j + 1].transform.position);
			p1 = controlPoints[j + 1].transform.position;
			p2 = 0.5f * (controlPoints[j + 1].transform.position
			+ controlPoints[j + 2].transform.position);
            float startTime = Time.realtimeSinceStartup;
            float fraction = 0;

            while (fraction < 1f)
			{
				fraction = Mathf.Clamp01(((Time.realtimeSinceStartup - startTime) / time) * (controlPoints.Count-2));
				moveObject.transform.position = (1.0f - fraction) * (1.0f - fraction) * p0
				+ 2.0f * (1.0f - fraction) * fraction * p1 + fraction * fraction * p2;
				moveObject.transform.rotation = Quaternion.LookRotation(2f * (1.0f - fraction) * (p1 - p0) +
					2f * fraction * (p2 - p1));
				yield return null;
			}
			if(loop == 1)
            {
                if (j == controlPoints.Count - 3)
                {
					p0 = 0.5f * (controlPoints[j + 1].transform.position
					+ controlPoints[j + 2].transform.position);
					p1 = controlPoints[0].transform.position;
					p2 = 0.5f * (controlPoints[1].transform.position
					+ controlPoints[2].transform.position);
					startTime = Time.realtimeSinceStartup;
					fraction = 0;
					while (fraction < 1f)
					{
						fraction = Mathf.Clamp01(((Time.realtimeSinceStartup - startTime) / time) * (controlPoints.Count - 3));
						moveObject.transform.position = (1.0f - fraction) * (1.0f - fraction) * p0
						+ 2.0f * (1.0f - fraction) * fraction * p1 + fraction * fraction * p2;
						moveObject.transform.rotation = Quaternion.LookRotation(2f * (1.0f - fraction) * (p1 - p0) +
						2f * fraction * (p2 - p1));
						yield return null;
					}
					j = 0;
                }
            }
			yield return null;
		}
	

	}
}
