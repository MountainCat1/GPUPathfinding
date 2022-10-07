using UnityEngine;

public struct Data
{
    public Vector3 position;
    public Color color;
}

public class ComputeShaderBufferTest : MonoBehaviour
{
    [SerializeField] private int count = 5;

    [SerializeField] private ComputeShader computeShader;
    
    private Data[] Datas;

    private void Start()
    {
        // Create data
        Datas = new Data[count * count];
        
        for (int i = 0; i < count * count; i++)
        {
            Datas[i] = new Data()
            {
                color = Color.blue,
                position = new Vector3(1, 0, 0)
            };
        }

        // Create buffer
        int size = sizeof(float) * 4 // color 
                   + sizeof(float) * 3; // position

        using var computeBuffer = new ComputeBuffer(Datas.Length, size);
        
        computeBuffer.SetData(Datas);

        // Dispatch compute shader
        computeShader.SetBuffer(0, "return_data", computeBuffer);
        computeShader.SetFloat("resolution", Datas.Length);
        
        computeShader.Dispatch(computeShader.FindKernel("cs_main"), Datas.Length / 10, 1, 1);

        // Get and use data
        computeBuffer.GetData(Datas);
        
        foreach (var data in Datas)
        {
            Debug.Log($"{data.color}  {data.position}");
        }
    }
}
