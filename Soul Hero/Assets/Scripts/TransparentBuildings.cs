using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentBuildings : MonoBehaviour
{
    public bool lerpTransparent = false;
    private List<Material> materials;

    private void Start()
    {
        materials = new List<Material>();
        foreach (Material material in transform.GetComponent<Renderer>().materials)
        {
            materials.Add(material);
        }
        foreach (Material material in materials)
        {
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = -1;
            Color32 col = material.GetColor("_Color");
            col.a = 255;
            material.SetColor("_Color", col);
        }
    }

    private void SetTransparent()
    {
        foreach(Material material in materials)
        {
          //  material.SetFloat("_Mode", 3);
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;
        }
        lerpTransparent = true;
        lerp = 0f;
    }

    private void SetOpaque()
    {
        lerp = 0f;
        lerpTransparent = false;
        foreach (Material material in materials)
        {
         //   material.SetFloat("_Mode", 0);
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = -1;
            Color32 col = material.GetColor("_Color");
            col.a = 255;
            material.SetColor("_Color", col);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.PLAYER_TAG)
        {
            SetTransparent();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.PLAYER_TAG)
        {
            SetOpaque();
        }
    }
    private float alpha = 0;
    private float lerp = 0;
    private byte a;

    private void Update()
    {
        if (lerpTransparent)
        {
            foreach (Material material in materials)
            {
                Color32 col = material.GetColor("_Color");
                alpha = Mathf.Lerp(col.a, 50f, lerp);

                a = (byte)alpha;
                lerp += Time.deltaTime * 0.05f;
                col.a = a;
                material.SetColor("_Color", col);
            }
        }
    }
}
