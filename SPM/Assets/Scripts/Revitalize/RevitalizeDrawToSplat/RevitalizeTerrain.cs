using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevitalizeTerrain : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Shader drawRevitalizeShader;
    [SerializeField]
    private Shader eraseRevitalizeShader;
    [SerializeField]
    private LayerMask layerMask;
    private RenderTexture splatMap;
    private Material scorchedMaterial, drawMaterial, eraseMaterial;
    void Awake()
    {
        drawMaterial = new Material(drawRevitalizeShader);
        eraseMaterial = new Material(eraseRevitalizeShader);
        scorchedMaterial = GetComponent<MeshRenderer>().material;
        splatMap = new RenderTexture(512, 512, 0, RenderTextureFormat.ARGBFloat);
        scorchedMaterial.SetTexture("_Splat", splatMap);
    }
    public void InitializeMaps()
    {
             drawMaterial = new Material(drawRevitalizeShader);
        eraseMaterial = new Material(eraseRevitalizeShader);
        scorchedMaterial = GetComponent<MeshRenderer>().material;
        splatMap = new RenderTexture(512, 512, 0, RenderTextureFormat.ARGBFloat);
        scorchedMaterial.SetTexture("_Splat", splatMap);
    }
    public void RevitalizeArea(GameObject revitalizeObject, int brushSize, float strength = 1)
    {

        drawMaterial.SetFloat("_Strength", strength);
        drawMaterial.SetInt("_BrushSize", 100 / brushSize);
        RaycastHit hit;
        Debug.DrawRay(revitalizeObject.transform.position, Vector3.down);
        if (Physics.Raycast(revitalizeObject.transform.position, Vector3.down, out hit, layerMask))
        {
            drawMaterial.SetVector("_Coordinates", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
            RenderTexture tempTexture = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(splatMap, tempTexture);
            Graphics.Blit(tempTexture, splatMap, drawMaterial);
            RenderTexture.ReleaseTemporary(tempTexture);
        }
    }
    public void EraseFromSplatMap(GameObject revitalizeObject, int brushSize)
    {

        RaycastHit hit;
        eraseMaterial.SetFloat("_Strength", 0.1f);
        eraseMaterial.SetInt("_BrushSize", 100 / brushSize + 2);

        Debug.DrawRay(revitalizeObject.transform.position, Vector3.down);
        if (Physics.Raycast(revitalizeObject.transform.position, Vector3.down, out hit, layerMask))
        {
            
            eraseMaterial.SetVector("_Coordinates", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
            RenderTexture tempTexture = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(splatMap, tempTexture);
            Graphics.Blit(tempTexture, splatMap, eraseMaterial);
            RenderTexture.ReleaseTemporary(tempTexture);
        }
    }
}
