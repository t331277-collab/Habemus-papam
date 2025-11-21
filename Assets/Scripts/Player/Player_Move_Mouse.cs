using UnityEngine;
using UnityEngine.AI;

public class Player_Move_Mouse : MonoBehaviour
{
    public Vector3 target;
    GameObject last;
    NavMeshAgent agent;

    [SerializeField] Camera cam;
    [SerializeField] GameObject prefab;
    [SerializeField] float zDepth = 0f;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        cam = Camera.main;

    }

    void Update()
    {
        //마우스로 이동할 좌표 입력
        HandleClick();

        //도착시 커서 이펙트 Destroy, 위치 고정 해제
        Arrived();
    }

    void HandleClick()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 world = cam.ScreenToWorldPoint(Input.mousePosition);
            world.z = zDepth;


            //마우스 커서 이미지
            if(last != null)
            {
                Destroy(last);
            }

            last = Instantiate(prefab, world, Quaternion.identity);

            target = world;

            agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
        }


    }
   

    void Arrived()
    {
        //경로 탐색이 계산중이지 않고 and (목적지까지의 남은거리가 agent.stoppingDistance 보다 작거나 같을때)
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance) //agent.stoppingDistance의 default = 0
        {
            //유효한 경로가 있지 않거나 agent 의 속도가 0.001f 보다 작다(정지상태로 추정)면 커서 프리팹 Destroy 및 agent의 목적지 해제
            if(!agent.hasPath || agent.velocity.sqrMagnitude < 0.001f)
            {
                Destroy(last);          //커서 삭제
                agent.ResetPath();      //목적지 도착시 위치 고정하지 않음
            }
        }
    }
}