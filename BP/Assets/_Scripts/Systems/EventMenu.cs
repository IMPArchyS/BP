using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EventMenu : MonoBehaviour
{
    [SerializeField] private Canvas eventMenuCanvas;
    private void Start()
    {

    }

    private void Update()
    {

    }

    public void ToggleEventMenu()
    {
        if (eventMenuCanvas.gameObject.activeInHierarchy)
        {
            eventMenuCanvas.gameObject.SetActive(false);
            PlayerController.Instance.InMenu = false;

        }
        else
        {
            PlayerController.Instance.InMenu = true;
            eventMenuCanvas.gameObject.SetActive(true);
        }
    }
}
