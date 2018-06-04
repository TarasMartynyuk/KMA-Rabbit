using UnityEngine;

namespace InanimateObjects.Collectables
{
    /// <summary>
    /// represtnts a buff/debuff pick-up
    /// </summary>
    class Collectable : MonoBehaviour
    {
        public CollectableType Type => _type;

        [SerializeField]
        CollectableType _type;
    }

    enum CollectableType
    {
        Bomb, Mushroom, Fruit, Diamond
    }
}
