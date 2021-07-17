using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    [SerializeField] float delayInSeconds = 2f;
    [SerializeField] int nextLevelScore = 2000;
    [SerializeField] GameObject End;

    bool nextLevel = false;

    GameSession gameSession;

    void Start() {
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update(){
        if(gameSession.GetNextLevel() == true){
            Enemy[] enemyCounter = FindObjectsOfType<Enemy>();
            if (enemyCounter.Length == 0 ){
                End.SetActive(true);
                LoadNextLevel();
            };

        }
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Level 1");
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public int GetNextLevelScore(){
        return nextLevelScore;
    }

    public void NextLevel(){
        nextLevel = true;
    }

    public void LoadNextLevel(){
        if(gameSession.GetCurrentLevel() <= 2){
            gameSession.SetNextLevel(false);
            StartCoroutine(LoadLevel2());
        }
        else{
            LoadGameOver();
        }
    }

    IEnumerator LoadLevel2(){
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Level 2");
    }

}
