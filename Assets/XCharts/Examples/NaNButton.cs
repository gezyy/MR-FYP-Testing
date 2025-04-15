using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaNButton : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Chart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivePanel()
    {
        if(Panel != null)
        {
            Panel.SetActive(true);
        }
        if (Chart != null)
        {
            Chart.SetActive(true);
        }
    }
}
