using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridToCamera : MonoBehaviour
{
    [SerializeField] private Camera camObject;
    [SerializeField] private OverviewMovement ovm;
    [SerializeField] float offsetX = 1;
    [SerializeField] float offsetZ = 1;
    private void Awake()
    {
        camObject = GameObject.Find("FPSCamera").GetComponent<Camera>();
        ovm = GameObject.Find("OverviewCamera").GetComponent<OverviewMovement>();
    }

    private void LateUpdate()
    {
        if (camObject.enabled)
        {
            transform.position = new Vector3(camObject.transform.position.x + offsetX, transform.position.y, camObject.transform.position.z + offsetZ);
        }
        else
        {
            transform.position = new Vector3(ovm.LookedAtObject.transform.position.x, ovm.LookedAtObject.transform.position.y, ovm.LookedAtObject.transform.position.z);
        }
    }
}
