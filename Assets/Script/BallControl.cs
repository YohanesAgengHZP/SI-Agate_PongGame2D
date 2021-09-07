using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public float speed;
    // Rigidbody 2D bola
    private Rigidbody2D rigidBody2D;

    // Besarnya gaya awal yang diberikan untuk mendorong bola
    public float xInitialForce;
    public float yInitialForce;
    // Titik asal lintasan bola saat ini
    private Vector2 trajectoryOrigin;

    void ResetBall()
    {
        // Reset posisi menjadi (0,0)
        transform.position = Vector2.zero;

        // Reset kecepatan menjadi (0,0)
        rigidBody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        // Flip a coin to determine if the ball starts left or right
        float x = Random.value < 0.5f ? -1.0f : 1.0f;

        // Flip a coin to determine if the ball goes up or down. Set the range
        // between 0.5 -> 1.0 to ensure it does not move completely horizontal.
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f)
                                      : Random.Range(0.5f, 1.0f);

        Vector2 direction = new Vector2(x, y);
        this.rigidBody2D.AddForce(direction * this.speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (rigidBody2D.velocity.x < 0)
        {
            rigidBody2D.velocity = new Vector2(-10, rigidBody2D.velocity.y);
        }
            else
            {
                rigidBody2D.velocity = new Vector2(10, rigidBody2D.velocity.y);
            }
    }

    void RestartGame()
    {
        // Kembalikan bola ke posisi semula
        ResetBall();

        // Setelah 2 detik, berikan gaya ke bola
        Invoke("PushBall", 2);
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

        // Mulai game
        RestartGame();
    }

    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}
