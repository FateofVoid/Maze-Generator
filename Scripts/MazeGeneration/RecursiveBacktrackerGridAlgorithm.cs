using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

//this class contains the algorithms to generate a simple maze
public class RecursiveBacktrackerGridAlgorithm : MonoBehaviour
{
    // this method is called externaly to get new Maze
    public static MazeCell[,] GenerateMaze(uint width, uint height)
    {
        MazeCell[,] squareGrid = new MazeCell[width, height];
        MazeCell initial = new MazeCell { wallState =  WallState.LEFT | WallState.RIGHT | WallState.UP | WallState.DOWN, visited = false };
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                squareGrid[i, j] = initial;
            }
        }

        return ApplyRecursiveBacktracker(squareGrid, width, height);
    }

    // this is the algorithm that generates the maze
    private static MazeCell[,] ApplyRecursiveBacktracker(MazeCell[,] squareGrid, uint width, uint height)
    {
        // selects a random point in the grid to begin generating maze
        Random rnd = new Random();
        Stack<Position> positionStack = new Stack<Position>();
        Position position = new Position { X = rnd.Next(0, (int)width), Y = rnd.Next(0, (int)height) };
        
        //exit point is defined here
        squareGrid[rnd.Next(0, (int)width), (int)height - 1].wallState &= ~WallState.DOWN;

        squareGrid[position.X, position.Y].visited = true;
        positionStack.Push(position);

        while (positionStack.Count > 0)
        {
            //gets all neightbour
            Position current = positionStack.Pop();
            List<Neighbour> neighbours = GetUnvisitedNeighbours(current, squareGrid, width, height);

            
            if (neighbours.Count > 0)
            {
                //select random neighbour
                int rndIndex = rnd.Next(0, neighbours.Count);
                Neighbour rndNeighbour = neighbours[rndIndex];

                positionStack.Push(current);

                // delete wall between them and sets new cell as visited
                Position nPosition = rndNeighbour.NPosition;
                squareGrid[current.X, current.Y].wallState &= ~rndNeighbour.SharedWall;
                squareGrid[nPosition.X, nPosition.Y].wallState &= ~GetOppositeWall(rndNeighbour.SharedWall);
                squareGrid[nPosition.X, nPosition.Y].visited = true;
                
                positionStack.Push(nPosition);
            }
        }

        //returns generated Maze
        return squareGrid;
    }

    //gets opposite wall to be deleted
    public static WallState GetOppositeWall(WallState wall)
    {
        switch (wall)
        {
            case WallState.RIGHT: return WallState.LEFT;
            case WallState.LEFT: return WallState.RIGHT;
            case WallState.UP: return WallState.DOWN;
            case WallState.DOWN: return WallState.UP;
            default: return WallState.UP;
        }
    }

    // gets all unvisited neighbours
    private static List<Neighbour> GetUnvisitedNeighbours(Position p, MazeCell[,] squareGrid, uint width, uint height)
    {
        List<Neighbour> list = new List<Neighbour>();

        if (p.X > 0) //left
        {
            if (!squareGrid[p.X - 1, p.Y].visited)
            {
                list.Add(new Neighbour
                {
                    NPosition = new Position
                    {
                        X = p.X - 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.LEFT
                });
            }
        }

        if (p.X < width - 1) //right
        {
            if (!squareGrid[p.X + 1, p.Y].visited)
            {
                list.Add(new Neighbour
                {
                    NPosition = new Position
                    {
                        X = p.X + 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.RIGHT
                });
            }
        }

        if (p.Y < height - 1) //down
        {
            if (!squareGrid[p.X, p.Y + 1].visited)
            {
                list.Add(new Neighbour
                {
                    NPosition = new Position
                    {
                        X = p.X,
                        Y = p.Y + 1
                    },
                    SharedWall = WallState.DOWN
                });
            }
        }

        if (p.Y > 0) //up
        {
            if (!squareGrid[p.X, p.Y - 1].visited)
            {
                list.Add(new Neighbour
                {
                    NPosition = new Position
                    {
                        X = p.X,
                        Y = p.Y - 1
                    },
                    SharedWall = WallState.UP
                });
            }
        }

        return list;
    }
}
