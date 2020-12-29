using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class pixelatte : MonoBehaviour
{
    public Material EffectMaterial;

    // Use this for initialization 
    void Start() {
        Debug.Log("start");
    }


    // Update is called once per frame
    void Update() {

    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, EffectMaterial);
        Debug.Log("onrenderimageisworking");
    }
}