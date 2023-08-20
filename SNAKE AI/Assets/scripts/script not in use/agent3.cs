using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using static UnityEngine.GraphicsBuffer;
using System.Reflection;

public class agent3 : MonoBehaviour
{
    [SerializeField] GameObject points;

    public bool auto;

    public int radius ;

    private float[] bd;
    private float[] sd;
    private int[] td;
    private Qtable qt;
    private snake_movement sm;

    public float delta = 10;

    

    // Start is called before the first frame update
    void Start()
    {
        qt = GetComponent<Qtable>();
        sm = GetComponent<snake_movement>();
        qt.loadtable();
        //constants.visual_radius = radius;
        radius = constants.visual_radius;
        qt.updatelock = false;
        auto = constants.auto;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            nextmove();
        }

        if (Input.GetKeyDown(KeyCode.Space)) auto = !auto;

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    qt.savetable();
        //}

        //if (Input.GetKeyDown(KeyCode.V)) {
        //    GetComponent<dijkstra>().showtable();
        //}

        

    }

    private void FixedUpdate()
    {
        if (auto)
            nextmove();
    }

    float find_distance()
    {
        GameObject point = points.transform.GetChild(0).gameObject;
        return Mathf.Sqrt(Mathf.Pow(transform.position.x - point.transform.position.x, 2) + Mathf.Pow(transform.position.y - point.transform.position.y, 2));
    }


    void nextmove()
    {
        if (!qt.updatelock)
        {
            //Debug.Log(GetComponent<boundry>().checkboundry(new Vector3(transform.position.x,transform.position.y,transform.position.z)));

            qt.updatelock = true;
            
            bd = GetComponent<boundry>().distance;
            sd = GetComponent<sidebody>().distance;
            td = GetComponent<target_distance>().distance;


            float[] actions;
            int index;

            //float old_distance = find_distance();


            float front, side;
            float ahead, left, right;
            ahead = Mathf.Min(bd[1], sd[1], radius);
            left = Mathf.Min(bd[0], sd[0], radius);
            right = Mathf.Min(bd[2], sd[2], radius); ;

            //Debug.Log(sd[0] + " " + sd[1] + " " + sd[2] + " " + bd[0] + " " + bd[1] + " " + bd[2]);


            //if (td[0] == td[1] || (td[0] == -td[1])) {

            //}

            //if (td[0] == td[1])
            //Debug.Log("td  (" + td[0] + "," + td[1] + ")");

            if (td[0] > radius) front = radius;
            else if (td[0] < -radius) front = -radius;
            else front = td[0];

            if (td[1] > radius) side = radius;
            else if (td[1] < -radius) side = -radius;
            else side = td[1];

            if (front == side)
            {
                //Debug.Log("same");
                if (front > 0)
                {
                    if (td[0] > td[1]) front++;
                    else if (td[1] > td[0]) side++;
                }
                else if (front < 0)
                {
                    if (td[0] < td[1]) front--;
                    else if (td[1] < td[0]) side--;
                }
            }

            else if (front == -side)
            {

                //Debug.Log("opposit");
                if (front > 0)
                {
                    if (td[0] > -td[1]) front++;
                    else if (td[0] < -td[1]) side--;
                }
                else if (front < 0)
                {
                    if (td[1] > -td[0]) side++;
                    else if (td[1] < -td[0]) front--;
                }
            }


            //Debug.Log(left+" "+ahead + " " + right);


            actions = qt.getaction(left, ahead, right, front, side);

            if (UnityEngine.Random.Range(0, 100f) > delta)
            {
                //actions = qt.getaction(bd[0], bd[1], bd[2], td[0], td[1]);
                //Debug.Log(actions[0]+" "+ actions[1]+" " + actions[2]);
                if (actions[0] == actions[1] && actions[0] == actions[2]) index = (int)UnityEngine.Random.Range(0, 3);
                else index = Array.IndexOf(actions, actions.Max());

                //Debug.Log("index " + index);
            }
            else
            {
                index = (int)UnityEngine.Random.Range(0, 3);
            }

//            Debug.Log(points.transform.GetChild(0));

            
            int a = GetComponent<dijkstra>().find(points.transform.GetChild(0));
            //Debug.Log(a);


            



            //Debug.Log();

            //float new_distance = find_distance();

            //float distance = old_distance - new_distance;
            //ebug.Log("distance "+distance);

            //if (a == index) Debug.Log("right");
            //else Debug.Log("wrong");
            
            if (a == index && actions[index] < 100) actions[index] += 1;
            else if (a != index && actions[index] > -10) { actions[index] -= 1; }

            //if (distance > 0)
            //    actions[index] = 1;

            //else if (distance < 0) { actions[index] = -5;

            //}
            ////Debug.Log("new actions   ( "+actions[0] + " " + actions[1] + " " + actions[2]);

            //if (points.GetComponent<points>().Point())
            //{
            //    actions[index] = 1;
            //}

            //if (ahead == 1 && index == 1) { actions[index] = -10;
            //    //Debug.Log("f strike     actions ("+ actions[0] + "," + actions[1] + "," + actions[2]+")");
            //}

            //if (left == 1 && index == 0)
            //{
            //    actions[index] = -10;
            //    //Debug.Log("l strike     actions (" + actions[0] + "," + actions[1] + "," + actions[2] + ")");
            //}
            //if (right == 1 && index == 2)
            //{
            //    actions[index] = -10;
            //    //Debug.Log("r strike     actions (" + actions[0] + "," + actions[1] + "," + actions[2] + ")");
            //}

            if (points.GetComponent<points>().Point() && actions[index] < 10)
            {
                actions[index] += 10;
            }

            if (ahead != 0)
            {
                qt.savescenerio(left, ahead, right, front, side, actions);

                //Debug.Log("   scenerio (" + left + "," + ahead + "," + right + "," + front + "," + side + ")  action ( " + a + "," + index + " )     actions (" + actions[0] + "," + actions[1] + "," + actions[2] + ")");


                //if (left == 4 && ahead == 4 && right == 4 && front <0 && side>=0 && a!=2)
                //{
                //    Debug.Log("mode -" + GetComponent<snake_movement>().mode + "   scenerio (" + left + "," + ahead + "," + right + "," + front + "," + side + ")    td (" + td[0] + "," + td[1] + ")  action ( " + a + "," + index + " )     actions (" + actions[0] + "," + actions[1] + "," + actions[2] + ")");
                //    qt.updatelock = true;
                //    GetComponent<dijkstra>().showtable();
                //}
                ////if (left == 4 && ahead == 4 && right < 4 && front > 0 && side < 0 && a != 0)
                ////{
                ////    Debug.Log("mode -" + GetComponent<snake_movement>().mode + "   scenerio (" + left + "," + ahead + "," + right + "," + front + "," + side + ")    td (" + td[0] + "," + td[1] + ")  action ( " + a + "," + index + " )     actions (" + actions[0] + "," + actions[1] + "," + actions[2] + ")");
                ////    qt.updatelock = true;
                ////    GetComponent<dijkstra>().showtable();
                ////}

                //if (actions[0] < -1 || actions[1] < -1 || actions[2] < -1)
                //{
                //    Debug.Log("mode -" + GetComponent<snake_movement>().mode + "   scenerio (" + left + "," + ahead + "," + right + "," + front + "," + side + ")    td (" + td[0] + "," + td[1] + ")  action ( " + a + "," + index + " )     actions (" + actions[0] + "," + actions[1] + "," + actions[2] + ")");
                //    qt.updatelock = true;
                //    GetComponent<dijkstra>().showtable();
                //}
            }
            //else Debug.Log("collide");
            //if (!qt.updatelock)
            //{
            //    Debug.Log("moved");

            //}
            //GetComponent<dijkstra>().showtable();
            //Debug.Log("new");
            //Debug.Log("action ( " + a + "," + index + " )");
            sm.move(a);

            qt.updatelock = checkcollide(a,index); ;
            
            
            
        }

    }

    bool checkcollide(int a, int index)
    {
        //Debug.Log(transform.GetChild(0).GetComponent<follow>().check(new Vector3(transform.position.x, transform.position.y, transform.position.z)));
        if (GetComponent<boundry>().checkboundry(new Vector3(transform.position.x, transform.position.y, transform.position.z)) && !transform.GetChild(0).GetComponent<follow>().check(new Vector3(transform.position.x, transform.position.y, transform.position.z)))
        {
            return false;
        }
        else
        {
            qt.savetable();
            //Debug.Log("action ( " + a + "," + index + " )");
            //if (a == index)
            //GetComponent<dijkstra>().showtable();


            GetComponent<restart>().restartlevel();
            return true;
        }
    }
}
