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

    /*private void OnEnable()
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
    }*/
    private void OnEnable()
    {
        if (_interactAction == null)
        {
            Debug.LogError("[PlayerInteractor] Поле Interact Action пустое!", this);
            return;
        }

        Debug.Log($"[PlayerInteractor] подписка на «{_interactAction.action.name}»");
        _interactAction.action.performed += OnInteract;
        _interactAction.action.Enable();
        Debug.Log($"[PlayerInteractor] действие включено: {_interactAction.action.enabled}");
    }

    private void Update()
    {
        if (Time.frameCount == 10)
            Debug.Log("[PlayerInteractor] Update работает");

        FindTarget();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact");
        if (_currentTarget == null)
            return;

        InteractableObjectManager.Instance.TryInteract(_currentTarget.ObjectID);
    }

    /*private void FindTarget()
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
    */
    
    private IInteractable _lastLoggedTarget;

    private void FindTarget()
    {
        _currentTarget = null;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _interactRadius, _interactableLayer);

        float closestSqrDist = float.MaxValue;

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent<IInteractable>(out var target))
                continue;

            if (!target.CanInteract())
                continue;

            float sqrDist = (hit.transform.position - transform.position).sqrMagnitude;
            if (sqrDist < closestSqrDist)
            {
                closestSqrDist = sqrDist;
                _currentTarget = target;
            }
        }

        // Логируем только когда цель изменилась — чтобы не спамить консоль
        if (_currentTarget != _lastLoggedTarget)
        {
            Debug.Log($"[PlayerInteractor] цель: {(_currentTarget != null ? _currentTarget.ObjectID : "нет")}");
            _lastLoggedTarget = _currentTarget;
        }
    }

}
