using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class agent : MonoBehaviour
{
    private float[] bd;
    private int[] td;
    private snake_movement sm;
    private q_learn ql;
    public float delta;
    public int[] action;
    public points po;

    int[] scenerio;
    int[] past_scenerio;
    int[] past_action;
    int pastindex;


    // Start is called before the first frame update
    void Start()
    {
        
        sm = GetComponent<snake_movement>();
        ql = GetComponent<q_learn>();
        bd = GetComponent<boundry>().distance;
        td = GetComponent<target_distance>().distance;
        past_scenerio = new int[5] { 0,0,0,0,0};
        past_action = new int[3] { 0, 0, 0 };
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //if (Input.GetKeyDown(KeyCode.Space)) ixedUpdate();
    //}

    public void FixedUpdate()
    {
        bd = GetComponent<boundry>().distance;
        td = GetComponent<target_distance>().distance;
        scenerio = new int[5] { (int)bd[0], (int)bd[1], (int)bd[2], td[0], td[1] };

        //(int) bd[0], (int)bd[1], (int)bd[2], td[0], td[1] 

        //action = ql.get_action(scenerio);

        //Debug.Log(past_action[pastindex] + "    " + action.Max());
        if ((past_action[pastindex] < action.Max()) && action.Max()>0 )
        {
            past_action[pastindex] = action.Max() - 1;
            //Debug.Log("up  "+past_action[pastindex]);
            ql.update_action(past_scenerio, past_action);
        }
        if ((past_action[pastindex] > action.Min()) && action.Min() < 0)
        {
            past_action[pastindex] = action.Min() + 1;
            //Debug.Log("up  "+past_action[pastindex]);
            ql.update_action(past_scenerio, past_action);
        }

        int index;
//        Debug.Log(action);

        // find index of for choose
        if (UnityEngine.Random.Range(0, 100f) > delta)
        {
            //            Debug.Log("if");
            if (action.Max() == 0)
            {
                index = (int)UnityEngine.Random.Range(0, 3);
                
            }
            else
            {
                index = Array.IndexOf(action, action.Max());
                
            }
            

        }
        else
        {
            index = (int)UnityEngine.Random.Range(0, 3);
           
        }

        //Debug.Log((int)bd[0]);

        

        //index = 1;

        // give a reword of hundred if it collide with cherry (reach right destination)
        if (po.Point())
        {
            action[index] = 100;
            //Debug.Log(td[0]);
            ql.update_action(scenerio, action);
        }
        if (bd[1] == 0 )
        {
            action[index] = -100;
            //Debug.Log(bd[1]);
            ql.update_action(scenerio, action);
        }

        sm.move(index);

        Debug.Log(action[index]);

        pastindex = index;
        past_scenerio = scenerio;
        past_action = action;

    }

}
