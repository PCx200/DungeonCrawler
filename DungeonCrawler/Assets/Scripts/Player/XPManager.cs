using UnityEngine;

public class XPManager : MonoBehaviour
{
    public static XPManager Instance { get; private set; }

    [SerializeField] int currentXP = 0;
    [SerializeField] int currentLevel = 1;
    [SerializeField] int xpToNextLevel = 100;

    public int CurrentXP => currentXP;
    public int CurrentLevel => currentLevel;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        EventBus.OnEnemyDieEvent.Subscribe(OnEnemyDie);
    }

    private void OnDisable()
    {
        EventBus.OnEnemyDieEvent.Unsubscribe(OnEnemyDie);
    }

    private void OnEnemyDie(EnemyDieEvent e)
    {
        GainXP(e.XPGivenAmount);
    }

    private void GainXP(int amount)
    {
        currentXP += amount;
        Debug.Log($"Gained {amount} XP! Total XP: {currentXP}");

        while (currentXP >= xpToNextLevel) // for multiple leveling if the xp exceedes 
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        currentXP -= xpToNextLevel;
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.25f);
        Debug.Log($"Level up! New Level: {currentLevel}");

        EventBus.OnLevelUp.Publish(new LevelUpEvent { NewLevel = currentLevel });
    }
}
