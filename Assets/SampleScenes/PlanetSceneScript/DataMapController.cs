using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMapController : MonoBehaviour
{
    public GameObject Map;
    public GameObject Quad;
    public GameObject Map1;
    public GameObject Quad1;
    public GameObject particlel;
    public AthenaAudioController athenaAudioController;
    public GameObject hint4;
    public GameObject hint5;
    public GameObject ActiveButton;
    public GameObject MidV1;
    public GameObject MidV2;
    public GameObject MidV3;
    public GameObject MidV11;
    public GameObject MidV22;
    public GameObject MidV33;
    private int counter = 0;
    public GameObject hint6;
    public GameObject hint7;
    public GameObject Expression;
    public GameObject ExpressionButtons;
    public GameObject AtoButtons;
    public GameObject TempButtons;
    public GameObject TempChart;
    public GameObject TempText;
    public GameObject GenerateButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(counter >= 3)
        {
            TempChart.SetActive(false);
            TempButtons.SetActive(false);
            TempText.SetActive(false);
            AtoButtons.SetActive(false);
            GenerateButton.SetActive(false);
            hint6.SetActive(false);
            hint7.SetActive(true);
            Expression.SetActive(true);
            ExpressionButtons.SetActive(true);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MidValueTemp"))
        {
            MidV1.SetActive(true);
            MidV11.SetActive(false);
            counter++;
        }
        else if (other.CompareTag("MidValueHum"))
        {
            MidV2.SetActive(true);
            MidV22.SetActive(false);
            counter++;
        }
        else if (other.CompareTag("MidValueAto"))
        {
            MidV3.SetActive(true);
            MidV33.SetActive(false);
            counter++;
        }
    }
    public void ActiveMap()
    {
        Map.SetActive(false);
        Map1.SetActive(true);
        Quad.SetActive(false);
        Quad1.SetActive(true);
        particlel.SetActive(true);
        hint4.SetActive(false);
        hint5.SetActive(true);
        athenaAudioController.PlayVoiceClip(4);
        ActiveButton.SetActive(false);
    }
}
