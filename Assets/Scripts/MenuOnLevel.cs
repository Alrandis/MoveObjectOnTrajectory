using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuOnLevel : MonoBehaviour
{
    public LoaderJson json;
    public Slider slider;
    private Camera mainCamera;
    [SerializeField] private Camera additionalCamera;
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        mainCamera = Camera.main;
        additionalCamera.enabled = false;
        slider.value = json.item.time;  
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
