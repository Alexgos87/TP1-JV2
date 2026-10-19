using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 0.5f;

    [Header("Projectiles")]
    [SerializeField] private int maxMissiles = 10;
    [SerializeField] private int missilesNumber = 10;

    private readonly List<Portal> activePortals = new();

    private bool isGameOver;
    private bool isRestarting;

    private void Awake()
    {
        var others = FindObjectsByType<GameController>(
            FindObjectsSortMode.None
        );

        if (others.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        var eventChannels = Finder.EventChannels;

        if (eventChannels != null)
            eventChannels.PublishMissilesChanged(missilesNumber);
    }

    private void OnEnable()
    {
        var eventChannels = Finder.EventChannels;

        if (eventChannels != null)
            eventChannels.OnGameEnded += OnGameEnded;

        RefreshPortals();

        if (!isGameOver)
            StartCoroutine(SpawnAliensRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();

        var eventChannels = Finder.EventChannels;

        if (eventChannels != null)
            eventChannels.OnGameEnded -= OnGameEnded;
    }

    private void RefreshPortals()
    {
        activePortals.Clear();

        foreach (var portal in FindObjectsByType<Portal>(
                     FindObjectsSortMode.None))
        {
            RegisterPortal(portal);
        }
    }

    public void RegisterPortal(Portal portal)
    {
        if (portal != null && !activePortals.Contains(portal))
            activePortals.Add(portal);
    }

    public void UnRegisterPortal(Portal portal)
    {
        if (portal == null || isGameOver || isRestarting)
            return;

        activePortals.Remove(portal);

        if (activePortals.Count == 0)
        {
            Finder.EventChannels?.PublishGameEnded(true);
        }
    }

    public void AddMissiles()
    {
        if (isGameOver)
            return;

        missilesNumber = Mathf.Min(missilesNumber + 5, maxMissiles);
        Finder.EventChannels?.PublishMissilesChanged(missilesNumber);
    }

    public void UseMissiles()
    {
        if (isGameOver || missilesNumber <= 0)
            return;

        missilesNumber--;
        Finder.EventChannels?.PublishMissilesChanged(missilesNumber);
    }

    public bool CanUseMissiles()
    {
        return !isGameOver && missilesNumber > 0;
    }

    private IEnumerator SpawnAliensRoutine()
    {
        var wait = new WaitForSeconds(spawnInterval);

        while (!isGameOver)
        {
            if (activePortals.Count > 0)
            {
                int index = Random.Range(0, activePortals.Count);
                Portal portal = activePortals[index];

                if (portal != null)
                    portal.SpawnAlien();
            }

            yield return wait;
        }
    }

    private void OnGameEnded(bool isVictory)
    {
        if (isGameOver)
            return;

        isGameOver = true;
        StopAllCoroutines();
    }

    public void RestartGame()
    {
        if (isRestarting)
            return;

        isRestarting = true;
        StopAllCoroutines();

        // Empêcher les portails de déclencher une victoire
        // pendant leur réactivation.
        isGameOver = true;

        // Réinitialiser le joueur.
        SpaceMarineDeath playerDeath =
            FindFirstObjectByType<SpaceMarineDeath>();

        if (playerDeath != null)
            playerDeath.ResetPlayer();

        // Réinitialiser les portails.
        activePortals.Clear();

        foreach (Portal portal in FindObjectsByType<Portal>(
                     FindObjectsSortMode.None))
        {
            if (portal != null)
                portal.ResetPortal();
        }

        // Réinitialiser les munitions.
        missilesNumber = maxMissiles;
        Finder.EventChannels?.PublishMissilesChanged(missilesNumber);

        // Réinitialiser l'état de la partie.
        isGameOver = false;
        isRestarting = false;

        // Relancer le spawn des aliens.
        StartCoroutine(SpawnAliensRoutine());

        Debug.Log("GameController : partie réinitialisée.");
    }
    public void QuitGame()
    {
        Application.Quit();

        UnityEditor.EditorApplication.isPlaying = false;
    }
}