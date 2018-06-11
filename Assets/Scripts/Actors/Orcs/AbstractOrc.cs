using InanimateObjects.Environment;
using UnityEngine;
using System.Linq;
using UnityEngine.Assertions;

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
            // contacts prop produces garbage, yada-yada - 
            // we do this only usually once for orc instance
            if(collision.contacts.Any(contact => contact.normal.y >= 0.9f))
            { Object.Destroy(_parent); }
            else
            {
                var rabbitScript = collision.gameObject.GetComponent<RabbitMonoBehaviour>();
                Assert.IsNotNull(collision.gameObject.GetComponent<RabbitMonoBehaviour>());
                rabbitScript.Rabbit.Lives.LoseLife();
            }
        }
    }
}
