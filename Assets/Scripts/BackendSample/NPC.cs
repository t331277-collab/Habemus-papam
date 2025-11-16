using UnityEngine;


public class NPC : Cardinal, IAIControl
{
    // (예시) NPC는 Update에서 스스로 AI를 판단함
    private void Update()
    {
        AIDecision();
    }


    public void AIDecision()
    {

    }
}