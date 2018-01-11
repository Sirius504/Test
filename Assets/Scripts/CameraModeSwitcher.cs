using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

// That class responsible for switching beetwen Camera modes

public class CameraModeSwitcher : MonoBehaviour {

    [HideInInspector]
    public Vector3 initialCameraPosition;
    [HideInInspector]
    public Quaternion initialCameraRotation;

    private List<ICameraController> cameraControllers = new List<ICameraController>();

    private string log;

    public CameraModes currCameraMode = CameraModes.ThirdPersonView;

#region Unity_Callbacks

    void OnEnable () {

        initialCameraPosition =  Camera.main.transform.localPosition;
        initialCameraRotation = Camera.main.transform.localRotation;

        foreach (ICameraController camCtrl in GetComponents<ICameraController>())
        {
            cameraControllers.Add(camCtrl);
        }

        TurnOn(currCameraMode);
    }

    private void OnGUI()
    {
        if (log != null)
            StartCoroutine(ShowLog());
    }

#endregion

    // This method can be called from Event Handler to switch Camera modes
    public void ChangeCameraMode(string mode)   
    {
       
        //Parse string from argument to CameraMode. Argument "mode" has to be string, so we can call this method from Event Handler
        CameraModes cameraMode = (CameraModes) System.Enum.Parse(typeof(CameraModes), mode);             
       
        if (currCameraMode == cameraMode)
            { return; }

        // if Camera Mode has changed
        // disable all CameraControllers,..
        foreach (ICameraController camCtrl in cameraControllers)
        {
            camCtrl.SetEnable(false);
        }

        // ...restore initial parameters (if any Camera Mode need different on the start, it must change it itself)...
        transform.localPosition = initialCameraPosition;
        transform.localRotation = initialCameraRotation;

        // ...and enable one we need
        switch (cameraMode)
        {
            case CameraModes.FirstPersonViewGyro:
                {

                    if (SystemInfo.supportsGyroscope)
                    {
                        TurnOn(CameraModes.FirstPersonViewGyro);
                    }
                    else
                    {
                        log = "Gyroscope isn't supported by your device";
                    }

#if UNITY_EDITOR
                    TurnOn(CameraModes.FirstPersonViewGyro);
#endif
                }
                break;
            case CameraModes.ThirdPersonView:
                {

                    if (SystemInfo.supportsAccelerometer)
                    {
                        TurnOn(CameraModes.ThirdPersonView);
                    }
                    else
                    {
                        log = "Accelerometer isn't supported by your device";
                    }

#if UNITY_EDITOR
                    TurnOn(CameraModes.ThirdPersonView);
#endif
                }
                break;

            case CameraModes.FirstPersonViewAcc:
                {

                    if (SystemInfo.supportsAccelerometer)
                    {
                        TurnOn(CameraModes.FirstPersonViewAcc);
                    }
                    else
                    {
                        log = "Accelerometer isn't supported by your device";
                    }


#if UNITY_EDITOR
                    TurnOn(CameraModes.FirstPersonViewAcc);
#endif
                }
                break;

            case CameraModes.StrategyView:
                {

                    if (SystemInfo.supportsAccelerometer)
                    {
                        TurnOn(CameraModes.StrategyView);
                    }
                    else
                    {
                        log = "Accelerometer isn't supported by your device";
                    }

#if UNITY_EDITOR
                    TurnOn(CameraModes.StrategyView);
#endif
                }
                break;

        }
    }

    private void TurnOn(CameraModes cameraMode)
    {
        currCameraMode = cameraMode;
        cameraControllers.Find(item => item.GetModeName() == cameraMode).SetEnable(true);        
    }

    public IEnumerator ShowLog()
    {
        GUI.Box(new Rect(5, 35, 300, 40), log);
        yield return new WaitForSeconds(2.0f);
        log = null;
    }

    public enum CameraModes
    {
        FirstPersonViewAcc,
        FirstPersonViewGyro,
        ThirdPersonView,
        StrategyView,
        FollowingMode
        
    }
}
