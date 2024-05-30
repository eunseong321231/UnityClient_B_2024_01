using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public float timeRemaining;
    public void Set(float time)
    {
        timeRemaining = time;
    }

    public void Update(float deltaTime)
    {
        if(timeRemaining > 0)
        {
            timeRemaining -= deltaTime;
        }
    }

    public bool IsFinished()
    {
        return timeRemaining <= 0;
    }
}

