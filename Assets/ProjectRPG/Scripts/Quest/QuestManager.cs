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
        else Destroy(this);
    }

    public void RegistQuest(Quest data)
    {
        if (data == null) return;
        if (!currentQuests.Contains(data))
        {
            currentQuests.Add(data);
            data.OnRegistedQuest();
            data.isQuestProceeding = true;
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
    public QuestData questData;

    public bool isRegistInteraction;
    public bool isQuestProceeding;

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