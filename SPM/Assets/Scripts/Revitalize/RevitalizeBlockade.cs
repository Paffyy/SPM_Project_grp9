using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevitalizeBlockade : RevitalizeGeometry
{
    [SerializeField]
    private Collider blockCollider;
    [SerializeField]
    private Collider objectCollider;

    public override void Revitalize(float offset = 0)
    {
        // Magi kod för att ändra renderMode i play
        Material mat = GetComponent<MeshRenderer>().material;
        mat.SetFloat("_Mode", 2);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.DisableKeyword("_ALPHABLEND_ON");
        mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
        GetComponent<Animator>().SetBool("Revitalized", true);
        IsRevitalized = true;
        Destroy(gameObject, 4f);
    }

    public override void InstantRevitalize()
    {
        transform.parent.gameObject.SetActive(false);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void DisableCollider()
    {
        if(blockCollider != null && blockCollider.enabled)
        {
            blockCollider.enabled = false;
        }
        objectCollider.enabled = false;
    }
}
