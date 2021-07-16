using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Simple Move Config")]
public class SimpleMoveConfig : ScriptableObject {

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float randomMinX = -5f;
    [SerializeField] float randomMaxX = 5f;
    [SerializeField] float startY = 8.4f;
    [SerializeField] float endY = -10f;
    [SerializeField] float speed = 0.2f;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float minScore = -1f;


    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float GetMoveSpeed() { return speed; }
    public Vector2 GetRandomStart() {
        return new Vector3(
            Random.Range(randomMinX, randomMaxX),startY,0f
        );
    }

    public float GetEndY(){
        return endY;
    }

    public float GetSpeed(){
        return speed;
    }
    
    public float GetMinScore(){
        return minScore;
    }


}
