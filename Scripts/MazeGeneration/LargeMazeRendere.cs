using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LargeMazeRendere : BaseMazeRenderer
{
    public static bool BeginMazeRendering = false;//maze will be rendered once this is enabled
    public static bool DestroyMaze = false;//maze will be rendered once this is enabled

    [SerializeField]
    private Transform wallPrefab = null;// the wall to be generated
    [SerializeField]
    private Transform SpawnPointPrefab = null;// the spawnpoint for the player
    [SerializeField]
    public Transform Player;
    //default values.
    private float size = 1f;
    private float scale = 3f;
    private float wallHeigh = 1f;


    public void DrawMaze(MazeCell[,] squareGrid)
    {

        uint width = (uint)squareGrid.GetLength(0);
        uint height = (uint)squareGrid.GetLength(1);

        Destroy(MazeField);
        MazeField = new GameObject();

        wallPrefab.localScale = new Vector3(scale * 1f, scale * wallHeigh, scale * 0.1f);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                MazeCell cell = squareGrid[i, j];
                Vector3 position = new Vector3(scale * (-width / 2 + i), 0, scale * (-height / 2 + j));

                DrawCell(cell, position,i,j);
            }
        }

        //create spawn point and spawn player.
        Transform SpawnPoint = Instantiate(SpawnPointPrefab, MazeField.transform);
        SpawnPoint.position = Vector3.zero;

        Player.position = SpawnPoint.position;
    }

    //draws the walls of a single cell
    private void DrawCell(MazeCell cell, Vector3 position,int i,int j)
    {
        if (cell.wallState.HasFlag(WallState.UP) && j == 0)
        {
            Transform topWall = Instantiate(wallPrefab, MazeField.transform) as Transform;
            topWall.position = position + new Vector3(0, 0, scale * -size / 2);
        }

        if (cell.wallState.HasFlag(WallState.RIGHT))
        {
            Transform rightWall = Instantiate(wallPrefab, MazeField.transform);
            rightWall.position = position + new Vector3(scale * size / 2, 0, 0);

            rightWall.eulerAngles = new Vector3(0, 90, 0);
        }


        //generate only if first row
        if (cell.wallState.HasFlag(WallState.DOWN))
        {
            Transform downWall = Instantiate(wallPrefab, MazeField.transform);
            downWall.position = position + new Vector3(0, 0, scale * size / 2);
        }

        if (cell.wallState.HasFlag(WallState.LEFT) && i == 0)
        {
            Transform leftWall = Instantiate(wallPrefab, MazeField.transform);
            leftWall.position = position + new Vector3(scale * -size / 2, 0, 0);
            leftWall.eulerAngles = new Vector3(0, 90, 0);
        }
    }
    void Update()
    {

        if (DestroyMaze)
        {
            Debug.Log("destroy large maze");
            DestroyMazeField();
            DestroyMaze = false;
        }
        if (BeginMazeRendering)
        {
            Debug.Log("generate large maze");
            BeginMazeRendering = false;
            DrawMaze(MazeGenerator.GenerateRecusiveBacktrackerMaze(widthSlider.value, heightSlider.value));

        }
    }
}
