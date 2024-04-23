using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    public float PlayerSpeed { get; set; }
    public float PlayerSprintSpeed { get; set; }
    public float PlayerSensitivity { get; set; }
    public bool PlayerIsInventoryOpen { get; private set; }

    public float Acceleration { get; set; }

    public float CurrentSpeed { get; private set; }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CurrentSpeed = PlayerSpeed;
    }

    public void HandleInventoryToggle()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerIsInventoryOpen = !PlayerIsInventoryOpen;

            if (PlayerIsInventoryOpen)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                Debug.Log("Player is in inventory");
            }
            else
            {
                LockCursor();
                Debug.Log("Player is not in inventory");
            }
        }
    }

    public void HandlePlayerMovementAndLook()
    {
        if (!PlayerIsInventoryOpen)
        {
            // Player can move and look
            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.z = 0f;
            transform.rotation = Quaternion.Euler(currentRotation);

            MovePlayer();
            RotatePlayer();
        }
    }

    public void MovePlayer()
    {
        float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? PlayerSprintSpeed : PlayerSpeed;
        CurrentSpeed = Mathf.MoveTowards(CurrentSpeed, targetSpeed, Acceleration * Time.deltaTime);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = CurrentSpeed * Time.deltaTime * new Vector3(horizontalInput, 0f, verticalInput);
        transform.Translate(movement);
    }

    public void RotatePlayer()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            float mouseX = Input.GetAxis("Mouse X") * PlayerSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * PlayerSensitivity;

            transform.Rotate(Vector3.up, mouseX);
            transform.Rotate(Vector3.left, mouseY);
        }
    }
}
