using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class NotificationListener : MonoBehaviour
{
    public UnityEvent myEvent;
    [SerializeField]public List<Notification> myNotifications;

    public void OnEnable()
    {
        foreach(Notification note in myNotifications)
        note.RegisterListener(this);
    }    

    public void OnDisable()
    {
        foreach (Notification note in myNotifications)
            note.DeregisterListener(this);
    }

    public void Raise()
    {
        myEvent.Invoke();
    }
}
