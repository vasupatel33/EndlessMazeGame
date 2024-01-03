using TMPro;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    int txtVal;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            txtVal = int.Parse(collision.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text);
            txtVal--;
            Debug.Log("Text value is = " + txtVal);
            foreach(Transform child in collision.gameObject.transform)
            {
                child.gameObject.GetComponent<TextMeshPro>().text = txtVal.ToString();
            }
            if(collision.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text == 0.ToString())
            {
                Destroy(collision.gameObject);
            }
            //collision.gameObject.GetComponent<EnemyHealth>().tectChange();
            //  EnemyHealth.instance.HealthText--;
            Debug.Log("Bullet destroyed");
        }
    }
}
