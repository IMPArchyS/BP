using System.Collections;
using System.Collections.Generic;
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

    [field: SerializeField] public bool InMenu { get; set; } = false;
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
    }

    private void Update()
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
            fps.HandlePlayerMovementAndLook();
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

    private void LateUpdate()
    {
        if (InMenu) return;
        if (Input.GetKeyDown(KeyCode.P)) SetCamera();
        if (fpsCamera) { }
        else
        {
            ovm.UpdateOrbitalAngle();
        }
    }

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
        fpsCamera.transform.SetPositionAndRotation(overviewCamera.transform.position, overviewCamera.transform.rotation);
        SetCamera();
        SetCursorBasedOnCam();
    }
}