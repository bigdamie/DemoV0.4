using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //private CoolTMP animatedText;
    [SerializeField] public TextAsset inkJSON;

    private void Start()
    {
        //animatedText = DialogueManager.instance.animatedText;
        //animatedText.onEmotionChange.AddListener((newEmote) => EmotionChanger(newEmote));
        //animatedText.PoseChanger.AddListener((newPose) => PoseChanger(newPose));
    }

    public void EmotionChanger(string emotion)
    {
        //if emotion==something
        // doSomething();
    }

    public void PoseChanger(string pose)
    {
        //if pose == something
        // doSomething();
    }

  
}
