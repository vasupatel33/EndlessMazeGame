using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEnd : MonoBehaviour
{   
    void OnTriggerEnter(Collider col) {
        GameObject.Find("GameManager").GetComponent<ObstacleManagment>().CreateObstacle();
        Destroy(transform.parent.gameObject, 2f);
    }
}
