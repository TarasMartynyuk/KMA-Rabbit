using Actors;
using UnityEngine;
using Utils;

namespace GameFlow
{
    class Respawner : NonGlobalSingleton<Respawner>
    {
        [SerializeField]
        Rabbit _rabbit;
        [SerializeField]
        Transform _respawnPoint;

        public void RespawnRabbit()
        {
            _rabbit.transform.position = _respawnPoint.transform.position;
            _rabbit.Lives.ResetLives();
        }
    }
}
