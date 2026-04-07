using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float jumpForce = 5f; // 점프하는 힘
    public float fallMultiplier = 2.5f; // 떨어질 때 가속되는 배수

    // 더 이상 횟수(JumpCount)를 세지 않고, 현재 바닥에 닿아있는지(접지 상태)만 확인합니다.
    private bool isGrounded = false;

    private Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 스페이스바를 누른 상태이면서, 바닥(isGrounded == true)일 때만 점프를 허용합니다.
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (isGrounded == true)
            {
                Debug.Log("스페이스바 눌림! 바닥에서 1단 점프 시도 중...");

                // 점프 전 기존 떨어지던 속도를 0으로 초기화
                rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, 0f);

                // 위쪽으로 힘을 가해 점프시킵니다.
                rigidBody.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);
                
                // 뛰었으니 바로 공중 상태가 됩니다.
                isGrounded = false; 
            }
        }

        // 공중에서 떨어질 때 마리오처럼 빠르게 떨어지는 효과
        if (rigidBody.linearVelocity.y < 0f)
        {
            Vector2 currentVelocity = rigidBody.linearVelocity;
            currentVelocity += Physics2D.gravity * (this.fallMultiplier - 1f) * Time.deltaTime;
            rigidBody.linearVelocity = currentVelocity;
        }
    }

    // 캐릭터가 어떤 물체와 부딪혔을 때 발동
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 부딪힌 물체의 태그(이름표)가 "Ground"라면 바닥으로 인정합니다.
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // 캐릭터가 물체에서 완전히 벗어나거나 떨어졌을 때 발동
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 절벽에서 그냥 걸어서 떨어졌을 때 허공에서 점프하는 것을 막습니다.
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
