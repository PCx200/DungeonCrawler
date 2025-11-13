using TMPro;
using UnityEngine;

public class PlayerNamePresenter : UIPresenter
{
    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] Player player;

    public override void RefreshUI()
    {
        playerNameText.text = player.Name;
    }

    void Start()
    {
        RefreshUI();
    }
}
