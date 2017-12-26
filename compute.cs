using System.Collections;
using System.Collections.Generic;
//using System.Runtime.InteropServices;
using UnityEngine;

public class compute : MonoBehaviour {
    /*[DllImport("dllproject", EntryPoint = "TestSort")]
    public static extern void TestSort(int[] a, int length);*/

    public Material material;
    public ComputeShader computeShader;
    ComputeBuffer buffer;
    public int CSMainIndex;
    public int count = 5000000;
    const float size = 5.0f;
   private const int  c_groupesize=128;
    private const int c_stride = 12;
    struct Vert
    {
      public Vector3 pos;
    };

 
  
 
    // Use this for initialization
    void Start()

    {

        // buffer=new ComputeBuffer(count,sizeof(float)*3,ComputeBufferType.Default);

        //  CSMainIndex = computeShader.FindKernel("CSMain");

        buffer = new ComputeBuffer(count, sizeof(float)*4);


        Vert[] points = new Vert[count];
        for (int i = 0; i < count; i++)

        {
               points[i].pos   =  Random.insideUnitSphere*3;
   
        }
        Debug.Log("" + points[3].pos);
        Debug.Log("" + points[4].pos);
        buffer.SetData(points);

    }

    void OnRenderObject()
    {  material.SetPass(0);
       material.SetBuffer("buffer", buffer);
         Graphics.DrawProcedural(MeshTopology.Points, count, 1);
    }
   
    private void OnDestroy()
    {
        if (buffer != null)
            buffer.Release();
         
    }
    // Update is called once per frame
    void Update () {
       computeShader.SetBuffer(computeShader.FindKernel("CSMain"), "buffer", buffer);
        //  computeShader.SetFloat(("deltaTime"),Time.deltaTime);
        int nbgroupes = Mathf.CeilToInt((float)count / c_groupesize);
       computeShader.Dispatch(computeShader.FindKernel("CSMain"), nbgroupes, 1, 1);
       
    }
}
