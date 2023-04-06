using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
	public float timeRemaining = 10;
	public bool timerIsRunning = false;
	
    // Start is called before the first frame update
    void Start()
    {
	    // Starts the timer automatically
	    timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
	    if (timerIsRunning)
	    {
		    if (timeRemaining > 0)
		    {
			    timeRemaining -= Time.deltaTime;
		    }
		    else
		    {
			    Debug.Log("Time has run out!");
			    timeRemaining = 0;
			    timerIsRunning = false;
		    }
	    }
    }
}
