using UnityEngine;
using UnityEngine.UI;

public class ToggleButtonIcon : MonoBehaviour
{
    [SerializeField] private Sprite originalImageSprite;
    [SerializeField] private Sprite alternateImageSprite;
    private void Start()
    {
        Image currentImage = GetComponent<Image>();
        currentImage.sprite = originalImageSprite;
        MainTimeController.Instance.OnSimToggle.AddListener(ToggleButtonImage);
    }

    public void ToggleButtonImage(bool toggle)
    {
        Image currentImage = GetComponent<Image>();
        if (toggle)
        {
            currentImage.sprite = alternateImageSprite;
        }
        else
        {
            currentImage.sprite = originalImageSprite;
        }
    }
}
