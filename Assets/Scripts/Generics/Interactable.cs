using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    [SerializeField] public bool playerInRange;
    public bool stay = false;
    [SerializeField] public string otherTag;
	[SerializeField] public Notification myNotification;

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(otherTag))
        {
            playerInRange = true;
			myNotification?.Raise();
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(otherTag))
        {
            playerInRange = false;
			myNotification?.Raise();
        }
    }
}
