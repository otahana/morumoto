using UnityEngine;

public class CameraFollowXOnly : MonoBehaviour
{
    [SerializeField]
    private Transform target; // character_normal_3 をセットする

    [SerializeField]
    private float smoothSpeed = 0.125f;

    private float fixedY; // 初期Y座標を保持

    private void Start()
    {
        fixedY = transform.position.y; // Yは固定
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, fixedY, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
