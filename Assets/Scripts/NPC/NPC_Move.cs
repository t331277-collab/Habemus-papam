using UnityEngine;

public class NPC_Move : MonoBehaviour
{

    public float speed = 3.0f;
    public float interval = 3.0f;

    private Vector2 vector;
    private Rigidbody2D rgd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();

        vector.x = 1;

        InvokeRepeating(nameof(FlipDirection), interval, interval);

    }

    // Update is called once per frame
    void Update()
    {

        
        

        rgd.linearVelocity = vector * speed;
    }

    void FlipDirection()
    {
        vector.x = -vector.x;
        vector.y = 0;

        
    }
}
