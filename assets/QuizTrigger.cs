using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class QuizTrigger : MonoBehaviour
{
    public string nextSceneName = "QuizScene";
    public Image fadePanel;  // 黒いImageをInspectorでセット
    public float fadeDuration = 1f;

    private bool isTransitioning = false;

    void Start()
    {
        // 最初は透明にしておく
        Color c = fadePanel.color;
        c.a = 0f;
        fadePanel.color = c;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTransitioning) return;

        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeAndLoadScene());
        }
    }

    private IEnumerator FadeAndLoadScene()
    {
        isTransitioning = true;

        float timer = 0f;
        Color color = fadePanel.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadePanel.color = color;
            yield return null;
        }

        // フェードイン完了後、シーン切り替え
        SceneManager.LoadScene(nextSceneName);
    }
}
