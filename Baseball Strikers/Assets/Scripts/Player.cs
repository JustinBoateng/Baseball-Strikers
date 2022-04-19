using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject HorHitbox; [SerializeField] GameObject VerHitbox;  [SerializeField] GameObject BuntHitbox; [SerializeField] GameObject StrikeHitbox;

    [SerializeField] bool HorSwing; [SerializeField] bool HorSwingStartup;[SerializeField] bool HorSwinging; [SerializeField] bool HorSwingEndlag;

    [SerializeField] float HorStartingSwingFrames; [SerializeField] float HorSwingingFrames; [SerializeField] float HorEndlagFrames;

    [SerializeField] bool VerSwing;[SerializeField] bool VerSwingStartup; [SerializeField] bool VerSwinging; [SerializeField] bool VerSwingEndlag;

    [SerializeField] float VerStartingSwingFrames; [SerializeField] float VerSwingingFrames;  [SerializeField] float VerEndlagFrames;

    [SerializeField] bool SwingState; [SerializeField] bool BuntState; [SerializeField] bool StrikeState;
   
    [SerializeField] float BaseStartingSwingFrames; [SerializeField] float BaseSwingingFrames; [SerializeField] float BaseEndlagFrames;



    GameObject Ball;
    [SerializeField] private GameObject BallPrefab;
    [SerializeField] private float ThrowSpeed;
    [SerializeField] private Transform SpawnPoint;

    [SerializeField] private float maxSpeedMultiplier;

    [SerializeField] private float maxTimer = 10;
    [SerializeField] private float timer = 10;
    [SerializeField] private float timerrate = 0.5f;

    [SerializeField] private bool throwing;
    [SerializeField] private bool throwstartup;
    [SerializeField] private bool throwendlag;

    [SerializeField] private float throwStartupframes;
    [SerializeField] private float throwEndlagframes;
    [SerializeField] private float OGthrowStartupframes;
    [SerializeField] private float OGthrowEndlagframes;


    [SerializeField] private Sprite[] SpriteArray; //0 = Bat idle, 1 = startup, 2 = Swinging, 3 = endlag, 4 = Vertical startup, 5 = Vertical swinging, 6 = Vertical Endlag, 7 = Bunt Sprite
                                                   //8 = Pitch Idle, 9 = throw startup, 10 = throwing, 11 = throw endlag
    [SerializeField] float PlayerFrameRate;

    SpriteRenderer mySR;
    [SerializeField] int facing = 1; //1 = facing right, -1 = facing left

    private float HorAxis;
    [SerializeField] bool BuntSpriteSnap = false;
    [SerializeField] int SpecialMoveCost = 50;

    [SerializeField] string PlayMode = "Batter";
    //either Pitcher or Batter

    // Start is called before the first frame update
    void Start()
    {
        HorHitbox.SetActive(false);
        VerHitbox.SetActive(false);
        BuntHitbox.SetActive(false);
        StrikeHitbox.SetActive(false);
        HorSwinging = false;

        mySR = GetComponent<SpriteRenderer>();
        mySR.sprite = SpriteArray[0];

        HorEndlagFrames = BaseEndlagFrames;
        HorSwingingFrames = BaseSwingingFrames;
        HorStartingSwingFrames = BaseStartingSwingFrames;
    }

    // Update is called once per frame
    void Update()
    {

        HorAxis = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown("b"))
        {
            ModeSwitch();    
        }


        //BATTER MODE
        if(PlayMode == "Batter")
        {
            //Bunt and Strike States
            {
                if (HorAxis == -1 && !SwingState)
                {
                    BuntState = true;
                    StrikeState = false;
                }

                else if (HorAxis == 1)
                {
                    StrikeState = true;
                    BuntState = false;

                }

                else
                {
                    BuntState = false;
                    StrikeState = false;
                }

            }

            if (Input.GetKeyDown("m") && !HorSwing && !BuntState)
            {
                HorSwing = true;
                SwingState = true;

            }

            if (Input.GetKeyDown("n") && !VerSwing && !BuntState)
            {
                VerSwing = true;
                SwingState = true;
            }

            //Bunting
            if (!SwingState)
            {
                if (BuntState && !VerSwing)
                {
                    if (!BuntSpriteSnap)
                    {
                        mySR.sprite = SpriteArray[7];
                        BuntSpriteSnap = true;
                    }

                    BuntHitbox.SetActive(true);
                }

                else if (!BuntState)
                {
                    if (BuntSpriteSnap)
                    {
                        mySR.sprite = SpriteArray[0];
                        BuntSpriteSnap = false;
                    }
                    BuntHitbox.SetActive(false);
                }
            }

            //Swinging
            if (!BuntState)
            {
                //Horizontal Swing and StrikeSwing
                {
                    if (HorSwing && !HorSwingStartup)
                    {
                        HorSwing = false;
                        HorSwingStartup = true;
                        mySR.sprite = SpriteArray[1];

                    }

                    if (HorSwingStartup && HorStartingSwingFrames > 0)
                    {
                        HorStartingSwingFrames = HorStartingSwingFrames - PlayerFrameRate;
                    }


                    //Swinging Frames
                    if (HorSwingStartup && HorStartingSwingFrames <= 0 && !HorSwinging)
                    {
                        HorSwingStartup = false;
                        HorSwinging = true;
                        mySR.sprite = SpriteArray[2];

                        //StrikeState Special Move
                        if (StrikeState && GameState.GS.MeterCheck() >= SpecialMoveCost)
                        {
                            StrikeHitbox.SetActive(true);
                            GameState.GS.MeterGain(-SpecialMoveCost);
                            HorSwingingFrames = HorSwingingFrames / 2;
                            HorStartingSwingFrames = HorStartingSwingFrames / 2;
                        }

                        else HorHitbox.SetActive(true);
                    }

                    if (HorSwinging && HorSwingingFrames > 0)
                    {
                        HorSwingingFrames = HorSwingingFrames - PlayerFrameRate;
                    }

                    //swing endlag
                    if (HorSwinging && HorSwingingFrames <= 0 && !HorSwingEndlag)
                    {
                        HorSwinging = false;
                        HorSwingEndlag = true;
                        HorHitbox.SetActive(false);
                        StrikeHitbox.SetActive(false);
                        mySR.sprite = SpriteArray[3];
                    }

                    if (HorSwingEndlag && HorEndlagFrames > 0)
                    {
                        HorEndlagFrames = HorEndlagFrames - PlayerFrameRate;
                    }

                    if (HorSwingEndlag && HorEndlagFrames <= 0)
                    {

                        HorSwingEndlag = false;
                        HorSwing = false;

                        HorSwingStartup = false;
                        HorSwinging = false;

                        HorHitbox.SetActive(false);
                        StrikeHitbox.SetActive(false);

                        HorEndlagFrames = BaseEndlagFrames;
                        HorSwingingFrames = BaseSwingingFrames;
                        HorStartingSwingFrames = BaseStartingSwingFrames;
                        mySR.sprite = SpriteArray[0];

                        SwingState = false;
                    }

                }
                //

                //Vertical Swing
                {
                    {
                        //VerSwing Startup
                        if (VerSwing && !VerSwingStartup)
                        {
                            VerSwing = false;
                            VerSwingStartup = true;
                            mySR.sprite = SpriteArray[4];

                        }

                        if (VerSwingStartup && VerStartingSwingFrames > 0)
                        {
                            VerStartingSwingFrames = VerStartingSwingFrames - PlayerFrameRate;
                        }


                        //Swinging Frames
                        if (VerSwingStartup && VerStartingSwingFrames <= 0 && !VerSwinging)
                        {
                            VerSwingStartup = false;
                            VerSwinging = true;
                            mySR.sprite = SpriteArray[5];

                            VerHitbox.SetActive(true);
                        }

                        if (VerSwinging && VerSwingingFrames > 0)
                        {
                            VerSwingingFrames = VerSwingingFrames - PlayerFrameRate;
                        }

                        //swing endlag
                        if (VerSwinging && VerSwingingFrames <= 0 && !VerSwingEndlag)
                        {
                            VerSwinging = false;
                            VerSwingEndlag = true;
                            VerHitbox.SetActive(false);
                            mySR.sprite = SpriteArray[6];
                        }

                        if (VerSwingEndlag && VerEndlagFrames > 0)
                        {
                            VerEndlagFrames = VerEndlagFrames - PlayerFrameRate;
                        }

                        if (VerSwingEndlag && VerEndlagFrames <= 0)
                        {

                            VerSwingEndlag = false;
                            VerSwing = false;

                            VerSwingStartup = false;
                            VerSwinging = false;

                            VerEndlagFrames = BaseEndlagFrames;
                            VerSwingingFrames = BaseSwingingFrames;
                            VerStartingSwingFrames = BaseStartingSwingFrames;

                            VerHitbox.SetActive(false);

                            mySR.sprite = SpriteArray[0];

                            SwingState = false;
                        }

                    }
                }
                //
            }

        }

        if(PlayMode == "Pitcher")
        {
            if (Input.GetKeyDown("m") && !throwstartup && !throwendlag)  throwing = true;
            

            //Throw Startup
            if (throwing && !throwstartup)
            {
                throwing = false;
                throwstartup = true;
                mySR.sprite = SpriteArray[9];
            }

            if (throwstartup && throwStartupframes > 0)
            {
                throwStartupframes = throwStartupframes - PlayerFrameRate;
            }

            //Throwing Frames
            if (throwstartup && throwStartupframes <= 0 && !throwendlag)
            {
                throwstartup = false;
                throwendlag = true;
                mySR.sprite = SpriteArray[10];

                if (GameState.GS.MeterCheck() > SpecialMoveCost)
                {
                    Ball = Instantiate(BallPrefab, SpawnPoint.position, this.transform.rotation);
                    Ball.GetComponent<Ball>().SetSpeed(ThrowSpeed * Random.Range(1, maxSpeedMultiplier) * facing);
                    Ball.GetComponent<Ball>().setHit(true);

                    GameState.GS.MeterGain(-(SpecialMoveCost * 2));
                }
            }

            if (throwendlag && throwEndlagframes > 0)
            {
                throwEndlagframes = throwEndlagframes - PlayerFrameRate;
            }

            //endlag frames
            if (throwendlag && throwEndlagframes <= 0)
            {
                throwing = false;
                throwstartup = false;
                throwendlag = false;
                throwStartupframes = OGthrowStartupframes;
                throwEndlagframes = OGthrowEndlagframes;
                mySR.sprite = SpriteArray[8];

              
            }
        }
    }

    public void Reset()
    {
        VerEndlagFrames = 0;

        HorEndlagFrames = 0;
    }

    private void ModeSwitch()
    {
        Debug.Log("Mode Switch Registered");
        if (PlayMode == "Batter")
        {
            PlayMode = "Pitcher";
            mySR.sprite = SpriteArray[8];
        }

        else if (PlayMode == "Pitcher")
        {
            PlayMode = "Batter";
            mySR.sprite = SpriteArray[0];
        }
    }
}
