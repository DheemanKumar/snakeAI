using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qtable : MonoBehaviour
{
    public bool updatelock;

    List<scenerio> table;
    List<scenerio> actions;
    List<int> actionindex;

    public int presentstate;

    private void Awake()
    {
        table = new List<scenerio>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        actions = new List<scenerio>();
        actionindex = new List<int>();
    }

    // Update is called once per frame
    


    /* compare the given scenerio with all other scenerio and update actions as per the existing one*/
    private float[] createActions(scenerio scenerio)
    {
        float[] action = new float[3] { 0, 0, 0 };
        //float[] marks =new float[3] {0,0,0 };
        int total = 0;

        for (int i = 0; i < table.Count; i++)
        {
            float points = 0;
            scenerio scene = table[i];
            //if (scenerio.getboundry()[0] == scene.getboundry()[0]) points += 0.2f;
            //if (scenerio.getboundry()[1] == scene.getboundry()[1]) points += 0.2f;
            //if (scenerio.getboundry()[2] == scene.getboundry()[2]) points += 0.2f;
            if (scenerio.getlength()[0] == scene.getlength()[0]) points += 0.5f;
            if (scenerio.getlength()[1] == scene.getlength()[1]) points += 0.5f;
            //if (scenerio.getpastaction() == scene.getpastaction()) points += 0.2f;
            if (points >= 1 ) {
                total++;
                action[0] = action[0] + (scene.getactions()[0] * points);
                action[1] = action[1] + (scene.getactions()[1] * points);
                action[2] = action[2] + (scene.getactions()[2] * points);
                Debug.Log("points " + points +"  total "+total+ "  actions    (" + scene.getactions()[0] + "  " + scene.getactions()[1] + "  " + scene.getactions()[2]+")               (" + action[0] + "  " + action[1] + "  " + action[2]+")");
            }
            
        }
        if (total > 0)
        {
            action[0] = action[0] / total;
            action[1] = action[1] / total;
            action[2] = action[2] / total;
        }

        Debug.Log("new created actions "+action[0] + " "+ action[1] + " "+ action[2]);

        return action;
    }

    /*you enter the variables of scenerio and it returns a array of actions*/
    public float[] getaction(float Left_Boundry, float Front_Boundry, float Right_Boundry, float Front_Length, float Side_Length,int Past_action=0)
    {
        scenerio scenerio = new scenerio(Right_Boundry, Front_Boundry, Left_Boundry, Front_Length, Side_Length,Past_action);
        int index = find_scenerio_table(scenerio);
        float[] action;
        if (index != -1)
        {
            //Debug.Log("found");
            action=table[index].getactions();
        }
        else
        {
            //action = createActions(scenerio);
            action = new float[3] { 0, 0, 0 };

        }

        //Debug.Log("scenerio ( " + scenerio.getboundry()[0] +","+ scenerio.getboundry()[1] + "," + scenerio.getboundry()[2] + "," + scenerio.getlength()[0] + "," + scenerio.getlength()[1]+" )   actions ( " + action[0] + "," + action[1] + "," + action[2] + " )");

        return action;
    }

    /*you enter the variables of scenerio and actions and it saves them*/
    public void setaction(float Left_Boundry, float Front_Boundry, float Right_Boundry, float Front_Length, float Side_Length, int Past_action, float Left, float Front, float Right)
    {
        scenerio scenerio = new scenerio(Right_Boundry, Front_Boundry, Left_Boundry, Front_Length, Side_Length,Past_action);
        int index = find_scenerio_table(scenerio);

        if (index != -1)
        {
            scenerio.setaction(Left,Front,Right);
        }
    }

    public void savescenerio(float Left_Boundry, float Front_Boundry, float Right_Boundry, float Front_Length, float Side_Length, float[] actions)
    {
        scenerio scenerio = new scenerio(Right_Boundry, Front_Boundry, Left_Boundry, Front_Length, Side_Length,0);
        int index = find_scenerio_table(scenerio);

        if (index == -1)
        {
            scenerio.setaction(actions[0], actions[1], actions[2]);
            table.Add(scenerio);
            //Debug.Log("scenerio not found");
        }
        else
        {
            table[index].setaction(actions[0], actions[1], actions[2]);
        }
    }

    

    public bool savescenerio(float Left_Boundry, float Front_Boundry, float Right_Boundry, float Front_Length, float Side_Length, int Past_action, int Actionindex)
    {
        scenerio scenerio = new scenerio(Right_Boundry, Front_Boundry, Left_Boundry, Front_Length, Side_Length,Past_action);

        //Debug.Log("check " + (scenerio.getboundry()[0] == scenerio2.getboundry()[0]));

        int index = find_scenerio_table(scenerio);

        Debug.Log("index  " + index);

        if (index == -1)
        {
            float[] action = createActions(scenerio);
            scenerio.setaction(action[0], action[1], action[2]);
            table.Add(scenerio);
            //Debug.LogError("scenerio not found");
        }
        else
        {
            scenerio.setaction(table[index].getactions()[0], table[index].getactions()[1], table[index].getactions()[2]);
        }
        

        int exist = find_scenerio_actions(scenerio);

        if (exist == -1)
        {

            //Debug.Log("store");
            actions.Add(scenerio);
            actionindex.Add(Actionindex);
            

            return false;
        }
        else return true;

    }

    
    private int find_scenerio_table (scenerio scenerio)
    {
        int index=-1;
        //Debug.Log(table);

        for (int i=0;i<table.Count;i++){
            scenerio scene = table[i];
            if (scenerio.getboundry()[0]==scene.getboundry()[0] && scenerio.getboundry()[1] == scene.getboundry()[1] && scenerio.getboundry()[2] == scene.getboundry()[2] && scenerio.getlength()[0] == scene.getlength()[0] && scenerio.getlength()[1] == scene.getlength()[1])
            {
                index = i;
                break;
            }
        }

        presentstate = index;

        return index;
    }

    private int find_scenerio_actions(scenerio scenerio)
    {
        int index = -1;

        for (int i = 0; i < actions.Count; i++)
        {
            scenerio scene = actions[i];
            if (scenerio.getboundry()[0] == scene.getboundry()[0] && scenerio.getboundry()[1] == scene.getboundry()[1] && scenerio.getboundry()[2] == scene.getboundry()[2] && scenerio.getlength()[0] == scene.getlength()[0] && scenerio.getlength()[1] == scene.getlength()[1])
            {
                //Debug.Log("new "+scenerio.getboundry()[0] + " " + scenerio.getboundry()[1] + " " + scenerio.getboundry()[2]+" "+scenerio.getlength()[0]+ " " + scenerio.getlength()[1]);
                //Debug.Log("old "+scene.getboundry()[0] + " " + scene.getboundry()[1] + " " + scene.getboundry()[2] + " " + scene.getlength()[0] + " " + scene.getlength()[1]);
                index = i;
                break;
            }
        }

        return index;
    }

    public void savetable()
    {
        GetComponent<save_load>().SaveData(table);
    }

    public void loadtable()
    {
        //Debug.Log("start");
        table = (List<scenerio>)GetComponent<save_load>().LoadData();
        if (table == null)
        {
            table = new List<scenerio>();
            constants.generation = 0;
        }
        else
        {
            //for (int i = 0; i < table.Count; i++)
            //{
            //    Debug.Log("past table  " + i + " " + table[i].getactions()[0] + "  " + table[i].getactions()[1] + "  " + table[i].getactions()[2]);
            //}
            //Debug.Log("total states " + table.Count);
        }

    }

    public void showtable()
    {
        if (table != null)
        {
            for (int i = 0; i < table.Count; i++)
            {
                Debug.Log("past table  " + i + " " + table[i].getactions()[0] + "  " + table[i].getactions()[1] + "  " + table[i].getactions()[2]);
            }
            //Debug.Log("total states " + table.Count);
        }
    }

    

    public void update_Actions(float points,float steps=-1 ,bool increment=true,bool reset=false)
    {
        //Debug.Log(points);
        if (points != 0)
        {

            if (points < 0) points *= -1;
            if (steps == -1) steps = actions.Count;
            //Debug.Log("restart " + actions.Count);

            float dec = points / steps;


            if (increment)
            {

                for (int i = actions.Count - 1; i >= 0 && steps > 0; i--, steps--, points -= dec)
                {
                    //Debug.Log(actionindex[i] + " before " + find_scenerio_table(actions[i]) + " " + table[find_scenerio_table(actions[i])].getactions()[0] + " " + table[find_scenerio_table(actions[i])].getactions()[1] + " " + table[find_scenerio_table(actions[i])].getactions()[2]);

                    switch (actionindex[i])
                    {
                        case 0:
                            actions[i].incrementLeft(points);
                            table[find_scenerio_table(actions[i])].setleft(actions[i].getactions()[0]);
                            break;
                        case 1:
                            actions[i].incrementFront(points);
                            table[find_scenerio_table(actions[i])].setfront(actions[i].getactions()[1]);
                            break;
                        case 2:
                            actions[i].incrementRight(points);
                            table[find_scenerio_table(actions[i])].setfront(actions[i].getactions()[2]);
                            break;
                        default:
                            Debug.Log("error 1   index is " + actionindex[i]);
                            break;
                    }

                    //table[find_scenerio_table(actions[i])].setaction(actions[i].getactions()[0], actions[i].getactions()[1], actions[i].getactions()[2]);
                    //Debug.Log(actionindex[i] + " after " + find_scenerio_table(actions[i]) + " " + table[find_scenerio_table(actions[i])].getactions()[0] + " " + table[find_scenerio_table(actions[i])].getactions()[1] + " " + table[find_scenerio_table(actions[i])].getactions()[2]);
                }
            }
            else
            {
                for (int i = actions.Count - 1; i >= 0 && steps > 0; i--, steps--, points -= dec)
                {
                    //Debug.Log(actionindex[i] + " before "+find_scenerio_table(actions[i]) + " " + table[find_scenerio_table(actions[i])].getactions()[0] + " " + table[find_scenerio_table(actions[i])].getactions()[1] + " " + table[find_scenerio_table(actions[i])].getactions()[2]);

                    switch (actionindex[i])
                    {
                        case 0:
                            actions[i].decrementLeft(points);
                            //Debug.Log(actions[i].getactions()[0]);
                            table[find_scenerio_table(actions[i])].setleft(actions[i].getactions()[0]);
                            //Debug.Log(table[find_scenerio_table(actions[i])].getactions()[0]);
                            break;
                        case 1:
                            actions[i].decrementFront(points);
                            //Debug.Log(actions[i].getactions()[1]);
                            table[find_scenerio_table(actions[i])].setfront(actions[i].getactions()[1]);
                            //Debug.Log(table[find_scenerio_table(actions[i])].getactions()[1]);
                            break;
                        case 2:
                            actions[i].decrementRight(points);
                            //Debug.Log(actions[i].getactions()[2]);
                            table[find_scenerio_table(actions[i])].setright(actions[i].getactions()[2]);
                            //Debug.Log(table[find_scenerio_table(actions[i])].getactions()[2]);
                            break;
                    }

                    //table[find_scenerio_table(actions[i])].setaction(actions[i].getactions()[0], actions[i].getactions()[1], actions[i].getactions()[2]);
                    //Debug.Log(actionindex[i]+" after " + find_scenerio_table(actions[i]) + " " + table[find_scenerio_table(actions[i])].getactions()[0] + " " + table[find_scenerio_table(actions[i])].getactions()[1] + " " + table[find_scenerio_table(actions[i])].getactions()[2]);

                }
            }
        }
        if (reset) {
            GetComponent<agent2>().enabled = false;
            GetComponent<restart>().restartlevel();
        }
        savetable();
    }

    

}

