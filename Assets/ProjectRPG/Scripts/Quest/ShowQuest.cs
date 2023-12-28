using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowQuest : MonoBehaviour
{
    public static ShowQuest Instance { get; private set; }

    public Transform Parent;
    public GameObject QuestPrefeb;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public void AddQuest(int idx)
    {
        if (idx == -1) return;
        QuestPrefeb.GetComponentInChildren<Text>().text = QuestManager.Instance.CurrentQuests[idx].QuestData.QuestContents + " (" + QuestManager.Instance.CurrentQuests[idx].CurrentTargetCount + "/" + QuestManager.Instance.CurrentQuests[idx].QuestData.TargetCount + ")";
        Instantiate(QuestPrefeb, Parent);
    }

    public void UpdateQuest(int idx)
    {
        if (idx == -1) return;
        Parent.GetChild(idx).GetComponentInChildren<Text>().text = QuestManager.Instance.CurrentQuests[idx].QuestData.QuestContents + " (" + QuestManager.Instance.CurrentQuests[idx].CurrentTargetCount + "/" + QuestManager.Instance.CurrentQuests[idx].QuestData.TargetCount + ")";
    }

    public void RemoveQuest(int idx)
    {
        if (idx == -1) return;
        Destroy(Parent.GetChild(idx).gameObject);
    }
}
