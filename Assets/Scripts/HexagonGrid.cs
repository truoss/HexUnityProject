using UnityEngine;
using System.Collections;

public class HexagonGrid : MonoBehaviour
{
    public bool isPointyTopped = true;
    public float hexSize = 1;
    public Vector2 gridSize = new Vector2(3,2);
    public Material hexMaterial;

    public GameObject[,] Grid;

    [ContextMenu("ClearGrid")]
    public void ClearGrid()
    {
        if (Grid != null)
        {
            for (int x = 0; x < Grid.GetLength(0); x++)
            {
                for (int y = 0; y < Grid.GetLength(1); y++)
                {
                    DestroyImmediate(Grid[x, y]);
                }
            }
        }

        for (int i = 0; i < this.transform.childCount; i++)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    [ContextMenu("GenerateGrid")]
    public void GenerateGrid()
    {
        ClearGrid();

        Grid = CreateGrid(isPointyTopped, hexSize, gridSize);
    }

    public GameObject[,] CreateGrid(bool isPointyTopped, float hexSize, Vector2 gridSize)
    {
        GameObject[,] hexs = new GameObject[(int)gridSize.x , (int)gridSize.y];
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                var gObj = new GameObject(x + "," + y);
                gObj.transform.SetParent(this.transform);
                var meshfilter = gObj.AddComponent<MeshFilter>();
                var meshrenderer = gObj.AddComponent<MeshRenderer>();

                //Create Mesh
                meshfilter.mesh = Hexagon.CreateHexagonMesh2D(isPointyTopped, hexSize);

                //Create Material
                meshrenderer.material = hexMaterial;

                //position
                if (!isPointyTopped)
                {
                    float height = (hexSize * 2);
                    float vert = height * 0.75f;
                    float width = (Mathf.Sqrt(3) * 0.5f) * height;                                      

                    gObj.transform.position = new Vector3(width * x + width * 0.5f * (IsOdd(y) ? 1 : 0), 0, (height * 0.5f) * (y + 1) + height * 0.25f * y);
                }
                else
                {
                    float width = (hexSize * 2);
                    float vert = width * 0.75f;
                    float height = (Mathf.Sqrt(3) * 0.5f) * width;
                    float horiz = height;

                    gObj.transform.position = calcWorldCoords(new Vector2(x,y));
                }

                hexs[x, y] = gObj;     
            }
        }

        return hexs;
    }

    //Calculate first hexagon position
    Vector3 calcInitPos()
    {
        Vector3 initPos;
        float width = (hexSize * 2);
        float height = (Mathf.Sqrt(3) * 0.5f) * width;
        //Inital position left bottom corner
        initPos = new Vector3(-width * gridSize.x / 2f + width / 2, 0, -gridSize.y / 2f * height * 0.5f);
        return initPos;
    }

    //Convert Hex grids coordinates to world coordinates
    public Vector3 calcWorldCoords(Vector2 gridPos)
    {
        //Position of the first hex tile
        Vector3 initPos = calcInitPos();
        //Every second row is offset by half of the tile width
        float offset = 0;
        if (gridPos.y % 2 != 0)
        {
            offset = hexSize*1.5f;
        }

        float x = initPos.x + offset + gridPos.x * (hexSize * 2);
        //Every new line is offset in z direction by 3/4 of the hexagon height
        float z = initPos.z + gridPos.y * (hexSize * 2) * 0.25f;
        return new Vector3(x, 0, z);
    }

    public static bool IsOdd(int value)
    {
        return value % 2 != 0;
    }
}
