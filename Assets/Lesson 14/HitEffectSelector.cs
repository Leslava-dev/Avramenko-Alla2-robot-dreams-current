using UnityEngine;

namespace Dummies
{
    public class HitEffectSelector : MonoBehaviour
    {
        [SerializeField] private GameObject _hitEffectPrefab;

        public GameObject HitEffectPrefab => _hitEffectPrefab;
    }
}