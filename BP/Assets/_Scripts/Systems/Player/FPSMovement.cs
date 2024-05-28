using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    #region PlayerSettings
    public float PlayerSpeed { get; set; }
    public float PlayerSprintSpeed { get; set; }
    public float PlayerSensitivity { get; set; }
    public float Acceleration { get; set; }
    public float CurrentSpeed { get; private set; }
    private float xRotation = 0f;
    #endregion

    #region Mouse Logic
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CurrentSpeed = PlayerSpeed;
    }
    #endregion

    #region Player logic
    public void HandlePlayerLook()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            float mouseX = Input.GetAxis("Mouse X") * PlayerSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * PlayerSensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.parent.Rotate(Vector3.up * mouseX);
        }
    }

    public void MovePlayer()
    {
        float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? PlayerSprintSpeed : PlayerSpeed;
        CurrentSpeed = Mathf.MoveTowards(CurrentSpeed, targetSpeed, Acceleration * Time.deltaTime);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        Vector3 direction = forward * verticalInput + right * horizontalInput;

        Vector3 movement = CurrentSpeed * Time.deltaTime * direction;
        transform.parent.Translate(movement, Space.World);
    }
    #endregion
}
