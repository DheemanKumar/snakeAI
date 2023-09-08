using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Astar : MonoBehaviour
{
    public Vector2 Maxrange;
    public Vector2 Minrange;
    float[,] grid;

    public boundry bd;
    public Transform head;
    public Transform point;

    public GameObject dot;

    public int it;


    class cordinate
    {
        public int X { get; }
        public int Y { get; }
        public float Value { get; }
        public float Avalue;
        public cordinate Parent;

        public cordinate(int x, int y, float value ,float avalue, cordinate parent)
        {
            
            X = x;
            Y = y;
            Value = value;
            Parent = parent;
            Avalue = avalue;
        }
    }


    private List<cordinate>visited = new List<cordinate>();
    private List<cordinate> next = new List<cordinate>();




    // Start is called before the first frame update
    void Start()
    {
        grid = new float[(int)(Maxrange.x-Minrange.x), (int)(Maxrange.y-Minrange.y)];
        it = 50;

        findpath();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            map();
            drawgrid();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            findpath();
            //Debug.Log($"First: {side.X}, Second: {side.Y} , Value: {side.Value}");
        }
    }

    void map()
    {
        int x, y;
        x =(int) Minrange.x;
        y = (int)Minrange.y;

        for (int i=0; i < (int)(Maxrange.x - Minrange.x); i++)
        {
            for (int j = 0; j < (int)(Maxrange.y - Minrange.y); j++)
            {
                if ((head.position.x == x + i && head.position.y == y + j) ||
                    (head.GetChild(0).GetComponent<follow>().check(new Vector3(x+i,y+j,0)))||
                    (bd.boundry_x.x<=x+i) || (bd.boundry_x.y > x + i) || (bd.boundry_y.x <= y + j) || (bd.boundry_y.y > j + y))
                    grid[i, j] = float.MaxValue;
                else grid[i, j] = Vector3.Distance(point.GetChild(0).position,new Vector3(x+i,y+j));

                //Debug.Log(i + "  " + j + "    " + grid[i, j]);
            }
        }

        //for (int i = (int)(Maxrange.y - Minrange.y)-1; i >=0; i--)
        //{
        //    string ans = "[";
        //    for (int j = 0; j < (int)(Maxrange.x - Minrange.x); j++)
        //    {
        //        ans += (grid[j, i] + ", ");
        //    }
        //    ans += "]";
        //    Debug.Log(ans);
        //}

    }

    public int findpath()
    {
        map();
        //Debug.Log(head.position.x + " " + head.position.y);

        int x, y;
        x = (int)(Maxrange.x+ head.position.x);
        y = (int)(Maxrange.y+ head.position.y);

        cordinate c = new cordinate(x, y, grid[x, y],1,null);
        next.Add(c);


        //Debug.Log("       " + grid[x, y + 1] + "       ");
        //Debug.Log(grid[x - 1, y] + "    " + grid[x + 1, y]);
        //Debug.Log("       " + grid[x, y - 1] + "       ");

        //Debug.Log("---------------------------------------------------------------------");

        for (int i = 0; i < 1000; i++)
        {
            //Debug.Log(next[0].X + " " + next[0].Y);

            c = next[0];
            next.Remove(c);

            if (c.Value == 0) break;

            

            if (visited.Exists(cor => cor.X == c.X && cor.Y == c.Y + 1))
            {
                //Debug.Log("update");
                int index = visited.FindIndex(pair => pair.X == c.X && pair.Y == c.Y + 1);
                //Debug.Log((visited[index].Parent).Value);
                //if((visited[index].Parent) !=null)
                //Debug.Log(c.Avalue + " " + visited[index].Avalue + "  " + (c.Avalue > visited[index].Avalue) + "  " + (visited[index].Parent).X+" "+ (visited[index].Parent).Y);
                if (c.Avalue < visited[index].Avalue)
                    visited[index].Parent = c;
                //if ((visited[index].Parent) != null)
                //    Debug.Log(c.Avalue + " " + visited[index].Avalue + "  " + (c.Avalue > visited[index].Avalue) + "  " + (visited[index].Parent).X + " " + (visited[index].Parent).Y);
            }
            else
            {
                //Debug.Log("create");
                cordinate co = new cordinate(c.X, c.Y + 1, grid[c.X, c.Y + 1], c.Avalue + 1, c);
                next.Add(co);

            }

            //Debug.Log("next_len " + next.Count);


            if (visited.Exists(cor => cor.X == c.X + 1 && cor.Y == c.Y))
            {
                //Debug.Log("update");
                int index = visited.FindIndex(pair => pair.X == c.X + 1 && pair.Y == c.Y);
                // Debug.Log(index);
                //if ((visited[index].Parent) != null)
                //    Debug.Log(c.Avalue + " " + visited[index].Avalue + "  " + (c.Avalue > visited[index].Avalue) + "  " + (visited[index].Parent).X + " " + (visited[index].Parent).Y);

                if (c.Avalue < visited[index].Avalue)
                    visited[index].Parent = c;
                //if ((visited[index].Parent) != null)
                //    Debug.Log(c.Avalue + " " + visited[index].Avalue + "  " + (c.Avalue > visited[index].Avalue) + "  " + (visited[index].Parent).X + " " + (visited[index].Parent).Y);
            }
            else
            {
                //Debug.Log("create");
                next.Add(new cordinate(c.X + 1, c.Y, grid[c.X + 1, c.Y], c.Avalue + 1, c));
              
            }


            //Debug.Log("next_len " + next.Count);


            if (visited.Exists(cor => cor.X == c.X - 1 && cor.Y == c.Y))
            {
                //Debug.Log("update");
                int index = visited.FindIndex(pair => pair.X == c.X - 1 && pair.Y == c.Y);
                //Debug.Log(index);
                //if ((visited[index].Parent) != null)
                //    Debug.Log(c.Avalue + " " + visited[index].Avalue + "  " + (c.Avalue > visited[index].Avalue) + "  " + (visited[index].Parent).X + " " + (visited[index].Parent).Y);

                if (c.Avalue < visited[index].Avalue)
                    visited[index].Parent = c;
                //if ((visited[index].Parent) != null)
                //    Debug.Log(c.Avalue + " " + visited[index].Avalue + "  " + (c.Avalue > visited[index].Avalue) + "  " + (visited[index].Parent).X + " " + (visited[index].Parent).Y);
            }
            else
            {
                //Debug.Log("create");
                next.Add(new cordinate(c.X - 1, c.Y, grid[c.X - 1, c.Y], c.Avalue + 1, c));
               
            }

            //Debug.Log("next_len " + next.Count);


            if (visited.Exists(cor => cor.X == c.X && cor.Y == c.Y - 1))
            {
                //Debug.Log("update");
                int index = visited.FindIndex(pair => pair.X == c.X && pair.Y == c.Y - 1);
                //Debug.Log(index);
                //if ((visited[index].Parent) != null)
                //    Debug.Log(c.Avalue + " " + visited[index].Avalue + "  " + (c.Avalue > visited[index].Avalue) + "  " + (visited[index].Parent).X + " " + (visited[index].Parent).Y);

                if (c.Avalue < visited[index].Avalue)
                    visited[index].Parent = c;
                //if ((visited[index].Parent) != null)
                //    Debug.Log(c.Avalue + " " + visited[index].Avalue + "  " + (c.Avalue > visited[index].Avalue) + "  " + (visited[index].Parent).X + " " + (visited[index].Parent).Y);
            }
            else
            {
                //Debug.Log("create");
                next.Add(new cordinate(c.X, c.Y - 1, grid[c.X, c.Y - 1], c.Avalue + 1, c));
              
            }



            next.Sort((a, b) => a.Value.CompareTo(b.Value));

            visited.Add(c);

            //Debug.Log("next_len " + next.Count + "  visited_len " + visited.Count);



            //Debug.Log("----------------------------------------");

            //foreach(var v in visited)
            //{
            //    if (v.Parent == null)
            //    {
            //        Debug.Log(v.X + " " + v.Y + "   " + v.Value + "   " + v.Avalue + "   NA" );
            //    }
            //    else
            //    Debug.Log(v.X + " " + v.Y + "   " + v.Value + "   " + v.Avalue + "  " + v.Parent.X + " " + v.Parent.Y);
            //}

            //Debug.Log("----------------------------------------");


            //Debug.Log("---------------------------------------------------------------------");



        }

        //Debug.Log("final "+c.Parent.X + "  " + c.Parent.Y);


       


        //while (transform.childCount > 0)
        //{
        //    DestroyImmediate(transform.GetChild(0).gameObject);
        //}
        drawpath(c);

        int ans=(findcost(c));

        //foreach(var side in path)
        //{
        //    GameObject p = Instantiate(dot, transform);
        //    p.transform.position = new Vector2(side.X-Maxrange.x, side.Y-Maxrange.y);
        //    p.GetComponent<SpriteRenderer>().color = new Vector4(0, 1, 0, 1);
        //}



        //cordinate last = path[path.Count - 1];

        visited.Clear();
        next.Clear();

        return ans;
    }

    void drawpath(cordinate c)
    {

        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        drawpathhelp(c);
    }

    int findcost(cordinate c)
    {
        int cost=0;

        if (c.Parent == null)
        {
            return 1;
        }
        else
            cost=findcost(c.Parent);

        return cost + 1;
    }

    void drawpathhelp(cordinate c)
    {

        //Debug.Log("final " + c.X + "  " + c.Y);
        GameObject p = Instantiate(dot, transform);
        p.transform.position = new Vector2(c.X - Maxrange.x, c.Y - Maxrange.y);
        p.GetComponent<SpriteRenderer>().color = new Vector4(0, 1, 0, 1);
        if(c.Parent != null)
            drawpathhelp(c.Parent);
    }





    void drawgrid()
    {
        int x, y;
        x = (int)Minrange.x;
        y = (int)Minrange.y;

        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        for (int i = 0; i < (int)(Maxrange.x - Minrange.x); i++)
        {
            for (int j = 0; j < (int)(Maxrange.y - Minrange.y); j++)
            {
                GameObject p=Instantiate(dot,transform);
                p.transform.position = new Vector2(x + i, y + j);
                p.GetComponent<SpriteRenderer>().color = new Vector4((grid[i,j]/ 10),1-(grid[i, j] / 10),0,1);
            }
        }

    }


    private int FindIndexOfMin(cordinate[] arr)
    {
        if (arr.Length == 0)
        {
            return -1;
        }

        int minIndex = 0;

        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i].Value < arr[minIndex].Value)
            {
                minIndex = i;
            }
        }

        return minIndex;
    }
}
