using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionAvoider : MonoBehaviour {

    public Transform target;

    private ThirdPersonCamera camCtrl;

    public struct ClipPlanePoints
    {
        public Vector3 UpperLeft;
        public Vector3 UpperRight;
        public Vector3 LowerLeft;
        public Vector3 LowerRight;
    }

    public ClipPlanePoints ClipPlaneAtNear(Vector3 pos) // pos - position of our camera/target
    {
        ClipPlanePoints clipPlanePoints = new ClipPlanePoints();


        float halfFOV = (Camera.main.fieldOfView / 2) * Mathf.Deg2Rad;
        float aspect = Camera.main.aspect;
        float distance = Camera.main.nearClipPlane;
        float height = distance * Mathf.Tan(halfFOV);
        float width = height * aspect;


        clipPlanePoints.LowerLeft = pos - transform.right * width;
        clipPlanePoints.LowerLeft -= transform.up * height;
        clipPlanePoints.LowerLeft += transform.forward * distance;

        clipPlanePoints.LowerRight = pos + transform.right * width;
        clipPlanePoints.LowerRight -= transform.up * height;
        clipPlanePoints.LowerRight += transform.forward * distance;

        clipPlanePoints.UpperLeft = pos - transform.right * width;
        clipPlanePoints.UpperLeft += transform.up * height;
        clipPlanePoints.UpperLeft += transform.forward * distance;

        clipPlanePoints.UpperRight = pos + transform.right * width;
        clipPlanePoints.UpperRight += transform.up * height;
        clipPlanePoints.UpperRight += transform.forward * distance;

        return clipPlanePoints;
    }

    public float CheckCameraPoints(Vector3 from, Vector3 to)
    {
        float nearDistance = -1.0f;

        RaycastHit hitInfo;

        ClipPlanePoints clipPlanePoints = ClipPlaneAtNear(to);


 
        Debug.DrawLine(from, to + transform.forward * -Camera.main.nearClipPlane, Color.red);
        Debug.DrawLine(from, clipPlanePoints.UpperLeft);
        Debug.DrawLine(from, clipPlanePoints.LowerLeft);
        Debug.DrawLine(from, clipPlanePoints.UpperRight);
        Debug.DrawLine(from, clipPlanePoints.LowerRight);

        Debug.DrawLine(clipPlanePoints.UpperLeft, clipPlanePoints.UpperRight);
        Debug.DrawLine(clipPlanePoints.UpperRight, clipPlanePoints.LowerRight);
        Debug.DrawLine(clipPlanePoints.LowerRight, clipPlanePoints.LowerLeft);
        Debug.DrawLine(clipPlanePoints.LowerLeft, clipPlanePoints.UpperLeft);


        if (Physics.Linecast(from, clipPlanePoints.UpperLeft, out hitInfo) && hitInfo.collider.tag != "Player")
            if ((hitInfo.distance < nearDistance) || nearDistance == -1)
            {
                Debug.Log(hitInfo.distance);
                Debug.Break();
                nearDistance = hitInfo.distance;
            }
        if (Physics.Linecast(from, clipPlanePoints.LowerLeft, out hitInfo) && hitInfo.collider.tag != "Player")
            if ((hitInfo.distance < nearDistance) || nearDistance == -1)
            {
                Debug.Log(hitInfo.distance);
                //   Debug.Break();
                //   nearDistance = hitInfo.distance;
            }
        if (Physics.Linecast(from, clipPlanePoints.UpperRight, out hitInfo) && hitInfo.collider.tag != "Player")
            if ((hitInfo.distance < nearDistance) || nearDistance == -1)
            {
                Debug.Log(hitInfo.distance);
                nearDistance = hitInfo.distance;
            }
        if (Physics.Linecast(from, clipPlanePoints.LowerRight, out hitInfo) && hitInfo.collider.tag != "Player")
            if ((hitInfo.distance < nearDistance) || nearDistance == -1)
            {
                Debug.Log(hitInfo.distance);
                nearDistance = hitInfo.distance;
            }
        if (Physics.Linecast(from, to + transform.forward * -Camera.main.nearClipPlane, out hitInfo) && hitInfo.collider.tag != "Player")
            if ((hitInfo.distance < nearDistance) || nearDistance == -1)
            {
                Debug.Log(hitInfo.distance);
                nearDistance = hitInfo.distance;
            }


        return nearDistance;
    }

    private void FixOcclusions()
    {
        float nearDistance = CheckCameraPoints(target.transform.position, transform.position);
        if (nearDistance != -1)
        {
           camCtrl.SetDistance(nearDistance);
        }
    }

    // Use this for initialization
    void Start () {
        camCtrl = GetComponent<ThirdPersonCamera>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        FixOcclusions();
	}
}
