using System.Collections;
using UnityEngine;

public class bezie : MonoBehaviour
{
	// � �������� �� ������ ����������� ���������� ������ � ������� LineRenderer

	public GameObject moveObject;   // ������ ������� ����� ��������� �� ����
	// ����, ������ � ����������� �������� � ����� ����
	public Color color = Color.white; 
	public float width = 0.2f;
	public int numberOfPoints = 20;
	// ������ ��������� ������ LineRenderer ��� ����������� �����
	public LineRenderer lineRenderer;
	public LoaderJson json; // ��������� ������ LoaderJson � ������� �������� ������ � json �����

	void Start()
	{
		// ���������� LineRenderer 
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.useWorldSpace = true;
		lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
		// ������ ������ �� ��������� �������
		moveObject.transform.position = json.item.pathPoints[0];
	}


	void Update()
	{
		// ���� LineRenderer ��� ����� ����������� ��� �� ������ 3 �������� ����������
		if (null == lineRenderer || json.item.pathPoints == null || json.item.pathPoints.Count < 3)
		{
			return; 
		}
		// ����, ������ �����
		lineRenderer.startColor = color;
		lineRenderer.endColor = color;
		lineRenderer.startWidth = width;
		lineRenderer.endWidth = width;

		// ���� ����������� ����� �������� ���������� ����� ����� ������� ���� ������ 2, �� ����� �� ������� 2
		if (numberOfPoints < 2)
		{
			numberOfPoints = 2;
		}

		// ����� ����������� ������ �������� ���������� ���� �� ���������� ����� ����
		// �� ���� ���������� ����������� ��������� ����� ����� ������� �� ����������� ����� �� ������� �������� ������
		lineRenderer.positionCount = numberOfPoints * (json.item.pathPoints.Count - 2);
	

		Vector3 p0, p1, p2; // ������ ������������� ����� ��� ���������� �������� � ��������� �����
		for (int j = 0; j < json.item.pathPoints.Count - 2; j++)
		{
			// ���� ������ ��� ����� ��� ������� ����� � ���� ����������, �� ��������� �������
			if (json.item.pathPoints[j] == null || json.item.pathPoints[j + 1] == null
			|| json.item.pathPoints[j + 2] == null)
			{
				return;
			}
			// ��������� ������������� �����
			p0 = 0.5f * (json.item.pathPoints[j]
			+ json.item.pathPoints[j + 1]);
			p1 = json.item.pathPoints[j + 1];
			p2 = 0.5f * (json.item.pathPoints[j + 1]
			+ json.item.pathPoints[j + 2]);


			// � ���� ������ ������������ ������� ����� �� ������ ����� 
			Vector3 position;
			float t;
			float pointStep = 1.0f / numberOfPoints;
			if (j == json.item.pathPoints.Count - 3)
			{
				pointStep = 1.0f / (numberOfPoints - 1.0f);
				// ��������� pointStep ������ ���� ����� p2
			}
			// ������� ��������� �����
			for (int i = 0; i < numberOfPoints; i++)
			{
				t = i * pointStep;
				// ������ ����� ����� ������� ������ ��� �������������� ����
				position = (1.0f - t) * (1.0f - t) * p0
				+ 2.0f * (1.0f - t) * t * p1 + t * t * p2;
				lineRenderer.SetPosition(i + j * numberOfPoints, position);
			}
			// ���� ���� ��������, �� �������� ��������� � ���������� �����
			if (json.item.loop == true)
			{
				if (j == json.item.pathPoints.Count - 3)
				{
					p0 = 0.5f * (json.item.pathPoints[j + 1]
					+ json.item.pathPoints[j + 2]);
					p1 = json.item.pathPoints[0];
					p2 = json.item.pathPoints[1];
					

					for (int i = 0; i < numberOfPoints; i++)
					{
						t = i * pointStep;
						position = (1.0f - t) * (1.0f - t) * p0
						+ 2.0f * (1.0f - t) * t * p1 + t * t * p2;
						lineRenderer.SetPosition(i + j * numberOfPoints, position);
					}
				}
			}
		}
		// ��� ������� �� ������ (space) ������ �������� ��������
		if (Input.GetKeyDown(KeyCode.Space))
		{
			// ������ ��������
			StartCoroutine(DoMove());
		}
	}
	private IEnumerator DoMove()
	{
		Vector3 p0, p1, p2;
		for (int j = 0; j < json.item.pathPoints.Count - 2; j++)
        {
			p0 = 0.5f * (json.item.pathPoints[j]
			+ json.item.pathPoints[j + 1]);
			p1 = json.item.pathPoints[j + 1];
			p2 = 0.5f * (json.item.pathPoints[j + 1]
			+ json.item.pathPoints[j + 2]);
            float startTime = Time.realtimeSinceStartup;
            float fraction = 0;

            while (fraction < 1f)
			{
				// ��������� ���������� ���, ����� ��� ������ ������ ���� ���� �� ������������� � json ��������� �������
				fraction = Mathf.Clamp01(((Time.realtimeSinceStartup - startTime) / json.item.time) * (json.item.pathPoints.Count-2));
				// �������� ������� �� ������
				moveObject.transform.position = (1.0f - fraction) * (1.0f - fraction) * p0
				+ 2.0f * (1.0f - fraction) * fraction * p1 + fraction * fraction * p2;
				
				// ��� ������� ���������� ������ �������� � ����������� ��������. 
				// ���������� �� ��� ������ ����������� �� ������� ��������
				moveObject.transform.rotation = Quaternion.LookRotation(2f * (1.0f - fraction) * (p1 - p0) +
					2f * fraction * (p2 - p1));
				yield return null;
			}
			// ���� �������� ���������, ��������� ������ � ��������� ����� �� ��������� � ������� �������� ������
			if(json.item.loop == true)
            {
                if (j == json.item.pathPoints.Count - 3)
                {
					p0 = 0.5f * (json.item.pathPoints[j + 1]
					+ json.item.pathPoints[j + 2]);
					p1 = json.item.pathPoints[0];
					p2 = 0.5f * (json.item.pathPoints[1]
					+ json.item.pathPoints[2]);
					startTime = Time.realtimeSinceStartup;
					fraction = 0;
					while (fraction < 1f)
					{
						fraction = Mathf.Clamp01(((Time.realtimeSinceStartup - startTime) / json.item.time) * (json.item.pathPoints.Count - 3));
						// �������� ������� �� ������
						moveObject.transform.position = (1.0f - fraction) * (1.0f - fraction) * p0
						+ 2.0f * (1.0f - fraction) * fraction * p1 + fraction * fraction * p2;
						
						// ��� ������� ���������� ������ �������� � ����������� ��������. 
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
