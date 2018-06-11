using UnityEngine;

namespace PlayModeTests
{
    public static class GameObjectShouldExtentions
    {
        public static GameObjectAssertions Should(this GameObject gameObject) =>  new GameObjectAssertions(gameObject);
    }
}