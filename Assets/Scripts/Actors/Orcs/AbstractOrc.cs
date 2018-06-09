using InanimateObjects.Environment;
using UnityEngine;

namespace Actors.Orcs
{
    public abstract class AbstractOrc
    {
        PendulumMovement _pendulumMovement;
        GameObject _parent;

        protected AbstractOrc(GameObject gameObject)
        {
            _parent = gameObject;
        }

        public void ManageCollision(Collision2D collision)
        {

        }



    }
}
