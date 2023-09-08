using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;

public class E_greedy : MonoBehaviour
{
    class state
    {
        public float Front { get; }
        public float Left { get; }
        public float Right { get; }

        public float Ahead { get; }
        public float Side { get; }

        public List<int> Actions=new List<int>();
        public List<float> Rewards=new List<float>();

        public state(float front, float left, float right, float ahead,float side)
        {
            Front = front;
            Left = left;
            Right = right;
            Ahead = ahead;
            Side = side;
        }

        public void AddActionReward(int action_index,float reward)
        {
            Actions.Add(action_index);
            Rewards.Add(reward);
        }

        public float GetQ(int action_index)
        {
            float ans = 0;
            float n = 0;

            for (int i = 0; i < Actions.Count; i++)
            {
                if (Actions[i] == action_index)
                {
                    ans += Rewards[i];
                    n += 1;
                }
            }

            return (ans / n);
        }
    }


    List<state> States;

    public GameObject player;
    public GameObject environment;
    public GameObject point;

    private boundry bd;
    private sidebody sd;
    private pointdiatance pd;
    private Astar star;

    int past, present;

    // Start is called before the first frame update
    void Start()
    {
        States = new List<state>();
        bd = player.GetComponent<boundry>();
        sd = player.GetComponent<sidebody>();
        pd = player.GetComponent<pointdiatance>();
        star = environment.GetComponent<Astar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //state s = new state(Mathf.Min(bd.distance[1], sd.distance[1]), Mathf.Min(bd.distance[0], sd.distance[0]), Mathf.Min(bd.distance[2], sd.distance[2]));
            //updatStates(s, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log(States.Count);
            state s = States[States.Count - 1];
            Debug.Log(s.Front+" "+s.Left+" "+s.Right+" "+s.Actions.Count+" "+s.Rewards.Count);
        }

        if (Input.GetKey(KeyCode.Z))
        {
            past=star.findpath();
            state s = new state(Mathf.Min(bd.distance[1], sd.distance[1]), Mathf.Min(bd.distance[0], sd.distance[0]), Mathf.Min(bd.distance[2], sd.distance[2]), pd.distance[0], pd.distance[1]);
            //Debug.Log(s.GetQ(0) + " " + s.GetQ(1) + " " + s.GetQ(2));
            float[] q = getQ(s);
            Debug.Log(q[0] + " " + q[1] + " " + q[2]);
            int act = FindIndexOfMax(q);
            player.GetComponent<snake_movement>().move(act);
            present = star.findpath();
            int reward = past - present;

            if (point.GetComponent<points>().Point()) reward = 1;

            if (sd.distance[0] == 0 || sd.distance[1] == 0 || sd.distance[2] == 0) reward = -1;

            updatStates(s, act, reward);
            Debug.Log(act + "     (" + past + "  " + present + ")    " + (reward));
        }
    }

    float[] getQ(state state)
    {
        int index = States.FindIndex(s => s.Front == state.Front && s.Left == state.Left && s.Right == state.Right && s.Ahead==state.Ahead && s.Side==state.Side);

        //Debug.Log(index);

        if (index == -1)
            return new float[3] { 0, 0, 0 };
        else
        {
            float a = States[index].GetQ(0);
            if (float.IsNaN(a)) a = 0;

            float b = States[index].GetQ(1);
            if (float.IsNaN(b)) b = 0;

            float c = States[index].GetQ(2);
            if (float.IsNaN(c)) c = 0;

            return new float[3] { a, b, c };
        }
    }


    void updatStates(state state ,int action_index,float reward)
    {
        if(!States.Exists(s => s.Front == state.Front && s.Left == state.Left && s.Right == state.Right && s.Ahead == state.Ahead && s.Side == state.Side))
        {
            States.Add(new state(state.Front,state.Left,state.Right,state.Ahead,state.Side));
        }

        int index = States.FindIndex(s => s.Front == state.Front && s.Left == state.Left && s.Right == state.Right && s.Ahead == state.Ahead && s.Side == state.Side);

        States[index].AddActionReward(action_index, reward);
    }



    private int FindIndexOfMax(float[] array)
    {
        if (array.Length == 0)
        {
            // Handle the case where the array is empty
            return -1; // or throw an exception, depending on your requirements
        }

        int maxIndex = 0; // Assume the first element is the maximum

        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] > array[maxIndex])
            {
                // Update maxIndex if a larger element is found
                maxIndex = i;
            }
        }

        return maxIndex;
    }
}
