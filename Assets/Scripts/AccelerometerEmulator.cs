using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Static class which emulates accelerometer, so we don't need phone for debug
// and it's easy to adjust these parameters via inspector

// However, we can't adjust static parameters direcly in inspector, 
// so we AccelerometerEmulatorController script on a scene do it.

public static class AccelerometerEmulator  {

    public static float xAcceleration;

    public static float zAcceleration;

    public static float slopeCorrection;    // Users don't hold their phones straight, so we need this to correct our input

}
