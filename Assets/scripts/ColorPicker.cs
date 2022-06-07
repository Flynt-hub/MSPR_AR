using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Vuforia;

public class ColorPicker : DefaultObserverEventHandler
{
    [SerializeField]
    Camera mCamera;
    [SerializeField]
    public RenderTexture mCameraTexture;
    [SerializeField]
    GameObject mTargetScreenMarker;
    //private Transform mTargetTransform;
    [SerializeField]
    //public ModelMarkerReference[] mModelMarkerReferences;
    public ColorPickerReference[] mColorPickersReference;
    public ScreenMarkerReference[] mScreenMarkerReferences;
    public Material[] mModelMaterials;

    private Dictionary<BodyMarkerType, Material> mMaterialMap;

    protected override void Start()
    {
        base.Start();

        this.mColorPickersReference = this.GetComponentsInChildren<ColorPickerReference>(true);
        //this.mModelMarkerReferences = this.GetComponentsInChildren<ModelMarkerReference>(true);
        this.mMaterialMap = new Dictionary<BodyMarkerType, Material>();
        //for (int i = 0; i < mModelMaterials.Length; ++i) Debug.Log($"MATERIAL : {mModelMaterials[i].name}");
        //for (int i = 0; i < mColorPickersReference.Length; ++i) Debug.Log($"COLOR_PICKER_REFERENCE : {mColorPickersReference[i].mMarkerType.ToString()}");
        for (int i = 0; i < mColorPickersReference.Length; ++i)
        {
            for(int j = 0; j < mModelMaterials.Length; ++j)
            {
                if(mColorPickersReference[i].mMarkerType.ToString() == mModelMaterials[j].name)
                {
                    mMaterialMap.Add(mColorPickersReference[i].mMarkerType, mModelMaterials[j]);
                    continue;
                }
            }
        }
        //foreach (var pair in mMaterialMap)
        //    Debug.Log($"KEY : {pair.Key}, VALUE : {pair.Value.name}");
    }

    protected override void OnTrackingFound()
    {
        ImageTargetBehaviour pouf = GetComponent<ImageTargetBehaviour>();

        InvokeRepeating("GetColor", 1, 3);

        base.OnTrackingFound();
    }
    
    protected override void OnTrackingLost()
    {
        CancelInvoke("GetColor");
        base.OnTrackingLost();
    }

    protected override void HandleTargetStatusChanged(Status previousStatus, Status newStatus)
    {        
        base.HandleTargetStatusChanged(previousStatus, newStatus);
    }

    protected override void HandleTargetStatusInfoChanged(StatusInfo newStatusInfo)
    {
        base.HandleTargetStatusInfoChanged(newStatusInfo);
    }

    // Update is called once per frame
    void Update()
    {
        //++mFrameCounter;
        //if (mObserverBehaviour.TargetStatus.Status == Status.TRACKED)
        //{
        //    Debug.Log($"HEIGHT : {Screen.height}, WIDTH : {Screen.width}, RESOLUTION : {Screen.currentResolution}");
        //    //Texture2D lScreenShot = ScreenCapture.CaptureScreenshotAsTexture();
        //    //mCameraTexture = mCamera.activeTexture;

        //    int c = this.mColorPickersReference?.Length ?? 0;

        //    for (int i = 0; i < c; i++)
        //    {
        //        BodyMarkerType lType = this.mColorPickersReference[i].mMarkerType;
        //        ModelMarkerReference lMarker = System.Array.Find(this.mModelMarkerReferences, m => m.mBodyMarkerType == lType);
        //        Vector2 lPositionInScreen = mCamera.WorldToScreenPoint(this.mColorPickersReference[i].transform.position);

        //        lMarker.GetComponent<MeshRenderer>().material.color = ToTexture2D(mCameraTexture).GetPixel((int)lPositionInScreen.x, (int)lPositionInScreen.y);
        //    }
        //    mFrameCounter = 0;
        //}
        if (mObserverBehaviour.TargetStatus.Status == Status.TRACKED)
        {
            //Vector3 screenPos = mCamera.WorldToScreenPoint(mObserverBehaviour.transform.position);
            //mTargetScreenMarker.transform.position = screenPos;
            //Debug.Log("target is " + screenPos.x + " pixels from the left");
        }
        //if (mTargetTransform)
        //{
        //}
    }

    private void GetColor()
    {
        if (mObserverBehaviour.TargetStatus.Status == Status.TRACKED)
        {
            Debug.Log($"HEIGHT : {Screen.height}, WIDTH : {Screen.width}, RESOLUTION : {Screen.currentResolution}");

            int c = this.mColorPickersReference?.Length ?? 0;

            for (int i = 0; i < c; i++)
            {
                BodyMarkerType lType = this.mColorPickersReference[i].mMarkerType;
                Vector2 lPositionInScreen = mCamera.WorldToScreenPoint(this.mColorPickersReference[i].transform.localPosition);


                ScreenMarkerReference lScreenMarkerReference = System.Array.Find(this.mScreenMarkerReferences, m => m.mBodyMarkerType == lType);
                lScreenMarkerReference.transform.position = lPositionInScreen;

                //ModelMarkerReference lMarker = System.Array.Find(this.mModelMarkerReferences, m => m.mBodyMarkerType == lType);
                //lMarker.GetComponent<MeshRenderer>().material.color = ToTexture2D(mCameraTexture).GetPixel((int)lPositionInScreen.x, (int)lPositionInScreen.y);

                Material lMaterial = mMaterialMap[mColorPickersReference[i].mMarkerType];
                //lMaterial.color = ToTexture2D(mCameraTexture).GetPixel((int)lPositionInScreen.x / 2, (int)lPositionInScreen.y / 2);
                Color[] lColors = ToTexture2D(mCameraTexture).GetPixels((int)lPositionInScreen.x / 2, (int)lPositionInScreen.y / 2, 5, 5);
                Color lAverageColor = new Color();
                float r = 0, g = 0, b = 0, a = 0;
                for(int j = 0; j  < lColors.Length; ++j)
                {
                    r += lColors[j].r;
                    g += lColors[j].g;
                    b += lColors[j].b;
                    a += lColors[j].a;
                }
                lAverageColor.r = r / lColors.Length;
                lAverageColor.g = g / lColors.Length;
                lAverageColor.b = b / lColors.Length;
                lAverageColor.a = a / lColors.Length;
                lMaterial.color = lAverageColor;
            }
        }
    }

    private Texture2D ToTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGBAFloat, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}
