using System;
using InanimateObjects.Environment;
using UnityEngine;
using System.Linq;
using Object = UnityEngine.Object;

namespace Actors.Orcs
{
    public abstract class AbstractOrc
    {
        PendulumMovement _pendulumMovement;
        readonly GameObject _parent;

        protected AbstractOrc(GameObject gameObject) 
        { _parent = gameObject; }

        public void ManageCollision(Collision2D collision)
        {
            if(! collision.gameObject.CompareTag("Player"))
            { return; }

            // contacts prop produces garbage, yada-yada - 
            // we do this only usually once for orc instance
            if(collision.contacts.Any(contact => Math.Abs(contact.normal.y) >= 0.9f))
                { Object.Destroy(_parent); }
            else
            {
                var rabbitScript = collision.gameObject.GetComponent<RabbitMonoBehaviour>();
                if(rabbitScript == null)
                {
                    throw new MissingComponentException(
                        $"the object which tagged Player must have a {nameof(RabbitMonoBehaviour)} component");
                }
                
                rabbitScript.Rabbit.Lives.LoseLife();
            }
        }
    }
}
