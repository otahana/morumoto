using UnityEngine;
using UnityEngine.UI;

public class MobuManager : MonoBehaviour
{
    public GameObject mobu1;     // クイズ前にいるモブ
    public GameObject mobu01;    // クイズ正解後に出す追従モブ
    public Text countText;       // UI テキスト
    public Transform player;     // プレイヤー Transform

    void Start()
    {
        if (GameManager.Instance != null && GameManager.Instance.mobu1Cleared)
        {
            // クイズ正解後
            mobu1.SetActive(false);
            mobu01.SetActive(true);
            countText.text = "モルモット：" + GameManager.Instance.guineaPigCount.ToString();

            // プレイヤーに追従
            FollowPlayer follow = mobu01.GetComponent<FollowPlayer>();
            if (follow == null)
            {
                follow = mobu01.AddComponent<FollowPlayer>();
            }
            follow.target = player;
        }
        else
        {
            // クイズ前
            mobu1.SetActive(true);
            mobu01.SetActive(false);
            countText.text = "モルモット：" + GameManager.Instance.guineaPigCount.ToString();
        }
    }
}
