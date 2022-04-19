using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public static GameState GS;

    public int HitBalls = 0;
    public int[] BallHitType = new int[5];

    public Player player;
    [SerializeField] public float time;
    [SerializeField] public Text timer;
    [SerializeField] public float timerate;

    [SerializeField] public int Energy;
    [SerializeField] public Text EnergyMeter;

    /*
        HorJust
        HorEarly
        VerJust
        VerEarly
        Bunt         
         */


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
        time = time - timerate;
        timer.text = Mathf.Round(time).ToString();
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
