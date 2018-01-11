using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class FirstPersonAccCamera : MonoBehaviour, ICameraController
{
    public GameObject playersFace;                   // Place on player where camera will be
    public Vector3 tiltForTriggering;                // How hard user must tilt device to trigger camera moving

    public float maxDeltaFromInitialRotation = 90;   // How far can we deviate from initial rotation (in degrees)
    private float deltaFromInitialRotation;          // Current deviation from initial rotation

    public float cameraSpeed;

#region Unity_Callbacks
    private void OnEnable()
    {
        transform.localPosition = playersFace.transform.localPosition;
        transform.localRotation = playersFace.transform.localRotation;
    }

    void LateUpdate()
    {
        //Rotation delta for this frame
        float deltaX = 0;       
        float deltaY = 0;

        //Calculating delta
#if UNITY_ANDROID
        if (Mathf.Abs(Input.acceleration.z) > tiltForTriggering.z - AccelerometerEmulator.slopeCorrection)
        {
            deltaX = -(Input.acceleration.z + AccelerometerEmulator.slopeCorrection);            
        }

        if (Mathf.Abs(Input.acceleration.x) > tiltForTriggering.x)
        {
            deltaY = Input.acceleration.x;
        }
#endif

#if UNITY_EDITOR
        if (Mathf.Abs(AccelerometerEmulator.zAcceleration) > tiltForTriggering.z - AccelerometerEmulator.slopeCorrection)
        {
            deltaX = -(AccelerometerEmulator.zAcceleration + AccelerometerEmulator.slopeCorrection);
        }

        if (Mathf.Abs(AccelerometerEmulator.xAcceleration) > tiltForTriggering.x)
        {
            deltaY = AccelerometerEmulator.xAcceleration;
        }
#endif
        
        deltaFromInitialRotation += deltaX * cameraSpeed;

        //if final deviation keep in bounds - rotate
        if (Mathf.Abs(deltaFromInitialRotation) < maxDeltaFromInitialRotation)          
            transform.Rotate(deltaX * cameraSpeed, 0.0f, 0.0f);

        //we need this line in case deviation became bigger than bounds, so we need to push it back
        //so it'll pass previous check on the next frame
        deltaFromInitialRotation = Mathf.Clamp(deltaFromInitialRotation, -maxDeltaFromInitialRotation, maxDeltaFromInitialRotation);
       
        transform.RotateAround(transform.position, Vector3.up, deltaY * cameraSpeed);
    }
#endregion

    public CameraModeSwitcher.CameraModes GetModeName()
    {
        return CameraModeSwitcher.CameraModes.FirstPersonViewAcc;
    }

    public void SetEnable(bool enable)
    {
        enabled = enable;
    }


}