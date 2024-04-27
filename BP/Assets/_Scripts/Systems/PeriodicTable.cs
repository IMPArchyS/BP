using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        }
        else
        {
            periodicTableCanvas.gameObject.SetActive(true);
        }
    }
}
