using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float sensitivity = 2f;

    private bool isInventoryOpen = false;

    private void Start()
    {
        // Lock cursor to the window
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isInventoryOpen = !isInventoryOpen;

            if (isInventoryOpen)
            {
                // Player is in inventory, disable movement and look
                Cursor.lockState = CursorLockMode.None;
                Debug.Log("Player is in inventory");
            }
            else
            {
                // Player is not in inventory, enable movement and look
                Cursor.lockState = CursorLockMode.Locked;
                Debug.Log("Player is not in inventory");
            }
        }

        if (!isInventoryOpen)
        {
            // Player can move and look
            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.z = 0f;
            transform.rotation = Quaternion.Euler(currentRotation);

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;
            transform.Translate(movement);

            if (Cursor.lockState == CursorLockMode.Locked)
            {
                float mouseX = Input.GetAxis("Mouse X") * sensitivity;
                float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

                transform.Rotate(Vector3.up, mouseX);
                transform.Rotate(Vector3.left, mouseY);
            }
        }
    }
}
