using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;



    interface ICameraController 
    {

     CameraModeSwitcher.CameraModes GetModeName();

     void SetEnable(bool enable);
    }
