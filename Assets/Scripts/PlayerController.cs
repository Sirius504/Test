using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float maxSpeed;

    private Rigidbody rb;

    Transform cameraTransform;    // Need this to keep movement direction dependent from camera

    void Start () {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
	}
	

	void FixedUpdate () {        
        Vector3 forwardAxis = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up);
        Vector3 rightAxis = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up);

        Vector3 moveVec = (forwardAxis.normalized * CrossPlatformInputManager.GetAxis("Vertical") + rightAxis.normalized * CrossPlatformInputManager.GetAxis("Horizontal")) * speed;

        rb.AddForce(moveVec);
	}

}
