using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    int score = 0;
    int currentLevel = 1;
    bool nextLevel = false;

    private void Awake()
    {
        SetUpSingleton();
    }

    void Update(){
    
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void SetNextLevel(bool nextLevel){
        this.nextLevel = nextLevel;
    }

    public bool GetNextLevel(){
        return nextLevel;
    }

    public int GetCurrentLevel(){
        return currentLevel;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public void NextLevel()
    {
        currentLevel += 1;
        nextLevel = true;
    }

 }
