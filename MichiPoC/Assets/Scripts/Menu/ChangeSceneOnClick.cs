using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnClick : MonoBehaviour
{
    public string SceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneName);
        Debug.Log("Load scene");
        
        
        //Should be cleaner, but breaks the connections of the GameManager to the UI elements on EditorSceneManager.LoadScene(...);

//        //If we are running in a standalone build of the game
//#if UNITY_STANDALONE
//        //Quit the application
//        //SceneManager.LoadScene(SceneName);
//#endif

//        //If we are running in the editor
//#if UNITY_EDITOR
//        //Stop playing the scene
//        //EditorSceneManager.LoadScene(SceneName);
//#endif
    }
}
