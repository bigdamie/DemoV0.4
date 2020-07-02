using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Movement3D
{
    public float lifeSpan;
    public Vector3 dir;


    private void Start()
    {
        transform.localPosition = Vector3.zero;
    }

    private void FixedUpdate()
    {
        Motion(dir);

        lifeSpan -= Time.fixedDeltaTime;

        if (lifeSpan <= 0)
            Destroy(this.gameObject);
    }

}
