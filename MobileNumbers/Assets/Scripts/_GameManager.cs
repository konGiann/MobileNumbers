using Assets.Scripts.Helpers;
using MathNet.Numerics.Distributions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class _GameManager : MonoBehaviour
{
    #region Public Fields

    public static _GameManager _instance = null;
    
    public GameDifficulty CurrentDiffulty;
    
    public enum GameDifficulty
    {
        Easy = 0,
        Normal = 1,
        Hard = 2
    }    

    [Header("Game Modes and States")]
    
    public NumbersOperation CurrentNumbersGameMode;
    public GameState currentGameState;
    public GameMode currentGameMode;

    public enum NumbersOperation
    {
        Addition = 0,
        Subtraction = 1,
        Multiply = 2
    }
    
    public enum GameState
    {
        Play = 0,
        Paused = 1,
        GameOver = 2
    }

    public enum GameMode
    {
        Numbers = 0
    }
    
    #endregion


    public void Awake()
    {
        Singleton();        
    }
  
    void Singleton()
    {
        if (_instance == null)
            _instance = this;
        else
            if (_instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
      
    private void Update()
    {        
        switch (currentGameState)
        {
            case GameState.Play:
                break;
            case GameState.Paused:
                // Pause time
                break;
            case GameState.GameOver:
                // Show game over screen
                break;
            default:
                break;
        }        
    }
}
