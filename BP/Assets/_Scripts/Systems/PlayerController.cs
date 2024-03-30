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

    [Header("DEBUG Settings")]
    [SerializeField] private Material gridMaterial;
    [SerializeField] private bool fpsCameraOn = true; // yet to implement
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private Camera overviewCamera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
    }

    private void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.P))
            fpsCameraOn = !fpsCameraOn;

        if (fpsCameraOn)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            fpsCamera.enabled = true;
            overviewCamera.enabled = false;
            ovm.CamController.enabled = false;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            fpsCamera.enabled = false;
            ovm.CamController.enabled = true;
            overviewCamera.enabled = true;
        }
    }
}