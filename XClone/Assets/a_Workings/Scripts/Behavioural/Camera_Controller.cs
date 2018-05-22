using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour {

    [Header("Anchors")]
    [SerializeField] private GameObject Target;
    [SerializeField] private GameObject Pivot;
    [SerializeField] private GameObject Camera;

    protected Vector3 followPosition;
    protected Vector3 currentPosition;

    protected Vector3 followRotation;

    [Space(20)]
    [Header("Camera Profile")]
    [Tooltip("How quickly the camera moves per second")]
    [Range(0.100f, 0.500f)]
    [SerializeField] private float moveSpeed;
    [Tooltip("How lagged behind the camera is")]
    [Range(2.00f, 20.00f)]
    [SerializeField] private float followDelay;
    [Tooltip("How quickly the camera moves in the z axis per second")]
    [Range(0.100f, 10.000f)]
    [SerializeField] private float zoomSpeed;
    [Tooltip("How much the camera rotates on 1 rotation")]
    [Range(0.000f, 90.000f)]
    [SerializeField] private float rotateAngle;
    [Tooltip("How the camera should be positioned relative to a target at pos(0,0,0)")]
    [SerializeField] private Vector3 offset;
   // [Tooltip("How the camera should be rotated relative to a target at pos(0,0,0)")]
   // [SerializeField] private Vector3 rotation;
    [Tooltip("How far out the player should be able to zoom")]
    [SerializeField] private float maxHeight;
    [Tooltip("How close up the player should be able to zoom")]
    [SerializeField] private float minHeight;
    [Tooltip("If edge scrolling is enabled")]
    [SerializeField] private bool edgeScrollingEnabled;
    [Tooltip("Edge scrolling multiplier")]
    [Range(0.100f, 10.000f)]
    [SerializeField] private float edgeScrollingMultiplier;


    void Start () {
        Camera.transform.localPosition = new Vector3(offset.x, Camera.transform.localPosition.y, offset.z);
        followPosition = Target.transform.localPosition;
        currentPosition = Pivot.transform.localPosition;
	}
	
	void Update () {

		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            followPosition += new Vector3(moveSpeed, 0, moveSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            followPosition -= new Vector3(moveSpeed, 0, moveSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            followPosition += new Vector3(-moveSpeed, 0, moveSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            followPosition -= new Vector3(-moveSpeed, 0, moveSpeed);
        }

        Target.transform.localPosition = followPosition;
        currentPosition = new Vector3(Mathf.Lerp(currentPosition.x, followPosition.x, Time.deltaTime * followDelay), Mathf.Lerp(currentPosition.y, followPosition.y, Time.deltaTime * followDelay), Mathf.Lerp(currentPosition.z, followPosition.z, Time.deltaTime * followDelay));
        Pivot.transform.localPosition = currentPosition;


        followRotation = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            followRotation = new Vector3(0, rotateAngle, 0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            followRotation = new Vector3(0, -rotateAngle, 0);
        }

        Target.transform.Rotate(followRotation);
        Pivot.transform.rotation = new Quaternion(Mathf.LerpAngle(Pivot.transform.rotation.x, Target.transform.rotation.x, followDelay * Time.deltaTime), Mathf.LerpAngle(Pivot.transform.rotation.y, Target.transform.rotation.y, followDelay * Time.deltaTime), Mathf.LerpAngle(Pivot.transform.rotation.z, Target.transform.rotation.z, followDelay * Time.deltaTime), Mathf.LerpAngle(Pivot.transform.rotation.w, Target.transform.rotation.w, followDelay * Time.deltaTime));

    }
}
