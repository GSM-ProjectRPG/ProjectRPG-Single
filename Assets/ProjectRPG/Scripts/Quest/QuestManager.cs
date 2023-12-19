using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

    private void Start()
    {
        GameObject.Find("Player").GetComponent<AttackSystem>().OnKill += (die) =>
        {
            Action _onCheckQuest = null;
            foreach (var quest in Instance.CurrentQuests)
            {
                if (quest.QuestData.TargetObject.name == die.name)
                {
                    _onCheckQuest += () =>
                    {
                        quest.CurrentTargetCount++;
                        quest.QuestClearCheck();
                    };
                }
            }
            _onCheckQuest?.Invoke();
        };
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
    public int CurrentTargetCount;
    public bool IsQuestProceeding;

    public abstract void OnRegistedQuest();

    public void QuestClearCheck()
    {
        if (IsQuestClear())
        {
            Debug.Log("sklfjsdf");
            OnQuestClear();
        }
    }

    public abstract bool IsQuestClear();
    public abstract void OnQuestClear();
}