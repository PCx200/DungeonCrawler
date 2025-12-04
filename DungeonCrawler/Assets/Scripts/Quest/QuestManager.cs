using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    List<Quest> questsAvailable = new List<Quest>();

    List<Quest> completedQuests = new List<Quest>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddQuest(List<Quest> quests, Quest quest)
    {
        quests.Add(quest);
    }

    void RemoveQuest(List<Quest> quests, Quest quest)
    { 
        quests.Remove(quest);
    }
}
