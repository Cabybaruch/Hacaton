using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private string _objectID;
    [SerializeField] private Sprite _spriteUsed;

    public string ObjectID => _objectID;

    private bool _isUsed;

    private void OnEnable()
    {
        // Автомат добавления со сцены
        InteractableObjectManager.Instance.RegInDict(this);
    }

    private void OnDisable()
    {
        // Убираем когда нулл
        if (InteractableObjectManager.Instance != null)
            InteractableObjectManager.Instance.Unregister(this);
    }

    public bool CanInteract() => !_isUsed;

    public void Interact()
    {
        //_isUsed = true;

        if (_spriteUsed != null)
        {
            GetComponent<SpriteRenderer>().sprite = _spriteUsed;
        }
    }
}