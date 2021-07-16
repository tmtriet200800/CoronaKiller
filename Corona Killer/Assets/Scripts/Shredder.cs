using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

    Player player;

    [Header("Sound Effects")]
    [SerializeField] AudioClip enemyGoToHouse;
    [SerializeField] [Range(0, 1)] float enemyGoToHouseVolume = 0.25f;

    void Start () {
        player = FindObjectOfType<Player>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if(enemy){
            player.EnemyGoInHouseDamage();
            AudioSource.PlayClipAtPoint(enemyGoToHouse, Camera.main.transform.position, enemyGoToHouseVolume);
        }


        Destroy(other.gameObject);
    }

}
