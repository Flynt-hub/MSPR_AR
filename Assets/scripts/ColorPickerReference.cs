using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyMarkerType
{
    None,
    Head,
    Mouth,
    Body,
    Belly,
    Hand,
    Face
};
public class ColorPickerReference : MonoBehaviour
{
    public BodyMarkerType mMarkerType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        this.name = this.mMarkerType.ToString();
    }
}
