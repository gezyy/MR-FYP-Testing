using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class code3Trigger : MonoBehaviour
{
    public GameObject code3;
    public GameObject code3Text;
    public GameObject error;
    public CodePartCounter counterManager;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("code3"))
        {
            error.SetActive(false);
            code3.SetActive(false);
            code3Text.SetActive(true);
            counterManager.IncrementCounter();
        }
        else
        {
            error.SetActive(true);
        }
    }
}
