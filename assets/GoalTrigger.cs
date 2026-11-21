using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections; // ← これが必要！


public class GoalTrigger : MonoBehaviour
{
    public int requiredCount = 1;
    public string nextSceneName = "NextStage";
    public Text warningText; // モルモットが足りません用のUIテキスト

    private void Start()
    {
        if (warningText != null)
        {
            warningText.gameObject.SetActive(false); // 最初は非表示
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                if (GameManager.Instance.guineaPigCount >= requiredCount)
                {
                    StartCoroutine(LoadSceneAfterDelay(0.5f)); // ← 1秒待ってからシーン遷移
                }
                else
                {
                    if (warningText != null)
                    {
                        warningText.text = "モルモットが足りません";
                        warningText.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName);
    }
}
