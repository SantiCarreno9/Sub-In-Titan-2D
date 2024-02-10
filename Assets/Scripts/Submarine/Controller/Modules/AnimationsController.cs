using UnityEngine;
using UnityEngine.VFX;

namespace Submarine
{
    public class AnimationsController : MonoBehaviour
    {
        [Header("Animations")]
        [SerializeField] private Animator _animator;
        
        [Header("Movement")]
        [Header("Bubbles")]
        [SerializeField] private VisualEffect[] _bubbles;
        [SerializeField] private Transform _bubblesCont;
        [SerializeField] private MovementModule _movementController;
        private int _spawnDirectionProperty = 0;

        //[Header("Health")]        
        //[SerializeField] private VisualEffect[] _bubbles;
        //[SerializeField] private Transform _bubblesCont;
        //[SerializeField] private MovementModule _movementController;



        // Start is called before the first frame update
        void Start()
        {
            _spawnDirectionProperty = Shader.PropertyToID("SpawnDirection");
        }

        // Update is called once per frame
        void Update()
        {
            UpdateBubblesDirection();
        }

        private void UpdateBubblesDirection()
        {
            Vector2 movementDirection = -_movementController.GetMovementDirection();
            Vector2 normalizedMovement = movementDirection.normalized;
            if (normalizedMovement.magnitude == 0)
            {
                _bubbles[0].enabled = false;
                _bubbles[1].enabled = false;
            }
            else
            {
                _bubbles[0].enabled = true;
                _bubbles[1].enabled = true;
                _bubbles[0].SetVector3(_spawnDirectionProperty, normalizedMovement);
                _bubbles[1].SetVector3(_spawnDirectionProperty, normalizedMovement);
                float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg - 90;                
                _bubblesCont.eulerAngles = Vector3.forward * angle;
            }            
                        
        }
    }
}