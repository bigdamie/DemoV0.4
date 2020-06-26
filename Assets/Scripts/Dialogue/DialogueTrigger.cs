using UnityEngine;


public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] public DialogueManager ui;
    [SerializeField] NPC theNPC = null;

    private void Start()
    {
        ui = DialogueManager.instance;

    }

    private void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            if (theNPC)
            {
                ui.currentNPC = theNPC;
                ui.inDialogue = true;
                ui.FadeUI(true, .2f, .65f);
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPCdialogue"))
            theNPC = other.GetComponent<NPC>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPCdialogue"))
            theNPC = null;
    }
}
