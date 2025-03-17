using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidValueCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnColiisionEnter(Collider other)
    {
        if (other.CompareTag("Map"))
        {
            Destroy(gameObject);
        }
        
    }
}
