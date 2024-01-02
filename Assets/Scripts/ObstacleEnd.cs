using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEnd : MonoBehaviour
{   
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<ObstacleManagment>().CreateObstacle();
            Destroy(transform.parent.gameObject, 2f);
        }
    }
}
