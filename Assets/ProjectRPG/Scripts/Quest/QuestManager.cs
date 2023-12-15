using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance { get; private set; }

    public List<Quest> currentQuests = new();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);
    }

    public void RegistQuest(Quest data)
    {
        if (data == null) return;
        if (!currentQuests.Contains(data))
        {
            currentQuests.Add(data);
            data.OnRegistedQuest();
        }
    }

    public void RemoveQuest(Quest data)
    {
        if (data == null) return;
        if (currentQuests.Contains(data))
        {
            currentQuests.Remove(data);
        }
    }
}

public abstract class Quest : MonoBehaviour
{
    public QuestData QuestData;

    public abstract void OnRegistedQuest();

    private void Update()
    {
        if (QuestClearCheck())
        {
            OnQuestClear();
        }
    }

    public abstract bool QuestClearCheck();
    public abstract void OnQuestClear();
}