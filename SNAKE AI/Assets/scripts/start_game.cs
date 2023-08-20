using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class start_game : MonoBehaviour
{
    public void startgame()
    {
        constants.visual_radius = 5;
        constants.dijkstra_radius = 4;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void setAuto() {
        constants.auto = !constants.auto;
        if (constants.auto) 
        transform.GetChild(0).GetComponent<Image>().color = new Color32(0, 255, 0, 255);

        else transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 0, 0, 255);
    }
}
