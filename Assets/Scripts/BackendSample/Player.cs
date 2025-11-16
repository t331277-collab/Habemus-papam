using UnityEngine;


public class Player : Cardinal, IPlayable
{
    // (예시) 플레이어는 Update에서 스스로 입력을 받음
    private void Update()
    {
        ProcessPlayerInput();
    }

    public void ProcessPlayerInput()
    {
        // 1. Input.GetAxis, Input.GetKeyDown 등으로 입력 받기
        // float h = Input.GetAxis("Horizontal");
        
        // 2. 입력에 따라 부모(Cardinal)의 기능 호출
        // MoveTo(new Vector3(h, 0, 0));
        // if (Input.GetKeyDown(KeyCode.Space)) Perform("Attack");

        // 이건 예시고 다양하게.. 핵심은 이동이나 공작같은 핵심 기능의 구현은 Cardinal로 공통으로 뽑아놓고 실제 사람과의 상호작용
        // 을 이 함수로 분리해서 이 함수에서 처리하고 실제 Cardinal의 행동과 연결하는 것임.
    }
}