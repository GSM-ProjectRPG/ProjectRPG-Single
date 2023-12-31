using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    public List<Quest> CurrentQuests = new();
    public event Action<Quest> _onRegist;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        ActorManager.Instance.OnRegistedPlayer += () =>
        {
            ActorManager.Instance.Player.GetComponent<AttackSystem>().OnKill += (die) =>
            {
                Action _onCheckHuntQuest = null;
                foreach (var quest in CurrentQuests)
                {
                    if (quest.QuestData.QuestType == EQuestType.Hunt &&
                        quest.QuestData.TargetObject.GetComponent<MonsterStatSystem>()._statData.Name ==
                        die.GetComponent<MonsterStatSystem>()._statData.Name)
                    {
                        _onCheckHuntQuest += () =>
                        {
                            quest.CurrentTargetCount++;
                            ShowQuest.Instance.UpdateQuest(CurrentQuests.IndexOf(quest));
                            if (quest.QuestClearCheck()) StartCoroutine(quest.OnQuestClear());
                        };
                    }
                }

                _onCheckHuntQuest?.Invoke();
            };

            Action<Item> _onItemChange = (item) =>
            {
                Action _onCheckItemQuest = null;
                foreach (var quest in Instance.CurrentQuests)
                {
                    if (quest.QuestData.QuestType == EQuestType.Item && quest.QuestData.ItemData.itemID == item.ItemID)
                    {
                        _onCheckItemQuest += () =>
                        {
                            quest.CurrentTargetCount = item.count;
                            ShowQuest.Instance.UpdateQuest(CurrentQuests.IndexOf(quest));
                            if (quest.QuestClearCheck()) StartCoroutine(quest.OnQuestClear());
                        };
                    }
                }

                _onCheckItemQuest?.Invoke();
            };

            ActorManager.Instance.Player.GetComponent<Inventory>().OnAddItemAction += _onItemChange;
            ActorManager.Instance.Player.GetComponent<Inventory>().OnReduceItemAction += _onItemChange;

            _onRegist += (quest) =>
            {
                if (quest.QuestData.QuestType == EQuestType.Item)
                {
                    foreach (var item in ActorManager.Instance.Player.GetComponent<Inventory>().ItemList)
                    {
                        if (item.ItemID == quest.QuestData.ItemData.itemID)
                        {
                            quest.CurrentTargetCount = item.count;
                            ShowQuest.Instance.UpdateQuest(CurrentQuests.IndexOf(quest));
                            if (quest.QuestClearCheck()) StartCoroutine(quest.OnQuestClear());
                        }
                    }
                }
            };
        };
    }

    public void RegistQuest(Quest data)
    {
        if (data == null) return;
        foreach (var quest in CurrentQuests)
        {
            if (quest.QuestData.QuestId == data.QuestData.QuestId) return;
        }
        CurrentQuests.Add(data);
        ShowQuest.Instance.AddQuest(CurrentQuests.Count - 1);
        _onRegist?.Invoke(data);
    }

    public void RemoveQuest(Quest data)
    {
        if (data == null) return;
        foreach (var quest in CurrentQuests)
        {
            if (quest.QuestData.QuestId == data.QuestData.QuestId)
            {
                ShowQuest.Instance.RemoveQuest(CurrentQuests.IndexOf(data));
                CurrentQuests.Remove(data);
                return;
            }
        }
    }
}

public class Quest
{
    public QuestData QuestData;
    public int CurrentTargetCount;

    public Quest(QuestData questData)
    {
        QuestData = questData;
        CurrentTargetCount = 0;
    }

    public bool QuestClearCheck()
    {
        return CurrentTargetCount == QuestData.TargetCount;
    }

    public IEnumerator OnQuestClear()
    {
        yield return new WaitForSeconds(0.5f);
        ActorManager.Instance.Player.GetComponent<CoinSystem>().Coin += QuestData.RewardCoin;
        ActorManager.Instance.Player.GetComponent<PlayerStatSystem>().AddExp(QuestData.RewardExp);
        if (QuestData.RewardItem != null)
        {
            ActorManager.Instance.Player.GetComponent<Inventory>().AddItem(new Item(QuestData.RewardItem, QuestData.RewardItemCount));
        }
        CurrentTargetCount = 0;
        QuestManager.Instance.RemoveQuest(this);
    }
}