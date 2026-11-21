using UnityEngine;

public class URL : MonoBehaviour
{
    [SerializeField] private string Url = "https://rutube.ru/video/19936b03156acfd10a9b0cfd63c6a89b/";
    
    public void OpenURL()
    {
        Application.OpenURL(Url);
    }
}