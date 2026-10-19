using Assets.Scripts.Collectables;
using UnityEngine;

namespace Assets.Scripts.Common.Collectables
{
    public class ShieldCollectables : MonoBehaviour, ICollectable
    {
        public void UseEffect()
        {
            var playerHealth = FindFirstObjectByType<SpaceMarineHealth>();

            if (playerHealth != null)
                playerHealth.ActivateArmor();
        }
    }
}