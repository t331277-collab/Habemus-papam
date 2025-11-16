using UnityEngine;

public class Cardinal : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] int influence;
    [SerializeField] int piety;
    //[SerializeField] Avatar avatar;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Move() { }
    void Perform() { }
}

public interface IPlayable
{
    void ProcessPlayerInput(); //예시 함수이름
}

public interface IAIControl
{
    void AIDecision(); // 비슷한 내용..
}
