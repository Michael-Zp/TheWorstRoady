using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public int MaxEnemiesInLevel;
    private int _currentEnemiesInLevel = 0;

    private void Start()
    {
        EventSystem.Instance.IncrementEnemiesInLevelEvent += IncrementCount;
        EventSystem.Instance.DecrementEnemiesInLevelEvent += DecrementCount;
        EventSystem.Instance.GetHowManyEnemiesAreInLevelEvent += ReturnCount;
        EventSystem.Instance.GetMaxEnemiesInLevelEvent += ReturnMaxCount;
    }

    private void OnDestroy()
    {
        EventSystem.Instance.IncrementEnemiesInLevelEvent -= IncrementCount;
        EventSystem.Instance.DecrementEnemiesInLevelEvent -= DecrementCount;
        EventSystem.Instance.GetHowManyEnemiesAreInLevelEvent -= ReturnCount;
        EventSystem.Instance.GetMaxEnemiesInLevelEvent += ReturnMaxCount;
    }

    private void IncrementCount()
    {
        _currentEnemiesInLevel++;
    }

    private void DecrementCount()
    {
        _currentEnemiesInLevel--;
    }

    private int ReturnCount()
    {
        return _currentEnemiesInLevel;
    }

    private int ReturnMaxCount()
    {
        return MaxEnemiesInLevel;
    }
}
