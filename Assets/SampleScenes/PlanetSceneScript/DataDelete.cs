using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDelete : MonoBehaviour
{
    public GameObject RedData1;
    public GameObject RedData2;
    public GameObject RedData3;
    public GameObject RedData4;
    private int counter = 0;
    public AircraftMovement aircraft;
    public GameObject datapad;
    public GameObject temperatureButtons;
    public GameObject temperatureText;
    public GameObject DeleteArea;
    public GameObject DeleteAreaV;
    public GameObject hint6;
    public GameObject defaultChart;
    public GameObject GenerateButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(counter >= 4)
        {
            hint6.SetActive(true);
            aircraft.MoveToPositionByIndex(3);
            DeleteArea.SetActive(false);
            DeleteAreaV.SetActive(false);
            datapad.SetActive(true);
            temperatureButtons.SetActive(true);
            temperatureText.SetActive(true);
            defaultChart.SetActive(false);
            GenerateButton.SetActive(true);
        }

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RedData1"))
        {
            RedData1.SetActive(false);
            counter++;
        }
        else if(other.gameObject.CompareTag("RedData2"))
        {
            RedData2.SetActive(false);
            counter++;
        }
        else if (other.gameObject.CompareTag("RedData3"))
        {
            RedData3.SetActive(false);
            counter++;
        }
        else if (other.gameObject.CompareTag("RedData4"))
        {
            RedData4.SetActive(false);
            counter++;
        }
    }
}
