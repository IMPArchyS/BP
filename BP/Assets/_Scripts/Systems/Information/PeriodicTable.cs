using UnityEngine;

public class PeriodicTable : MonoBehaviour
{
    [SerializeField] private Canvas periodicTableCanvas;

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void ToggleTable()
    {
        if (periodicTableCanvas.gameObject.activeInHierarchy)
        {
            periodicTableCanvas.gameObject.SetActive(false);
            PlayerController.Instance.InMenu = false;
        }
        else
        {
            PlayerController.Instance.InMenu = true;
            periodicTableCanvas.gameObject.SetActive(true);
        }
    }
}
