using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Movement3D
{
    [SerializeField] private string targetTag;
    [SerializeField] private Transform target;
    [SerializeField] private float chaseRadius;
    [SerializeField] private float attackRadius;
    private float targetDistance;
    [SerializeField] AnimatorController anim; // literally just so I can wake up the log lol
    [SerializeField] StateMachine myState;

    private Vector3 tempMovement = Vector3.zero;

    public Projectile projectile;
    public float timeBtwnShoot, timer;

    void SetState(GenericState newState)
    {
        myState.ChangeState(newState);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetState(GenericState.idle);

        target = GameObject.FindGameObjectWithTag(targetTag).GetComponent<Transform>();

        Debug.Log(transform.parent);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetDistance = Vector3.Distance(transform.position, target.position);

        if (targetDistance < chaseRadius && targetDistance > attackRadius)
        {
            tempMovement = -(Vector3)(target.position - transform.position);
            Wait();
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

    public IEnumerator WeaponCo()
    {
        myState.ChangeState(GenericState.attack);
        //anim.SetAnimParameter("attacking", true);

        yield return new WaitForSeconds(timeBtwnShoot);
        Instantiate(projectile, transform.parent).dir = tempMovement;

        //anim.SetAnimParameter("attacking", false);
        myState.ChangeState(GenericState.idle);
    }

    public void Wait()
    {
        if (timer <= 0f)
        {
            Instantiate(projectile, transform.parent).dir = -tempMovement;
            timer = timeBtwnShoot;
        }
        else
            timer -= Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
