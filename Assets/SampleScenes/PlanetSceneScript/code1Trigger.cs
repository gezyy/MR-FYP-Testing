using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class code1Trigger: MonoBehaviour
{
    public GameObject code1;
    public GameObject code1Text;
    public GameObject error;
    public CodePartCounter counterManager;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("code1")) {
            error.SetActive(false);
            code1.SetActive(false);
            code1Text.SetActive(true);
            counterManager.IncrementCounter();
        }
        else
        {
            error.SetActive(true);
        }
    }
}
