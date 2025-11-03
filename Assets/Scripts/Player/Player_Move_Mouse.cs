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
        HandleClick();
        SetAgentPosition();
    }

    void HandleClick()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 world = cam.ScreenToWorldPoint(Input.mousePosition);
            world.z = zDepth;

            if(last != null)
            {
                Destroy(last);
            }

            last = Instantiate(prefab, world, Quaternion.identity);

            target = world;
        }


    }
    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
    }
}