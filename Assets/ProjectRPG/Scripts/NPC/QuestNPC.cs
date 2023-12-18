using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    public AQuest Quest;

    private void Start()
    {
        GetComponent<InteractableObject>().OnInteracted += (regist) => { QuestManager.Instance.RegistQuest(Quest); };
    }
}
