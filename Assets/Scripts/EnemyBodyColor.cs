using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyColor : MonoBehaviour
{
    private Renderer rend;
    public Texture texture;
    private Texture startTexture;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startTexture = rend.material.GetTexture("_MainTex");
    }

    public void ChangeBodyColor()
    {
        rend.material.SetTexture("_MainTex", texture);
    }

    public void ReturnStartColor()
    {
        rend.material.SetTexture("_MainTex", startTexture);
    }
}
