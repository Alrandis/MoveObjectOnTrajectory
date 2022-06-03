using System.Collections;
using UnityEngine;

public class bezie : MonoBehaviour
{
	// В движении по кривой отображение траектории сделал с помошью LineRenderer

	public GameObject moveObject;   // объект который будет двигаться по пути
	// Цвет, ширина и колличество отрезков в одной дуге
	public Color color = Color.white; 
	public float width = 0.2f;
	public int numberOfPoints = 20;
	// Создаю экземпляр класса LineRenderer для отображения линии
	public LineRenderer lineRenderer;
	public LoaderJson json; // Экземпляр класса LoaderJson в котором хранятся данные с json файла

	void Start()
	{
		// Настраиваю LineRenderer 
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.useWorldSpace = true;
		lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
		// Смещаю объект на стартовую позицию
		moveObject.transform.position = json.item.pathPoints[0];
	}


	void Update()
	{
		// Если LineRenderer или точки отсутствуют или их меньше 3 прерываю выполнение
		if (null == lineRenderer || json.item.pathPoints == null || json.item.pathPoints.Count < 3)
		{
			return; 
		}
		// Цвет, ширина линии
		lineRenderer.startColor = color;
		lineRenderer.endColor = color;
		lineRenderer.startWidth = width;
		lineRenderer.endWidth = width;

		// Если колличество точек рисующих траекторию между двумя точками пути меньше 2, то делаю их равными 2
		if (numberOfPoints < 2)
		{
			numberOfPoints = 2;
		}

		// Задаю колличество вершин ресующих траекторию люча на протяжении всего пути
		// по сути перемножаю колличество сегментов между двумя точками на колличество точек по которым движется объект
		lineRenderer.positionCount = numberOfPoints * (json.item.pathPoints.Count - 2);
	

		Vector3 p0, p1, p2; // Создаю промежуточные точки для скривления движения и отрисовки линии
		for (int j = 0; j < json.item.pathPoints.Count - 2; j++)
		{
			// Если первая или вторя или треться точка в пути отсутсвует, то прекращаю расчеты
			if (json.item.pathPoints[j] == null || json.item.pathPoints[j + 1] == null
			|| json.item.pathPoints[j + 2] == null)
			{
				return;
			}
			// Расчитваю промежуточные точки
			p0 = 0.5f * (json.item.pathPoints[j]
			+ json.item.pathPoints[j + 1]);
			p1 = json.item.pathPoints[j + 1];
			p2 = 0.5f * (json.item.pathPoints[j + 1]
			+ json.item.pathPoints[j + 2]);


			// В этот вектор записывается позиция точки на кривой бизье 
			Vector3 position;
			float t;
			float pointStep = 1.0f / numberOfPoints;
			if (j == json.item.pathPoints.Count - 3)
			{
				pointStep = 1.0f / (numberOfPoints - 1.0f);
				// Последний pointStep должен быть равен p2
			}
			// Начинаю отрисовку линии
			for (int i = 0; i < numberOfPoints; i++)
			{
				t = i * pointStep;
				// Расчет точек через которые пойдет луч отрисовывающий путь
				position = (1.0f - t) * (1.0f - t) * p0
				+ 2.0f * (1.0f - t) * t * p1 + t * t * p2;
				lineRenderer.SetPosition(i + j * numberOfPoints, position);
			}
			// Если путь зациклен, то связываю последнюю и началььную точку
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
		// При нажатии на пробел (space) объект начинает движение
		if (Input.GetKeyDown(KeyCode.Space))
		{
			// запуск корутины
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
				// Расчитваю переменную так, чтобы мой объект прошел весь путь за установленный в json временной отрезок
				fraction = Mathf.Clamp01(((Time.realtimeSinceStartup - startTime) / json.item.time) * (json.item.pathPoints.Count-2));
				// Движение обьекта по кривой
				moveObject.transform.position = (1.0f - fraction) * (1.0f - fraction) * p0
				+ 2.0f * (1.0f - fraction) * fraction * p1 + fraction * fraction * p2;
				
				// Эта строчка заставляет объект смотреть в направлении движения. 
				// Реализовал за чет взятия производной от функции движения
				moveObject.transform.rotation = Quaternion.LookRotation(2f * (1.0f - fraction) * (p1 - p0) +
					2f * fraction * (p2 - p1));
				yield return null;
			}
			// Если движение зациклено, возвращаю объект с последней точки на начальную и начинаю движение заново
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
						// Движение обьекта по кривой
						moveObject.transform.position = (1.0f - fraction) * (1.0f - fraction) * p0
						+ 2.0f * (1.0f - fraction) * fraction * p1 + fraction * fraction * p2;
						
						// Эта строчка заставляет объект смотреть в направлении движения. 
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
