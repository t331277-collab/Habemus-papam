using UnityEngine;

public class Player_Move_Keyboard : MonoBehaviour
{

    private Rigidbody2D rgd;
    private Vector2 vector;


    public float speed = 3.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        vector.x = Input.GetAxisRaw("Horizontal");
        vector.y = Input.GetAxisRaw("Vertical");

        rgd.linearVelocity = vector * speed;
    }
}
