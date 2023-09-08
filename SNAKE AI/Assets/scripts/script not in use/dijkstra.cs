using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class dijkstra : MonoBehaviour
{
    public int radius ;
    int n ;
    float[,] array;


    private void Start()
    {
        constants.dijkstra_radius = radius;
        //radius = 5;
        n = (radius * 2) + 1;
        array = new float[n,n];
    }

    

    public int find(Transform point) {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                array[i,j] = 100;
            }
        }

        //showtable();

        boundry bd=GetComponent<boundry>();
        //Debug.Log("block A complete");

        for (int i = 1; i <= (n/2); i++)
        {
            for (int j = i; j < n - i; j++)
            {
                //Debug.Log(i + "" + j);
                float X = (transform.position.x - (int)(n / 2) + j);
                float Y = transform.position.y + (int)(n / 2)-i+1;
                if (!transform.GetChild(0).GetComponent<follow>().check(new Vector3(X, Y, 0)) && bd.checkboundry(new Vector3(X, Y, 0)))
                    array[i-1, j] = Mathf.Sqrt(((point.position.x - X) * (point.position.x - X)) + ((point.position.y - Y) * (point.position.y - Y)));
                else {
                    array[i - 1, j] = 888;
                }
                if (array[i - 1, j] > radius && i > 1) array[i - 1, j] = 100;
                //Debug.Log(X + " , " + Y + " - - "+ point.position.x+" , "+ point.position.y +"  -"+ array[i-1, j]);

                X = transform.position.x + (int)(n / 2)-i+1;
                Y = transform.position.y + (int)(n / 2) - j;
                if (!transform.GetChild(0).GetComponent<follow>().check(new Vector3(X, Y, 0)) && bd.checkboundry(new Vector3(X, Y, 0)))
                    array[j, n - i] = Mathf.Sqrt(((point.position.x - X) * (point.position.x - X)) + ((point.position.y - Y) * (point.position.y - Y)));
                else
                {
                    array[j,n-i] = 888;
                }
                if (array[j,n-i] > radius && i > 1) array[j,n-i] = 100;
                //Debug.Log(X + " , " + Y + " - - " + point.position.x + " , " + point.position.y + "  -" + array[j,n-1]);

                X = (transform.position.x - (int)(n / 2) + j);
                Y = transform.position.y - (int)(n / 2) + i - 1;
                if (!transform.GetChild(0).GetComponent<follow>().check(new Vector3(X, Y, 0)) && bd.checkboundry(new Vector3(X, Y, 0)))
                    array[n - i, j] = Mathf.Sqrt(((point.position.x - X) * (point.position.x - X)) + ((point.position.y - Y) * (point.position.y - Y)));
                else
                {
                    array[n - i, j] = 888;
                }
                if (array[n-i, j] > radius && i > 1) array[n-i, j] = 100;

                X = transform.position.x - (int)(n / 2) + i - 1;
                Y = transform.position.y + (int)(n / 2) - j;
                if (!transform.GetChild(0).GetComponent<follow>().check(new Vector3(X, Y, 0)) && bd.checkboundry(new Vector3(X, Y, 0)))
                    array[j, i - 1] = Mathf.Sqrt(((point.position.x - X) * (point.position.x - X)) + ((point.position.y - Y) * (point.position.y - Y)));
                else
                {
                    array[j, i - 1] = 888;
                }
                if (array[j,i-1] > radius && i > 1) array[j,i-1] = 100;

                //array[j][i - 1] = sqrt(((x - j) * (x - j)) + ((y - (i - 1)) * (y - (i - 1))));
                //Debug.Log(i + "" + j);

                //cout<<i-1<<","<<j<< "  "<<j<<","<<n-i<< "  "<<n-i<<","<<j<< "  "<<j<<","<<i-1<<endl;
            }
        }


        //Debug.Log("block B complete");
        //*
        for (int i = 2; i <= (n / 2); i++)
        {
            for (int j = i; j < n - i; j++)
            {
                float X = (transform.position.x - (int)(n / 2) + j);
                float Y = transform.position.y + (int)(n / 2) - i + 1;
                if (!transform.GetChild(0).GetComponent<follow>().check(new Vector3(X, Y, 0)) && bd.checkboundry(new Vector3(X, Y, 0)))
                    array[i - 1,j] = Math.Min(findMaxNeighbour(array, i - 1, j) + 1.5f, array[i - 1, j]);
                else
                {
                    array[i - 1, j] = 888;
                }


                //Debug.Log("block C1 complete");

                X = transform.position.x + (int)(n / 2) - i + 1;
                Y = transform.position.y + (int)(n / 2) - j;
                if (!transform.GetChild(0).GetComponent<follow>().check(new Vector3(X, Y, 0)) && bd.checkboundry(new Vector3(X, Y, 0)))
                    array[j,n - i] = Math.Min(findMaxNeighbour(array, j, n - i) + 1.5f, array[j,n - i]);
                else
                {
                    array[j,n-i] = 888;
                }

                //Debug.Log("block C2 complete");

                X = (transform.position.x - (int)(n / 2) + j);
                Y = transform.position.y - (int)(n / 2) + i - 1;
                //Debug.Log("block C3.1 complete");
                if (!transform.GetChild(0).GetComponent<follow>().check(new Vector3(X, Y, 0)) && bd.checkboundry(new Vector3(X, Y, 0)))
                {
                    //Debug.Log("block C3.2 IF start");
                    array[n - i, j] = Math.Min(findMaxNeighbour(array, n - i, j) + 1.5f, array[n - i, j]);
                    //Debug.Log("block C3.2 IF complete");
                }

                else
                {
                    //Debug.Log("block C3.2 ELSE start");
                    array[n-i , j] = 888;
                    //Debug.Log("block C3.2 ELSE complete");
                }

                //Debug.Log("block C3 complete");

                X = transform.position.x - (int)(n / 2) + i - 1;
                Y = transform.position.y + (int)(n / 2) - j;
                if (!transform.GetChild(0).GetComponent<follow>().check(new Vector3(X, Y, 0)) && bd.checkboundry(new Vector3(X, Y, 0)))
                    array[j,i - 1] = Math.Min(findMaxNeighbour(array, j, i - 1) + 1.5f, array[j, i - 1]);
                else
                {
                    array[j,i-1] = 888;
                }

                //Debug.Log("block C4 complete");

            }
        }
        //*/


        //Debug.Log("block C complete");

        //for (int i = 0; i < n; i++)
        //{
        //    string ans = "[";
        //    for (int j = 0; j < n; j++)
        //    {
        //        ans += (array[i, j] + ", ");
        //    }
        //    ans += "]";
        //    Debug.Log(ans);
        //}
        //Debug.Log(array[3, 2]);
        //Debug.Log(array[4,3]);
        //Debug.Log(array[3,4]);
        //Debug.Log(array[2,3]);

        return findside()+1;

    }

    int findside() {
        int mode = GetComponent<snake_movement>().mode;

        switch (mode){
            case 0:
//                Debug.Log(array[3, 2] + " , " + array[2, 3] + " , " + array[3, 4]);
                if (array[n/2-1, n/2] <= array[n/2, n/2-1] && array[n/2-1, n/2] <= array[n/2, n/2+1]) return 0;
                else if (array[n/2, n/2+1] <= array[n/2, n/2-1]) return 1;
                else return -1;
            case 1:
                if (array[n/2, n/2+1] <= array[n/2-1, n/2] && array[n/2, n/2+1] <= array[n/2+1, n/2]) return 0;
                else if (array[n/2+1, n/2] <= array[n/2-1, n/2]) return 1;
                else return -1;
            case 2:
                if (array[n/2+1, n/2] <= array[n/2, n/2+1] && array[n/2+1, n/2] <= array[n/2, n/2-1]) return 0;
                else if (array[n/2,n/2-1] <= array[n/2, n/2+1]) return 1;
                else return -1;
            case 3:
                if (array[n/2, n/2-1] <= array[n/2-1, n/2] && array[n/2, n/2-1] <= array[n/2+1, n/2]) return 0;
                else if (array[n/2-1, n/2] <= array[n/2+1, n/2]) return 1;
                else return -1;
            default:
                return 0;
        }


        //if (array[3, 2] < array[2, 3] && array[3, 2] < array[4, 3] && array[3, 2] < array[3, 4]) return 3;
        //else if (array[4, 3] < array[2, 3] && array[4, 3] < array[3, 4]) return 2;
        //else if (array[3, 4] < array[2, 3]) return 1;
        //else return 0;
    }

    public void showtable() {
        for (int i = 0; i < n; i++)
        {
            string ans = "[";
            for (int j = 0; j < n; j++)
            {
                ans += (array[i, j].ToString("F4") + ", ");
            }
            ans += "]";
            Debug.Log(ans);
        }
    }


    float findMaxNeighbour(float[,] array, int i, int j)
    {
        return Math.Min(Math.Min(array[i + 1,j], array[i - 1,j]), Math.Min(array[i,j + 1], array[i,j - 1]));
    }


}
