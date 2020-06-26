using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Knockback : MonoBehaviour
{

    [SerializeField] string otherTag;
    [SerializeField] float knockTime;
    [SerializeField] float knockStrength;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(otherTag) && other.isTrigger )
        {

            Rigidbody temp = other.GetComponentInParent<Rigidbody>();
            if (temp)
            {
                Vector3 direction = other.transform.position - transform.position;

                //CheckCollision(other, direction);

                temp.DOMove((Vector3)other.transform.position +
                    (direction.normalized * knockStrength), knockTime);
            }


        }
    }

   
}
