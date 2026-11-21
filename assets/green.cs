using UnityEngine;

public class green : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 2f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;
    private bool isJumping = false;

    private SpriteRenderer spriteRenderer;
    private Vector3 lastTargetPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastTargetPos = target.position;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 diff = target.position - transform.position;

            // プレイヤーとの距離が大きいときだけ追従
            if (Mathf.Abs(diff.x) > 0f || Mathf.Abs(diff.y) > 0f)
            {
                Vector3 newPos = Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);
                transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);

            }

            // プレイヤーの移動方向に応じて反転（左右）
            float moveX = target.position.x - lastTargetPos.x;
            if (moveX > 0.01f)
                spriteRenderer.flipX = false; // 右向き
            else if (moveX < -0.01f)
                spriteRenderer.flipX = true;  // 左向き

            lastTargetPos = target.position;
        }
    }
}
