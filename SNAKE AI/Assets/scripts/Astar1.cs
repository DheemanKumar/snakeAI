using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Astar1 : MonoBehaviour
{
    public Vector2 Maxrange;
    public Vector2 Minrange;
    float[,] grid;

    public Transform walls;
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

        public cordinate(int x, int y, float value, float avalue, cordinate parent)
        {

            X = x;
            Y = y;
            Value = value;
            Parent = parent;
            Avalue = avalue;
        }
    }


    private List<cordinate> visited = new List<cordinate>();
    private List<cordinate> next = new List<cordinate>();




    // Start is called before the first frame update
    void Start()
    {
        grid = new float[(int)(Maxrange.x - Minrange.x), (int)(Maxrange.y - Minrange.y)];
        it = 50;

        //findpath();

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
        }
    }

    void map()
    {
        int x, y;
        x = (int)Minrange.x;
        y = (int)Minrange.y;


        


        for (int i = 0; i < (int)(Maxrange.x - Minrange.x); i++)
        {
            for (int j = 0; j < (int)(Maxrange.y - Minrange.y); j++)
            {
                if ((head.position.x == x + i && head.position.y == y + j) ||
                    iswall(x+i,y+j) ||
                    i + 1 == (int)(Maxrange.x - Minrange.x) || i == 0 ||
                    j + 1 == (int)(Maxrange.y - Minrange.y) || j == 0
                    )
                    grid[i, j] = float.MaxValue;
                else grid[i, j] = Vector3.Distance(point.position, new Vector3(x + i, y + j));
            }
        }


    }

    bool iswall(int x,int y)
    {
        

        for (int i = 0; i < walls.childCount; i++)
        {
            //Debug.Log(walls.GetChild(i));
            if (walls.GetChild(i).position.x==x && walls.GetChild(i).position.y == y)
            {
                return true;
            }
        }
        return false;
    }

    void findpath()
    {
        //create map
        map();

        
        int x, y;
        x = (int)(Maxrange.x + head.position.x);
        y = (int)(Maxrange.y + head.position.y);

        cordinate c = new cordinate(x, y, grid[x, y], 1, null);
        next.Add(c);


        for (int i = 0; i < 10000; i++)
        {
            //Debug.Log("start "+next.Count);
            if (next.Count == 0) { 
                break;
            }
            if (i >= 9995) {
                break; }

            c = next[0];
            next.Remove(c);

            if (c.Value == 0) break;

            Debug.Log("("+c.X+","+c.Y+")  values "+c.Value + "  "+c.Avalue);

            if (grid[c.X, c.Y + 1] != float.MaxValue)
            {
                if (visited.Exists(cor => cor.X == c.X && cor.Y == c.Y + 1))
                {
                    int index = visited.FindIndex(pair => pair.X == c.X && pair.Y == c.Y + 1);
                    if (c.Avalue < visited[index].Avalue)
                        visited[index].Parent = c;
                }
                else if (!next.Exists(cor => cor.X == c.X && cor.Y == c.Y + 1))
                {
                    if (c.Y + 1 < (Maxrange.y - Minrange.y))
                    {
                        cordinate co = new cordinate(c.X, c.Y + 1, grid[c.X, c.Y + 1], c.Avalue + 1, c);
                        next.Add(co);
                        Debug.Log("new added   up(" + next[next.Count-1].X +" "+ next[next.Count - 1].Y + ")   values  "+ next[next.Count - 1].Value+" "+ next[next.Count - 1].Avalue);
                    }
                }
            }


            if (grid[c.X+1, c.Y] != float.MaxValue)
            {
                if (visited.Exists(cor => cor.X == c.X + 1 && cor.Y == c.Y))
                {
                    int index = visited.FindIndex(pair => pair.X == c.X + 1 && pair.Y == c.Y);
                    if (c.Avalue < visited[index].Avalue)
                        visited[index].Parent = c;
                }
                else if (!next.Exists(cor => cor.X == c.X + 1 && cor.Y == c.Y))
                {
                    if (c.X + 1 < (Maxrange.x - Minrange.x))
                    {
                        next.Add(new cordinate(c.X + 1, c.Y, grid[c.X + 1, c.Y], c.Avalue + 1, c));

                        Debug.Log("new added   right(" + next[next.Count - 1].X + " " + next[next.Count - 1].Y + ")   values  " + next[next.Count - 1].Value + " " + next[next.Count - 1].Avalue);
                    }
                }
            }



            if (grid[c.X-1, c.Y] != float.MaxValue)
            {
                if (visited.Exists(cor => cor.X == c.X - 1 && cor.Y == c.Y))
                {
                    int index = visited.FindIndex(pair => pair.X == c.X - 1 && pair.Y == c.Y);
                    if (c.Avalue < visited[index].Avalue)
                        visited[index].Parent = c;
                }
                else if (!next.Exists(cor => cor.X == c.X - 1 && cor.Y == c.Y))
                {
                    if (c.X - 1 > 0)
                    {
                        next.Add(new cordinate(c.X - 1, c.Y, grid[c.X - 1, c.Y], c.Avalue + 1, c));
                        Debug.Log("new added   left(" + next[next.Count - 1].X + " " + next[next.Count - 1].Y + ")   values  " + next[next.Count - 1].Value + " " + next[next.Count - 1].Avalue);
                    }
                }
            }



            if (grid[c.X, c.Y - 1] != float.MaxValue)
            {
                if (visited.Exists(cor => cor.X == c.X && cor.Y == c.Y - 1))
                {
                    int index = visited.FindIndex(pair => pair.X == c.X && pair.Y == c.Y - 1);
                    if (c.Avalue < visited[index].Avalue)
                        visited[index].Parent = c;
                }
                else if (!next.Exists(cor => cor.X == c.X && cor.Y == c.Y - 1))
                {
                    if (c.Y - 1 > 0)
                    {
                        next.Add(new cordinate(c.X, c.Y - 1, grid[c.X, c.Y - 1], c.Avalue + 1, c));
                        Debug.Log("new added   down(" + next[next.Count - 1].X + " " + next[next.Count - 1].Y + ")   values  " + next[next.Count - 1].Value + " " + next[next.Count - 1].Avalue);
                    }
                }
            }


            next.Sort((a, b) => (a.Value+a.Avalue).CompareTo(b.Value+b.Avalue));

            visited.Add(c);

            Debug.Log("visited " + visited.Count + "  next" + next.Count);

            //Debug.Log("end " + next.Count);

        }



        //allnodes();
        drawpath(c);

        //Debug.Log("next length  "+next.Count);

        //int ans = (findcost(c));
        //if (br) ans = -1;

        ////Debug.Log("cost " + ans);

        //visited.Clear();
        //next.Clear();

        //return ans;
        
    }

    void allnodes()
    {
        for (int i = 0; i < visited.Count; i++)
        {
            cordinate c = visited[i];

            if (c.Parent == null)
                Debug.Log("(" + c.X + "  " + c.Y + ")   values (" + c.Avalue + " , " + c.Value + ")       head ");
            else
                Debug.Log("(" + c.X + "  " + c.Y + ")   values (" + c.Avalue + " , " + c.Value + ")       (" + c.Parent.X + " " + c.Parent.Y + ") ");

        }
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
        int cost = 0;

        if (c.Parent == null)
        {
            return 1;
        }
        else
            cost = findcost(c.Parent);

        return cost + 1;
    }

    void drawpathhelp(cordinate c)
    {
        //if (c.Parent == null)
        //    Debug.Log("(" + c.X + "  " + c.Y + ")   values (" + c.Avalue + " , " + c.Value + ")       head ");
        //else
        //    Debug.Log("("+c.X + "  " + c.Y + ")   values ("+c.Avalue+" , "+c.Value+ ")       (" + c.Parent.X + " " + c.Parent.Y+") ");
        GameObject p = Instantiate(dot, transform);
        p.transform.position = new Vector2(c.X - Maxrange.x, c.Y - Maxrange.y);
        p.GetComponent<SpriteRenderer>().color = new Vector4(0, 1, 0, 1);
        
        if (c.Parent != null)
            drawpathhelp(c.Parent);
    }

    //void draw_visited()
    //{

    //    GameObject p = Instantiate(dot, transform);
    //    p.transform.position = new Vector2(c.X - Maxrange.x, c.Y - Maxrange.y);
    //    p.GetComponent<SpriteRenderer>().color = new Vector4(0, 1, 0, 1);
    //}



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
                GameObject p = Instantiate(dot, transform);
                p.transform.position = new Vector2(x + i, y + j);
                p.GetComponent<SpriteRenderer>().color = new Vector4((grid[i, j] / 100), 1 - (grid[i, j] / 100), 0, 1);
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
