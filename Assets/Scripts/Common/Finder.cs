using UnityEngine;

// TODO : Ajouter tous vos objets globaux ici.
//        Basez-vous sur le code existant.
public static class Finder
{
    private static EventChannels eventChannels;
    private static ObjectPools objectPools;
    
    private static GameController gameController;
    
    public static GameController GameController
    {
        get
        {
            if (gameController == null)
                gameController = Object.FindAnyObjectByType<GameController>();
            return gameController;
        }
    }

    public static EventChannels EventChannels
    {
        get
        {
            if (eventChannels == null)
                eventChannels = FindWithTag<EventChannels>("GameController");
            return eventChannels;
        }
    }
    public static ObjectPools ObjectPools
    {
        get
        {
            if (objectPools == null)
                objectPools = FindWithTag<ObjectPools>("GameController");
            return objectPools;
        }
    }

    private static T FindWithTag<T>(string tag) where T : class
    {
        var gameObject = GameObject.FindWithTag(tag);
        if (gameObject == null) return null;
        return gameObject.GetComponent<T>();
    }
}