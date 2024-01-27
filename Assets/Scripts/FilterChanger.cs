using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterChanger : MonoBehaviour
{
    public Texture[] leftTextures;
    public Texture[] rightTextures;
    public MeshRenderer left;
    public MeshRenderer right;

    public int filterDuration = 30;

    int filterIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting Pauls Filter Changer");
        InvokeRepeating("ChangeFilters", filterDuration, filterDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeFilters()
    {
        filterIndex++;
        filterIndex %= leftTextures.Length;

        left.material.SetTexture("_OverlayTex", leftTextures[filterIndex]);
        right.material.SetTexture("_OverlayTex", rightTextures[filterIndex]);
    }
}
