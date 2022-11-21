using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    private float currentZoom = 10f;
    public float pitch = 2.0f;
    public float yawSpeed = 100f; 
    private float currentYaw = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void LateUpdate()

    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.up, currentYaw);

    }

}
