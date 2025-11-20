using UnityEngine;

public class TitleManager : MonoBehaviour
{
    // 전역 GameManager 객체 가져오기
    GameManager gameManager = GameManager.Instance;
    
    public void OnClickNewGame()
    {
        gameManager.StartNewGame();
    }
}
