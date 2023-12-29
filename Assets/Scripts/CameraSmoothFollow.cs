using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    void OnEnable() {
        target = GameObject.Find("PlayerCube").transform;
    }
    
    void FixedUpdate() {
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 6, -8));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
