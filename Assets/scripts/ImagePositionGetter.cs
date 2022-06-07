using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagePositionGetter : MonoBehaviour
{
    Camera mCamera;
    public Transform mTargetTransform;
    // Start is called before the first frame update
    void Start()
    {
        mCamera = GetComponent<Camera>();        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = mCamera.WorldToScreenPoint(mTargetTransform.position);
        Debug.Log("target is " + screenPos.x + " pixels from the left");
    }
}
