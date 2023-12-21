using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    public QuestData QuestData;
    
    private void Start()
    {
        GetComponent<InteractableObject>().OnInteracted += (_) => { QuestManager.Instance.RegistQuest(new Quest(QuestData)); };
    }
}
