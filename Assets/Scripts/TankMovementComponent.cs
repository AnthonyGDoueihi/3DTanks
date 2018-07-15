using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TankMovementComponent : MonoBehaviour {

    [SerializeField] float moveSpeed = 100.0f;
    public float rotationSpeed = 20.0f;
    private Rigidbody rigid;

    // Use this for initialization
    void Start () {
        rigid = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
    }

    public void Move(float forward, Quaternion aimRotation)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, aimRotation, rotationSpeed * Time.deltaTime);

        rigid.AddForce(transform.forward * moveSpeed * forward);

    }

}
