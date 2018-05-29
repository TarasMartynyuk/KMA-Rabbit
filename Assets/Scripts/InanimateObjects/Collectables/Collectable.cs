using UnityEngine;

namespace InanimateObjects.Collectables
{
    /// <summary>
    /// represtnts a buff/debuff pick-up
    /// </summary>
    class Collectable : MonoBehaviour
    {

        CollectableType Type { get; }

    }

    enum CollectableType
    {
        Fruit, Bomb
    }
}
