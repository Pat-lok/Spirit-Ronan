using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   [Header("Spawn Area")]
    public BoxCollider spawnArea;
    public Transform targetPoint;

    [Header("Prefabs")]
    public GameObject[] armoredEnemies;
    public GameObject splitterEnemy;
    public GameObject friendlyEnemy;

    [Header("Timing")]
    public float minDelay = 0.7f;
    public float maxDelay = 1.3f;

    [Header("Movement")]
    public float minSpeed = 1.2f;
    public float maxSpeed = 2f;

    [Header("Final Boss")]
    public GameObject finalBossPrefab;
    public float bossSpawnInterval = 30f;


    public float difficultyIncreaseRate = 0.02f;
    public float minAllowedDelay = 0.4f;

    private void OnEnable()
    {
        StartCoroutine(SpawnLoop());
        StartCoroutine(SpawnBossLoop());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator SpawnBossLoop()
    {
        yield return new WaitForSeconds(bossSpawnInterval);

        while (true)
        {
            SpawnFinalBoss();
            yield return new WaitForSeconds(bossSpawnInterval);
        }
    }


    IEnumerator SpawnLoop()
    {
       yield return new WaitForSeconds(1.5f);

    while (true)
    {
        SpawnEnemy();

        // minDelay = Mathf.Max(minAllowedDelay, minDelay - difficultyIncreaseRate);
        // maxDelay = Mathf.Max(minAllowedDelay + 0.3f, maxDelay - difficultyIncreaseRate);

        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
    }
    }

    void SpawnEnemy()
    {
        GameObject prefab = ChooseEnemy();
        Vector3 pos = GetEdgePosition();

        GameObject enemy = Instantiate(prefab, pos, Quaternion.identity);
        EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();

        if (enemyBase != null)
        {
            float speed = Random.Range(minSpeed, maxSpeed);
            enemyBase.Init(targetPoint.position, speed);
        }
    }

    GameObject ChooseEnemy()
    {
        float r = Random.value;

        if (r < 0.6f)
            return armoredEnemies[Random.Range(0, armoredEnemies.Length)];
        else if (r < 0.85f)
            return splitterEnemy;
        else
            return friendlyEnemy;
    }

    Vector3 GetEdgePosition()
    {
        Bounds b = spawnArea.bounds;
        int edge = Random.Range(0, 4);

        switch (edge)
        {
            case 0: return new Vector3(b.min.x, Random.Range(b.min.y, b.max.y), 0); // Left
            case 1: return new Vector3(b.max.x, Random.Range(b.min.y, b.max.y), 0); // Right
            case 2: return new Vector3(Random.Range(b.min.x, b.max.x), b.max.y, 0); // Top
            default: return new Vector3(Random.Range(b.min.x, b.max.x), b.min.y, 0); // Bottom
        }
    }
    void SpawnFinalBoss()
{
    Vector3 pos = GetEdgePosition();
    GameObject boss = Instantiate(finalBossPrefab, pos, Quaternion.identity);

    EnemyBase enemyBase = boss.GetComponent<EnemyBase>();
    if (enemyBase != null)
    {
        enemyBase.Init(targetPoint.position, Random.Range(minSpeed, maxSpeed));
    }
}

    
}
