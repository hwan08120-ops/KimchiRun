using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float jumpForce = 5f;

    public float fallMultiplier = 2.5f;

    public int maxJumps = 2;


    private bool isGrounded = false;

    private int currentJumps = 0;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private Collider2D collider2D;

    public int lives = 3;
    public bool isInvincible = false;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // 스페이스바를 눌렀을 때, 바닥에 있거나 최대 점프 횟수에 도달하지 않았다면 점프를 허용합니다.
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (isGrounded == true || currentJumps < maxJumps)
            {
                Debug.Log("스페이스바 눌림! " + (currentJumps + 1) + "단 점프 시도 중...");

                // 점프 전 기존 떨어지던 속도를 0으로 초기화하여 더블 점프 시 깔끔하게 올라가도록 합니다.
                rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, 0f);

                // 위쪽으로 힘을 가해 점프시킵니다.
                rigidBody.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);

                // 스페이스바를 눌러 점프할 때 애니메이터(Animator) 파라미터의 state 값을 1로 변경합니다.
                animator.SetInteger("state", 1);

                // 뛰었으니 바로 공중 상태가 됩니다.
                isGrounded = false;

                // 점프 횟수를 늘려줍니다.
                currentJumps++;
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
            currentJumps = 0; // 바닥에 닿으면 점프 횟수를 초기화합니다.

            // 바닥에 닿았을 때(착지) 애니메이터(Animator) 파라미터의 state 값을 2로 변경합니다.
            animator.SetInteger("state", 2);
        }
    }

    // 캐릭터가 물체에서 완전히 벗어나거나 떨어졌을 때 발동
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 절벽에서 그냥 걸어서 떨어졌을 때도 점프 횟수를 관리합니다.
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;

            // 점프를 누르지 않고 떨어졌더라도, 기본 점프 1회를 사용한 것으로 처리해 공중에서 한 번만 뛸 수 있게 돕습니다.

            if (currentJumps == 0)
            {
                currentJumps = 1;
            }
        }
    }

    // 캐릭터가 트리거(Trigger)로 설정된 물체와 겹쳤을 때 발동
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player triggerEnter in Enemy :" + other.gameObject.name);

            Destroy(other.gameObject);
            if (!isInvincible)
            {
                Damage();
            }
        }
        else if (other.gameObject.CompareTag("Food"))
        {
            Debug.Log("Player triggerEnter in Food :" + other.gameObject.name);

            Destroy(other.gameObject);
            Heal();
        }
        else if (other.gameObject.CompareTag("Gold"))
        {
            Debug.Log("Player triggerEnter in Gold :" + other.gameObject.name);

            Destroy(other.gameObject);
            StartInvincible();
        }
    }

    private void Heal()
    {
        lives = Mathf.Min(lives + 1, 3);
        Debug.Log("Player lives : " + lives);
    }

    private void Damage()
    {
        lives--;
        if (lives <= 0)
        {
            Debug.Log("Game Over");
            KillPlayer();
        }
        Debug.Log("Player lives : " + lives);
    }

    private void StartInvincible()
    {
        isInvincible = true;
        Invoke("StopInvincible", 5f);
    }

    private void StopInvincible()
    {
        isInvincible = false;
    }

    private void KillPlayer()
    {
        collider2D.enabled = false;
        animator.enabled = false;
        rigidBody.AddForceY(5f, ForceMode2D.Impulse);
    }

}


