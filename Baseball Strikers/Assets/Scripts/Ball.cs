using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    //private float Duration;
    //private float d = 0;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private bool isHit;
    [SerializeField] private float speed;

    private float tempstoredspeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc = gameObject.GetComponent<BoxCollider2D>();

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "BatHitbox")
        {
            isHit = true;
            tempstoredspeed = speed * -1;
            Debug.Log("Hit by " + collision.GetComponent<HitBox>().getType() + " --" + collision.GetComponent<HitBox>().getTiming());

            rb.velocity = Vector2.zero;

            if(collision.GetComponent<HitBox>().getType() == "Hor")
            {
                if(collision.GetComponent<HitBox>().getTiming() == "Late")
                    rb.AddForce(new Vector2(speed, 5), ForceMode2D.Impulse);

                if (collision.GetComponent<HitBox>().getTiming() == "Just")
                    rb.AddForce(new Vector2(tempstoredspeed, 10), ForceMode2D.Impulse);

                if (collision.GetComponent<HitBox>().getTiming() == "Early")
                    rb.AddForce(new Vector2(tempstoredspeed, 15), ForceMode2D.Impulse);

            }

            else if (collision.GetComponent<HitBox>().getType() == "Ver")
            {
                if (collision.GetComponent<HitBox>().getTiming() == "Late")
                    rb.AddForce(new Vector2(5, -speed), ForceMode2D.Impulse);

                if (collision.GetComponent<HitBox>().getTiming() == "Just")
                    rb.AddForce(new Vector2(10, -speed), ForceMode2D.Impulse);

                if (collision.GetComponent<HitBox>().getTiming() == "Early")
                    rb.AddForce(new Vector2(15, -speed), ForceMode2D.Impulse);

            }

            else if (collision.GetComponent<HitBox>().getType() == "Bunt")
            {
                rb.gravityScale = 1;
                rb.AddForce(new Vector2(2, 2), ForceMode2D.Impulse);
            }
            //DestroyBall();

            //Destroy(bc);
        }

        else if(collision.tag == "OutZone" )
        {
            Debug.Log("Ball Has Left");
            DestroyBall();
        }

        else if(collision.tag == "HurtZone" && !isHit)
        {
            Debug.Log("Ball Hit Player");
            DestroyBall();
        }
    }
}
