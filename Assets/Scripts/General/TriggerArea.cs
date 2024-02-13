using UnityEngine;
using UnityEngine.Events;

public class TriggerArea : MonoBehaviour
{
    public UnityEvent OnTriggerEnter;
    [SerializeField] private LayerMask _layer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _layer)
            OnTriggerEnter?.Invoke();
    }
}
