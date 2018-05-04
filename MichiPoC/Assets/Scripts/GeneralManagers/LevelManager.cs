using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int LevelToUnlock;
    public string NextLevel;

    void Start()
    {
        EventSystem.Instance.GameWonEvent += UnlockLevel;
        EventSystem.Instance.LoadNextLevelEvent += LoadNextLevel;
    }

    private void OnDestroy()
    {
        EventSystem.Instance.GameWonEvent -= UnlockLevel;
        EventSystem.Instance.LoadNextLevelEvent -= LoadNextLevel;
    }

    private void UnlockLevel()
    {
        EventSystem.Instance.UnlockLevel(LevelToUnlock);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(NextLevel);
    }
}
