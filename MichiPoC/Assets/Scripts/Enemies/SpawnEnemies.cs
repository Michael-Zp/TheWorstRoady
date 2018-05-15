using System.Collections;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public Transform RootObject;
    public GameObject[] Prefabs;
    public Vector2 Direction;
    [Range(5, 180)]
    public float SpawnRadiusInDegree;
    [Range(5, 100)]
    public float SpawnForce; 
    [Range(1, 10)]
    public float TimeBetweenSpawns;

    private bool _nextSpawn = false;
    

    private void Start()
    {
        StartCoroutine(WaitRandomTime());
    }

    private void Update()
    {
        if(_nextSpawn)
        {
            _nextSpawn = false;

            StartCoroutine(WaitForSpawn());

            if(EventSystem.Instance.GetHowManyEnemiesAreInLevel() < EventSystem.Instance.GetMaxEnemiesInLevel())
            {
                EventSystem.Instance.IncrementEnemiesInLevel();

                float rotation = Random.Range(-SpawnRadiusInDegree / 2.0f, SpawnRadiusInDegree / 2.0f);
                float force = Random.Range(SpawnForce / 2.0f, SpawnForce);

                Vector2 direction = Direction.Rotate(rotation);

                GameObject spawnedEnemy = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], transform.position, Quaternion.identity, RootObject);
                spawnedEnemy.GetComponent<Rigidbody2D>().AddForce(direction * force);

                spawnedEnemy.GetComponent<EnemyEnableDisableComponents>().DisableComponentsOnSpawn();
            }
        }
    }

    public IEnumerator WaitRandomTime()
    {
        yield return new WaitForSeconds(Random.Range(0, 5));

        _nextSpawn = true;
        StartCoroutine(WaitForSpawn());
    }

    public IEnumerator WaitForSpawn()
    {
        yield return new WaitForSeconds(TimeBetweenSpawns);

        _nextSpawn = true;
    }
}
