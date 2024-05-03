using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIElement : MonoBehaviour
{
    #region UIElements
    [Header("Text Elements")]
    [SerializeField] private TextMeshProUGUI atomicNumberText;
    [SerializeField] private TextMeshProUGUI symbolText;
    [SerializeField] private TextMeshProUGUI fullNameText;
    [SerializeField] private TextMeshProUGUI neutronAmountText;
    [SerializeField] private TextMeshProUGUI electronAmountText;
    [SerializeField] private TextMeshProUGUI atomicMassText;
    [Header("GUI Elements")]
    [SerializeField] private MapImageContainer stateIconsContainer;
    [SerializeField] private Image defaultStateIcon;
    [SerializeField] private Outline outline;
    [SerializeField] private Image bgImage;
    [SerializeField] private Color bgColor;
    #endregion

    #region Data
    [Header("Element Data")]
    [SerializeField] private Element elementData;
    [SerializeField] private bool existsInSolarSystem = false;
    #endregion

    private void Start()
    {
        UpdateUIData();
    }

    private void UpdateDataText()
    {
        atomicNumberText.text = elementData.AtomicNumber.ToString();
        symbolText.text = elementData.Symbol;
        fullNameText.text = TranslateToSlovak.Instance.ElementNameToSlovak(elementData.AtomicNumber);
        neutronAmountText.text = "N:" + elementData.NeutronAmount.ToString();
        electronAmountText.text = "E:" + elementData.ElectronAmount.ToString();
        atomicMassText.text = elementData.AtomicMass.ToString();
    }

    private void UpdateUIData()
    {
        UpdateDataText();
        UpdateGUIData();
        if (existsInSolarSystem) SetColors(bgColor);
        else SetColors(new Color(0.6f, 0.6f, 0.6f, 1));
    }

    private void UpdateGUIData()
    {
        switch (elementData.State)
        {
            case ElementState.Gas:
                defaultStateIcon.sprite = stateIconsContainer.ImageMappings.Find(mapping => mapping.state == ElementState.Gas.ToString()).image;
                break;
            case ElementState.Liquid:
                defaultStateIcon.sprite = stateIconsContainer.ImageMappings.Find(mapping => mapping.state == ElementState.Liquid.ToString()).image;
                break;
            case ElementState.Plasma:
                defaultStateIcon.sprite = stateIconsContainer.ImageMappings.Find(mapping => mapping.state == ElementState.Plasma.ToString()).image;
                break;
            case ElementState.Solid:
                defaultStateIcon.sprite = stateIconsContainer.ImageMappings.Find(mapping => mapping.state == ElementState.Solid.ToString()).image;
                break;
            case ElementState.Unknown:
                defaultStateIcon.sprite = stateIconsContainer.ImageMappings.Find(mapping => mapping.state == ElementState.Unknown.ToString()).image;
                break;
        }
    }

    private void SetColors(Color color)
    {
        bgImage.color = color;
        Color outlineColor = color;
        float h, s, v;
        Color.RGBToHSV(outlineColor, out h, out s, out v);
        outlineColor = Color.HSVToRGB(h, s, v - 0.5f);
        outline.effectColor = outlineColor;
    }

    public void SetExistenceOfElement(string elementName)
    {
        if (elementName == elementData.Name)
        {
            existsInSolarSystem = true;
            SetColors(bgColor);
        }
    }
}
