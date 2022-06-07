using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testScreenshot : MonoBehaviour
{
    [SerializeField]
    public Image mImageHolder;

    IEnumerator RecordFrame()
    {
        var texture = ScreenCapture.CaptureScreenshotAsTexture();
        Debug.Log("TEXTURE CREATED");
        yield return new WaitForEndOfFrame();
        // do something with texture
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));
        Debug.Log("SPRITE CREATED");
        mImageHolder.sprite = sp;
        // cleanup
        //Object.Destroy(texture);
    }

    public void TakeAShot()
    {
        StartCoroutine("RecordFrame");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
