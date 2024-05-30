using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
        celestialObjectInfoBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = foundCelestial.CurrentData.ObjectName;

        // generic data
        TextMeshProUGUI genericBundled = celestialObjectInfoBox.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        genericBundled.text = "hmotnosť: " + foundCelestial.CurrentData.Mass + "\n"
        + "polomer: " + foundCelestial.CurrentData.Radius + "\n"
        + "rýchlosť: " + foundCelestial.CurrentData.Velocity + "\n"
        + "vek: " + foundCelestial.AgeBigInt.ToString("N1") + "\n";

        // atmosphere data
        TextMeshProUGUI atmoBundled = celestialObjectInfoBox.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        string hasAtmoSk = foundCelestial.CurrentData.HasAtmosphere ? "áno" : "nie";
        string composition = "zloženie: ";
        foreach (Element e in foundCelestial.CurrentData.AtmosphereComposition)
        {
            composition += e.Symbol + ", ";
        }
        atmoBundled.text = "obsahuje: " + hasAtmoSk + "\n"
        + "atmosférický tlak: " + foundCelestial.CurrentData.AtmospherePressure + "\n"
        + "zloženie: " + composition;

        // orbit data
        TextMeshProUGUI orbitBundled = celestialObjectInfoBox.transform.GetChild(6).GetComponent<TextMeshProUGUI>();
        orbitBundled.text = "priemer: " + foundCelestial.CurrentData.OrbitalRadius + "\n"
        + "doba: " + foundCelestial.CurrentData.OrbitalPeriod + "\n"
        + "excentricita: " + foundCelestial.CurrentData.OrbitalEccentricity + "\n"
        + "sklon: " + foundCelestial.CurrentData.Inclination;

        // ground data
        TextMeshProUGUI groundBundled = celestialObjectInfoBox.transform.GetChild(8).GetComponent<TextMeshProUGUI>();
        string compositionGround = "zloženie: ";
        foreach (Element e in foundCelestial.CurrentData.GroundElements)
        {
            compositionGround += e.Symbol + ", ";
        }
        groundBundled.text = "popis: " + foundCelestial.CurrentData.Surface + "\n"
        + "teplota (min): " + foundCelestial.CurrentData.MinTemperature + "\n"
        + "teplota (max): " + foundCelestial.CurrentData.MaxTemperature + "\n"
        + "teplota (priemer): " + foundCelestial.CurrentData.AverageTemperature + "\n"
        + compositionGround;

        // magnetic field data
        TextMeshProUGUI magnetBundled = celestialObjectInfoBox.transform.GetChild(10).GetComponent<TextMeshProUGUI>();
        string hasGravitySk = foundCelestial.CurrentData.HasMagneticField ? "má" : "nemá";
        magnetBundled.text = "pole: " + hasGravitySk + "\n"
        + "konštanta: " + foundCelestial.CurrentData.Gravity + "\n";

        // specific data based on type
        TextMeshProUGUI specificBundled = celestialObjectInfoBox.transform.GetChild(12).GetComponent<TextMeshProUGUI>();
        GetSpecificInfoToText(foundCelestial, specificBundled);
    }

    private void GetSpecificInfoToText(CelestialObject foundCelestial, TextMeshProUGUI specificBundled)
    {
        TextMeshProUGUI specificHeader = celestialObjectInfoBox.transform.GetChild(11).GetComponent<TextMeshProUGUI>();
        Asteroid asteroid = foundCelestial.GetComponent<Asteroid>();
        Moon moon = foundCelestial.GetComponent<Moon>();
        Planet planet = foundCelestial.GetComponent<Planet>();
        Star star = foundCelestial.GetComponent<Star>();

        if (asteroid != null)
        {
            specificHeader.text = "Asteroid";
        }
        else if (moon != null)
        {
            specificHeader.text = "Mesiac";
            // Handle moon
        }
        else if (planet != null)
        {
            specificHeader.text = "Planéta";
            string hasMoonsSk = planet.CurrentData.HasMoons ? "má" : "nemá";
            string hasRingsSk = planet.CurrentData.HasRings ? "má" : "nemá";
            string planetMoonNames = "";
            foreach (Moon m in planet.CurrentData.Moons)
            {
                planetMoonNames += m.GetComponent<GenericCOData>().ObjectName + ", ";
            }

            specificBundled.text = "Mesiace: " + hasMoonsSk + "\n"
            + "Prstence: " + hasRingsSk + "\n";
        }
        else if (star != null)
        {
            specificHeader.text = "Hviezda";
            specificBundled.text = "Typ: " + TranslateToSlovak.Instance.StarTypeToSlovak(star.CurrentData.LuminosityType) + "\n"
            + "Farba: " + TranslateToSlovak.Instance.StarSpectralTypeToSlovak(star.CurrentData.SpectralType) + "\n"
            + "Svietivosť: " + star.CurrentData.Luminosity + "\n"
            + "Fúzny proces: " + star.CurrentData.FusionProcess + "\n"
            + "intenzita vetrov: " + star.CurrentData.StellarWindIntensity + "\n"
            + "koniec ako: " + TranslateToSlovak.Instance.StarDeathTypeToSlovak(star.CurrentData.EndOfLifeStage);
        }
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
