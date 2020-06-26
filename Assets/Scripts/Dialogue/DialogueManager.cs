using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public bool inDialogue;

    public static DialogueManager instance;


    [SerializeField]  public InkManager ink;
    [SerializeField]  public  NPC currentNPC;

    // UI Prefabs

    [SerializeField] private Button buttonPrefab = null;
    [SerializeField] private Sprite lilTriangle;
    public CanvasGroup dialogueBox;
    public GameObject contentArea, choiceArea;

    public CoolTMP CoolTextPrefab;
    [SerializeField] public float textSpeed;

    public string dialogueBlock; 

    private void Awake()
    {
        instance = this;
    }


    public void UpdateTextArea(CoolTMP coolTMP)
    {
        coolTMP.ReadText(dialogueBlock);
    }

    public void FadeUI(bool show, float time, float delay)
    {
        Sequence seq = DOTween.Sequence().SetUpdate(true);
        seq.AppendInterval(delay);
        seq.Append(dialogueBox.DOFade(show? 1 : 0, time));

        if(show)
        {
            seq.Join(dialogueBox.transform.DOScale(0, time * 2).From().SetEase(Ease.OutBack));

            //start the story after the box fades in.
            seq.AppendCallback(() => ink.StartStory(currentNPC.inkJSON));
        }
    }

    public void FadeOutTextbox()
    {
        FadeUI(false, 0.2f, 0);
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(.8f);
    }
  

    // Creates a textbox showing the the line of text
    public void CreateContentView(string text)
    {
        dialogueBlock = text;

        ink.animText = Instantiate(CoolTextPrefab, contentArea.transform);
        ink.animText.text = dialogueBlock;
        ink.animText.textSpeed = textSpeed;

        UpdateTextArea(ink.animText);
    }

    // Creates a button showing the choice text
    public Button CreateChoiceView(string text)
    {

        // Creates the button from a prefab
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(choiceArea.transform, false);

        // Gets the text from the button prefab
        CoolTMP choiceText = choice.GetComponentInChildren<CoolTMP>();
        choiceText.text = text;

        choiceText.nextDialogueBlock.AddListener(() => SetTriangle(choice));

        choiceText.textSpeed = textSpeed * 10f;
        choiceText.ReadText(choiceText.text);

        return choice;
    }

    void SetTriangle(Button butt)
    {
        butt.image.sprite = lilTriangle;
    }


    // Destroys all the children of this gameobject (all the UI)
    public void RemoveChildren()
    {
        int childCount = contentArea.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(contentArea.transform.GetChild(i).gameObject);
        }

        childCount = choiceArea.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(choiceArea.transform.GetChild(i).gameObject);
        }
    }

}
