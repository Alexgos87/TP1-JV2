using Assets.Scripts.Collectables;
using UnityEngine;

namespace Assets.Scripts.Common.Collectables
{
    public class AmmoCollectables : MonoBehaviour, ICollectable
    {
        public void UseEffect()
        {
            Finder.GameController.AddMissiles();
        }
    }
}