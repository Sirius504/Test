using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonGyroCamera : MonoBehaviour, ICameraController
{
    public GameObject playersFace;   // Place on player where camera will be

    private bool gyroEnabled;
    private Gyroscope gyro;

    private Quaternion rot;

#region Unity_Callbacks
    void OnEnable()
    {
        transform.localPosition = playersFace.transform.localPosition;
        transform.localRotation = playersFace.transform.localRotation;
        gyroEnabled = EnableGyro();
    }


    void LateUpdate()
    {
        if (gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rot;
        }
    }
#endregion

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            transform.rotation = playersFace.transform.rotation;
            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }
        return false;
    }


    public CameraModeSwitcher.CameraModes GetModeName()
    {
        return CameraModeSwitcher.CameraModes.FirstPersonViewGyro;
    }

    public void SetEnable(bool enable)
    {
        enabled = enable;
    }


}
