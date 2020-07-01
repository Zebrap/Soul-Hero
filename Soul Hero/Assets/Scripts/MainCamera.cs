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

    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis(AxisTags.AXIS_SCROLLWHEEL) * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;

        transform.position = player.transform.position + offset;
    }


}
