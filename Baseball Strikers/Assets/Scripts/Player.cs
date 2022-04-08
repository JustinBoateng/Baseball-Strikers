using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] BoxCollider2D Late;
    [SerializeField] BoxCollider2D Just;
    [SerializeField] BoxCollider2D Early;
    [SerializeField] BoxCollider2D VerticalLate;
    [SerializeField] BoxCollider2D VerticalJust;
    [SerializeField] BoxCollider2D VerticalEarly;

    [SerializeField] GameObject HorizontalHitbox;
    [SerializeField] GameObject VerticalHitbox;

    [SerializeField] bool Swing;
    [SerializeField] bool SwingStartup;
    [SerializeField] bool Swinging;
    [SerializeField] bool SwingEndlag;

    [SerializeField] float StartingSwingFrames;
    [SerializeField] float SwingingFrames;
    [SerializeField] float EndlagFrames;
    [SerializeField] float PlayerFrameRate;

    [SerializeField] float BaseStartingSwingFrames;
    [SerializeField] float BaseSwingingFrames;
    [SerializeField] float BaseEndlagFrames;

    [SerializeField] private Sprite[] SpriteArray; //0 = idle, 1 = swinging, 2 = endlag, 3 = readjust

    SpriteRenderer mySR;

    // Start is called before the first frame update
    void Start()
    {
        HorizontalHitbox.SetActive(false);
        VerticalHitbox.SetActive(false);
        Swinging = false;

        mySR = GetComponent<SpriteRenderer>();
        mySR.sprite = SpriteArray[0];

        EndlagFrames = BaseEndlagFrames;
        SwingingFrames = BaseSwingingFrames;
        StartingSwingFrames = BaseStartingSwingFrames;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && !Swing)
        {
            Swing = true;
        }

        //swing Startup
        if (Swing && !SwingStartup)
        {
            Swing = false;
            SwingStartup = true;
            mySR.sprite = SpriteArray[1];

        }

        if (SwingStartup && StartingSwingFrames > 0)
        {
            StartingSwingFrames = StartingSwingFrames - PlayerFrameRate;
        }


        //Swinging Frames
        if (SwingStartup && StartingSwingFrames <= 0 && !Swinging)
        {           
            SwingStartup = false;
            Swinging = true;
            mySR.sprite = SpriteArray[2];

            HorizontalHitbox.SetActive(true);
        }

        if(Swinging && SwingingFrames > 0)
        {
            SwingingFrames = SwingingFrames - PlayerFrameRate;
        }

        //swing endlag
        if (Swinging && SwingingFrames <= 0 && !SwingEndlag)
        {
            Swinging = false;
            SwingEndlag = true;
            HorizontalHitbox.SetActive(false);
            mySR.sprite = SpriteArray[3];
        }

        if(SwingEndlag && EndlagFrames > 0)
        {
            EndlagFrames = EndlagFrames - PlayerFrameRate;
        }

        if(SwingEndlag && EndlagFrames <= 0)
        {

            SwingEndlag = false;
            Swing = false;

            SwingStartup = false;
            Swinging = false;

            EndlagFrames = BaseEndlagFrames;
            SwingingFrames = BaseSwingingFrames;
            StartingSwingFrames = BaseStartingSwingFrames;
            mySR.sprite = SpriteArray[0];
        }
    }
}
