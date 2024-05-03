using UnityEngine;

public class OverviewCanvas : MonoBehaviour
{
    [SerializeField] private Canvas ovmCanvas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            bool isFpsCameraOn = PlayerController.Instance.FpsCameraOn;
            ovmCanvas.gameObject.SetActive(isFpsCameraOn);
        }
        else if (PlayerController.Instance.FpsCameraOn)
        {
            ovmCanvas.gameObject.SetActive(false);
        }
    }
}
