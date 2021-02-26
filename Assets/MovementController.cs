using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Camera playerCam;
    private float pitch, yaw;
    private float speed = 25f;
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
        Vector3 direction = this.transform.right * xDir + playerCam.transform.forward * zDir;
        Vector3 velocity = speed * direction * Time.deltaTime;
        transform.position += velocity;  
    }

    private void ProcessLook()
    {
        float lookX = Input.GetAxisRaw("Mouse X") * 1f;
        float lookY = Input.GetAxisRaw("Mouse Y") * 1f;

        pitch -= lookY;

        transform.Rotate(new Vector3(0f, lookX, 0f), Space.Self);
        pitch = Mathf.Clamp(pitch, -80f, 80f);

        playerCam.transform.localEulerAngles = Vector3.right * pitch;
    }
}
