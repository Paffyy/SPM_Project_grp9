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
    private void Awake()
    {
        drawMaterial = new Material(drawRevitalizeShader); 
        eraseMaterial = new Material(eraseRevitalizeShader);
        scorchedMaterial = GetComponent<MeshRenderer>().material;
        splatMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(scorchedMaterial.GetTexture("_Control"), splatMap);
    }
    public void RevitalizeArea(GameObject revitalizeObject, Color col, int brushSize, float strength = 1)
    {
        RaycastHit hit;
        drawMaterial.SetFloat("_Strength", strength);
        eraseMaterial.SetColor("_Color", col);
        drawMaterial.SetInt("_BrushSize", 100 / brushSize);
        if (Physics.Raycast(revitalizeObject.transform.position, Vector3.down, out hit, layerMask))
        {
            drawMaterial.SetVector("_Coordinates", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
            RenderTexture tempTexture = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(splatMap, tempTexture);
            Graphics.Blit(tempTexture, splatMap, drawMaterial);
            RenderTexture.ReleaseTemporary(tempTexture);
            scorchedMaterial.SetTexture("_Control", splatMap);
        }
    }
    public void EraseFromSplatMap(GameObject revitalizeObject, Color col, int brushSize, float strength = 1)
    {
        RaycastHit hit;
        eraseMaterial.SetFloat("_Strength", 0.1f);
        eraseMaterial.SetColor("_Color", col);
        eraseMaterial.SetInt("_BrushSize", 100 / brushSize + 2);
        if (Physics.Raycast(revitalizeObject.transform.position, Vector3.down, out hit, layerMask))
        {
            eraseMaterial.SetVector("_Coordinates", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
            RenderTexture tempTexture = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(tempTexture, splatMap, eraseMaterial);
            Graphics.Blit(splatMap, tempTexture);
            RenderTexture.ReleaseTemporary(tempTexture);
        }
    }
}
