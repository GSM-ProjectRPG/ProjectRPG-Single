using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    public List<AQuest> CurrentQuests = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public void RegistQuest(AQuest data)
    {
        if (data == null) return;
        if (!CurrentQuests.Contains(data))
        {
            CurrentQuests.Add(data);
            data.OnRegistedQuest();
            data.IsQuestProceeding = true;
        }
    }

    public void RemoveQuest(AQuest data)
    {
        if (data == null) return;
        if (CurrentQuests.Contains(data))
        {
            CurrentQuests.Remove(data);
        }
    }
}

public abstract class AQuest : MonoBehaviour
{
    public QuestData QuestData;

    public bool IsQuestProceeding;

    public abstract void OnRegistedQuest();

    public void QuestClearCheck()
    {
        if (IsQuestClear())
        {
            OnQuestClear();
        }
    }

    public abstract bool IsQuestClear();
    public abstract void OnQuestClear();
}