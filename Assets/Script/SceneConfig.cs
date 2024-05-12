using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneConfig : MonoBehaviour
{
    public void UpdateScene(string name = "")
    {
        string sceneName = (name == "") ? SceneManager.GetActiveScene().name : name;
        SceneManager.LoadScene(sceneName); // 現在シーンのリロード
    }
}