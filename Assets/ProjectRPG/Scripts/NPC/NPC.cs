using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Quest quest;

    private void Update()
    {
        if (!quest.isRegistInteraction)
        {
            GetComponent<InteractableObject>().OnInteracted += (regist) => { QuestManager.instance.RegistQuest(quest); };
            quest.isRegistInteraction = true;
        }
    }
}
