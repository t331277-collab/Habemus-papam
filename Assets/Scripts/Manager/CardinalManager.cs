using System.Collections.Generic;
using UnityEngine;

public class CardinalManager : MonoBehaviour
{
    public GameObject playerCardinalPrefab;
    public GameObject aiCardinalPrefab;
    private List<Cardinal> cardinals = new List<Cardinal>();

    void Start()
    {
        var player = Instantiate(playerCardinalPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        var playerCardinal = player.GetComponent<Cardinal>();

        cardinals.Add(playerCardinal);

        // 임시 AI카디널 생성 로직..
        // for (int i = 0; i < 3; i++)
        // {
        //     Vector3 pos = new Vector3(2 + i * 2, 0, 0);
        //     var ai = Instantiate(aiCardinalPrefab, pos, Quaternion.identity);
        //     var aiCardinal = ai.GetComponent<Cardinal>();
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
