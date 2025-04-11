using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRoomGenerator : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;

    MazeRoom[,] rooms = null;

    [SerializeField] float roomWidth = 5;
    [SerializeField] float roomHeight = 5;

    [SerializeField] int numX = 10;
    [SerializeField] int numY = 10;

    [SerializeField] int offsetX = 0;
    [SerializeField] int offsetY = 0;

    [SerializeField] int centerRoomSize = 1;

    Stack<MazeRoom> stack = new Stack<MazeRoom>();

    bool generating = false;
 

    private void RemoveRoomWall(int x, int y, MazeRoom.Directions dir)
    {
        if (dir != MazeRoom.Directions.NONE)
        {
            rooms[x, y].SetDirFlag(dir, false);
        }

        MazeRoom.Directions opp = MazeRoom.Directions.NONE;

        switch (dir)
        {
            case MazeRoom.Directions.TOP:
                if (y < numY - 1)
                {
                    opp = MazeRoom.Directions.BOTTOM;
                    ++y;
                }
                break;
            case MazeRoom.Directions.RIGHT:
                if (x < numX - 1)
                {
                    opp = MazeRoom.Directions.LEFT;
                    ++x;
                }
                break;
            case MazeRoom.Directions.BOTTOM:
                if (y > 0)
                {
                    opp = MazeRoom.Directions.TOP;
                    --y;
                }
                break;
            case MazeRoom.Directions.LEFT:
                if (x > 0)
                {
                    opp = MazeRoom.Directions.RIGHT;
                    --x;
                }
                break;
        }
        if (opp != MazeRoom.Directions.NONE)
        {
            rooms[x, y].SetDirFlag(opp, false);
        }
    }

    public List<Tuple<MazeRoom.Directions, MazeRoom>> GetNeighboursNotVisited(int cx, int cy)
    {
        List<Tuple<MazeRoom.Directions, MazeRoom>> neighbours = new List<Tuple<MazeRoom.Directions, MazeRoom>>();
        foreach (MazeRoom.Directions dir in Enum.GetValues(typeof(MazeRoom.Directions)))
        {
            int x = cx;
            int y = cy;
            switch (dir)
            {
                case MazeRoom.Directions.TOP:
                    if (y < numY - 1)
                    {
                        ++y;
                        if (!rooms[x, y].visited)
                        {
                            neighbours.Add(new Tuple<MazeRoom.Directions, MazeRoom>(MazeRoom.Directions.TOP, rooms[x, y]));
                        }
                    }
                    break;
                case MazeRoom.Directions.RIGHT:
                    if (x < numX - 1)
                    {
                        ++x;
                        if (!rooms[x, y].visited)
                        {
                            neighbours.Add(new Tuple<MazeRoom.Directions, MazeRoom>(MazeRoom.Directions.RIGHT, rooms[x, y]));
                        }
                    }
                    break;
                case MazeRoom.Directions.BOTTOM:
                    if (y > 0)
                    {
                        --y;
                        if (!rooms[x, y].visited)
                        {
                            neighbours.Add(new Tuple<MazeRoom.Directions, MazeRoom>(MazeRoom.Directions.BOTTOM, rooms[x, y]));
                        }
                    }
                    break;
                case MazeRoom.Directions.LEFT:
                    if (x > 0)
                    {
                        --x;
                        if (!rooms[x, y].visited)
                        {
                            neighbours.Add(new Tuple<MazeRoom.Directions, MazeRoom>(MazeRoom.Directions.LEFT, rooms[x, y]));
                        }
                    }
                    break;
            }
        }
        return neighbours;
    }

    private bool GenerateStep()
    {
        if (stack.Count == 0) return true;

        MazeRoom r = stack.Peek();

        var neighbours = GetNeighboursNotVisited(r.Index.x, r.Index.y);

        if (neighbours.Count != 0)
        {
            var index = 0;
            if (neighbours.Count > 1)
            {
                index = UnityEngine.Random.Range(0, neighbours.Count);
            }
            var item = neighbours[index];
            MazeRoom neighbour = item.Item2;
            neighbour.visited = true;
            RemoveRoomWall(r.Index.x, r.Index.y, item.Item1);
            stack.Push(neighbour);
        }
        else
        {
            stack.Pop();
        }
        return false;
    }

    public void CreateMaze()
    {
        if (generating) return;
        Reset();

        //RemoveRoomWall(0, 0, MazeRoom.Directions.BOTTOM);
        //RemoveRoomWall(numX - 1, numY - 1, MazeRoom.Directions.RIGHT);

        if (offsetX == 0 && offsetY == 0)
        {
            
        }
        else if (offsetX == -1 && offsetY == 0)
        {
            
        }
        else if (offsetX == -1 && offsetY == -1)
        {
            
        }
        else if (offsetX == 0 && offsetY == -1)
        {
            
        }

        stack.Push(rooms[0, 0]);

        while (!GenerateStep()) { }

        if (offsetX == 0 && offsetY == 0)
        {
            for (int i = 0; i < centerRoomSize; i++)
            {
                for (int j = 0; j < centerRoomSize; j++)
                {
                    rooms[i, j].SetAllWallsInactive();
                }
            }
            rooms[numX - 1, numY - 1].SetBrazierActive(true);
        }
        else if (offsetX == -1 && offsetY == 0)
        {
            Debug.Log("INHERE");
            for (int i = numX - centerRoomSize; i < numX; i++)
            {
                for (int j = 0; j < centerRoomSize; j++)
                {
                    rooms[i, j].SetAllWallsInactive();
                }
            }
            rooms[0, numY - 1].SetBrazierActive(true);
        }
        else if (offsetX == -1 && offsetY == -1)
        {
            for (int i = numX - centerRoomSize; i < numX; i++)
            {
                for (int j = numY - centerRoomSize; j < numY; j++)
                {
                    rooms[i, j].SetAllWallsInactive();
                }
            }
            rooms[0, 0].SetBrazierActive(true);
        }
        else if (offsetX == 0 && offsetY == -1)
        {
            for (int i = 0; i < centerRoomSize; i++)
            {
                for (int j = numY - centerRoomSize; j < numY; j++)
                {
                    rooms[i, j].SetAllWallsInactive();
                }
            }
            rooms[numX - 1, 0].SetBrazierActive(true);
        }

        generating = false;
    }

    private void Reset()
    {
        for (int i = 0; i < numX; ++i)
        {
            for (int j = 0; j < numY; ++j)
            {
                rooms[i, j].SetDirFlag(MazeRoom.Directions.TOP, true);
                rooms[i, j].SetDirFlag(MazeRoom.Directions.RIGHT, true);
                rooms[i, j].SetDirFlag(MazeRoom.Directions.BOTTOM, true);
                rooms[i, j].SetDirFlag(MazeRoom.Directions.LEFT, true);
                rooms[i, j].visited = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rooms = new MazeRoom[numX, numY];

        for (int i = 0; i < numX; ++i)
        {
            for (int j = 0; j < numY; ++j)
            {
                GameObject room = Instantiate(roomPrefab, new Vector3((i * roomWidth) + (offsetX * roomWidth * numX), (j * roomHeight) + (offsetY * roomHeight * numY), 0.0f), Quaternion.identity);
               
                GameObject polygonObject = new GameObject("PolygonColliderGenerator_" + i + "_" + j);
                PolygonColliderGenerator polygon = polygonObject.AddComponent<PolygonColliderGenerator>();

                if (polygon != null)
                {
                    polygon.SetPoints(j * roomHeight + (offsetY * roomHeight * numY) + roomHeight * 0.4f,
                  (j * roomHeight) - roomHeight + (offsetY * roomHeight * numY) + roomHeight * 0.4f,
                  i * roomWidth + (offsetX * roomWidth * numX),
                  (i * roomWidth) - roomWidth + (offsetX * roomWidth * numX));
                }
                else
                {
                    Debug.LogError("PolygonColliderGenerator component could not be instantiated!");
                } 

                room.name = "Room_" + i.ToString() + "_" + j.ToString();
                rooms[i, j] = room.GetComponent<MazeRoom>();
                rooms[i, j].Index = new Vector2Int(i, j);
            }
        }

        for (int i = 0; i < numX; i++)
        {
            for (int j = 0; j < numY; j++)
            {
                rooms[i, j].SetBrazierActive(false);
            }
        }

        CreateMaze();

        for (int i = 0; i < numX; i++)
        {
            for (int j = 0; j < numY; j++)
            {
                rooms[i, j].SetLanternActiveRand(true);
            }
        }
    }
}
