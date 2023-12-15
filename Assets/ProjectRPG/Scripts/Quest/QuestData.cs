using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType { Hunt, Item }

[CreateAssetMenu(fileName = "QuestData", menuName = "Scriptable Object/QuestData", order = int.MaxValue)]
public class QuestData : ScriptableObject
{
    public int QuestID;

    public List<string> story;
    public string questContents;

    public QuestType questType;

    public GameObject targetObject;
    public int targetCount;

    public int rewardGold;
    public GameObject rewardItem;
    public int rewardItemCound;
}
