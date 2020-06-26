using UnityEngine.EventSystems;
using UnityEngine;

public class ItemSlotButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Notification rightClick;
    [SerializeField] bool playerDisplayItem = false;
   public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(pointerEventData.button == PointerEventData.InputButton.Right)
        {
            if (playerDisplayItem)
            {
                rightClick.Raise();

                Destroy(transform.parent.gameObject);
            }
        }
    }

    public void PlayerInRange()
    {
        playerDisplayItem = !playerDisplayItem;
    }
}
