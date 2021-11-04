using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

// this class will serve as the intermediary between the different maze generation algorithms as well as a layer of security
//all checks regarding the validity of passed vaues should be checked here.
public static class MazeGenerator
{
    public static MazeCell[,] GenerateRecusiveBacktrackerMaze(float width, float height)
    {
        if (width < 0 && height < 0)
        {
            return new MazeCell[Convert.ToUInt32(-width), Convert.ToUInt32(-height)];
        }
        return RecursiveBacktrackerGridAlgorithm.GenerateMaze(Convert.ToUInt32(width), Convert.ToUInt32(height));
        
    }
}
