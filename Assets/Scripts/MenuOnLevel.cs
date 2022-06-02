using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOnLevel : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private Camera additionalCamera;
  
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        mainCamera = Camera.main;
        additionalCamera.enabled = false;
    }

    public void ExitMenuPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void SwitchCamersPressed()
    {
        if (mainCamera.enabled == true) {
            mainCamera.enabled = false;
            additionalCamera.enabled = true;
        }
        else
        {
            mainCamera.enabled = true;
            additionalCamera.enabled = false;
        }
    }
}
