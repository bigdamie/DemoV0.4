using Ink.Runtime;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class InkManager : MonoBehaviour
{
	public static event Action<Story> OnCreateStory;

	[SerializeField]
	public Story story;

	[SerializeField] DialogueManager ui;

	[SerializeField] public CoolTMP animText;

	void Awake()
	{
		// Remove the default message
		ui?.RemoveChildren();
	}

	// Creates a new Story object with the compiled story which we can then play!
	public void StartStory(TextAsset inkJSONAsset)
	{
		Time.timeScale = 0;
		story = new Story(inkJSONAsset.text);
		OnCreateStory?.Invoke(story);
		RefreshView();
	}

	// This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
	void RefreshView()
	{
		// Remove all the UI on screen
		ui.RemoveChildren();

		// Read all the content until we can't continue any more
		while (story.canContinue)
		{
			// Continue gets the next line of the story
			string text = story.Continue();
			// This removes any white space from the text.
			text = text.Trim();
			// Display the text on screen!
			ui.CreateContentView(text);
		}

		
			// Display all the choices, if there are any!
			if (story.currentChoices.Count > 0)
			{
				for (int i = 0; i < story.currentChoices.Count; i++)
				{
					Choice choice = story.currentChoices[i];
					Button button = ui.CreateChoiceView(choice.text.Trim());
					// Tell the button what to do when we press it
					button.onClick.AddListener(delegate
					{
						OnClickChoiceButton(choice);
					});
				}
			}
			// If we've read all the content and there's no choices, the story is finished!
			else
			{
				ui.FadeOutTextbox();
				Time.timeScale = 1;
			}

		
	}

	// When we click the choice button, tell the story to choose that choice!
	void OnClickChoiceButton(Choice choice)
	{
		story.ChooseChoiceIndex(choice.index);
		RefreshView();
	}

}
