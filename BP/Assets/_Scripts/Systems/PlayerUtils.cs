using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUtils : MonoBehaviour
{
    #region UIElements
    [SerializeField] private Canvas playerUtilCanvas;
    [SerializeField] private TextMeshProUGUI playerSpeedValueText;
    [SerializeField] private TextMeshProUGUI playerFOVValueText;
    [SerializeField] private Slider speedSlider;
    [SerializeField] private Slider fovSlider;
    #endregion

    private void Start()
    {
        playerSpeedValueText.text = PlayerController.Instance.Speed.ToString();
        speedSlider.value = PlayerController.Instance.Speed;
        fovSlider.value = PlayerController.Instance.FpsCamera.fieldOfView;
        playerFOVValueText.text = PlayerController.Instance.FpsCamera.fieldOfView.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && PlayerController.Instance.FpsCameraOn) ToggleUtilCanvas();
        if (playerUtilCanvas.gameObject.activeInHierarchy) PlayerController.Instance.InMenu = true;
    }

    public void ToggleUtilCanvas()
    {
        if (playerUtilCanvas.gameObject.activeInHierarchy)
        {
            PlayerController.Instance.InMenu = false;
            PlayerController.Instance.SetCursorBasedOnCam();
            playerUtilCanvas.gameObject.SetActive(false);
        }
        else
        {
            PlayerController.Instance.InMenu = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            playerUtilCanvas.gameObject.SetActive(true);
        }
    }

    public void AdjustPlayerSpeed(float val)
    {
        playerSpeedValueText.text = val.ToString();
        PlayerController.Instance.Speed = val;
        PlayerController.Instance.UpdateSprintSpeed();
    }

    public void AdjustPlayerFov(float val)
    {
        playerFOVValueText.text = val.ToString();
        PlayerController.Instance.FpsCamera.fieldOfView = val;
    }

    public void ResetToDefaultValues()
    {
        AdjustPlayerSpeed(10f);
        AdjustPlayerFov(65f);
        MainTimeController.Instance.ResetTimeScale();
        speedSlider.value = PlayerController.Instance.Speed;
        fovSlider.value = PlayerController.Instance.FpsCamera.fieldOfView;
    }
}
