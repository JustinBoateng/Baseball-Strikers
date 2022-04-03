using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    //private float Duration;
    //private float d = 0;

    private Rigidbody2D rb;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(speed, 0);
        //for now, the ball is being thrown horizontally, not vertically

    }

    // Update is called once per frame
    void Update()
    {
        //d = d + Time.deltaTime;
        //if (d > Duration) Destroy(this.gameObject);

    }
    /*
    public void SetDuration(float y)
    {
        Duration = y;
    }
    */
    public void SetSpeed(float y)
    {
        speed = y;
    }

    public void DestroyBall()
    {
        Destroy(this.gameObject);
    }//have the MissZone call this function to erase the ball from the game
    //in fact... let the destruction of the ball be determined by if it hits a zone rather than a time limit
}
