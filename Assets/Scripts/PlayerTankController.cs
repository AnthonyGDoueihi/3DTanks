using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankAimingComponent))]
[RequireComponent(typeof(TankMovementComponent))]
public class PlayerTankController : MonoBehaviour {

    TankMovementComponent movementComponent;
    TankAimingComponent aimingComponent;
    Camera cam;
    Vector3 camForward;
    Vector3 target;
    public int HitsCanTake = 3;
    
    bool isDead;

    // Use this for initialization
    void Start () {
        movementComponent = GetComponent<TankMovementComponent>();
        aimingComponent = GetComponent<TankAimingComponent>();
        cam = Camera.main;

	}
	
	// Update is called once per frame
	void Update ()
    {
        GetRaycast();

        if (Input.GetButtonDown("Fire1"))
        {
            aimingComponent.Fire();
        }

        if (HitsCanTake < 0)
        {
            Dead();
        }

    }

    private void FixedUpdate()
    {
        InputMovement();

        aimingComponent.Aim(target);

    }


    private void InputMovement()
    {
        float right = Input.GetAxis("Horizontal");
        float forward = Input.GetAxis("Vertical");

        if (right != 0 || forward != 0)
        {
            camForward = Vector3.Scale(cam.transform.forward, new Vector3(1, 0, 1)).normalized;

            Quaternion deltaRotation = Quaternion.Euler(0, right * movementComponent.rotationSpeed* Time.deltaTime, 0);
            Quaternion aimRotation = transform.rotation * deltaRotation;
            movementComponent.Move(forward, aimRotation);

        }
    }


    void GetRaycast()
    {
        Vector3 origin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cam.nearClipPlane));
        RaycastHit rayHit;

        if (Physics.Raycast(origin, cam.transform.forward, out rayHit, Mathf.Infinity))
        {
            target = rayHit.point;
        }
    }

    void Dead()
    {
        FindObjectOfType<GameSceneController>().YouLose();
        Destroy(gameObject);
    }

}


