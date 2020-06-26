using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{

    [SerializeField] private GameObject deathEffect;


    public override void Damage(int damage)
    {
        base.Damage(damage);
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    

    public virtual void Die()
    {
        if(deathEffect)
        Instantiate(deathEffect, transform.position, transform.rotation);
        
        this.transform.parent.gameObject.SetActive(false);
    }

}
