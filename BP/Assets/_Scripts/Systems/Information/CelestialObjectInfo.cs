using UnityEngine;
using UnityEngine.UI;

public class CelestialObjectInfo : MonoBehaviour
{
    #region Atributes
    [SerializeField] Canvas celestialObjectInfoBox;
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

        // TextMeshProUGUI objectText = celestialObjectInfoBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        // TextMeshProUGUI sizeText = celestialObjectInfoBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        // TextMeshProUGUI typeText = celestialObjectInfoBox.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        // TextMeshProUGUI habitableText = celestialObjectInfoBox.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        // TextMeshProUGUI gasGiantText = celestialObjectInfoBox.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
    }

    private void ToggleObjectUI(CelestialObject foundCelestial)
    {
        celestialObjectInfoBox.gameObject.SetActive(!celestialObjectInfoBox.gameObject.activeInHierarchy);
        if (celestialObjectInfoBox.gameObject.activeInHierarchy)
            UpdateObjectDataUI(foundCelestial);
    }
    #endregion

    public void OnEnterRange(CelestialObject celestialObject)
    {
        Debug.Log("Hello from CSO-Info");
    }

    public void DisplayObjectInfo()
    {
        GetObjectData();
    }

    private void Update()
    {

    }
}
