using System;
using System.Linq;
using GameFlow;
using UnityEngine;

namespace InanimateObjects.Environment
{
    /// <summary>
    /// handles sticking one gameobject to another upon entering trigger
    /// (sticked objects move along with the ones they've sticked to)
    /// </summary>
    public class StickerTrigger 
    {
        public GameObject HoldingObject { get; }

        /// <summary>
        /// <paramref name="holdingObject"/> must have a trigger collider attached
        /// </summary>
        public StickerTrigger(GameObject holdingObject)
        {
            if(! HasTriggerCollider(holdingObject))
            { throw new ArgumentException($"{nameof(holdingObject)} must have a trigger collider attached"); }

            RegisterTriggerEventHandlers(holdingObject);

            HoldingObject = holdingObject;
        }

        void RegisterTriggerEventHandlers(GameObject holdingObject)
        {
            var triggerListener = holdingObject.AddComponent<Trigger2DListener>();

            triggerListener.EnterredTrigger += collider => 
            {
                Stick(collider.gameObject);
            };

            triggerListener.ExitedTrigger += collider => 
            {
                UnStick(collider.gameObject);
            };

        }

        void Stick(GameObject gameObject)
        {
            gameObject.transform.parent = HoldingObject.transform;
        }

        void UnStick(GameObject gameObject)
        {
            gameObject.transform.parent = null;
        }

        static bool HasTriggerCollider(GameObject gameObject)
        {
            var colliders = gameObject.GetComponents<Collider2D>();

            return colliders != null && colliders.Any(col => col.isTrigger);
        }

    }
}
