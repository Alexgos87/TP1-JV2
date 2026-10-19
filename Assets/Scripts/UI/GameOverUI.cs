using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private string victoryMessage = "Victoire !";
    [SerializeField] private string defeatMessage = "Défaite...";
    [SerializeField] private Color victoryColor = new Color32(0x49, 0xFF, 0x7A, 0xFF);
    [SerializeField] private Color defeatColor = new Color32(0xFF, 0x4A, 0x56, 0xFF);

    private void Awake()
    {
        messageText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Finder.EventChannels.OnGameEnded += ShowMessage;
    }

    private void OnDisable()
    {
        var eventChannels = Finder.EventChannels;
        if (eventChannels != null)
            eventChannels.OnGameEnded -= ShowMessage;
    }

    private void ShowMessage(bool isVictory)
    {
        messageText.text = isVictory ? victoryMessage : defeatMessage;
        messageText.color = isVictory ? victoryColor : defeatColor;
        messageText.gameObject.SetActive(true);
    }
}