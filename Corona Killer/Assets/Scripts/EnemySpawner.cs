using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<PathMoveConfig> pathMoveConfigs;
    [SerializeField] List<HeartConfig> HeartConfigs;
    [SerializeField] List<SimpleMoveConfig> simpleMoveConfigs;
    [SerializeField] int startingPath = 0;
    [SerializeField] int startingEnemy = 0;
    [SerializeField] int startingHeart = 0;
    [SerializeField] bool looping = false;

    [SerializeField] GameObject superLaserPrefab;
    [SerializeField] float supeLaserTime = 5f;

    [SerializeField] GameObject warningZone;
    [SerializeField] float warningTime = 2f;

    [SerializeField] float superLaserMinX = -5f;
    [SerializeField] float superLaserMaxX = 5f;
    [SerializeField] float superLaserMinY = 8.4f;
    [SerializeField] float superLaserMaxY = 8.4f;
    [SerializeField] float superLaserPeriodTime = 10;
    [SerializeField] bool haveSuperLaser = false;

    GameSession gameSession;

    bool shootLaser = false;

    float timeRemaining = 10;

    void Update()
    {
        if(haveSuperLaser){
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                // Debug.Log("Time Remaining: " + timeRemaining);
            }
            else{
                timeRemaining = superLaserPeriodTime;
                shootLaser = true;
            }
        }
    }
    

	// Use this for initialization
	IEnumerator Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        timeRemaining = superLaserPeriodTime;
        StartCoroutine(SpawnHeart());
        do
        {
            yield return StartCoroutine(SpawEnemy());
        }
        while (looping);
	}

    public void DestroySelf(){
        Destroy(gameObject);
    }
	private IEnumerator SpawnHeart()
	{

        for (int heartIndex = startingHeart; heartIndex < HeartConfigs.Count; heartIndex++)
        {
            HeartConfig heartConfig = HeartConfigs[heartIndex];

            while (gameSession.GetScore() > heartConfig.GetMinScore())
            {
                yield return new WaitForSeconds(HeartConfigs[heartIndex].GetTimeBetweenSpawns());
                Vector3 randomStart = HeartConfigs[heartIndex].GetRandomStart();
                var newHeart = Instantiate(
                    HeartConfigs[heartIndex].GetHeartPrefab(),
                    randomStart,
                    Quaternion.identity
                );
                newHeart.GetComponent<HeartMove>().SetStartPoint(randomStart);
                newHeart.GetComponent<HeartMove>().SetHeartConfig(HeartConfigs[heartIndex]);
            }
        }
    }

    private IEnumerator SpawnHeart(HeartConfig HeartConfig){
        Vector3 randomStart = HeartConfig.GetRandomStart();

        var newHeart = Instantiate(
            HeartConfig.GetHeartPrefab(),
            randomStart,
            Quaternion.identity
        );
        newHeart.GetComponent<HeartMove>().SetStartPoint(randomStart);
        newHeart.GetComponent<HeartMove>().SetHeartConfig(HeartConfig);

        yield return new WaitForSeconds(HeartConfig.GetTimeBetweenSpawns());
    }

    private IEnumerator SpawEnemy()
    {
        for (int simpleIndex = startingPath; simpleIndex < simpleMoveConfigs.Count; simpleIndex++)
        {
            SimpleMoveConfig simpleMoveConfig = simpleMoveConfigs[simpleIndex];

            if(gameSession.GetScore() > simpleMoveConfig.GetMinScore()){
                yield return StartCoroutine(SpawnAllSimpleEnemies(simpleMoveConfig));
            }
        }

        for (int pathIndex = startingPath; pathIndex < pathMoveConfigs.Count; pathIndex++)
        {
            PathMoveConfig pathMoveConfig = pathMoveConfigs[pathIndex];

            if (gameSession.GetScore() > pathMoveConfig.GetMinScore())
            {
                yield return StartCoroutine(SpawnAllPathEnemies(pathMoveConfigs[pathIndex]));
            }
        }

        if (shootLaser && haveSuperLaser){
            var newWarningZone = Instantiate(
                warningZone,
                new Vector3(Random.Range(superLaserMinX, superLaserMaxX),Random.Range(superLaserMinY, superLaserMaxY), 0f),
                Quaternion.Euler(0f, 0f, -90f)
            );            

            yield return StartCoroutine(SpawnLaser(newWarningZone));
            timeRemaining = superLaserPeriodTime;
            shootLaser = false;
        }
    }

    private IEnumerator SpawnLaser(GameObject warningZone){
        yield return new WaitForSeconds(warningTime);

        var newSuperLaser = Instantiate(
            superLaserPrefab,
            warningZone.transform.position,
            Quaternion.Euler(0f, 0f, -90f)
        );

        Destroy(warningZone);

        yield return new WaitForSeconds(supeLaserTime);

        Destroy(newSuperLaser);
    }

    private IEnumerator SpawnAllSimpleEnemies(SimpleMoveConfig simpleMoveConfig){
        Vector3 randomStart = simpleMoveConfig.GetRandomStart();

        var newEnemy = Instantiate(
            simpleMoveConfig.GetEnemyPrefab(),
            randomStart,
            Quaternion.identity
        );

        newEnemy.GetComponent<EnemySimpleMove>().SetStartPoint(randomStart);
        newEnemy.GetComponent<EnemySimpleMove>().SetSimpleMoveConfig(simpleMoveConfig);

        yield return new WaitForSeconds(simpleMoveConfig.GetTimeBetweenSpawns());
    }
    
    

    private IEnumerator SpawnAllPathEnemies(PathMoveConfig pathMoveConfig)
    {

        for (int enemyCount = 0; enemyCount < pathMoveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
                pathMoveConfig.GetEnemyPrefab(),
                pathMoveConfig.GetWaypoints()[0].transform.position,              
                Quaternion.identity
            );
            newEnemy.GetComponent<EnemyPathMove>().SetPathMoveConfig(pathMoveConfig);
            yield return new WaitForSeconds(pathMoveConfig.GetTimeBetweenSpawns());
        }
    }

}
