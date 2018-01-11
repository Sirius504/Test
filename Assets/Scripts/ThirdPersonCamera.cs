using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThirdPersonCamera : MonoBehaviour, ICameraController
{


    private Transform target;                  //Not public because it must be parent

    public Vector3 tiltForTriggering;          // Min tilt for triggering
    
    public float zoomSpeed = 0.1f;
    public float slopeCorrection = 0.15f;      // Nobody hold his phone straight

    public float minDistanceFromPlayer;     
    public float maxDistanceFromPlayer;

    public float rotationSpeed = 3f;           // Speed camera'll be rotating around camera with

    private float distance;

#region Unity_Callbacks
    private void OnEnable()
    {
        target = transform.parent;
        UpdateDistance();
    }

    private void LateUpdate()
    {
            transform.LookAt(target);
            UpdateDistance();
            RotateAround();
            Zoom();
    }

#endregion

    //Calculate distance between camera and target
    private void UpdateDistance()
    {
        distance = Vector3.Distance(target.transform.position, transform.position);
    }

    public void RotateAround()
    {
#if UNITY_ANDROID
        transform.RotateAround(target.transform.position, Vector3.up, rotationSpeed * Input.acceleration.x);
#endif

#if UNITY_EDITOR
        transform.RotateAround(target.transform.position, Vector3.up, rotationSpeed * AccelerometerEmulator.xAcceleration);
#endif
        UpdateDistance();
    }

    public void Zoom()
    {
        float newDistance = distance;
        float delta;                             //value we add to distance every frame

#if UNITY_ANDROID
        if (Mathf.Abs(Input.acceleration.z) > tiltForTriggering.z)        
        {
            delta = (Input.acceleration.z + slopeCorrection) * zoomSpeed;
            newDistance = distance + delta;
        }
#endif

#if UNITY_EDITOR
        if (Mathf.Abs(AccelerometerEmulator.zAcceleration) > tiltForTriggering.z)
        {
            delta = (AccelerometerEmulator.zAcceleration + slopeCorrection) * zoomSpeed;
            newDistance = distance + delta;
        }
#endif
        if ((newDistance > minDistanceFromPlayer) && (newDistance < maxDistanceFromPlayer))
            SetDistance(newDistance);

        if (newDistance < minDistanceFromPlayer)
        {
            // SetDistance(newDistance - (newDistance - minDistanceFromPlayer) * 2);            
            SetDistance(minDistanceFromPlayer);
        }
        if (newDistance > maxDistanceFromPlayer)
        {
         //   SetDistance(newDistance + (maxDistanceFromPlayer - newDistance) * 2);
            SetDistance(maxDistanceFromPlayer);
        }

        UpdateDistance();
    }

    public void SetDistance(float newDistance)
    {
        if (newDistance < 0)
            return;
        distance = Vector3.Distance(target.transform.position, transform.position);
        Vector3 dir = Vector3.forward;
        float delta = distance - newDistance;
        transform.Translate(dir * delta);       
    }

    public CameraModeSwitcher.CameraModes GetModeName()
    {
        return CameraModeSwitcher.CameraModes.ThirdPersonView;
    }

    public void SetEnable(bool enable)
    {
        enabled = enable;
    }

}
