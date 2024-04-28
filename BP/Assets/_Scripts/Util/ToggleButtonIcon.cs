using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButtonIcon : MonoBehaviour
{
    [SerializeField] Sprite originalImageSprite;
    [SerializeField] Sprite alternateImageSprite;
    private bool toggled = false;

    private void Start()
    {
        Image currentImage = GetComponent<Image>();
        currentImage.sprite = originalImageSprite;
    }

    public void ToggleButtonImage()
    {
        Image currentImage = GetComponent<Image>();
        if (!toggled)
        {
            toggled = true;
            currentImage.sprite = alternateImageSprite;
        }
        else
        {
            toggled = false;
            currentImage.sprite = originalImageSprite;
        }
    }
}
