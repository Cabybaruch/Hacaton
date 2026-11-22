using UnityEngine;

public class ObjectDelatel : MonoBehaviour
{
    [SerializeField] private string _targetID = "object_01";

    private void OnEnable()
    {
        InteractableObjectManager.Instance.OnInteracted += OnInteracted;
    }

    private void OnDisable()
    {
        InteractableObjectManager.Instance.OnInteracted -= OnInteracted;
    }

    private void OnInteracted(string id)
    {
        if (id != _targetID)
            return;

        Debug.Log($"Дверь {id} открыта!");
        // deystviye
    }
}