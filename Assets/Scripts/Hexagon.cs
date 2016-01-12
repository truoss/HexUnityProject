using UnityEngine;
using System.Collections;

//Based of http://www.redblobgames.com/grids/hexagons/
public class Hexagon
{
    //mesh = CreateHexagonMesh2D(true, 1.0f);

    public static Mesh CreateHexagonMesh2D(bool isPointyTopped, float size)
    {
        //center + 6 edges + 6 corners
        Vector3 center = Vector3.zero;
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[7];
        int[] tris;
        Vector3[] normals = new Vector3[7];        
        Vector2[] uvs = new Vector2[vertices.Length];

        //vertices + normals
        vertices[0] = center;
        for (int i = 1; i < vertices.Length; i++)
        {
            vertices[i] = GetHexCorner(center, size, i, isPointyTopped);
            //Debug.LogWarning(vertices[i].ToString("f4"));
            normals[i] = -Vector3.forward;
        }

        //tris
        tris = new int[]
        {
            0,2,1,
            0,3,2,
            0,4,3,
            0,5,4,
            0,6,5,
            0,1,6,
        };

        int vIdx = 0;
        uvs = new Vector2[]
        {
            
            new Vector2((vertices[vIdx].x*0.5f)+0.5f,(vertices[vIdx++].z*0.5f)+0.5f),
            new Vector2((vertices[vIdx].x*0.5f)+0.5f,(vertices[vIdx++].z*0.5f)+0.5f),
            new Vector2((vertices[vIdx].x*0.5f)+0.5f,(vertices[vIdx++].z*0.5f)+0.5f),
            new Vector2((vertices[vIdx].x*0.5f)+0.5f,(vertices[vIdx++].z*0.5f)+0.5f),
            new Vector2((vertices[vIdx].x*0.5f)+0.5f,(vertices[vIdx++].z*0.5f)+0.5f),
            new Vector2((vertices[vIdx].x*0.5f)+0.5f,(vertices[vIdx++].z*0.5f)+0.5f),
            new Vector2((vertices[vIdx].x*0.5f)+0.5f,(vertices[vIdx++].z*0.5f)+0.5f),
        };

        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.normals = normals;
        mesh.uv = uvs;

        return mesh;
    }

    static Vector3 GetHexCorner(Vector3 center, float size, int i, bool isPointyTopped)
    {
        float angle_deg;
        float angle_rad;
        if (isPointyTopped)
        {
            angle_deg = 60 * i;
            angle_rad = Mathf.PI / 180 * angle_deg;
        }
        else
        {
            angle_deg = 60 * i + 30;
            angle_rad = Mathf.PI / 180 * angle_deg;
        }
        return new Vector3(center.x + size * Mathf.Cos(angle_rad), center.y, center.z + size * Mathf.Sin(angle_rad));
    }
}
