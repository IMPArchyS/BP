using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{
    private Button button;
    private void Awake()
    {
        try
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => SoundManager.Instance.PlaySfx("BUTTON_CLICK"));
        }
        catch (Exception) { }
    }
}
