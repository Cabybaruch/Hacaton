using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectManager : MonoBehaviour
{
    public static InteractableObjectManager Instance { get; private set; }

    private readonly Dictionary<string, IInteractable> _interactables = new();

    public event Action<string> OnInteracted;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    
    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void RegInDict(IInteractable interactable)
    {
        string id = interactable.ObjectID;

        if (string.IsNullOrEmpty(id))
        {
            Debug.LogWarning("object without ObjectID");
            return;
        }
        
        _interactables[id]  = interactable;
    }
    
    public void Unregister(IInteractable interactable)
    {
        if (_interactables.TryGetValue(interactable.ObjectID, out var current) && ReferenceEquals(current, interactable))
        {
            _interactables.Remove(interactable.ObjectID);
        }
    }

    public bool TryInteract(string id)
    {
        if (!_interactables.TryGetValue(id, out var target))
        {
            Debug.LogWarning($"объекта с айди «{id}» нету(((((");
            return false;
        }

        if (!target.CanInteract())
        {
            return false;
        }
        target.Interact();
        OnInteracted?.Invoke(id);
        return true;
    }



}
