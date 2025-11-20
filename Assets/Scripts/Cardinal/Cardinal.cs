using UnityEngine;
using UnityEngine.AI;

public class Cardinal : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] int influence;
    [SerializeField] int piety;
    [SerializeField] Avatar avatar;
    [SerializeField] float moveSpeed = 3.0f;

    private Vector2 moveDir;
    private Rigidbody2D rb;
    private NavMeshAgent agent;
    private InGameManager inGameManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        inGameManager = InGameManager.Instance;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void MoveByRigidbody(Vector2 v)
    {
        moveDir.x = v.x;
        moveDir.y = v.y;

        rb.linearVelocity = moveDir * moveSpeed;
    }

    public void MoveByNavmesh(Vector2 v)
    {
        
    }

    void Perform() { }

    public void IncreaseHp(int amount)
    {
        hp = Mathf.Clamp(hp + amount, 0, 100);
    }

    public void DecreaseHp(int amount)
    {
        hp = Mathf.Clamp(hp - amount, 0, 100);
    }

    public void IncreaseInfluence(int amount)
    {
        influence = Mathf.Clamp(influence + amount, 0, 100);
    }

    public void DecreaseInfluence(int amount)
    {
        influence = Mathf.Clamp(influence - amount, 0, 100);
    }

    public void IncreasePiety(int amount)
    {
        influence = Mathf.Clamp(piety + amount, 0, 100);
    }

    public void DecreasePiety(int amount)
    {
        influence = Mathf.Clamp(piety - amount, 0, 100);
    }

    // 행동: 기도
    public void Pray()
    {
        IncreasePiety(inGameManager.PietyChangeOnPray);

        if(Random.value < inGameManager.HealChanceOnPray)
        {
            IncreaseHp(inGameManager.HpChangeOnPray);
        }
    }

    // 행동: 연설 (-50% ~ 50% 이런식으로 사용하는걸로 이해했습니다.)
    public void Speech()
    {
        float factor = Random.Range(inGameManager.InfluenceChangeMinOnSpeech, inGameManager.InfluenceChangeMaxOnSpeech);
        int amount = Mathf.FloorToInt(influence * factor);
        
        if(amount < 0)
        {
            DecreaseInfluence(-amount);
        }
        else
        {
            IncreaseInfluence(amount);
        }
    }

    // 행동: 공작(아직 더미함수)
    public void Plot()
    {
        
    }

}