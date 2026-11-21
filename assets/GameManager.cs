using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool mobu1Cleared = false; // mobu1を消すかどうか
    public int guineaPigCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいで維持
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
