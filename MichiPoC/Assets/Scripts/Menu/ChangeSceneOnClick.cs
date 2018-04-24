using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnClick : MonoBehaviour
{
    public string SceneName;

    public void LoadScene()
    {
        Debug.Log("Load scene");
        //If we are running in a standalone build of the game
#if UNITY_STANDALONE
        //Quit the application
        SceneManager.LoadScene(SceneName);
#endif

        //If we are running in the editor
#if UNITY_EDITOR
        //Stop playing the scene
        EditorSceneManager.LoadScene(SceneName);
#endif
    }
}
