using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float Yspeed = 0;
    public float Xspeed = 0;
    public float Zspeed = 0;

    private float xf;
    private float yf;
    private float zf;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        yf += Yspeed * Time.deltaTime;
        xf += Xspeed * Time.deltaTime;
        zf += Zspeed * Time.deltaTime;

        transform.localEulerAngles = new Vector3(xf, yf, zf);
    }
}
