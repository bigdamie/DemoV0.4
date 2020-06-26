using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TMPro
{
    [System.Serializable] public class DialogueEvent : UnityEvent { }
    [System.Serializable] public class ChangeSomething : UnityEvent<string> { }
    
    public class CoolTMP : TextMeshProUGUI
    {
        public CoolTMP instance;

        public float textSpeed;

        public ChangeSomething onEmotionChange;
        public ChangeSomething onPoseChange;

        public DialogueEvent onDialogueFinish;
        public DialogueEvent nextDialogueBlock;
        public DialogueEvent newDialogue;
        

        protected override void OnEnable()
        {
            instance = this;
        }

        public void ReadText(string newText)
        {
            text = string.Empty;

            // split the whole text into parts based off the <> tags 
            // even numbers in the array are text, odd numbers are tags
            string[] subTexts = newText.Split('/', '/');

            // textmeshpro still needs to parse its built-in tags, so we only include noncustom tags
            string displayText = "";
            for (int i = 0; i < subTexts.Length; i++)
            {
                if (i % 2 == 0)
                    displayText += subTexts[i];
                else if (!isCustomTag(subTexts[i].Replace(" ", "")))
                    displayText += $"<{subTexts[i]}>";
            }

            bool isCustomTag(string tag)
            {
                // /pose/ = char portrait change /emote/ = sprite changes
                // /new/ = new dialogue available /c/ = continue             

                //add more custom tags with Shannon and pals
                return tag.StartsWith("emote=") || tag.StartsWith("pose=") 
                    || tag.StartsWith("c") || tag.StartsWith("new") ;
            }

            text = displayText;

            maxVisibleCharacters = 0;
            StartCoroutine(Read());

            IEnumerator Read()
            {
                int subCounter = 0;
                int visibleCounter = 0;
                while(subCounter < subTexts.Length)
                {
                    if(subCounter % 2 ==1)
                    { yield return CheckTag(subTexts[subCounter].Replace(" ", "")); }
                    else
                    {
                        while( visibleCounter < subTexts[subCounter].Length)
                        {
                            //onTextReveal event invocation here
                            visibleCounter++;
                            maxVisibleCharacters++;
                            yield return new WaitForSecondsRealtime(1f / textSpeed);
                        }
                        visibleCounter = 0;
                    }
                    subCounter++;
                }
                yield return null;

                WaitForSecondsRealtime CheckTag(string tag)
                {
                    if(tag.Length > 0)
                    {
                        if (tag.StartsWith("emote="))
                        {
                            onEmotionChange.Invoke(tag.Split('=')[1]);
                        }

                        else if (tag.StartsWith("pose="))
                        {
                            onPoseChange.Invoke(tag.Split('=')[1]);
                        }

                        else if(tag.StartsWith("c"))
                        {
                            nextDialogueBlock.Invoke();
                        }

                        else if(tag.StartsWith("new"))
                        {
                            newDialogue.Invoke();
                        }
                        //else if more tags              
                    }

                    return null;
                }

                onDialogueFinish.Invoke();
            }
        }
    }
}