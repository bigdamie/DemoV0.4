using UnityEngine.EventSystems;
using UnityEngine;

public class ItemSlotButton : MonoBehaviour, IPointerClickHandler
{
    InventoryManager invMan;
    [SerializeField] Notification rightClick;
    [SerializeField] bool playerDisplayItem = false;
    [SerializeField] DisplayCaseManager display;

    private void Start()
    {
        invMan = FindObjectOfType<InventoryManager>();
        display = FindObjectOfType<DisplayCaseManager>();
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(pointerEventData.button == PointerEventData.InputButton.Right)
        {
            if(display.currDisplay)
            {
                PlayerInRange();
            }

            if (playerDisplayItem)
            {
                rightClick.Raise();

                invMan.RemoveItem(this.GetComponentInParent<InvItemShell>());

                Destroy(transform.parent.gameObject);
            }
        }
    }

    public void PlayerInRange()
    {
        playerDisplayItem = !playerDisplayItem;
    }
}
