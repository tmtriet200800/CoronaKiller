using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Shooting")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;

    [Header("Sound Effects")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;
    [SerializeField] bool isShoot = false;

    Level level;
    EnemySpawner enemySpawner;

    GameSession gameSession;

    bool nextLevel = false;


    // Use this for initialization
    void Start () {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        level = FindObjectOfType<Level>();	
        enemySpawner = FindObjectOfType<EnemySpawner>();
        gameSession = FindObjectOfType<GameSession>();
    }
	
	// Update is called once per frame
	void Update () {
        if(isShoot){CountDownAndShoot();}
	}

    private void CountDownAndShoot()
    {
        // Debug.Log("shotCounter " + shotCounter);

        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            Fire();
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
            projectile,
            transform.position,
            Quaternion.identity
            ) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageController damageController = other.gameObject.GetComponent<DamageController>();
        if (!damageController) { return; }
        ProcessHit(damageController);
    }

    private void ProcessHit(DamageController damageController)
    {
        health -= damageController.GetDamage();
        damageController.Hit();
        if (health <= 0)
        {
            // Debug.Log("nextSCore " +  level.GetNextLevelScore());

            if(!gameSession.GetNextLevel() && gameSession.GetScore() > level.GetNextLevelScore()){
                // level.LoadNextLevel();
                // Destroy(enemySpawner);
                // Debug.Log("Destroy spawner");
                enemySpawner.DestroySelf();
                gameSession.NextLevel();
            }

            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }
}
