using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInOnReturn : MonoBehaviour
{
    public Image fadePanel;          // 黒いImage（Canvas内）
    public float fadeDuration = 1f;  // フェード時間
    public float waitBeforeFade = 5f; // 表示しておく時間

    void Start()
    {
        if (fadePanel != null)
        {
            StartCoroutine(FadeOutAfterDelay());
        }
    }

    private IEnumerator FadeOutAfterDelay()
    {
        // 最初は黒のままで少し待つ
        yield return new WaitForSeconds(waitBeforeFade);

        float timer = 0f;
        Color color = fadePanel.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            fadePanel.color = color;
            yield return null;
        }

        // 最後に完全に透明にする（念のため）
        color.a = 0f;
        fadePanel.color = color;
    }
}
