using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a structured enum that contains basic information of the MazeType
public struct MazeType
{
    //returns all possible MazeTypes
    public static List<MazeType> GetAll()
    {
        return new List<MazeType>() { SIMPLE, LARGE };
        
    }

    private enum MazeTypeE
    {
        SIMPLE = 1,
        LARGE
    }

    //the values of the enum
    private readonly MazeTypeE value;
    public readonly string name;
    public readonly int minSize;
    public readonly int maxSize;
    public readonly int minZoom;
    public readonly int maxZoom;

    //The Enums, must be one for every MazeTypeE value.
    public static MazeType SIMPLE { get { return new MazeType(MazeTypeE.SIMPLE,"Simple Grid Maze",10,100,21,300); } }
    public static MazeType LARGE { get { return new MazeType(MazeTypeE.LARGE, "Large Grid Maze",100,250,21,700); } }

    private MazeType(MazeTypeE value, string name, int minSize, int maxSize, int minZoom,int maxZoom)
    {
        this.value = value;
        this.name = name;
        this.minSize = minSize;
        this.maxSize = maxSize;
        this.minZoom = minZoom;
        this.maxZoom = maxZoom;
    }

    public override string ToString()
    {
        return name;
    }
}
