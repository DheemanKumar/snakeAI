using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class q_learn : MonoBehaviour
{
    List<scenerio> Qtable = new List<scenerio>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void save_action(int[] scenerio, int[] action)
    {
        
        int[] data = new int[8] { scenerio[0], scenerio[1], scenerio[2], scenerio[3], scenerio[4], action[0], action[1], action[2] };
        //Qtable.Add(data);
    }

    public void update_action(int[] scenerio, int[] action)
    {
        int foundindex = Qtable.FindIndex(array => array.getboundry()[0] == scenerio[0] && array.getboundry()[1] == scenerio[1] && array.getboundry()[2] == scenerio[2] && array.getlength()[0] == scenerio[3] && array.getlength()[1] == scenerio[4]);
        Qtable[foundindex].getactions()[0] = action[0];
        Qtable[foundindex].getactions()[0] = action[1];
        Qtable[foundindex].getactions()[0] = action[2];
    }

    //public int[] get_action(int[] scenerio)
    //{
    //    int[] foundArray = Qtable.Find(array => array[0] == scenerio[0] && array[1] == scenerio[1] && array[2] == scenerio[2] && array[3] == scenerio[3] && array[4] == scenerio[4]);
    //    if (foundArray == null)
    //    {
    //        save_action(scenerio, new int[3] { 0, 0, 0 });
    //        return new int[3] { 0, 0, 0 };
    //    }
    //    else
    //    {
    //        //Debug.Log(foundArray[5] + " " + foundArray[6] + " " + foundArray[7]);

    //        return new int[3] { foundArray[5], foundArray[6], foundArray[7] };
    //    }
    //}

}
