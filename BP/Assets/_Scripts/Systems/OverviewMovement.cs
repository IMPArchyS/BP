using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro.Examples;

public class OverviewMovement : MonoBehaviour
{
    public float ZoomSpeed { get; set; }

    public float DragSpeed { get; set; }
    public bool LoopCamera { get; set; } = false;
    public int CameraLoopSpeed { get; set; } = 3;
    public CameraController CamController { get; private set; }
    public Material GridMaterial { private get; set; }
    public GameObject LookedAtObject
    {
        get { return lookAtObj; }
        set { lookAtObj = value; }
    }
    [SerializeField] private GameObject lookAtObj;

    public void SetupOvm()
    {
        lookAtObj = GameObject.FindGameObjectWithTag("Star");
        transform.position = new Vector3(5, 14, -20);
        CamController = GetComponent<CameraController>();
        CamController.CameraTarget = lookAtObj.transform;
    }

    public void UpdateOrbitalAngle()
    {
        if (LoopCamera)
            CamController.OrbitalAngle += CameraLoopSpeed * Time.deltaTime;
    }

    public void UpdateStar()
    {
        if (lookAtObj == null)
        {
            GridMaterial.SetVector("_ObjectPos", transform.position);
            lookAtObj = GameObject.FindGameObjectWithTag("Star");
            CamController.CameraTarget = lookAtObj.transform;
        }
    }

    public void UpdateCameraAngles()
    {
        float axisX = Input.GetAxisRaw("Mouse X");
        float axisY = Input.GetAxisRaw("Mouse Y");
        if (Input.GetMouseButton(0))
        {
            CamController.OrbitalAngle += axisX * DragSpeed / 100;
            CamController.ElevationAngle -= axisY * DragSpeed / 100;
        }
    }

    public void MouseZoom()
    {
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheel != 0)
        {
            CamController.FollowDistance += mouseWheel * -10 * ZoomSpeed * 75 * Time.deltaTime;
        }
    }

    public void RaycastOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Camera cam = GetComponent<Camera>();
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log(hit.collider.gameObject);
                lookAtObj = hit.collider.gameObject;
                CamController.CameraTarget = lookAtObj.transform;
            }
        }
    }
}