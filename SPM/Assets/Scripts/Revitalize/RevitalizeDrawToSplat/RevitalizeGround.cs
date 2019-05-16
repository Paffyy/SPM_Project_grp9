using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevitalizeGround : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    [Range(1,100)]
    private int brushSize;
    [SerializeField]
    [Range(1, 10)]
    private int brushIntensity;
    [SerializeField]
    private Shader shader;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private GameObject revitalizeObject;
    private RenderTexture splatMap;
    private Material scorchedMaterial, revitalizeMaterial;
    void Start()
    {
        revitalizeMaterial = new Material(shader);
        revitalizeMaterial.SetVector("_Color", Color.red);
        revitalizeMaterial.SetInt("_BrushSize", 1001 - brushSize);
        scorchedMaterial = GetComponent<Terrain>().materialTemplate;
        splatMap = new RenderTexture(512, 512, 0, RenderTextureFormat.ARGBFloat);
        scorchedMaterial.SetTexture("_Splat", splatMap);
    }

    private void RevitalizeArea()
    {
        RaycastHit hit;
        Debug.DrawRay(revitalizeObject.transform.position, Vector3.down);
        if (Physics.Raycast(revitalizeObject.transform.position, Vector3.down, out hit, layerMask))
        {
            revitalizeMaterial.SetVector("_Coordinates", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
            RenderTexture tempTexture = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(splatMap, tempTexture);
            Graphics.Blit(tempTexture, splatMap, revitalizeMaterial);
            RenderTexture.ReleaseTemporary(tempTexture);
        }
    }
    private void Update()
    {
        RevitalizeArea();
    }
    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 256, 256), splatMap, ScaleMode.ScaleToFit,false,1);
    }

}
