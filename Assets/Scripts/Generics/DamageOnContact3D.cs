using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact3D : Damage
{
    [SerializeField] private string otherString;
    [SerializeField] private int damageAmount;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(otherString) && other.isTrigger)
        {
            Health temp = other.gameObject.GetComponent<Health>();
            if (temp)
            {
                ApplyDamage(temp, damageAmount);
            }
        }
    }
}
