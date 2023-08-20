using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_animation : MonoBehaviour
{
    grid2D gr;
    //public int[] moves;
    public int index = 0;
    public string moves;
    public int maxlength;
    //int length;

    public string ans = "";

    float timer = 0;

    Vector2 current_location;
    // Start is called before the first frame update
    void Start()
    {

        gr = new grid2D(gameObject);
        //length = 0;
        current_location = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.UpArrow)) {
        //    gr.move_up();
        //    ans += "0";
        //    update_tail();
        //}
        //if (Input.GetKeyDown(KeyCode.RightArrow)) { gr.move_right(); ans += "1"; update_tail(); }
        //if (Input.GetKeyDown(KeyCode.DownArrow)) { gr.move_down(); ans += "2"; update_tail(); }
        //if (Input.GetKeyDown(KeyCode.LeftArrow)) { gr.move_left(); ans += "3"; update_tail(); }


        //if (length < maxlength)
        //{
        //    GetComponent<addlength>().addnew();
        //    length++;
        //}
        

        //else
        //{
            if (moves.Length > index && timer > 0.01f)
            {
                if (moves[index] == '0') gr.move_up();
                if (moves[index] == '1') gr.move_right();
                if (moves[index] == '2') gr.move_down();
                if (moves[index] == '3') gr.move_left();
                index++;
                update_tail();
                //GetComponent<addlength>().add = true;
                timer = 0;
            }
            if (timer < 1) timer += Time.deltaTime;

        
    }
    void update_tail()
    {
        //Debug.Log(transform.GetChild(0).name);
        if (transform.childCount > 0) transform.GetChild(0).GetComponent<follow>().move(current_location);
         current_location = transform.position;
        
        //if (index < 192) 
        //Debug.Log(transform.GetChild(0).GetComponent<follow>().check(transform.position));

    }
}
