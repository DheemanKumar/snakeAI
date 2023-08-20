using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkdata : MonoBehaviour
{
    List<scenerio> table;

    public int index=0;


    public float[] Boundry;
    public float[] Length;
    public float[] actions;

    // Start is called before the first frame update
    private void Awake()
    {
        table = (List<scenerio>)GetComponent<save_load>().LoadData();
        if (table == null)
        {
            table = new List<scenerio>();
            GetComponent<save_load>().SaveData(table);
        }
    }

    void Start()
    {
        table = (List<scenerio>)GetComponent<save_load>().LoadData();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            table = (List<scenerio>)GetComponent<save_load>().LoadData();
        }

        if (index < table.Count)
        {
            Boundry = table[index].getboundry();
            Length = table[index].getlength();
            actions = table[index].getactions();
        }
    }


}
