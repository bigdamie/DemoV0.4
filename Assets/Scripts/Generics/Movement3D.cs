using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public Rigidbody myRigidbody;

    public virtual void Motion(Vector3 direction)
    {

        direction = direction.normalized;
        myRigidbody.velocity = direction * speed;
    }
}
