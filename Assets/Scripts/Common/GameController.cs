using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int playerHealth = 50;
    [SerializeField] private float spawnInterval = 0.5f; // Intervalle entre chaque spawn

    [Header("Projectiles")]
    [SerializeField] private int maxMissiles = 10; // Nombre maximum de missiles actifs
    [SerializeField] private int missilesNumber = 10; // nombre de missiles actuellement actifs

    private List<Portal> activePortals = new();

    private static bool s_isQuitting;

    public static bool IsQuitting => s_isQuitting;

    private void Awake()
    {
        // Evite les doublons si vous avez plusieurs GameController dans la sc�ne
        var others = FindObjectsOfType<GameController>();
        if (others.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        // D�commenter si vous souhaitez que ce GameObject survive entre les sc�nes
        // DontDestroyOnLoad(gameObject);

        // Tentative de s'assurer que l'objet poss�de le bon tag (ignore si le tag n'existe pas)
        if (gameObject.tag != "GameController")
        {
            try
            {
                gameObject.tag = "GameController";
            }
            catch
            {
                // Le tag "GameController" n'existe pas dans le projet � cr�ez-le manuellement si besoin.
            }
        }
    }

    private void OnApplicationQuit()
    {
        s_isQuitting = true;
    }

    private void OnDestroy()
    {
        // Si l'application est en train de quitter, ne recr�ez rien ici.
        if (s_isQuitting) return;

        // Placez ici le nettoyage qui n'instancie pas de nouveaux GameObjects.
    }

    private void OnEnable()
    {

        foreach (var portal in FindObjectsByType<Portal>(FindObjectsSortMode.None))
            RegisterPortal(portal);

        StartCoroutine(SpawnAliensRoutine());

        var eventChannels = Finder.EventChannels;
        if (eventChannels != null)
            eventChannels.OnPlayerHealthChanged += OnPlayerHealthChanged;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        var eventChannels = Finder.EventChannels;
        if (eventChannels != null)
            eventChannels.OnPlayerHealthChanged -= OnPlayerHealthChanged;
    }

    private void OnPlayerHealthChanged(int _playerHealth)
    {
        this.playerHealth += _playerHealth;
    }
    
    public void RegisterPortal(Portal portal)
    {
        if (!activePortals.Contains(portal))
        {
            activePortals.Add(portal);
        }
    }
    public void UnRegisterPortal(Portal portal)
    {
        if (activePortals.Contains(portal))
        {
            activePortals.Remove(portal);
        }
    }
    
    private IEnumerator SpawnAliensRoutine()
    {
        var waitForSeconds = new WaitForSeconds(spawnInterval);

        while (true)
        {
            if (activePortals.Count > 0)
            {
                var selectedPortal = activePortals[Random.Range(0, activePortals.Count)];
                selectedPortal.SpawnAlien();
            }

            yield return waitForSeconds;
        }
    }

    public void getMisslesAvaible()
    {
        if (missilesNumber < maxMissiles)
        {
            missilesNumber++;
            Finder.EventChannels.PublishMissilesChanged(missilesNumber);
        }
    }

    public void useMissles()
    {
        if (missilesNumber > 0)
        {
            missilesNumber--;
            Finder.EventChannels.PublishMissilesChanged(missilesNumber);
        }
    }

    public bool canUseMissiles()
    {
        return missilesNumber > 0;
    }

    public int getMissilesLeft()
    {
        return missilesNumber;
    }
}

