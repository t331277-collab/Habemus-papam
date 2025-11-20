using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [Tooltip("기도 관련 파라미터")]
    [SerializeField] int pietyChangeOnPray = 10;
    [SerializeField] float healChanceOnPray = 0.8f;
    [SerializeField] int hpChangeOnPray = 10;

    [Tooltip("연설 관련 파라미터")]
    [SerializeField] float influenceChangeMinOnSpeech = 0.1f;
    [SerializeField] float influenceChangeMaxOnSpeech = 0.3f;

    public int PietyChangeOnPray => pietyChangeOnPray;
    public float HealChanceOnPray => healChanceOnPray;
    public int HpChangeOnPray => hpChangeOnPray;
    public float InfluenceChangeMaxOnSpeech => influenceChangeMaxOnSpeech;
    public float InfluenceChangeMinOnSpeech => influenceChangeMinOnSpeech;


    // 싱글톤 구현
    public static InGameManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
