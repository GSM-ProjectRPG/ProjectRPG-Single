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
            ShowStory();
            QuestManager.Instance.RegistQuest(new Quest(QuestData));
        };
    }

    public void ShowStory()
    {
        foreach (var quest in QuestManager.Instance.CurrentQuests)
        {
            if (quest.QuestData.QuestId == QuestData.QuestId) return;
        }
        SpeechBubble.SetActive(true);
        StartCoroutine(SpeechBubble.GetComponent<StoryProcessor>().Story(QuestData.Story));
    }
}
