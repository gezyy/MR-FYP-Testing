using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class OpenButtonUI: MonoBehaviour
{
    public GameObject UIwindow;

    // Start is called before the first frame update
    void Start()
    {
        if (UIwindow == null)
        {
            Debug.LogWarning("UIwindow is not assigned in the inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This method toggles the active state of the UIwindow
    public void ActiveWindow()
    {
        if (UIwindow != null)
        {
            bool isActive = UIwindow.activeSelf;
            UIwindow.SetActive(!isActive);
        }
        else
        {
            Debug.LogError("UIwindow is not assigned. Please assign it in the inspector.");
        }
    }
}
