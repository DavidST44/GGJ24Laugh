using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDisable : MonoBehaviour
{
    public GameObject ovrCR;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("disableMe", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void disableMe()
    {
        ovrCR.SetActive(true);

    }
}
