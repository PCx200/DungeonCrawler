using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest")]
public class QuestData : ScriptableObject
{
    public string questName;

    [TextArea]
    public string description;
} 
