using Actors;
using UnityEngine;
using UnityEngine.Assertions;
using Utils;

namespace GameFlow
{
    public class Respawner : NonGlobalSingleton<Respawner>
    {
        [SerializeField]
        RabbitMonoBehaviour _rabbit;
        [SerializeField]
        Transform _respawnPoint;

        public void RespawnRabbit()
        {
            Assert.IsNotNull(_rabbit.GetComponent<RabbitMonoBehaviour>());
            _rabbit.transform.position = _respawnPoint.transform.position;
            _rabbit.Rabbit.Lives.ResetLives();
        }
    }
}
