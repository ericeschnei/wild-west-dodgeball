using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public GameObject boss;
    public float xPos;
    public float yPos;
    
    public int enemyMaxCount;
    public int enemyMinCount;
    public int totalEnemyBeforeBoss = 10;
    public float spawnInterval = 1f;
    private bool spawning = false;
    private bool bossSpawn = false;
    [SerializeField] private RectTransform spawnRange;
    [SerializeField] private SpriteRenderer preSpawnBossSprite;
    [SerializeField] private HealthUIController bossHealthBar;
    
    public int enemyRound = 3;

    private Vector3 groupPos;
    private int enemyGroupNum;
    private int enemyCount;

    private bool reSpawn;
    private bool canSpawn = false;

    public void StartSpawning()
    {
        Debug.Log("started spawning");
        canSpawn = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        bossHealthBar.gameObject.SetActive(false);
        enemyCount = 0;
        reSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canSpawn)
            return;
        if (enemyRound > 0)
        {
            if(enemyCount == 0)
            {
                getNewGroup();
            }
            if (reSpawn && (!spawning))
            {
                if(enemyCount < enemyGroupNum) { 
                    StartCoroutine(Spawn());
                } else
                {
                    reSpawn = false;
                }
            }
        } else
        {
            if (!bossSpawn)
            {
                bossSpawn = true;
                spawnBoss();
            }
            if(enemyCount == 0)
            {
                getNewGroup();
                StartCoroutine(Spawn());
            }
        }
    }

    void spawnBoss()
    {
        //bossHealthBar.gameObject.SetActive(true);
        //bossHealthBar.RenderHealth(100);
        preSpawnBossSprite.enabled = false;
        Debug.Log("BOSS");
        xPos = Random.Range(0, 20);
        yPos = Random.Range(0, 5);
        GameObject bossSpawned = GameObject.Instantiate(boss, new Vector3(xPos, yPos, 0), Quaternion.identity);
        bossSpawned.GetComponentInChildren<EnemyMovement>().healthBar = bossHealthBar;

    }

    IEnumerator Spawn()
    {
        spawning = true;
        Instantiate(enemy, groupPos + new Vector3(Random.value, Random.Range(-2,2)), Quaternion.identity);
        enemyCount += 1;
        yield return new WaitForSeconds(spawnInterval);
        spawning = false;
    }

    void getNewGroup()
    {
        enemyRound--;
        //xPos = Random.Range(spawnRange.anchorMin.x, spawnRange.anchorMax.x);
        //yPos = Random.Range(spawnRange.anchorMin.y, spawnRange.anchorMax.y);
        xPos = Random.Range(-16, 50);
        yPos = Random.Range(-15, 1);
        groupPos = new Vector3(xPos, yPos, 0);
        enemyGroupNum = Random.Range(enemyMinCount, enemyMaxCount);
        reSpawn = true;
    }

    public void enemyDied()
    {
        enemyCount -= 1;
    }
}
