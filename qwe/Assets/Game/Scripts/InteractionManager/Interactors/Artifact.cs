using UnityEngine;

public class Artifact : MonoBehaviour
{
    [SerializeField] private string _targetID = "artifact_01";
    [SerializeField] private Canvas  _canvas;

    private void OnEnable()
    {
        Debug.Log("artifactABIBA");
        if (InteractableObjectManager.Instance != null)
        {
            InteractableObjectManager.Instance.OnInteracted += OnObjectInteracted;
        }
    }

    private void OnDisable()
    {
        if (InteractableObjectManager.Instance != null)
        {
            InteractableObjectManager.Instance.OnInteracted -= OnObjectInteracted;
        }
    }

    private void OnObjectInteracted(string id)
    {
        Debug.Log("Interact");
        if (id != _targetID)
        {
            return;
        }
        else Debug.Log(_targetID);

        if (_canvas != null)
        {
            if (!_canvas.enabled)
            {
                _canvas.enabled = true;
            }
            else
            {
                _canvas.enabled = false;
            }
        }
    }
}
