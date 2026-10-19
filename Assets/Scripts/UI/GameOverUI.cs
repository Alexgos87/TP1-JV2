using System.Collections;
using TMPro;
using UnityEngine;
using Unity.Cinemachine;

public class GameOverUI : MonoBehaviour
{
    [Header("Interface")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private GameObject replayButton;
    [SerializeField] private GameObject quitButton;

    [Header("Caméra")]
    [SerializeField] private CinemachineBrain cinematicBrain;

    [Header("Messages")]
    [SerializeField] private string victoryMessage = "Victoire !";
    [SerializeField] private string defeatMessage = "Défaite...";

    [Header("Couleurs")]
    [SerializeField] private Color victoryColor = new Color32(0x49, 0xFF, 0x7A, 0xFF);
    [SerializeField] private Color defeatColor = new Color32(0xFF, 0x4A, 0x56, 0xFF);

    [Header("Durée")]
    [SerializeField] private float messageDuration = 3f;

    private bool gameEnded;

    private void Awake()
    {
        HideEndScreen();
    }

    private void OnEnable()
    {
        var eventChannels = Finder.EventChannels;

        if (eventChannels != null)
            eventChannels.OnGameEnded += ShowGameOver;
        else
            Debug.LogError("GameOverUI : EventChannels introuvable.");
    }

    private void OnDisable()
    {
        var eventChannels = Finder.EventChannels;

        if (eventChannels != null)
            eventChannels.OnGameEnded -= ShowGameOver;
    }

    private void ShowGameOver(bool isVictory)
    {
        if (gameEnded)
            return;

        gameEnded = true;

        // Désactiver uniquement le composant CinemachineBrain.
        if (cinematicBrain != null)
            cinematicBrain.enabled = false;

        // Libérer la souris.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Afficher l'écran de fin.
        canvas.enabled = true;

        messageText.text = isVictory ? victoryMessage : defeatMessage;
        messageText.color = isVictory ? victoryColor : defeatColor;
        messageText.gameObject.SetActive(true);

        replayButton.SetActive(false);
        quitButton.SetActive(false);

        StartCoroutine(ShowButtonsAfterDelay());
    }

    private IEnumerator ShowButtonsAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);

        messageText.gameObject.SetActive(false);

        replayButton.SetActive(true);
        quitButton.SetActive(true);
    }

    private void HideEndScreen()
    {
        StopAllCoroutines();

        // Cacher l'interface de fin.
        if (canvas != null)
            canvas.enabled = false;

        if (messageText != null)
            messageText.gameObject.SetActive(false);

        if (replayButton != null)
            replayButton.SetActive(false);

        if (quitButton != null)
            quitButton.SetActive(false);
    }

    public void ResetUI()
    {
        // Réinitialiser l'état de l'interface.
        gameEnded = false;
        HideEndScreen();

        // Réactiver la caméra.
        if (cinematicBrain != null)
            cinematicBrain.enabled = true;

        // Reverrouiller la souris pour reprendre le jeu.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Replay()
    {
        // Réinitialiser l'interface, la caméra et le curseur.
        ResetUI();

    }

    public void Quit()
    {

        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}