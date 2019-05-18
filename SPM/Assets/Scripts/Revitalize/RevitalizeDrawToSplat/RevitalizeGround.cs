using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevitalizeGround : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    [Range(1,8)]
    private int brushSize;
    [SerializeField]
    [Range(0, 1)]
    private float brushIntensity;
    [SerializeField]
    private Shader drawRevitalizeShader;
    [SerializeField]
    private Shader eraseRevitalizeShader;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private List<GameObject> revitalizeObjects;
    private RenderTexture splatMap;
    private Material scorchedMaterial, drawMaterial, eraseMaterial;
    void Start()
    {
        drawMaterial = new Material(drawRevitalizeShader);
        drawMaterial.SetFloat("_Strength", brushIntensity);
        drawMaterial.SetInt("_BrushSize", 100 / brushSize);

        eraseMaterial = new Material(eraseRevitalizeShader);
        eraseMaterial.SetFloat("_Strength", 0.1f);
        eraseMaterial.SetInt("_BrushSize", 100 / brushSize + 2);

        scorchedMaterial = GetComponent<Terrain>().materialTemplate;
        splatMap = new RenderTexture(512, 512, 0, RenderTextureFormat.ARGBFloat);
        scorchedMaterial.SetTexture("_Splat", splatMap);
        DrawSplatOnAllPoints();

    }

    private void DrawSplatOnAllPoints(bool isErasing = false)
    {
        if (isErasing)
        {
            foreach (var item in revitalizeObjects)
            {
                EraseFromSplatMap(item);
            }
        }
        else
        {
            foreach (var item in revitalizeObjects)
            {
                RevitalizeArea(item);
            }
        }
    }


    private void RevitalizeArea(GameObject revitalizeObject)
    {
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            DrawSplatOnAllPoints(true);
        }
    }
    private void EraseFromSplatMap(GameObject revitalizeObject)
    {
        RaycastHit hit;
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
    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 256, 256), splatMap, ScaleMode.ScaleToFit,false,1);
    }

}
