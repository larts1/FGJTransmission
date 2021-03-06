﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothTime = 0f;
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero;

    void Update() {
        Vector3 goalPos = target.position + offset;
        goalPos.y = transform.position.y;
        transform.position = Vector3.SmoothDamp( transform.position, goalPos, ref velocity, smoothTime );

        //var rot = target.rotation.eulerAngles;
        //transform.rotation.eulerAngles = new Vector3(90, rot.y, rot.z);
    }
}