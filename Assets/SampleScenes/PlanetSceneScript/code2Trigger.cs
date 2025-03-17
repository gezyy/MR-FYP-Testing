using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class code2Trigger : MonoBehaviour
{
    public GameObject code2;
    public GameObject code2Text;
    public GameObject error;
    public CodePartCounter counterManager;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("code2"))
        {
            error.SetActive(false);
            code2.SetActive(false);
            code2Text.SetActive(true);
            counterManager.IncrementCounter();
        }
        else
        {
            error.SetActive(true);
        }
    }
}
