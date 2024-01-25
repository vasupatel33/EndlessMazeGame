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
            int txtValue = Random.Range(5, 10);
            if (Random.Range(0, 2) == 1)
            {
                int val = Random.Range(0, this.transform.childCount);
                
                Vector3 spawnPos = new Vector3(transform.GetChild(val).gameObject.transform.position.x, transform.GetChild(val).gameObject.transform.position.y + 2, transform.GetChild(val).gameObject.transform.position.z);
                
                Instantiate(DoublePlayer, spawnPos, Quaternion.identity, transform.GetChild(val).transform);
            }
            else
            {
                Debug.Log("Else");
            }
            Transform childI = this.transform.GetChild(i);
            for (int j = 0; j < 4; j++)
            {
                //Debug.Log("name = " + childI.GetChild(j).name);
                GameObject childJ = childI.GetChild(j).gameObject;
                //Debug.Log("Child = " + childJ);
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
                        //Debug.LogError("TextMeshProUGUI component not found on childJ.");
                    }
                }
                else
                {
                    //Debug.LogError("Child at index j not found on childI.");
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
