using UnityEngine;

namespace Dummies
{
    public class FullBillboard : BillboardBase
    {
        private Camera _camera;
        private Transform _transform;

        public override void SetCamera(Camera camera)
        {
            _camera = camera;
            _transform = transform;
        }

        private void LateUpdate()
        {
            if (_camera == null) return;

            Vector3 direction = (_camera.transform.position - _transform.position).normalized;
            _transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}