using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private string SwingType;
    [SerializeField] private string SwingTiming;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getType()
    {
        return SwingType;
    }

    public string getTiming()
    {
        return SwingTiming;
    }

}
