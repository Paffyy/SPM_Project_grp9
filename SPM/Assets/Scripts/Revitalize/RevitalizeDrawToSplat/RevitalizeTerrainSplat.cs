using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevitalizeTerrainSplat : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Shader drawRevitalizeShader;
    [SerializeField]
    private Shader eraseRevitalizeShader;
    [SerializeField]
    private Texture drawTexture;
    [SerializeField]
    private Camera _camera;
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
    public void RevitalizeArea(GameObject revitalizeObject, int brushSize, Color col, float strength = 1)
    {
        RaycastHit hit;
        drawMaterial.SetFloat("_Strength", strength);
        drawMaterial.SetColor("_Color", col);
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
    public void EraseFromSplatMap(GameObject revitalizeObject, int brushSize, Color col, float strength = 1)
    {
        RaycastHit hit;
        eraseMaterial.SetFloat("_Strength", strength);
        eraseMaterial.SetColor("_Color", col);
        eraseMaterial.SetInt("_BrushSize", 100 / brushSize);
        if (Physics.Raycast(revitalizeObject.transform.position, Vector3.down, out hit, layerMask))
        {
            eraseMaterial.SetVector("_Coordinates", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
            RenderTexture tempTexture = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(tempTexture, splatMap, eraseMaterial);
            Graphics.Blit(splatMap, tempTexture);
            RenderTexture.ReleaseTemporary(tempTexture);
            scorchedMaterial.SetTexture("_Control", splatMap);
        }
    }
    public void DrawWithMouse(Color col,int brushSize, float strength = 1)
    {
        RaycastHit hit;
        eraseMaterial.SetFloat("_Strength", strength);
        eraseMaterial.SetColor("_Color", col);
        eraseMaterial.SetInt("_BrushSize", 100 / brushSize);
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 500, layerMask))
        {
            eraseMaterial.SetVector("_Coordinates", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
            RenderTexture tempTexture = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(splatMap, tempTexture, eraseMaterial);
            Graphics.Blit(tempTexture, splatMap);
            RenderTexture.ReleaseTemporary(tempTexture);
            scorchedMaterial.SetTexture("_Control", splatMap);
        }
    }
    public void DrawWithMouse2(Color col, int brushSize, float strength = 1)
    {
        RaycastHit hit;
        drawMaterial.SetFloat("_Strength", strength);
        drawMaterial.SetColor("_Color", col);
        drawMaterial.SetInt("_BrushSize", 100 / brushSize);
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 500, layerMask))
        {
            drawMaterial.SetVector("_Coordinates", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
            RenderTexture tempTexture = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(splatMap, tempTexture, drawMaterial);
            Graphics.Blit(tempTexture, splatMap);
            RenderTexture.ReleaseTemporary(tempTexture);
            scorchedMaterial.SetTexture("_Control", splatMap);
        }
    }


    public void SetSplat()
    {
        drawTexture = scorchedMaterial.GetTexture("_Control");
    }
    public void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 256, 256), splatMap, ScaleMode.ScaleToFit, false, 1);
    }
}

