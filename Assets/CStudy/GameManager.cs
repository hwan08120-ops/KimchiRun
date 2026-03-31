using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;

public enum gameState
{
    Playing,
    Paused,
    GameOver,
    Clear
}

public class GameManager : MonoBehaviour
{
    public  EnemyController enemyCtrl;
    //public int gameState = 0;
private gameState currentState = gameState.Playing;

    void Update()
    {
        Keyboard curKey = Keyboard.current;
        if (curKey != null && curKey.pKey.wasReleasedThisFrame)
        {
            currentState = gameState.Paused;
        }
        if (curKey != null && curKey.oKey.wasPressedThisFrame)
        {
            currentState = gameState.GameOver;
        }
        switch (currentState)
        {
            case gameState.Playing:
            Debug.Log("게암 잔행 중");
            break;
            case gameState.Paused:
            Debug.Log("일시 정지");
            break;
            case gameState.GameOver:
            Debug.Log("게임 오버!");
            break;
            case gameState.Clear:
            Debug.Log("클리어 !");
            break;
        }
    }
  }
    

    

        
        
            
            
            
        
    
            
        
        
            
        
                                                                

    
        
    
