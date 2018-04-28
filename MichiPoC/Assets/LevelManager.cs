using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int LevelToUnlock;

    void Start()
    {
        EventSystem.Instance.GameWonEvent += UnlockLevel;
    }

    private void OnDestroy()
    {
        EventSystem.Instance.GameWonEvent -= UnlockLevel;
    }

    private void UnlockLevel()
    {
        EventSystem.Instance.UnlockLevel(LevelToUnlock);
    }
}
