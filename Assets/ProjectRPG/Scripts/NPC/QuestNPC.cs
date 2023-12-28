using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    public QuestData QuestData;
    public GameObject SpeechBubble;
    
    private void Start()
    {
        SpeechBubble.SetActive(false);
        GetComponent<InteractableObject>().OnInteracted += (_) => { 
            QuestManager.Instance.RegistQuest(new Quest(QuestData));
            ShowStory();
        };
    }

    public void ShowStory()
    {
        SpeechBubble.SetActive(true);
        StartCoroutine(SpeechBubble.GetComponent<StoryProcessor>().Story(QuestData.Story));
    }
}
