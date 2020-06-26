using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : Movement3D
{
    [SerializeField] private AnimatorController anim;
    [SerializeField] private StateMachine myState;
    [SerializeField] private float WeaponAttackDuration;
    [SerializeField] private Collider hitBoxGO;
    [SerializeField] private Collider interactionColGO;
    //[SerializeField] private ReceiveItem myItem;

    private Vector3 tempMovement = Vector3.back;


    // Start is called before the first frame update
    void Start()
    {
        myState.ChangeState(GenericState.idle);
    }

    // Update is called once per frame
    void Update()
    {
        if (myState.myState == GenericState.receiveItem)
        {
            if (Input.GetButtonDown("Check"))
            {
                myState.ChangeState(GenericState.idle);
                anim.SetAnimParameter("receiveItem", false);
                //myItem.ChangeSpriteState();
            }
            return;
        }
        GetInput();
        SetAnimation();
    }


    void SetState(GenericState newState)
    {
        myState.ChangeState(newState);
    }


    void GetInput()
    {

        if (Input.GetButtonDown("Swing"))
        {           
            tempMovement = Vector3.zero;
            Motion(tempMovement);
            StartCoroutine(WeaponCo());
        }
        else if (myState.myState != GenericState.attack)
        {
            tempMovement.x = Input.GetAxisRaw("Horizontal");
            tempMovement.z = Input.GetAxisRaw("Vertical");


            Motion(tempMovement);
        }
        else
        {
            tempMovement = Vector3.zero;
            Motion(tempMovement);
        }

        

    }

    void SetAnimation()
    {
        if (tempMovement.magnitude > 0)
        {
            anim.SetAnimParameter("moveX", Mathf.Round(tempMovement.x));
            anim.SetAnimParameter("moveY", Mathf.Round(tempMovement.z));
            anim.SetAnimParameter("moving", true);
            SetState(GenericState.walk);

            hitBoxGO.transform.localPosition = new Vector3(tempMovement.x/2f, tempMovement.z/2f, tempMovement.z/2f);
            interactionColGO.transform.localPosition = tempMovement/1.75f;
        }
        else
        {
            anim.SetAnimParameter("moving", false);
            if (myState.myState != GenericState.attack)
            {
                SetState(GenericState.idle);
            }
        }
    }

    public IEnumerator WeaponCo()
    {
        

        myState.ChangeState(GenericState.attack);
        anim.SetAnimParameter("attacking", true);
        yield return new WaitForSeconds(WeaponAttackDuration);
        anim.SetAnimParameter("attacking", false);
        myState.ChangeState(GenericState.idle);
        tempMovement = Vector3.zero;
    }
}
