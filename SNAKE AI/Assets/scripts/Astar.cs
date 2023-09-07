using System;
using System.Collections;
using System.Collections.Generic;
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


    class cordinate
    {
        public int X { get; }
        public int Y { get; }
        public float Value { get; }

        public cordinate(int x, int y,float value)
        {
            X = x;
            Y = y;
            Value = value;
        }
    }

    private Stack<cordinate> cordinateStack = new Stack<cordinate>();

    private List<cordinate>visited = new List<cordinate>();
    private List<cordinate> path = new List<cordinate>();
    private List<cordinate> next = new List<cordinate>();




    // Start is called before the first frame update
    void Start()
    {
        grid = new float[(int)(Maxrange.x-Minrange.x), (int)(Maxrange.y-Minrange.y)];

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
            cordinate side= findpath();
            Debug.Log($"First: {side.X}, Second: {side.Y} , Value: {side.Value}");
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

    cordinate findpath()
    {
        map();
        //Debug.Log(head.position.x + " " + head.position.y);

        int x, y;
        x = (int)(Maxrange.x+ head.position.x);
        y = (int)(Maxrange.y+ head.position.y);

        Debug.Log("       " + grid[x, y + 1] + "       ");
        Debug.Log(grid[x - 1, y] + "    " + grid[x + 1, y]);
        Debug.Log("       " + grid[x, y - 1] + "       ");

        bool found = findpath(x, y);

        if (!found)
        {
            return null;
        }

        //cordinate[] sides = {new cordinate(x, y + 1, grid[x, y + 1]), new cordinate(x-1, y, grid[x-1, y]), new cordinate(x+1, y, grid[x+1, y]), new cordinate(x, y - 1, grid[x, y - 1]) };

        //Array.Sort(sides, (a, b) => b.Value.CompareTo(a.Value));

        

        //foreach (var side in sides)
        //{
        //    Debug.Log($"First: {side.X}, Second: {side.Y} , Value: {side.Value}");
        //    bool exists = visited.Exists(s => s.X ==side.X  && s.Y == side.Y);

        //    if (!exists)
        //    {
        //        cordinateStack.Push(side);
        //        visited.Add(side);
        //    }

        //}

        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        foreach(var side in path)
        {
            GameObject p = Instantiate(dot, transform);
            p.transform.position = new Vector2(side.X-Maxrange.x, side.Y-Maxrange.y);
            p.GetComponent<SpriteRenderer>().color = new Vector4(0, 1, 0, 1);
        }


        cordinate last = path[path.Count - 1];

        path.Clear();
        visited.Clear();
        next.Clear();

        return last;



    }

    bool findpath(int x,int y )
    {
        if (grid[x, y] == 0)
        {
            Debug.Log("found   ");
            path.Add(new cordinate(x, y, grid[x, y]));
            return true;
        }
        Debug.Log("data " + x + " " + y);
        bool ans=false;

        cordinate s1 = new cordinate(x, y + 1, grid[x, y + 1]);
        if (!visited.Exists(s => s.X == s1.X && s.Y == s1.Y))
        next.Add(s1);

        cordinate s2 = new cordinate(x-1, y, grid[x-1, y]);
        if (!visited.Exists(s => s.X == s2.X && s.Y == s2.Y))
            next.Add(s2);

        cordinate s3 = new cordinate(x+1, y, grid[x+1, y]);
        if (!visited.Exists(s => s.X == s3.X && s.Y == s3.Y))
            next.Add(s3);

        cordinate s4 = new cordinate(x, y - 1, grid[x, y - 1]);
        if (!visited.Exists(s => s.X == s4.X && s.Y == s4.Y))
            next.Add(s4);

        //cordinate[] sides = { new cordinate(x, y + 1, grid[x, y + 1]), new cordinate(x - 1, y, grid[x - 1, y]), new cordinate(x + 1, y, grid[x + 1, y]), new cordinate(x, y - 1, grid[x, y - 1]) };

        next.Sort((a, b) => (a.Value).CompareTo(b.Value));

        foreach (var side in next)
        {
            next.Remove(side);


            Debug.Log($"First: {side.X}, Second: {side.Y} , Value: {side.Value}");

            if (side.Value!=float.MaxValue)
            {
                
                visited.Add(side);

                ans=findpath(side.X, side.Y);
                if (ans) {
                    path.Add(side);
                    break;
                }
            }

        }

        

        return ans;
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
