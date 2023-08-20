
using UnityEngine;
using UnityEngine.UI;

public class agent3_web : MonoBehaviour
{
    [SerializeField] GameObject points;

    public Text restartText;

    public int radius ;

    private snake_movement sm;

    public bool update_lock;

    

    // Start is called before the first frame update
    void Start()
    {
        sm = GetComponent<snake_movement>();
        radius = constants.visual_radius;
        constants.score = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            nextmove();
        }

        if (Input.GetKeyDown(KeyCode.Space)) constants.auto = !constants.auto;

        if (constants.manual) {
            nextmove();
            constants.manual = false;
        }

        if(Input.GetKeyDown(KeyCode.R)) GetComponent<restart>().restartlevel();
    }

    private void FixedUpdate()
    {
        if (constants.auto)
            nextmove();
    }

    


    void nextmove()
    {
        if (!update_lock)
        {
            
            update_lock = true;
            
            int a = GetComponent<dijkstra>().find(points.transform.GetChild(0));
            sm.move(a);
            if(points.GetComponent<points>().Point()) constants.score++;

            update_lock = checkcollide(); ;
        }

    }

    bool checkcollide()
    {
        //Debug.Log(transform.GetChild(0).GetComponent<follow>().check(new Vector3(transform.position.x, transform.position.y, transform.position.z)));
        if (GetComponent<boundry>().checkboundry(new Vector3(transform.position.x, transform.position.y, transform.position.z)) && !transform.GetChild(0).GetComponent<follow>().check(new Vector3(transform.position.x, transform.position.y, transform.position.z)))
        {
            return false;
        }
        else
        {
            restartText.enabled = true;
            return true;
        }
    }
}
