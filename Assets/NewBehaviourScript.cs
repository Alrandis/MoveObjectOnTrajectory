using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Transform transformObj;
    public Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        transformObj = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(DoMove(3f, targetPosition));
        }
    }

    private IEnumerator DoMove(float time, Vector3 targetPosition)
    {
        Vector3 startPosition = transformObj.position;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0;
        while(fraction< 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time);
            transformObj.position = Vector3.Lerp(startPosition, targetPosition, fraction);
            yield return null;
        }
    }
}
