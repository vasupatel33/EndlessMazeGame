using TMPro;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    int txtVal;
    public GameObject PlayerPref;
    private void Start()
    {
        PlayerPref = GameObject.Find("PlayerCube");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Destroy(this.gameObject);
            Debug.Log("Collded");
            txtVal = int.Parse(collision.gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text);
            txtVal--;
            for(int i = 0; i < 4; i++)
            {
                collision.transform.GetChild(i).GetComponent<TextMeshPro>().text = txtVal.ToString();
            }
            if(collision.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text == 0.ToString())
            {
                Destroy(collision.gameObject);
                if(collision.transform.childCount > 4)
                {
                    PlayerLogic.instance.SpawnFollowingPlayer();







                    //Debug.Log("Player Generated");
                    //if (AllGeneteratedPlayer.Count > 0)
                    //{
                    //    Debug.Log("Iff");
                    //    Vector3 spawnPos = new Vector3(AllGeneteratedPlayer[AllGeneteratedPlayer.Count-1].transform.position.x - 1.5f, AllGeneteratedPlayer[AllGeneteratedPlayer.Count - 1].transform.position.y, AllGeneteratedPlayer[AllGeneteratedPlayer.Count - 1].transform.position.z);
                    //    GameObject g = Instantiate(PlayerPref, spawnPos, Quaternion.identity);
                    //    AllGeneteratedPlayer.Add(g);
                    //    Debug.Log("If cont = " + AllGeneteratedPlayer.Count);
                    //}
                    //else
                    //{
                    //    Debug.Log("Else");
                    //    Vector3 spawnPos = new Vector3(PlayerPref.transform.position.x - 1.5f, PlayerPref.transform.position.y, PlayerPref.transform.position.z);
                    //    GameObject g = Instantiate(PlayerPref, spawnPos, Quaternion.identity);
                    //    AllGeneteratedPlayer.Add(g);
                    //    Debug.Log("Count = "+AllGeneteratedPlayer.Count);
                    //}
                    //Instantiate(PlayerPref, spawnPos, Quaternion.,PlayerPref.transform);

                }
            }
            //collision.gameObject.GetComponent<EnemyHealth>().tectChange();
            //  EnemyHealth.instance.HealthText--;
            Debug.Log("Bullet destroyed");
        }
    }
}