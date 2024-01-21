using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] GameObject GameOverPanel;

    void OnEnable() {
        //target = GameObject.Find("PlayerCube").transform;
    }

    void FixedUpdate()
    {
        if (target)
        {
            //if (PlayerLogic.instance.AllGeneteratedPlayer.Count > 0)
            //{
            //    target = PlayerLogic.instance.AllGeneteratedPlayer[PlayerLogic.instance.AllGeneteratedPlayer.Count - 1].transform;
            //}
            if (target != null)
            {
                Vector3 targetPosition = target.TransformPoint(new Vector3(0, 6, -8));
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            }
            else
            {
                Debug.Log("Game Over");

            }
        }
        else
        {
            if (PlayerLogic.instance.AllGeneteratedPlayer.Count != 0)
            {
                target = PlayerLogic.instance.AllGeneteratedPlayer[Random.Range(0, PlayerLogic.instance.AllGeneteratedPlayer.Count)].transform;
            }
            else
            {
                GameOverPanel.SetActive(true);
            }
        }
    }
}
