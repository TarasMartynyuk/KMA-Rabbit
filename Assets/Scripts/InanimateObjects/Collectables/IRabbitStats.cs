using UnityEngine;

namespace InanimateObjects.Collectables
{
    public interface IRabbitStats
    {
        bool Enlarged { get; }
        int FruitsCollected { get; }
        int GemsCollected { get; }

        /// <summary>
        /// if <paramref name="gameObject"/> is a collectable,
        /// does the pickup logic and returns true,
        /// else returns false
        /// </summary>
        bool TryPickup(GameObject gameObject);
        void Update(double deltaTime);
    }
}
