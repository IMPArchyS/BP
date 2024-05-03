using UnityEngine;

public class CelestialObjectInfo : MonoBehaviour
{
    [SerializeField] GameObject celestialObjectInfoBox;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !PlayerController.Instance.FpsCameraOn)
            GetObjectInfo(true);
    }

    public void GetObjectInfo(bool mouseClick)
    {
        if (mouseClick)
        {
            Ray ray = PlayerController.Instance.OverviewCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("GetObjectInfoRAY : " + hit.collider.gameObject);
                if (hit.collider.TryGetComponent<CelestialObject>(out CelestialObject csObj))
                {
                    UpdateInfoUI(csObj);
                }
            }
            else
                celestialObjectInfoBox.SetActive(false);
        }
        else
        {
            PlayerController.Instance.Ovm.LookedAtObject.TryGetComponent<CelestialObject>(out CelestialObject csObj);
            if (csObj)
            {
                Debug.Log("GetObjectInfoBUTTON : " + csObj);
                UpdateInfoUI(csObj);
            }
        }
    }

    private void UpdateInfoUI(CelestialObject foundCelestial)
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
}
