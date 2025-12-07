using UnityEngine;


[CreateAssetMenu(menuName = "Quest/Data")]
public class QuestData : ScriptableObject
{
    public QuestType questType;

    public string questName;
    public string description;

    public int amountToComplete;

    public Enemy enemyType;
    public ItemData itemToFetch;
}

public enum QuestType
{
    Kill,
    Fetch
}