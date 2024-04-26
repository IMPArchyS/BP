using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverviewCanvas : MonoBehaviour
{
    [SerializeField] private Canvas ovmCanvas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && PlayerController.Instance.FpsCameraOn)
            ovmCanvas.gameObject.SetActive(true);
        else if (Input.GetKeyDown(KeyCode.P) && !PlayerController.Instance.FpsCameraOn)
            ovmCanvas.gameObject.SetActive(false);
    }
}
