using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int healthtxt;
    [SerializeField] GameObject DoublePlayer, OtherObj;

    private void Start()
    {
        DoublePlayer = GameObject.Find("PlayerDouble");
        OtherObj = GameObject.Find("OtherObj");
        SetHealthEnemy();
    }
    public void SetHealthEnemy()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            int txtValue = Random.Range(20, 100);

            //if(Random.Range(0,4) == 0)
            //{
            //    Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            //    Instantiate(DoublePlayer,spawnPos, Quaternion.identity,this.transform);
            //}
            //else
            //{
            //    Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            //    Instantiate(OtherObj, spawnPos, Quaternion.identity, this.transform);
            //}

            Transform childI = this.transform.GetChild(i);

            for (int j = 0; j < childI.childCount; j++)
            {
                //Debug.Log("name = " + childI.GetChild(j).name);
                GameObject childJ = childI.GetChild(j).gameObject;

                if (childJ != null)
                {
                    TextMeshPro textComponent = childJ.GetComponent<TextMeshPro>();

                    if (textComponent != null)
                    {
                        //Debug.Log("Text added");
                        textComponent.text = txtValue.ToString();
                        healthtxt = txtValue;
                    }
                    else
                    {
                        Debug.LogError("TextMeshProUGUI component not found on childJ.");
                    }
                }
                else
                {
                    Debug.LogError("Child at index j not found on childI.");
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
