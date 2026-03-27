using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController2 : MonoBehaviour
{
    void Update()
    {
        // 현재 연결된 키보드 장치가 있는지 먼저 체크 (안전장치)
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // 2.해당 키를 "누르는 순간" 한 번만 실행 (GetkeyDownn 대응)
        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("스페이스바를 눌렀습니다");
        }

        // 3. 해당 키를 "누르고 있는 동안" 계속 실행 (Getkey대응)
        if (keyboard.wKey.isPressed)
        {
            Debug.Log("W 키를 누르는중...");
        }
    }
}
        
    

    
    
    
        
    

