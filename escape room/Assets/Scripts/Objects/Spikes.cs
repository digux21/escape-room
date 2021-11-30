using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    [SerializeField] float damageAmount = 5;

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("player damaged!");
            Health health = collision.gameObject.GetComponent<Health>();
            health.DamageThePlayer(damageAmount);
        }

    }


}
