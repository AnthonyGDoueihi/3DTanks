using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelPivot : MonoBehaviour {

    Vector3 startingRot;
    float maxDegreePerSec = 10.0f;
    TurretPivot turret;

    // Use this for initialization
    void Start () {
        turret = GetComponentInParent<TurretPivot>();
	}

   public void Pivot(Quaternion aimRotation)        
    {
        if (turret != null)
        {
            float xRotate = Quaternion.RotateTowards(transform.rotation, aimRotation, maxDegreePerSec * Time.deltaTime).eulerAngles.x;
            Quaternion xToAdd = Quaternion.Euler(xRotate, 0, 0);
            transform.rotation = turret.transform.rotation * xToAdd;
        }

    }

}
