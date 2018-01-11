using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StrategyCamera : MonoBehaviour, ICameraController {

    private Transform initialParent;

    public float speed;

    private Vector3 dir;  // we move in this direction every frame


#region Unity_Callbacks

    private void OnEnable()
    {
        //Detaching camera from player
        initialParent = transform.parent;
        transform.parent = null;       
    }


    void LateUpdate () {

#if UNITY_ANDROID
        dir.x = Input.acceleration.x;
        dir.z = (Input.acceleration.z - AccelerometerEmulator.slopeCorrection) * -1;
#endif
#if UNITY_EDITOR
        dir.x = AccelerometerEmulator.xAcceleration;
        dir.z = (AccelerometerEmulator.zAcceleration - AccelerometerEmulator.slopeCorrection) * -1;
#endif

        if (dir.sqrMagnitude > 1)
            dir = dir.normalized;
        dir *= Time.deltaTime;

        transform.Translate(dir * speed, Space.World);    
    }

    private void OnDisable()
    {        
        transform.parent = initialParent;
    }

#endregion

    public CameraModeSwitcher.CameraModes GetModeName()
    {
        return CameraModeSwitcher.CameraModes.StrategyView;
    }

    public void SetEnable(bool enable)
    {
        enabled = enable;
    }
}
