using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraController : MonoBehaviour
{
    [Header("Adjustable player controls")] //should link to UI later
    public float mouseSensitivity = 100f;
    public Transform playerTransform;
    private float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; //this is a unity programmed axis, I'm using it for mouse input and multiplying that by the mouse sensitivity for smooth camera moving
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; //same as previous just using Y axis now
        xRotation -= mouseY;//lets flip this around so it lines up
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //adjusting camera
        playerTransform.Rotate(Vector3.up * mouseX); //adjusting player transform
    }
   
}
