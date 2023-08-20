using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class data_view : MonoBehaviour
{
    [SerializeField] Text FieldOfView;
    [SerializeField] Text DijkastraRadius;

    // Start is called before the first frame update
    void Start()
    {

        FieldOfView.text = "" + constants.visual_radius;
        DijkastraRadius.text = "" + constants.dijkstra_radius;
    }

    private void Update()
    {
    }

}
