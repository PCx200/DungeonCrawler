using UnityEngine;
using UnityEngine.UI;

public class XPManager : MonoBehaviour
{
    public static XPManager Instance { get; private set; }

    //[SerializeField] int currentXP = 0;
    //[SerializeField] int currentLevel = 1;
    //[SerializeField] int xpToNextLevel = 100;

    [SerializeField] PlayerProgressData progressData;

    [SerializeField] Image XPBar;

    public int CurrentXP => progressData.XP;
    public int CurrentLevel => progressData.Level;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        XPBar.fillAmount = (float)progressData.XP / progressData.XPToNextLevel;
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
        progressData.XP += amount;
        Debug.Log($"Gained {amount} XP! Total XP: {progressData.XP}");
        XPBar.fillAmount = (float)progressData.XP / progressData.XPToNextLevel;

        while (progressData.XP >= progressData.XPToNextLevel) // for multiple leveling if the xp exceedes 
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        progressData.Level++;
        progressData.XP -= progressData.XPToNextLevel;
        progressData.XPToNextLevel = Mathf.RoundToInt(progressData.XPToNextLevel * 1.25f);
        Debug.Log($"Level up! New Level: {progressData.Level}");

        EventBus.OnLevelUp.Publish(new LevelUpEvent { NewLevel = progressData.Level });
    }
}
