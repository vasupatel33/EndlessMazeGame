using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int HealthText;
    private void Start()
    {
        SetHealthEnemy();
    }
    public void SetHealthEnemy()
    {
        //for (int i = 0; i < this.transform.childCount; i++)
        //{
        //    if (Random.Range(0, 3) == 0)
        //    {
        //        Debug.Log("if");
        //        int txtValue = Random.Range(20, 100);
        //        Debug.Log("count = "+ this.transform.GetChild(i).childCount);
        //        Debug.Log("Obj = "+ this.transform.GetChild(i).name);   
        //        for (int j = 0; j < this.transform.GetChild(i).childCount; j++)
        //        {
        //            Debug.Log("name = " + this.transform.GetChild(i).name);
        //            Transform childI = this.transform.GetChild(i);

        //            if (childI != null)
        //            {
        //                Debug.Log("name = "+childI.GetChild(j).name);
        //                Transform childJ = childI.GetChild(j);

        //                if (childJ != null)
        //                {
        //                    TextMeshPro textComponent = childJ.GetComponent<TextMeshPro>();

        //                    if (textComponent != null)
        //                    {
        //                        textComponent.text = txtValue.ToString();
        //                    }
        //                    else
        //                    {
        //                        Debug.LogError("TextMeshProUGUI component not found on childJ.");
        //                    }
        //                }
        //                else
        //                {
        //                    Debug.LogError("Child at index j not found on childI.");
        //                }
        //            }
        //            else
        //            {
        //                Debug.LogError("Child at index i not found on this.transform.");
        //            }

        //            Debug.Log("1");
        //        }

        //    }

        for (int i = 0; i < this.transform.childCount; i++)
        {
            Debug.Log("if");
            int txtValue = Random.Range(20, 100);
            Debug.Log("count = " + this.transform.GetChild(i).childCount);
            Debug.Log("Obj = " + this.transform.GetChild(i).name);

            Transform childI = this.transform.GetChild(i);

            for (int j = 0; j < childI.childCount; j++)
            {
                Debug.Log("name = " + childI.GetChild(j).name);
                GameObject childJ = childI.GetChild(j).gameObject;

                if (childJ != null)
                {
                    TextMeshPro textComponent = childJ.GetComponent<TextMeshPro>();

                    if (textComponent != null)
                    {
                        Debug.Log("Text added");
                        textComponent.text = txtValue.ToString();
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

                Debug.Log("1");
            }
        }

        //else
        //{
        //    Debug.Log("else");
        //    int txtValue = Random.Range(20, 100);
        //    for (int j = 0; j < 4; j++)
        //    {
        //        Debug.Log("11 is = " + this.transform.GetChild(i).name);
        //        Debug.Log("2 is = " + this.transform.GetChild(j).name);
        //        this.transform.GetChild(i).GetChild(j).GetComponent<TextMeshProUGUI>().text = txtValue.ToString();
        //        Debug.Log("1");
        //    }
        //}
    }
}
