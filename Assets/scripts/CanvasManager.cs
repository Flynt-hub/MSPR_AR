using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CanvasType
{
    eMainScreen,
    eCaptureScreen
};

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    public Image mImageHolder;

    private testScreenshot mCapture;
    private CanvasController[] mCanvas;
    private CanvasController mActiveCanvas;

    // Start is called before the first frame update
    void Start()
    {
        mCanvas = GetComponentsInChildren<CanvasController>();
        mActiveCanvas = System.Array.Find(mCanvas, canvas => canvas.mType == CanvasType.eMainScreen);
        for(int i = 0; i < mCanvas.Length; ++i)
        {
            if (mCanvas[i].mType != mActiveCanvas.mType)
            {
                mCanvas[i].gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RecordFrame()
    {
        var texture = ScreenCapture.CaptureScreenshotAsTexture();
        Debug.Log("TEXTURE CREATED");
        yield return new WaitForEndOfFrame();
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));
        Debug.Log("SPRITE CREATED");
        mImageHolder.sprite = sp;
        // cleanup
        //Object.Destroy(texture);
    }

    public void TakeAShot()
    {
        mActiveCanvas.gameObject.SetActive(false);
        mActiveCanvas = System.Array.Find(mCanvas, canvas => canvas.mType == CanvasType.eCaptureScreen);
        mActiveCanvas.gameObject.SetActive(true);
        StartCoroutine("RecordFrame");
    }

    public void SendCapture(Image pCapture)
    {
        // send screenshot to twitter here
        // then send info to Dolibarr DB
        CancelCapture();
    }

    public void CancelCapture()
    {
        mActiveCanvas.gameObject.SetActive(false);
        mActiveCanvas = System.Array.Find(mCanvas, canvas => canvas.mType == CanvasType.eMainScreen);
        mActiveCanvas.gameObject.SetActive(true);
    }
}
