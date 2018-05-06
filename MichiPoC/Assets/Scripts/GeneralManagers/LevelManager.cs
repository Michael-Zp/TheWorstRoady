using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int LevelToUnlock;
    public string NextLevel;

    public int MaxFanBase;
    public int CurrentFanBase;
    public int CurrentRateToBeFired;

    private bool _lastFanBaseChangeWasPositive;
    private bool _lastRateToBeFiredChangeWasPositive;

    void Start()
    {
        EventSystem.Instance.GameWonEvent += UnlockLevel;
        EventSystem.Instance.LoadNextLevelEvent += LoadNextLevel;
        EventSystem.Instance.ScareOfFanBaseEvent += FanBaseIsScaredOf;
        EventSystem.Instance.GetCloserToBeFiredEvent += GetCloserToBeingFired;

        MaxFanBase = Mathf.Max(MaxFanBase, CurrentFanBase);

        EventSystem.Instance.SetScareOfFanBaseUI((float)CurrentFanBase / (float)MaxFanBase);
        EventSystem.Instance.SetGetCloserToBeFiredUI((float)CurrentRateToBeFired / 100f);
    }

    private void OnDestroy()
    {
        EventSystem.Instance.GameWonEvent -= UnlockLevel;
        EventSystem.Instance.LoadNextLevelEvent -= LoadNextLevel;
        EventSystem.Instance.ScareOfFanBaseEvent -= FanBaseIsScaredOf;
        EventSystem.Instance.GetCloserToBeFiredEvent -= GetCloserToBeingFired;
    }

    private void Update()
    {
        EventSystem.Instance.UpdateScareOfFanBaseUI((float)CurrentFanBase / (float)MaxFanBase, _lastFanBaseChangeWasPositive);
        EventSystem.Instance.UpdateGetCloserToBeFiredUI((float)CurrentRateToBeFired / 100f, _lastRateToBeFiredChangeWasPositive);
    }

    private void FanBaseIsScaredOf(int numberOfFans)
    {
        CurrentFanBase -= numberOfFans;
        CurrentFanBase = Mathf.Min(MaxFanBase, CurrentFanBase);

        if(numberOfFans >= 0)
        {
            _lastFanBaseChangeWasPositive = true;
        }
        else
        {
            _lastFanBaseChangeWasPositive = false;
        }

        if(CurrentFanBase <= 0)
        {
            EventSystem.Instance.GameWon();
        }
    }

    private void GetCloserToBeingFired(int percentageToIncrease)
    {
        CurrentRateToBeFired += percentageToIncrease;
        CurrentRateToBeFired = Mathf.Max(CurrentRateToBeFired, 0);
        
        if (percentageToIncrease < 0)
        {
            _lastRateToBeFiredChangeWasPositive = true;
        }
        else
        {
            _lastRateToBeFiredChangeWasPositive = false;
        }

        if (CurrentRateToBeFired >= 100)
        {
            EventSystem.Instance.GameOver();
        }
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
