using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class scenerio
{
    private float RightBoundry;
    private float FrontBoundry;
    private float LeftBoundry;
    private float FrontLength;
    private float SideLength;
    private float front;
    private float left;
    private float right;
    private int pastaction;


    public scenerio(float Right_Boundry, float Front_Boundry, float Left_Boundry, float Front_Length, float Side_Length, int Pastaction)
    {
        RightBoundry = Right_Boundry;
        LeftBoundry = Left_Boundry;
        FrontBoundry = Front_Boundry;
        FrontLength = Front_Length;
        SideLength = Side_Length;
        pastaction = Pastaction;
    }

    public float[] getboundry()
    {
        return new float[3] { LeftBoundry, FrontBoundry, RightBoundry };
    }

    public float[] getlength()
    {
        return new float[2] {FrontLength,SideLength};
    }

    public float[] getactions()
    {
        return new float[3] { left, front, right };
    }

    public int getpastaction()
    {
        return pastaction;
    }

    public void setaction(float Left,float Front, float Right)
    {
        front = Front;
        left = Left;
        right = Right;
    }
    public void setfront(float value)
    {
        front = value;
    }
    public void setleft(float value)
    {
        left = value;
    }
    public void setright(float value)
    {
        right = value;
    }

    public void incrementFront(float value)
    {
        front += value;
    }
    public void incrementRight(float value)
    {
        right += value;
    }
    public void incrementLeft(float value)
    {
        left += value;
    }
    public void decrementFront(float value)
    {
        front = front - value;
    }
    public void decrementRight(float value)
    {
        right -= value;
    }
    public void decrementLeft(float value)
    {
        left -= value;
    }



}