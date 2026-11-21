using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public Text questionText;
    public Text[] optionTexts; // 選択肢（4つ想定）

    private string question = "ナマケモノが木から降りる理由は？";
    private string[] options = { "A. ご飯を探しに行くため", "B. 息抜きしたいから", "C. トイレに行くため","D. うっかり落ちた" };
    private int correctIndex = 2;

    private int selectedIndex = 0;
    private bool answeredCorrectly = false;

    void Start()
    {
        ShowQuestionOnly();  // 問題文だけ表示
        StartCoroutine(ShowOptionsWithDelay(2f));  // 2秒後に選択肢表示開始
    }

    void ShowQuestionOnly()
    {
        questionText.text = question;

        // 選択肢は空文字にして非表示っぽくする（または色を透明にするなど）
        for (int i = 0; i < optionTexts.Length; i++)
        {
            optionTexts[i].text = "";
            optionTexts[i].color = Color.clear;  // 透明にして見えなくする
        }
    }

    System.Collections.IEnumerator ShowOptionsWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        int count = Mathf.Min(options.Length, optionTexts.Length);
        for (int i = 0; i < count; i++)
        {
            optionTexts[i].text = options[i];
            optionTexts[i].color = Color.black;  // 黒色で表示
        }

        UpdateSelection();  // 選択中の色を反映
    }



    void Update()
    {

        if (answeredCorrectly)
        {
            // 正解後のクリックで次に進む
            if (Input.GetKeyDown(KeyCode.Return)) // Enter
            {
                LoadReturnScene();
            }
            return;
        }


        // 上下キーで選択肢を移動
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
            UpdateSelection();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex = (selectedIndex + 1) % options.Length;
            UpdateSelection();
        }

        // Enterキーで決定
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckAnswer();
        }
    }

    void ShowQuestion()
    {
        questionText.text = question;
        int count = Mathf.Min(options.Length, optionTexts.Length);
        for (int i = 0; i < count; i++)
        {
            if (optionTexts[i] != null)
            {
                optionTexts[i].text = options[i];
            }
            else
            {
                Debug.LogError($"optionTexts[{i}] が null です！");
            }
        }
    }


    void UpdateSelection()
    {
        for (int i = 0; i < optionTexts.Length; i++)
        {
            optionTexts[i].color = (i == selectedIndex) ? Color.yellow : Color.black;
        }
    }

    void CheckAnswer()
    {
        if (selectedIndex == correctIndex)
        {
            Debug.Log("正解！");
            ShowCorrectAnswer();
            answeredCorrectly = true;

            // ここで状態を記録
            GameManager.Instance.mobu1Cleared = true;
            GameManager.Instance.guineaPigCount += 1;
        }
        else
        {
            Debug.Log("不正解！");
            PlayBadSound(); // ← 追加 ※ Assets/Resources/bad.mp3 に配置してください。
        }
    }

    // 効果音再生
    void PlayBadSound()
    {
        AudioClip badClip = Resources.Load<AudioClip>("bad");

        if (badClip != null)
        {
            AudioSource.PlayClipAtPoint(badClip, Camera.main.transform.position);
        }
        else
        {
            Debug.LogWarning("bad.mp3 が Resources フォルダに存在しません！");
        }
    }



    void ShowCorrectAnswer()
    {
        questionText.text = "正解！";

        // 正解選択肢を optionTexts[0] に表示（黒色）
        optionTexts[0].text = options[correctIndex];
        optionTexts[0].color = Color.black;

        // 3番目に「Enterキー」をグレー表示
        if (optionTexts.Length > 3)
        {
            optionTexts[3].text = "Enterキー";
            optionTexts[3].color = Color.gray;
        }

        // それ以外は空文字にする
        for (int i = 1; i < optionTexts.Length; i++)
        {
            if (i != 3) optionTexts[i].text = "";
        }
    }




    void LoadReturnScene()
    {
        // ここで前のシーン名を指定
        SceneManager.LoadScene("mainstage"); // ←任意のシーン名に変更
    }
}
