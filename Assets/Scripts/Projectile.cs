using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ProjectileWall")
        {
            Destroy(gameObject);
        }
        else if (other.tag == "Enemy")
        {
            other.GetComponent<AITankController>().HitsCanTake -= 1;
            Destroy(gameObject);
        }
        else if (other.tag == "Player")
        {
            other.GetComponent<PlayerTankController>().HitsCanTake -= 1;
            Destroy(gameObject);
        }
    }

}
