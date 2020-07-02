using UnityEngine;
using DG.Tweening;

public class PauseManager : MonoBehaviour
{
    [SerializeField] float tweenTime, tweenDelay;
    [SerializeField] Vector2 offScreen, pausePos, invPos;
    
    [SerializeField] private bool isPaused = false;
    [SerializeField] private CanvasGroup pauseMenu;
    [SerializeField] private CanvasGroup inventoryMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Pause"))
        {
            ChangePauseValue();
        }
    }

    private void Start()
    {
        //By default, the pausemenu will stay in its proper position
        //Only 1 UI element can be in the proper position at a time
        pausePos = pauseMenu.transform.position;
        invPos = inventoryMenu.transform.position;
    }
    void ChangePauseValue()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            //Time.timeScale = 0;

            inventoryMenu.transform.position = offScreen;
            pauseMenu.transform.position = pausePos;

            FadeUI(isPaused, tweenTime, tweenDelay, pauseMenu);

        }
        else
        {
            //Time.timeScale = 1;

            if (inventoryMenu.alpha == 1)
                FadeOutTextbox(inventoryMenu);
            else
                FadeOutTextbox(pauseMenu);
        }
    }

    
    public void OpenInventory()
    {
        if (isPaused)
        {
            pauseMenu.transform.position = offScreen;
            inventoryMenu.transform.position = invPos;

            FadeUI(isPaused, tweenTime, tweenDelay, inventoryMenu);

            FadeOutTextbox(pauseMenu);
        }
    }

    public void ResumeButton()
    {
        ChangePauseValue();
    }

    public void FadeUI(bool show, float time, float delay, CanvasGroup UIelement)
    {
        Sequence seq = DOTween.Sequence().SetUpdate(true);
        seq.AppendInterval(delay);
        seq.Append(UIelement.DOFade(show ? 1 : 0, time));

        if (show)
        {
            seq.Join(UIelement.transform.DOScale(0, time * 2).From().SetEase(Ease.OutBack));
            
        }
    }

    public void FadeOutTextbox(CanvasGroup UIelement)
    {
        FadeUI(false, 0.2f, 0, UIelement);
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(.8f);
    }

}
