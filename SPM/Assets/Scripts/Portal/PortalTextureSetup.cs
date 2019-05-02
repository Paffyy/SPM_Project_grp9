using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    public Camera CameraA;
    public Camera CameraB;
    public Material CameraMatA;
    public Material CameraMatB;
    // Start is called before the first frame update
    void Start()
    {
        if(CameraB.targetTexture != null)
        {
            CameraB.targetTexture.Release();
        }
        CameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        CameraMatB.mainTexture = CameraB.targetTexture;
        if (CameraA.targetTexture != null)
        {
            CameraA.targetTexture.Release();
        }
        CameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        CameraMatA.mainTexture = CameraA.targetTexture;
    }
}
