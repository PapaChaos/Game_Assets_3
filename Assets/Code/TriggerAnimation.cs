using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{

    public bool play = false;
    public bool playing = false;

    public Animation moveShip;
    public Animation openDoors;


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
     
        if (play == true && playing == false)
        {
            playing = true;
            play = false;
            moveShip.Play();
            openDoors.Play();

        }

    }
}
