using UnityEngine;


public class CardinalManager : MonoBehaviour
{
    [Tooltip("현재 씬에 있는 모든 카디널의 배열")]
    public Cardinal[] cardinals;
    
    public NPC npcPrefab; // 생성할 대상이 NPC이므로 NPC 타입으로 받는 것이 명확함


    private void Start()
    {
    }

    public void MakeNpcCardinal()
    {
        // 여기서 NPC카디널 생성 관리 등
    }

    // (추가 예정)
}