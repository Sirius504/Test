using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We can't adjust static AccelerometerEmulator parameters in inspector, 
// so we need this class let us to do it.

public class AccelerometerEmulatorController : MonoBehaviour {

    [Range(-1.0f, 1.0f)]
    public float xAcceleration;

    [Range(-1.0f, 1.0f)]
    public float zAcceleration;

    public float slopeCorrection;
       
    void Start () {
        AccelerometerEmulator.xAcceleration = xAcceleration;
        AccelerometerEmulator.zAcceleration = zAcceleration;
        AccelerometerEmulator.slopeCorrection = slopeCorrection;
    }

	void Update () {
        AccelerometerEmulator.xAcceleration = xAcceleration;
        AccelerometerEmulator.zAcceleration = zAcceleration;
        AccelerometerEmulator.slopeCorrection = slopeCorrection;
    }
}
