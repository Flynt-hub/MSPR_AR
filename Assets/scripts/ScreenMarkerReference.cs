using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMarkerReference : MonoBehaviour
{
    public BodyMarkerType mBodyMarkerType;

    private void OnValidate()
    {
        this.name = $"Screen{this.mBodyMarkerType}";
    }
}
