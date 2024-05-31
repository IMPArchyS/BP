using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    [SerializeField]
    private float orbitSpeed;
    [SerializeField]
    private GameObject parent;
    [SerializeField]
    private bool rotationClockwise;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private Vector3 rotationDirection;

    public void SetupBeltObject(float _speed, float _rotationSpeed, GameObject _parent, bool _rotateClockwise)
    {
        orbitSpeed = _speed;
        rotationSpeed = _rotationSpeed;
        parent = _parent;
        rotationClockwise = _rotateClockwise;
        rotationDirection = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }

    private void Update()
    {
        int stellarTimeScale;
        // Increment the angle based on time and speed
        if (MainTimeController.Instance.StellarTimeScale > MainTimeController.Instance.StellarYear)
            stellarTimeScale = MainTimeController.Instance.StellarYear;
        else
            stellarTimeScale = (int)MainTimeController.Instance.StellarTimeScale;

        if (rotationClockwise)
        {
            transform.RotateAround(parent.transform.position, parent.transform.up, orbitSpeed * Time.deltaTime * stellarTimeScale);
        }
        else
        {
            transform.RotateAround(parent.transform.position, -parent.transform.up, orbitSpeed * Time.deltaTime * stellarTimeScale);
        }

        transform.Rotate(rotationDirection, rotationSpeed * Time.deltaTime * stellarTimeScale);
    }
}
