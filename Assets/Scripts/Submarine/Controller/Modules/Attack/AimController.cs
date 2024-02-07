using UnityEngine;

namespace Submarine
{
    public class AimController : MonoBehaviour
    {
        private Vector2 _aimDirection = Vector2.zero;        

        [SerializeField] private Transform _cannonTransform;

        public Vector2 GetAimDirection() => _aimDirection;

        private void CalculateAimDirection()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            _aimDirection = mousePosition - transform.position;

            float angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg - 90;

            _cannonTransform.eulerAngles = Vector3.forward * angle;
        }

        private void Update()
        {
            CalculateAimDirection();
        }
    }
}