using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class agent2 : MonoBehaviour
{
    private float[] bd;
    private int[] td;
    private Qtable qt;
    private snake_movement sm;
    public points po;

    public float delta=10;

    public bool auto = true;

    public int steps;
    public int maxsteps = 100;

    private int repeats;
    private int points;
    private bool fixedauto;
    private int past_action;



    // Start is called before the first frame update
    void Start()
    {
        qt = GetComponent<Qtable>();
        sm = GetComponent<snake_movement>();
        qt.loadtable();
        //Debug.Log("ag");
        steps = 0;
        repeats = 0;
        points = 0;
        past_action = 0;
        fixedauto = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextmove();
        }
        if (Input.GetKey(KeyCode.N))
        {
            nextmove();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            auto = !auto;
        }
        fixedauto = auto;

        if (Input.GetKeyDown(KeyCode.S))
        {
            qt.showtable();
        }
    }

    public void FixedUpdate()
    {
        if (fixedauto) { nextmove(); }
    }

    void nextmove()
    {
        bd = GetComponent<boundry>().distance;
        td = GetComponent<target_distance>().distance;
        

        float[] actions;
        int index;


        if (UnityEngine.Random.Range(0, 100f) > delta)
        {
            actions = qt.getaction(bd[0], bd[1], bd[2], td[0], td[1],past_action);
            index = Array.IndexOf(actions, actions.Max());
            
            //Debug.Log("index " + index);
        }
        else
        {
            index = (int)UnityEngine.Random.Range(0, 3);
        }
        

        sm.move(index);

        bd = GetComponent<boundry>().distance;
        td = GetComponent<target_distance>().distance;
        //Debug.Log("bd1 " + bd[1]);

        bool repeat = qt.savescenerio(bd[0], bd[1], bd[2], td[0], td[1],past_action, index);
        ////Debug.Log("repeat " + repeat);
        if (repeat)
        {
            //Debug.Log("repeat");
            qt.update_Actions(100,5, false,false);
            repeats++;
        }
        if (po.Point())
        {
            qt.update_Actions(1000, -1);
            points++;
        }
        if (bd[1] == 0)
        {
            Debug.Log("boundry  &  repeats " + repeats+" points "+points);
            qt.update_Actions(300,-1, false,true);
        }

        if (steps >= maxsteps)
        {
            Debug.Log("repeats "+repeats + " points " + points);
            qt.update_Actions(0,-1, false, true);

        }
        past_action = index;
        steps++;
    }


}
