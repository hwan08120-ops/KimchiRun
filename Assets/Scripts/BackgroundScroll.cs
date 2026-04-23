using UnityEngine;
using UnityEngine.InputSystem; // 새로운 Input System 추가

[RequireComponent(typeof(MeshRenderer))]
public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 2.5f; // 기본 스크롤 속도
    public float sprintMultiplier = 2f; // 쉬프트 달리기 속도 배수
    
    private MeshRenderer meshRenderer;
    private float currentOffset = 0f; // 누적 오프셋 

    void Start()
    {
        // MeshRenderer 컴포넌트를 가져옵니다.
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        float currentSpeed = scrollSpeed;

        // 새로운 Input System: Shift 키를 누르고 있는 동안 속도 증가
        if (Keyboard.current != null && Keyboard.current.shiftKey.isPressed)
        {
            currentSpeed *= sprintMultiplier;
        }

        // Time.time 대신 업데이트마다 이전 오프셋에 더해주는 방식을 써야
        // 속도가 갑자기 변할 때 배경이 확 튀는(순간이동) 현상을 방지할 수 있습니다.
        currentOffset += currentSpeed * Time.deltaTime;

        // mainTextureOffset을 통해 Surface Inputs의 Offset 값을 변경합니다.
        meshRenderer.material.mainTextureOffset = new Vector2(currentOffset, 0);
    }
}
