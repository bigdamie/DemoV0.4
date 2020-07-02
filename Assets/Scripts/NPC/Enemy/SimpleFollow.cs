using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : Movement3D
{

    [SerializeField] private string targetTag;
    [SerializeField] private Transform target;
    [SerializeField] private float chaseRadius;
    [SerializeField] private float attackRadius;
    private float targetDistance;
    [SerializeField] AnimatorController anim; // literally just so I can wake up the log lol
    [SerializeField] StateMachine myState;

    private Vector3 tempMovement = Vector3.zero;


    void SetState(GenericState newState)
    {
        myState.ChangeState(newState);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetState(GenericState.idle);

        target = GameObject.FindGameObjectWithTag(targetTag).GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetDistance = Vector3.Distance(transform.position, target.position);

        if (targetDistance < chaseRadius && targetDistance > attackRadius)
        {
            tempMovement = (Vector3)(target.position - transform.position);
        }
        else
        {
            tempMovement = Vector3.zero;
        }

        Motion(tempMovement);


        SetAnimation();
     }

    void SetAnimation()
    {
        if (tempMovement.magnitude > 0)
        {
            anim.SetAnimParameter("moveX", Mathf.Round(tempMovement.x));
            anim.SetAnimParameter("moveY", Mathf.Round(tempMovement.z));
            anim.SetAnimParameter("wakeUp", true);
            SetState(GenericState.walk);
        }
        else
        {
            anim.SetAnimParameter("wakeUp", false);
            if (myState.myState != GenericState.attack)
            {
                SetState(GenericState.idle);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
