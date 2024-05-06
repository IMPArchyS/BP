using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CelestialObjectInfo : MonoBehaviour
{
    #region Atributes
    [SerializeField] Canvas celestialObjectInfoCanvas;
    [SerializeField] RectTransform celestialObjectInfoBox;
    [SerializeField] CelestialObject currentObject;
    #endregion

    private void Start()
    {

    }

    #region UI Logic
    private void GetObjectData()
    {
        PlayerController.Instance.Ovm.LookedAtObject.TryGetComponent<CelestialObject>(out CelestialObject csObj);
        if (csObj)
        {
            Debug.Log("GetObjectInfoBUTTON : " + csObj);
            ToggleObjectUI(csObj);
        }
    }

    private void UpdateObjectDataUI(CelestialObject foundCelestial)
    {
        Asteroid asteroid = foundCelestial.GetComponent<Asteroid>();
        Moon moon = foundCelestial.GetComponent<Moon>();
        Planet planet = foundCelestial.GetComponent<Planet>();
        Star star = foundCelestial.GetComponent<Star>();

        celestialObjectInfoBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = foundCelestial.objectName;
        // TextMeshProUGUI sizeText = celestialObjectInfoBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        // TextMeshProUGUI typeText = celestialObjectInfoBox.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        // TextMeshProUGUI habitableText = celestialObjectInfoBox.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        // TextMeshProUGUI gasGiantText = celestialObjectInfoBox.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
    }

    private void ToggleObjectUI(CelestialObject foundCelestial)
    {
        if (foundCelestial == null) return;

        ToggleUI();
        if (celestialObjectInfoCanvas.gameObject.activeInHierarchy)
            UpdateObjectDataUI(foundCelestial);
        else
            currentObject = null;
    }
    #endregion

    public void ToggleUI()
    {
        celestialObjectInfoCanvas.gameObject.SetActive(!celestialObjectInfoCanvas.gameObject.activeInHierarchy);
        if (celestialObjectInfoCanvas.gameObject.activeInHierarchy)
        {
            PlayerController.Instance.InMenu = true;
            PlayerController.Instance.InSubMenuOpen = true;
        }
        else
        {
            PlayerController.Instance.InMenu = false;
            PlayerController.Instance.InSubMenuOpen = false;
        }
    }

    public void OnEnterRange(CelestialObject celestialObject)
    {
        Debug.Log("Hello from CSO-Info");
        currentObject = celestialObject;
    }

    public void OnExitRange()
    {
        Debug.Log("Bye from CSO-Info");
        currentObject = null;
    }

    public void DisplayObjectInfo(bool isOverview)
    {
        if (isOverview)
            GetObjectData();
        else
            ToggleObjectUI(currentObject);
    }

    private void Update()
    {

    }
}
