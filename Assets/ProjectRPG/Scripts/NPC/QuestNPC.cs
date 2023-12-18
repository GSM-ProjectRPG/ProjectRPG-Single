using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    public Quest quest;

    private void Start()
    {
        GetComponent<InteractableObject>().OnInteracted += (regist) => { QuestManager.instance.RegistQuest(quest); };
    }
}
