using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankAimingComponent))]
[RequireComponent(typeof(TankMovementComponent))]
public class AITankController : MonoBehaviour {

    TankAimingComponent tankAim;
    TankMovementComponent tankMove;
    PlayerTankController player;

    Quaternion randomRotation;
    Vector3 randomDirection;
    float randomSpeed;

    public int HitsCanTake = 1;
    public float timeBetweenRandom = 5.0f;


    // Use this for initialization
    void Start () {
        tankAim = GetComponent<TankAimingComponent>();
        tankMove = GetComponent<TankMovementComponent>();
        player = FindObjectOfType<PlayerTankController>();
        InvokeRepeating("RandomStuff", 0, timeBetweenRandom);
    }

    // Update is called once per frame
    void Update () {

        Move();
        Attack();

        if (HitsCanTake < 0)
        {
            Dead();
        }
    }

    void RandomStuff()
    {
        Vector2 randomAimCircle = Random.insideUnitCircle;
        randomRotation = Quaternion.LookRotation(new Vector3(randomAimCircle.x, 0, randomAimCircle.y));
        randomSpeed = Random.value;
        Vector2 randomMoveCircle = Random.insideUnitCircle.normalized * 100;
        randomDirection = new Vector3(randomMoveCircle.x, 0, randomMoveCircle.y);
    }

    
    void Move()
    {
        if (player != null)
        {
            Quaternion aimRotation = Quaternion.LookRotation(player.transform.position - transform.position);

            if (Vector3.Distance(player.transform.position, transform.position) > 125.0f)
            {
                aimRotation = randomRotation;
            }
            else if (Vector3.Distance(player.transform.position, transform.position) < 40.0f)
            {
                aimRotation = aimRotation * Quaternion.Euler(0, 90, 0);
            }



            tankMove.Move(randomSpeed, aimRotation);
        }
    }

    void Attack()
    {
        if (player != null)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 125.0f)
            {
                tankAim.Aim(player.transform.position);

                Vector3 aimDirection = (player.transform.position - tankAim.transform.position);
                if (Mathf.Abs(Vector3.Distance(GetComponentInChildren<BarrelPivot>().transform.forward, aimDirection.normalized)) < 0.1f)
                {
                    tankAim.Fire();
                }
            }
            else
            {
                tankAim.Aim(randomDirection);
            }
        }
    }

    void Dead()
    {
        Destroy(gameObject);
    }


}
