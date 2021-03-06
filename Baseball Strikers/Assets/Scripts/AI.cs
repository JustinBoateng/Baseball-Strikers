using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;


public class AI : MonoBehaviour
{
    // An AI Will Throw and Hit balls on their own

    string Mode = "N/A";
    GameObject Ball; 
    [SerializeField] private GameObject BallPrefab;
    [SerializeField] private float ThrowSpeed;
    [SerializeField] private Transform SpawnPoint;


    [SerializeField] private Sprite[] SpriteArray;
    private SpriteRenderer mySR;

    [SerializeField] private float maxSpeedMultiplier;

    [SerializeField] private float maxTimer = 10;
    [SerializeField] private float timer = 10;
    [SerializeField] private float timerrate = 0.5f;

    [SerializeField] private bool throwing;
    [SerializeField] private bool startup;
    [SerializeField] private bool endlag;

    [SerializeField] private float throwStartup;
    [SerializeField] private float throwEndlag;
    [SerializeField] private float OGthrowStartup;
    [SerializeField] private float OGthrowEndlag;
    [SerializeField] private float ThrowFrameRate;

    [SerializeField] private int facing = -1; //-1 = left, 1 = right

    //this is either Striker or Pitcher

    [SerializeField] public int HP;
    [SerializeField] public int Meter;
    [SerializeField] public bool KOFlag = false;
    [SerializeField] public bool ActiveState;
    [SerializeField] public float ActiveTimer;

    // Start is called before the first frame update
    void Start()
    {
        mySR = GetComponent<SpriteRenderer>();
        mySR.sprite = SpriteArray[0];

        KOFlag = false;
        ActiveState = false;
        //SpriteArray[0] should have the idle frame
    }

    // Update is called once per frame
    void Update()
    {
        //basic idea for actions
        {
            //decrement timer from 10
            //when timer <= 0,  throwing = true

            //if throwing = true and startup = false,
            //change startup to true
            //change the current sprite from idle to windup until throwstartup hits zero
            //have throwstartup decrement at a rate of ThrowFrameRate until it hits 0 or less

            //if startup = true and throwstartup hits zero,
            //change endlag to true
            //change the current sprite from windup to throwing
            //have throwendlag decrement at a rate of ThrowFrameRate until it hits 0 or less
            //spawn an instance of a ball at the spawn point
            //set that speed of that instance of the ball to SpeedA

            //if throwendlag = 0 or less,
            //change throwing, startup and endlag to false
            //set timer to Random number between 0 and maxTimer
            //set throwstartup to OG throwstartup and throwEndlag to OG throwendlag
            //change the current sprite from throwing to idle
        }

        //Play the enterance animation.

        //THEN Set ActiveState to True
        if (!ActiveState && !KOFlag)
        {
            if (ActiveTimer > 0)
                ActiveTimer = ActiveTimer - ThrowFrameRate; //Just to have a countdown of sorts

            if (ActiveTimer <= 0)
            {
                ActiveState = true;
            }
        }

        if (KOFlag)
        {
            //set ActiveState to false
            //Play Defeat Animation
            //Call the GameState to call in the next Enemy
            //Delete this Enemy after a time
        }

        if (!GameState.GS.PauseFlag && ActiveState)
        {
            //throwing
            {
                if (timer > 0) timer = timer - timerrate;

                if (timer <= 0 && !startup && !endlag) throwing = true;

                //Throw Startup
                if (throwing && !startup)
                {
                    throwing = false;
                    startup = true;
                    mySR.sprite = SpriteArray[1];
                }

                if (startup && throwStartup > 0)
                {
                    throwStartup = throwStartup - ThrowFrameRate;
                }


                //Throwing Frames
                if (startup && throwStartup <= 0 && !endlag)
                {
                    startup = false;
                    endlag = true;
                    mySR.sprite = SpriteArray[2];

                    Ball = Instantiate(BallPrefab, SpawnPoint.position, this.transform.rotation);
                    Ball.GetComponent<Ball>().SetSpeed(ThrowSpeed * Random.Range(1, maxSpeedMultiplier) * facing);
                    Ball.GetComponent<Ball>().setHit(false);
                }

                if (endlag && throwEndlag > 0)
                {
                    throwEndlag = throwEndlag - ThrowFrameRate;
                }

                //endlag frames
                if (endlag && throwEndlag <= 0)
                {
                    throwing = false;
                    startup = false;
                    endlag = false;
                    throwStartup = OGthrowStartup;
                    throwEndlag = OGthrowEndlag;
                    mySR.sprite = SpriteArray[0];

                    timer = Random.Range(0, maxTimer);
                }
            }

        }
    }

    public void HPDec(int i)
    {
        HP = HP - i;
        if(HP <= 0)
        {
            KOFlag = true;
        }
    }

}
