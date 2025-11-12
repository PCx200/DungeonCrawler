using UnityEngine;
using UnityEngine.UI;

public class XPManager : MonoBehaviour
{
    public static XPManager Instance { get; private set; }

    //[SerializeField] int currentXP = 0;
    //[SerializeField] int currentLevel = 1;
    //[SerializeField] int xpToNextLevel = 100;

    [SerializeField] Player player;

    [SerializeField] Image XPBar;

    public int CurrentXP => player.ProgressData.XP;
    public int CurrentLevel => player.ProgressData.Level;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        XPBar.fillAmount = (float)player.ProgressData.XP / player.ProgressData.XPToNextLevel;
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
        player.ProgressData.XP += amount;
        Debug.Log($"Gained {amount} XP! Total XP: {player.ProgressData.XP}");
        XPBar.fillAmount = (float)player.ProgressData.XP / player.ProgressData.XPToNextLevel;

        while (player.ProgressData.XP >= player.ProgressData.XPToNextLevel) // for multiple leveling if the xp exceedes 
        {
            player.LevelUp();
        }
    }
}
