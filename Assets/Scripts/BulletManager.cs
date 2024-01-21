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
                }
            }
        }
    }
}