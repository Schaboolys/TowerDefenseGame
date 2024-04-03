using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationY;
    public float rotationX;
    public GameObject cam;
    public float speed = 5f;

    void Start()
    {
        
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        movementDirection.y = 0;
        transform.position += movementDirection *speed * Time.deltaTime;

        if (Input.GetButton("Mouse Appear"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetButton("Mouse Dissappear"))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        rotationY = Input.GetAxis("Mouse X");
        rotationX = -Input.GetAxis("Mouse Y");
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        transform.Rotate(0, rotationY, 0);
        cam.transform.Rotate(rotationX, 0, 0);
    }
}