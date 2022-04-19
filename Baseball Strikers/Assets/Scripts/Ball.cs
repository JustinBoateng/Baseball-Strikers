using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    //private float Duration;
    //private float d = 0;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    [SerializeField] private bool isHit;
    [SerializeField] private string wasHitBy = "";
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

    public void setHit(bool b)
    {
        isHit = b;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "BatHitbox")
        {
            tempstoredspeed = speed * -1;
            //Debug.Log("Hit by " + collision.GetComponent<HitBox>().getType() + " --" + collision.GetComponent<HitBox>().getTiming());

            rb.velocity = Vector2.zero;

            if(collision.GetComponent<HitBox>().getType() == "Hor")
            {
                isHit = true;
                if (collision.GetComponent<HitBox>().getTiming() == "Early" && wasHitBy == "")
                {
                    wasHitBy = "Early";
                    Debug.Log("Hit by " + collision.GetComponent<HitBox>().getType() + " --" + wasHitBy);

                    GameState.GS.HitTypeInc(0);
                    GameState.GS.MeterGain(1);

                }

                if (collision.GetComponent<HitBox>().getTiming() == "Just" && wasHitBy == "")
                {
                    wasHitBy = "Just";
                    Debug.Log("Hit by " + collision.GetComponent<HitBox>().getType() + " --" + wasHitBy);

                    GameState.GS.HitTypeInc(1);
                    GameState.GS.MeterGain(2);
                }

                if (wasHitBy == "Just")
                {
                    rb.AddForce(new Vector2(tempstoredspeed, 10), ForceMode2D.Impulse);
                           
                }

                if (wasHitBy == "Early")
                {
                    rb.AddForce(new Vector2(speed, 5), ForceMode2D.Impulse);
                    
                }

            }

            else if (collision.GetComponent<HitBox>().getType() == "Strike")
            {
                isHit = true;
                wasHitBy = "Strike";
                Debug.Log("Hit by " + collision.GetComponent<HitBox>().getType() + " --" + wasHitBy);
                rb.AddForce(new Vector2(tempstoredspeed + 2, 0), ForceMode2D.Impulse);
                GameState.GS.HitTypeInc(1);
            }

            else if (collision.GetComponent<HitBox>().getType() == "Ver")
            {
    

                if (collision.GetComponent<HitBox>().getTiming() == "Just" && wasHitBy == "")
                {
                    wasHitBy = "Just";
                    GameState.GS.HitTypeInc(2);
                    isHit = true;

                    GameState.GS.MeterGain(2);

                }

                if (collision.GetComponent<HitBox>().getTiming() == "Early" && wasHitBy == "")
                {
                    wasHitBy = "Early";
                    GameState.GS.HitTypeInc(3);
                    isHit = true;

                    GameState.GS.MeterGain(1);

                }

                if (wasHitBy == "Just")
                {
                    rb.AddForce(new Vector2(10, -speed), ForceMode2D.Impulse);

                }


                if (wasHitBy == "Early")
                {
                    rb.AddForce(new Vector2(5, -speed), ForceMode2D.Impulse);

                }
            }

            else if (collision.GetComponent<HitBox>().getType() == "Bunt")
            {
                rb.gravityScale = 1;
                rb.AddForce(new Vector2(2, 2), ForceMode2D.Impulse);
                GameState.GS.HitTypeInc(4);
                isHit = true;
            }
            //DestroyBall();

            //Destroy(bc);
        }

        else if(collision.tag == "OutZone" )
        {
            Debug.Log("Ball Has Left");
            DestroyBall();
        }

        else if(collision.tag == "HurtZone" && wasHitBy == "")
        {
            Debug.Log("Ball Hit Player");
            DestroyBall();
        }

        else if (collision.tag == "EnemyHurtZone" && isHit)
        {
            Debug.Log("Ball Hit Enemy");
            DestroyBall();
        }
    }


}
