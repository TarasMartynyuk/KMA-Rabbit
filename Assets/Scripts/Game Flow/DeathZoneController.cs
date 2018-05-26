using System;
using Actors;
using UnityEngine;
using Utils;

namespace Game_Flow
{
    /// <summary>
    /// Handles the inflicting of damage when rabbit contacs deathzones
    /// </summary>
    public class DeathZoneController : NonGlobalSingleton<DeathZoneController>
    {
        [SerializeField]
        Rabbit _rabbit;
        [SerializeField]
        GameObject[] _deathzones;
        [SerializeField]
        Transform _respawnPoint;

        static DeathZoneController instance;

        protected override void Awake()
        {
            base.Awake();

            var collisionListener = _rabbit.gameObject.AddComponent<Trigger2DListener>();
            collisionListener.OnEnteredCollision += OnRabbitEnterredCollision;
        }

        void Start()
        {

        }

        void OnRabbitEnterredCollision(Collider2D collision)
        {
            Debug.Log("Triggered");

            if(Array.IndexOf(_deathzones, collision.gameObject) >= 0)
            {
                if(_rabbit.LoseLife()) 
                { RespawnRabbit(); }
            }
        }

        void RespawnRabbit()
        {
            _rabbit.transform.position = _respawnPoint.transform.position;
            _rabbit.ResetLives();
        }
    }
}
