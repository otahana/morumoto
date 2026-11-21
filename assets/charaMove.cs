using UnityEngine;

public class chara_move : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.2f;

    [SerializeField]
    private float jumpForce = 5f;

    private Animator animator;
    private Rigidbody2D rb;

    private bool isGrounded = true; // 簡易的なジャンプ制御

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2Dが必要
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        bool isCrouch = Input.GetKey(KeyCode.LeftShift);

        // 移動処理（しゃがんでなければ移動）
        if (!isCrouch)
        {
            Vector3 movement = new Vector3(moveX, 0, 0).normalized * speed;
            transform.Translate(movement);
        }

        // ジャンプ（Shiftキー） - アニメーションなし
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        // 簡易的な接地判定（地面に着いたらジャンプ可能にする）
        if (rb.linearVelocity.y == 0)
        {
            isGrounded = true;
        }

        // アニメーション切り替え
        if (isCrouch && moveX > 0)
        {
            animator.SetInteger("direction", 10); // 右しゃがみ
        }
        else if (isCrouch && moveX < 0)
        {
            animator.SetInteger("direction", 11); // 左しゃがみ
        }
        else if (moveX > 0)
        {
            animator.SetInteger("direction", 0); // 右歩き
        }
        else if (moveX < 0)
        {
            animator.SetInteger("direction", 1); // 左歩き
        }
        else
        {
            animator.SetInteger("direction", -1); // 立ち止まり
        }
    }
}