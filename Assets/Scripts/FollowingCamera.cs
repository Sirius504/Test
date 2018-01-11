using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Non used mode which provides ability to follow object that isn't parent

public class FollowingCamera : MonoBehaviour, ICameraController  {

    public Transform target;
    public Vector3 offset;
    public float smoothTime;

    private Vector3 velocity = Vector3.zero;

#region Unity_Callbacks
    void Start () {
        transform.position = target.position + offset;
    }
	

	void LateUpdate () {
        Vector3 targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime * Time.deltaTime);
        
    }
#endregion

    public CameraModeSwitcher.CameraModes GetModeName()
    {
        return CameraModeSwitcher.CameraModes.FollowingMode;
    }

    public void SetEnable(bool enable)
    {
        enabled = enable;
    }
}
