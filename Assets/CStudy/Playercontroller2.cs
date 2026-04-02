using UnityEngine;
public class EnemyController2 : MonoBehaviour
{
    // 변수 선언
    string enemyName = "고블린";
    int health = 80;
    float moveSpeed = 2.5f;
    bool isAttacking = true;

    void Start()
    {
        Debug.Log("이름: " + enemyName);
        Debug.Log("체력: " + health);
        Debug.Log("이동 속도: " + moveSpeed);
        Debug.Log("공격 여부: " + isAttacking);
    }
}
        
    

    
    
    
        
    

