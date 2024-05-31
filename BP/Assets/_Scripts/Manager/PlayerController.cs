using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    #region FPSCameraSettings
    [Header("FPS Camera Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private FPSMovement fps;
    [SerializeField] private float acceleration = 25f;
    #endregion

    #region OverviewCameraSettings
    [Header("Overview Camera Settings")]
    [SerializeField] private float zoomSpeed = 6f;
    [SerializeField] private float dragSpeed = 600f;
    [SerializeField] private OverviewMovement ovm;
    [SerializeField] private bool loopCamera = false;
    [SerializeField] private int loopSpeed = 3;
    #endregion

    #region MISCSettings
    [Header("MISC Settings")]
    [SerializeField] private Material gridMaterial;
    [SerializeField] private bool fpsCameraOn = true;
    [SerializeField] private Camera fpsCamera;

    [SerializeField] private Camera overviewCamera;
    [SerializeField] private bool showOrbit = false;
    [field: SerializeField] public bool InMenu { get; set; } = false;
    [field: SerializeField] public bool InSubMenuOpen { get; set; } = false;
    #endregion

    #region GettersSetters
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public bool FpsCameraOn
    {
        get { return fpsCameraOn; }
        set { fpsCameraOn = value; }
    }
    public OverviewMovement Ovm
    {
        get { return ovm; }
    }
    public Camera FpsCamera { get { return fpsCamera; } }
    public Camera OverviewCamera { get { return overviewCamera; } }
    #endregion

    #region Startup
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // Setup components from object
        fpsCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        overviewCamera = GameObject.FindGameObjectWithTag("SecondaryCamera").GetComponent<Camera>();
        fps = fpsCamera.GetComponent<FPSMovement>();
        ovm = overviewCamera.GetComponent<OverviewMovement>();
        // setup overview movement controller
        ovm.ZoomSpeed = zoomSpeed;
        ovm.DragSpeed = dragSpeed;
        ovm.LoopCamera = loopCamera;
        ovm.CameraLoopSpeed = loopSpeed;
        ovm.GridMaterial = gridMaterial;
        ovm.SetupOvm();
        // setup fps movement controller
        sprintSpeed = speed * 2.5f;
        fps.PlayerSpeed = speed;
        fps.Acceleration = acceleration;
        fps.PlayerSprintSpeed = sprintSpeed;
        fps.PlayerSensitivity = sensitivity;
        fps.LockCursor();
        // switch bool based on UX & init camera
        if (fpsCameraOn) fpsCameraOn = false;
        SetCamera();
        foreach (Planet p in SolarSystem.Instance.Planets)
        {
            if (!p.gameObject.GetComponent<MeshRenderer>().enabled)
                p.GetComponent<OrbitalMovement>().showOrbit = false;
        }
    }
    #endregion

    #region Camera controls
    private void SetCamera()
    {
        fpsCameraOn = !fpsCameraOn;
        if (fpsCameraOn)
        {
            SetCursorBasedOnCam();
            fpsCamera.enabled = true;
            overviewCamera.enabled = false;
            ovm.CamController.enabled = false;
        }
        else
        {
            SetCursorBasedOnCam();
            fpsCamera.enabled = false;
            ovm.CamController.enabled = true;
            overviewCamera.enabled = true;
        }
    }

    public void SetCursorBasedOnCam()
    {
        if (fpsCameraOn)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void UpdateSprintSpeed()
    {
        sprintSpeed = speed * 2.5f;
        fps.PlayerSpeed = speed;
        fps.PlayerSprintSpeed = sprintSpeed;
    }

    public void JumpToObject()
    {
        fpsCamera.transform.parent.position = overviewCamera.transform.position;
        fpsCamera.transform.rotation = Quaternion.Euler(overviewCamera.transform.eulerAngles.x, fpsCamera.transform.eulerAngles.y, fpsCamera.transform.eulerAngles.z);
        fpsCamera.transform.parent.rotation = Quaternion.Euler(fpsCamera.transform.parent.rotation.x, overviewCamera.transform.eulerAngles.y, fpsCamera.transform.parent.rotation.z);
        SetCamera();
        SetCursorBasedOnCam();
    }
    #endregion

    private void FixedUpdate()
    {
        if (InMenu) return;
    }
    private void Update()
    {
        try
        {
            if (InMenu)
            {
                ovm.CamController.enabled = false;
                return;
            }
            else
            {
                ovm.CamController.enabled = true;
            }

            if (fpsCameraOn)
            {
                fps.HandlePlayerLook();
                fps.MovePlayer();

                gridMaterial.SetFloat("_FPSCamera", fpsCameraOn ? 1f : 0f);
            }
            else
            {
                ovm.CamController.MovementSmoothing = false;
                ovm.UpdateLookAtObject();
                ovm.UpdateCameraAngles();
                ovm.MouseZoom();
                ovm.RaycastOnClick();
            }
        }
        catch (System.Exception)
        {

        }
    }
    private bool ToggleOrbits(bool toggle)
    {
        toggle = !toggle;
        foreach (Planet p in SolarSystem.Instance.Planets)
        {
            if (p.gameObject.GetComponent<MeshRenderer>().enabled)
                p.GetComponent<OrbitalMovement>().showOrbit = toggle;
        }
        foreach (Moon m in SolarSystem.Instance.Moons)
        {
            if (m.gameObject.GetComponent<MeshRenderer>().enabled)
                m.GetComponent<OrbitalMovement>().showOrbit = toggle;
        }
        return toggle;
    }

    private void LateUpdate()
    {
        if (InMenu) return;
        if (Input.GetKeyDown(KeyCode.P)) SetCamera();
        if (Input.GetKeyDown(KeyCode.O)) showOrbit = ToggleOrbits(showOrbit);
        if (fpsCamera) { }
        else
        {
            ovm.UpdateOrbitalAngle();
        }
    }
}