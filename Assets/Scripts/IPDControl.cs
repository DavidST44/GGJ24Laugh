using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPDControl : MonoBehaviour
{
    public bool isLeft = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (isLeft)
                transform.position = transform.position + new Vector3(0.01f, 0, 0);
            else
                transform.position = transform.position - new Vector3(0.01f, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (isLeft)
                transform.position = transform.position - new Vector3(0.01f, 0, 0);
            else
                transform.position = transform.position + new Vector3(0.01f, 0, 0);
        }
    }
}
