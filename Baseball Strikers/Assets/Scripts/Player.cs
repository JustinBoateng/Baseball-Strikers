using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] BoxCollider2D HorLate;
    [SerializeField] BoxCollider2D HorJust;
    [SerializeField] BoxCollider2D HorEarly;
    [SerializeField] BoxCollider2D VerLate;
    [SerializeField] BoxCollider2D VerJust;
    [SerializeField] BoxCollider2D VerEarly;

    [SerializeField] GameObject HorHitbox;

    [SerializeField] bool HorSwing;
    [SerializeField] bool HorSwingStartup;
    [SerializeField] bool HorSwinging;
    [SerializeField] bool HorSwingEndlag;

    [SerializeField] float HorStartingSwingFrames;
    [SerializeField] float HorSwingingFrames;
    [SerializeField] float HorEndlagFrames;

    [SerializeField] GameObject VerHitbox;

    [SerializeField] bool VerSwing;
    [SerializeField] bool VerSwingStartup;
    [SerializeField] bool VerSwinging;
    [SerializeField] bool VerSwingEndlag;

    [SerializeField] float VerStartingSwingFrames;
    [SerializeField] float VerSwingingFrames;
    [SerializeField] float VerEndlagFrames;

    [SerializeField] bool SwingState;
    [SerializeField] bool BuntState;
    [SerializeField] GameObject BuntHitbox;


    [SerializeField] float PlayerFrameRate;
    [SerializeField] float BaseStartingSwingFrames;
    [SerializeField] float BaseSwingingFrames;
    [SerializeField] float BaseEndlagFrames;

    [SerializeField] private Sprite[] SpriteArray; //0 = idle, 1 = swinging, 2 = endlag, 3 = readjust

    SpriteRenderer mySR;

    private float HorAxis;
    [SerializeField] bool BuntSpriteSnap = false;

    // Start is called before the first frame update
    void Start()
    {
    

        HorHitbox.SetActive(false);
        VerHitbox.SetActive(false);
        BuntHitbox.SetActive(false);
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


        if (HorAxis == -1 && !SwingState)
        {
            BuntState = true;
        }

        else
        {
            BuntState = false;
        }


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
        if (!BuntState)
        {
            //Horizontal Swing
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

                    HorHitbox.SetActive(true);
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
        //Bunt
        {

            

        }
        //
    }
}
