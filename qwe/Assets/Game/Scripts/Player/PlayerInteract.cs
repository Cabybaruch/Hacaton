using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class PlayerInteractor : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private InputActionReference _interactAction; // действие (на какую кнопку, тазить из инпут системы)
    [SerializeField] private float _interactRadius = 1.5f;         // радиус
    [SerializeField] private LayerMask _interactableLayer;

    private IInteractable _currentTarget;

    private void OnEnable()
    {
        Debug.Log("playerABOBA");
        _interactAction.action.performed += OnInteract;
        _interactAction.action.Enable();
        
    }

    private void OnDisable()
    {
        _interactAction.action.performed -= OnInteract;
        _interactAction.action.Disable();
        
    }

    private void Update()
    {
        FindTarget();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact");
        if (_currentTarget == null)
            return;

        InteractableObjectManager.Instance.TryInteract(_currentTarget.ObjectID);
    }

    private void FindTarget()
    {
        _currentTarget = null;

        // Ищем вокруг игрока
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _interactRadius, _interactableLayer);

        float closestSqrDist = float.MaxValue;

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent<IInteractable>(out var target))
                continue;

            if (!target.CanInteract())
                continue; 

            // ближайшая цель
            float sqrDist = (hit.transform.position - transform.position).sqrMagnitude;
            if (sqrDist < closestSqrDist)
            {
                closestSqrDist = sqrDist;
                _currentTarget = target;
            }
        }
        
    }
    
    
    

}
