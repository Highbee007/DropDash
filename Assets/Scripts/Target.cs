using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 0.01f;
    }

    private void OnMouseDown()
    {
        Debug.Log("There was a click");
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
