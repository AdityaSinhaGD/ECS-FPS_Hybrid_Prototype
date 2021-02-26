using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Camera playerCam;
    private float pitch, yaw;
    private float speed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        playerCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessLook();
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        float xDir = Input.GetAxisRaw("Horizontal");
        float zDir = Input.GetAxisRaw("Vertical");
        Vector3 direction = transform.right * xDir + transform.forward * zDir;
        Vector3 velocity = speed * direction;
        transform.Translate(velocity * Time.deltaTime);
    }

    private void ProcessLook()
    {
        float lookX = Input.GetAxisRaw("Mouse X") * 1f;
        float lookY = Input.GetAxisRaw("Mouse Y") * 1f;

        pitch -= lookY;
        yaw += lookX;
        pitch = Mathf.Clamp(pitch, -80f, 80f);

        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }
}
