using UnityEngine;
using UnityEngine.VFX;

namespace Submarine
{
    public class AnimationsController : MonoBehaviour
    {
        [SerializeField] private VisualEffect _bubbles;
        [SerializeField] private MovementModule _movementController;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            UpdateBubblesDirection();
        }

        private void UpdateBubblesDirection()
        {
            Vector3 direction = -_movementController.GetVelocity().normalized;
            _bubbles.enabled = (direction != Vector3.zero);
            _bubbles.SetVector3("SpawnDirection", direction);

            //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            //_aimDirection = mousePosition - transform.position;

            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;


            //_bubbles.transform.eulerAngles = Vector3.forward * angle;                                    
            //_bubbles.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}