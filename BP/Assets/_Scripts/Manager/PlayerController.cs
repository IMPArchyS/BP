using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    #region FPSCameraSettings
    [Header("FPS Camera Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private FPSMovement fps;
    #endregion

    #region OverviewCameraSettings
    [Header("Overview Camera Settings")]
    [SerializeField] private float zoomSpeed = 6f;
    [SerializeField] private float dragSpeed = 600f;
    [SerializeField] private OverviewMovement ovm;
    [SerializeField] private bool loopCamera = false;
    [SerializeField] private int loopSpeed = 3;
    [SerializeField] private float acceleration = 25f;
    #endregion

    #region MISCSettings
    [Header("MISC Settings")]
    [SerializeField] private Material gridMaterial;
    [SerializeField] private bool fpsCameraOn = true;
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private Camera overviewCamera;
    [field: SerializeField] public bool CanMove { get; set; } = true;
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
        fpsCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        overviewCamera = GameObject.FindGameObjectWithTag("SecondaryCamera").GetComponent<Camera>();
        fps = fpsCamera.GetComponent<FPSMovement>();
        ovm = overviewCamera.GetComponent<OverviewMovement>();

        ovm.ZoomSpeed = zoomSpeed;
        ovm.DragSpeed = dragSpeed;
        ovm.LoopCamera = loopCamera;
        ovm.CameraLoopSpeed = loopSpeed;
        ovm.GridMaterial = gridMaterial;
        ovm.SetupOvm();

        fps.PlayerSpeed = speed;
        fps.Acceleration = acceleration;
        fps.PlayerSprintSpeed = sprintSpeed;
        fps.PlayerSensitivity = sensitivity;
        fps.LockCursor();
        if (fpsCameraOn)
            fpsCameraOn = false;
        SetCamera();
    }

    private void Update()
    {
        if (!CanMove) return;

        if (fpsCameraOn)
        {
            fps.HandleInventoryToggle();
            fps.HandlePlayerMovementAndLook();
            gridMaterial.SetFloat("_FPSCamera", fpsCameraOn ? 1f : 0f);
        }
        else
        {
            ovm.UpdateStar();
            ovm.UpdateCameraAngles();
            ovm.MouseZoom();
            ovm.RaycastOnClick();
        }
    }
    private void LateUpdate()
    {
        if (!CanMove) return;

        if (Input.GetKeyDown(KeyCode.P))
            SetCamera();
        if (fpsCamera)
        {

        }
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
}