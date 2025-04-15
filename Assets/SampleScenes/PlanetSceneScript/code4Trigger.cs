using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class code4Trigger : MonoBehaviour
{
    public GameObject code4;
    public GameObject code4Text;
    public GameObject error;
    public CodePartCounter counterManager;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("code4"))
        {
            error.SetActive(false);
            code4.SetActive(false);
            code4Text.SetActive(true);
            counterManager.IncrementCounter();
        }
        else
        {
            error.SetActive(true);
        }
    }
}
