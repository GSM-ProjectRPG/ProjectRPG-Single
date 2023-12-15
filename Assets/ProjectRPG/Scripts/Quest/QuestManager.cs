using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);
    }

    public List<QuestData> currentQuests = new();

    public void RegistQuest(QuestData data)
    {
        currentQuests.Add(data);
        Debug.Log(currentQuests.Count);
    }

    public void RemoveQuest(QuestData data)
    {
        if (currentQuests.Contains(data))
        {
            currentQuests.Remove(data);

        }
        Debug.Log(currentQuests.Count);
    }
}
