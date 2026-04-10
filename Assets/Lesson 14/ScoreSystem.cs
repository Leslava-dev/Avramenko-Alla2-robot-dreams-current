using System;
using UnityEngine;

namespace Dummies
{
    public class ScoreSystem : MonoBehaviour
    {
        public event Action OnDataUdpated;

        [SerializeField] private HealthSystem _healthSystem;
        [SerializeField] private GunDamageDealer _gunDamageDealer;
        [SerializeField] private Rect _guiRect = new Rect(20, 20, 180, 120);

        private Vector3Int _kda;
        private int _shotCount;
        private int _hitCount;

        public Vector3Int KDA => _kda;
        public int Accuracy => _shotCount == 0 ? 0 : (int)((_hitCount / (float)_shotCount) * 100f);

        private void Start()
        {
            _gunDamageDealer.OnHit += HitHandler;
            _gunDamageDealer.Gun.OnShot += ShotHandler;
            _healthSystem.OnCharacterDeath += CharacterDeathHandler;
        }

        private void OnGUI()
        {
            float accuracy = _shotCount == 0 ? 0f : _hitCount / (float)_shotCount;
            GUI.Box(_guiRect,
                $"Score\nKills: {_kda.x}\nDeaths: {_kda.y}\nAssists: {_kda.z}\nAccuracy: {accuracy * 100f:0}%");
        }

        private void HitHandler(int hits)
        {
            _hitCount += hits;
            OnDataUdpated?.Invoke();
        }

        private void ShotHandler()
        {
            _shotCount++;
            OnDataUdpated?.Invoke();
        }

        private void CharacterDeathHandler(Health health)
        {
            _kda.x++;
            OnDataUdpated?.Invoke();
        }
    }
}