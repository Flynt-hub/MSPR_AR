using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMaterialProvider : MonoBehaviour
{
    Material[] mModelMaterial;
    void Start()
    {
        this.mModelMaterial = this.GetComponent<Renderer>().materials;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
