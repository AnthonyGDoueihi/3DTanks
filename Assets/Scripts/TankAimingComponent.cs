using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAimingComponent : MonoBehaviour {

    public enum FiringState
    {
        Firing,
        Reloading,
        Moving        
    }
    public FiringState currentState;

    private Firing firing;
    private BarrelPivot barrel;
    private TurretPivot turret;

    [SerializeField] float projectileVelocity = 10.0f;
    [SerializeField] float reloadTime = 3.0f;
    

    float reloading = 0.0f;
    Vector3 aimDirection;
    public GameObject projectilePrefab;

    // Use this for initialization
    void Start () {
        firing = GetComponentInChildren<Firing>();
        barrel = GetComponentInChildren<BarrelPivot>();
        turret = GetComponentInChildren<TurretPivot>();
    }

    // Update is called once per frame
    void Update () {
        if (reloading >= 0)
        {
            reloading -= Time.deltaTime;
        }


        if (reloading > 0)
        {
            currentState = FiringState.Reloading;
        }
        else if (IsBarrelMoving())
        {
            currentState = FiringState.Moving;
        }
        else
        {
            currentState = FiringState.Firing;
        }
	}

    public void Aim(Vector3 target)
    {
        if (barrel == null || turret == null) { return; }
        aimDirection = (target - barrel.transform.position);
                
        Vector3 turretAimDirection = new Vector3(aimDirection.x, 0, aimDirection.z);
        Quaternion turretAimRotation = Quaternion.LookRotation(turretAimDirection, transform.up);
        turret.Rotate(turretAimRotation);
        
        Vector3 barrelAimDirection = new Vector3(aimDirection.x, aimDirection.y, aimDirection.z);
        Quaternion barrelAimRotation = Quaternion.LookRotation(barrelAimDirection, transform.up);
        barrel.Pivot(barrelAimRotation);
        

    }

    public void Fire()
    {
        if (firing == null) { return; }
        if (currentState == FiringState.Moving || currentState == FiringState.Firing)
        {
            GameObject projectile = Instantiate(projectilePrefab, firing.transform.position, firing.transform.rotation);
            projectile.GetComponent<Rigidbody>().velocity = projectileVelocity * firing.transform.forward;
            reloading = reloadTime;
        }

    }

    bool IsBarrelMoving()
    {
        if (Mathf.Abs(Vector3.Distance(barrel.transform.forward, aimDirection.normalized)) < 0.05)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public FiringState GetFiringState()
    {
        return currentState;
    }


}
