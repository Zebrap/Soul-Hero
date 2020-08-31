using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject player;
    Vector3 offset;
    float minFov = 15f;
    float maxFov = 90f;
    float sensitivity = 10f;
    private bool rotateCamera = false;
    public float cameraSpeed = 3f;

    private void Start()
    {
        offset = transform.position; // - player.transform.position;
    }

    private void LateUpdate()
    {
        if (rotateCamera)
        {
            transform.LookAt(player.transform);
            transform.RotateAround(player.transform.position, Vector3.up, cameraSpeed * Time.deltaTime);
            return;
        }

        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis(AxisTags.AXIS_SCROLLWHEEL) * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;

        transform.position = player.transform.position + offset;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            rotateCamera = !rotateCamera;
        }
    }


}
