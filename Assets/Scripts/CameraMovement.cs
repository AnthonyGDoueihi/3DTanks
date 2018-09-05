using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField] float camSpeed = 10.0f;
    [SerializeField] float minPitch = -15.0f;
    [SerializeField] float maxPitch = 20.0f;

    private float YawAngle;
    private float PitchAngle;

    CameraArm cameraArm;
    PlayerTankController playerTank;    

    // Use this for initialization
    void Start () {
        cameraArm = GetComponentInChildren<CameraArm>();
        playerTank = FindObjectOfType<PlayerTankController>();
	}
	
	// Update is called once per frame
	void Update () {
        MoveCamera();
        if (playerTank != null)
        {
            transform.position = playerTank.transform.position;
        }
	}

    void MoveCamera()
    {
        float x = Input.GetAxis("Look X");
        float y = Input.GetAxis("Look Y");

        YawAngle += x * camSpeed;
        PitchAngle = Mathf.Clamp(PitchAngle - y * camSpeed, minPitch, maxPitch);

        Quaternion targetYaw = Quaternion.Euler(transform.localRotation.eulerAngles.x, YawAngle , transform.localRotation.eulerAngles.z);
        transform.localRotation = targetYaw;

        Quaternion targetPitch = Quaternion.Euler(PitchAngle, cameraArm.transform.localRotation.eulerAngles.y, cameraArm.transform.localRotation.eulerAngles.z);
        cameraArm.transform.localRotation = targetPitch;

        

    }

}
