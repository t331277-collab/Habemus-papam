using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayable
{
    private Cardinal cardinal;

    void Awake()
    {
        cardinal = GetComponent<Cardinal>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        ProcessInput();
    }

    public void ProcessInput()
    {
        // 1) 키보드 이동 (WASD)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        cardinal.MoveByRigidbody(new Vector2(h, v));

        // 2) 마우스 클릭 이동 (오른쪽 클릭)
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                cardinal.MoveByNavmesh(hit.point);
            }
        }
    }
 }
