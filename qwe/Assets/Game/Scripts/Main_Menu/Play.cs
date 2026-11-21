using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    [SerializeField] private string sceneName = "Scene2";
    
    public void LoadScene()
    {
            SceneManager.LoadScene(sceneName);
    }
    
}