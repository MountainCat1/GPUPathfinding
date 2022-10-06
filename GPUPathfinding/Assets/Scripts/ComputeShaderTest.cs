using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ComputeShaderTest : MonoBehaviour
{
    [SerializeField] private ComputeShader computeShader;
    [SerializeField] private RenderTexture renderTexture;

    private void Start()
    {
        renderTexture = new RenderTexture(256, 256, 24)
        {
            enableRandomWrite = true
        };
        renderTexture.Create();
        
        computeShader.SetTexture(0, "Result", renderTexture);
        computeShader.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);
    }
}