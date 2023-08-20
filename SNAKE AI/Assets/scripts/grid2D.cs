using UnityEngine;


public class grid2D
{
    float length_X;
    float length_Y;
    GameObject player;

    public grid2D(GameObject player, float length_X = 1, float length_Y = 1)
    {
        this.player = player;
        this.length_X = length_X;
        this.length_Y = length_Y;
    }

    public void move_right()
    {
        player.transform.position = new Vector2(player.transform.position.x + length_X, player.transform.position.y);
    }
    public void move_left()
    {
        player.transform.position = new Vector2(player.transform.position.x - length_X, player.transform.position.y);
    }
    public void move_up()
    {
        player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + length_Y);
    }
    public void move_down()
    {
        player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - length_Y);
    }
}
