using TMPro;
using UnityEngine;

public class QuestPresenter : UIPresenter
{
    [SerializeField] QuestData questData;

    [SerializeField] TextMeshProUGUI questName;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI progressText;

    int progress = 0;

    private void Start()
    {
        questName.text = $"{questData.questName}";
        descriptionText.text = $"{questData.description}";
        progressText.text = $"Progress: ({progress}/{questData.amountToComplete})";
    }

    private void Awake()
    {
        EventBus.OnEnemyDieEvent.Subscribe(OnEnemyKilled);
        EventBus.OnItemTaken.Subscribe(OnItemTaken);
    }

    private void OnDestroy()
    {
        EventBus.OnEnemyDieEvent.Unsubscribe(OnEnemyKilled);
        EventBus.OnItemTaken.Unsubscribe(OnItemTaken);
    }

    public override void RefreshUI()
    {
        progressText.text = $"Progress: ({progress}/{questData.amountToComplete})";
    }

    void OnEnemyKilled(EnemyDieEvent e)
    {
        if (questData.questType == QuestType.Kill)
        {
            if (e.Enemy.GetType() == questData.enemyType.GetType())
            {
                progress++;
                RefreshUI();
            }
        }
    }

    void OnItemTaken(TakeItemEvent e)
    {
        if (questData.questType == QuestType.Fetch)
        {
            if (e.Item.ItemData == questData.itemToFetch)
            {
                progress++;
                RefreshUI();
            }
        }
    }
}
