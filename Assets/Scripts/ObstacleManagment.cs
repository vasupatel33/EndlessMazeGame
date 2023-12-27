using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManagment : MonoBehaviour
{

    public void CreateObstacle() {
        int randomObstacle = Random.Range(1, 10);
        GameObject obstacle = Instantiate (Resources.Load ("Obstacle" + randomObstacle) as GameObject);
        obstacle.transform.position = new Vector3(0, 0, 50 * Vars.obstacle);
        Vars.obstacle++;
    }
}
