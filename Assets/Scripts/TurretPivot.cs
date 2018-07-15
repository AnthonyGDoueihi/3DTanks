using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPivot : MonoBehaviour
{

    float maxDegreePerSec = 40.0f;
    float previousRotation;

    public void Rotate(Quaternion aimRotation)
    {
        Quaternion turretRotation = transform.rotation;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, aimRotation, maxDegreePerSec * Time.deltaTime);

    }
}