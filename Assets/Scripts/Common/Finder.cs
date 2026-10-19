using UnityEngine;

// TODO : Ajouter tous vos objets globaux ici.
//        Basez-vous sur le code existant.
public static class Finder
{
    private static EventChannels _eventChannels;
    private static ObjectPools _objectPools;
    private static GameController _gameController;

    public static GameController GameController
    {
        get
        {
            if (_gameController == null)
                _gameController = Object.FindAnyObjectByType<GameController>();

            if (_gameController == null)
            {
                var gameControllerObject = new GameObject("GameController (Runtime)");
                _gameController = gameControllerObject.AddComponent<GameController>();
            }

            return _gameController;
        }
    }

    // Recherche un GameController existant sans en créer un nouveau.
    public static GameController ExistingGameController
    {
        get
        {
            if (_gameController == null)
                _gameController = Object.FindAnyObjectByType<GameController>();

            return _gameController;
        }
    }

    public static EventChannels EventChannels
    {
        get
        {
            if (_eventChannels == null)
                _eventChannels = FindWithTag<EventChannels>("GameController");

            return _eventChannels;
        }
    }

    public static ObjectPools ObjectPools
    {
        get
        {
            if (_objectPools == null)
            {
                _objectPools = FindWithTag<ObjectPools>("ObjectPools")
                               ?? Object.FindAnyObjectByType<ObjectPools>();
            }

            return _objectPools;
        }
    }

    private static T FindWithTag<T>(string tag) where T : class
    {
        var gameObject = GameObject.FindWithTag(tag);

        if (gameObject == null)
            return null;

        return gameObject.GetComponent<T>();
    }
}