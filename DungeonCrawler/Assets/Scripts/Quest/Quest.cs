using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    [SerializeField] protected QuestData questData;

    [SerializeField] protected int amountToComplete;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void UpdateQuest();
}
