using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourCameraController : MonoBehaviour
{
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] private Camera cam;

    private float mouseX;
    private float mouseY;

    private float multiplier = 0.01f;

    private float xRotation;
    private float yRotation;

    private void Start()
    {
        //stop the cursor from flopping around, hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        MyInput();

        //modify camera rotation based off of variables in MyInput
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void MyInput()
    {
        //recieve input from player
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        //modify rotation based off above input
        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        //clamp rotation, stopping the camera from going too far up or down
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }
}
