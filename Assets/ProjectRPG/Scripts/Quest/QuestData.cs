using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EQuestType { Hunt, Item }

[CreateAssetMenu(fileName = "QuestData", menuName = "Scriptable Object/QuestData", order = int.MaxValue)]
public class QuestData : ScriptableObject
{
    public int QuestId;

    public List<string> Story;
    public string QuestContents;

    public EQuestType QuestType;

    public GameObject TargetObject;
    public ItemData ItemData;
    public int TargetCount;

    public int RewardCoin;
    public int RewardExp;
    public ItemData RewardItem;
    public int RewardItemCount;
}
