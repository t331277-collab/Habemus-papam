using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 싱글톤 구현
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // 게임이 시작되면 바로 Title 씬 로드
        SceneManager.LoadScene("TitleScene");
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("GameScene");
        SceneManager.UnloadSceneAsync("TitleScene");
    }
}
