using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] GameObject player;

    [Header("Health")]
    [SerializeField] [Range(0f, 100f)]
    private float _health;

    private float _maxHealth;


    public float health
    {
        get { return _health; }
        set
        {
            if (_health > _maxHealth)
                _health = _maxHealth;
            else if (_health < 0)
                _health = 0;
            else
                _health = value;
        }
    }

    

    public void DamageThePlayer(float damageAmount)
    {

        _health -= damageAmount;

        if (_health == 0)
        {
            Destroy(player);
        }
    }

}
