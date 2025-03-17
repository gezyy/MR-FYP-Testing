using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class code5Trigger : MonoBehaviour
{
    public GameObject code5;
    public GameObject code5Text;
    public GameObject error;
    public CodePartCounter counterManager;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("code5"))
        {
            error.SetActive(false);
            code5.SetActive(false);
            code5Text.SetActive(true);
            counterManager.IncrementCounter();
        }
        else
        {
            error.SetActive(true);
        }
    }
}
