using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GameState : MonoBehaviour
{
    public static GameState GS;

    public int HitBalls = 0;
    public int[] BallHitType = new int[5];
    /*
    HorJust
    HorEarly
    VerJust
    VerEarly
    Bunt         
     */

    public Player player;
    [SerializeField] public float time;
    [SerializeField] public Text timer;
    [SerializeField] public float timerate;

    [SerializeField] public int Energy;
    [SerializeField] public Text EnergyMeter;

    public GameObject GameUI;
    public GameObject PauseUI;
    public GameObject DefaultButton;
    public bool PauseFlag = false;
    public bool FreezeFlag = false;
    //FreezeFlag indicates when the game wants to stop in place for a sec.
    //similar to Pause flag, but isn't based around the player pausing the game

    //keeping the same GameState
    private void Awake()
    {

        if (GS == null)
        {
            DontDestroyOnLoad(gameObject);
            GS = this;
        }

        else if (GS != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        timer.text = time.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PauseFlag)
            {
                PauseGame();               
            }

            else
            {
                ResumeGame();
            }
        }

        time = time - timerate;
        timer.text = Mathf.Round(time).ToString();


    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        PauseFlag = true;
        PauseUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(DefaultButton);
        Debug.Log("Paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PauseFlag = false;
        PauseUI.SetActive(false);
        Debug.Log("Resumed");
    }

    public void GameSpeed(float f)
    {
        Time.timeScale = f;
    }


    public void BallHit()
    {
        HitBalls++;
    }

    public void ballreset()
    {
        HitBalls = 0;
    }

    public void HitTypeInc(int i)
    {
        BallHitType[i]++;
    }

    public void PlayerCancel()
    {
        player.Reset();
    }

    public void MeterGain(int i)
    {
        Energy = Energy + i;

        if (Energy > 100) Energy = 100;

        EnergyMeter.text = Energy.ToString();
    }

    public int MeterCheck()
    {
        return Energy;
    }
}
