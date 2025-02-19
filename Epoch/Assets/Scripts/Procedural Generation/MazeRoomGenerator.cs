using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRoomGenerator : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;

    MazeRoom[,] rooms = null;

    float roomWidth;
    float roomHeight;

    [SerializeField] int numX = 10;
    [SerializeField] int numY = 10;

    Stack<MazeRoom> stack = new Stack<MazeRoom>();

    bool generating = false;

    private void GetRoomSize()
    {
        SpriteRenderer[] spriteRenderers = roomPrefab.GetComponentsInChildren<SpriteRenderer>();

        Vector3 minBounds = Vector3.positiveInfinity;
        Vector3 maxBounds = Vector3.negativeInfinity;

        foreach (SpriteRenderer ren in spriteRenderers)
        {
            minBounds = Vector3.Min(minBounds, ren.bounds.min);

            maxBounds = Vector3.Max(maxBounds, ren.bounds.max);
        }

        roomWidth = maxBounds.x - minBounds.x;
        roomHeight = maxBounds.y - minBounds.y;
    }

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

        RemoveRoomWall(0, 0, MazeRoom.Directions.BOTTOM);
        RemoveRoomWall(numX - 1, numY - 1, MazeRoom.Directions.RIGHT);

        stack.Push(rooms[0, 0]);

        while (!GenerateStep()) { }

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
        GetRoomSize();

        rooms = new MazeRoom[numX, numY];

        for (int i = 0; i < numX; ++i)
        {
            for (int j = 0; j < numY; ++j)
            {
                GameObject room = Instantiate(roomPrefab, new Vector3(i * roomWidth, j * roomHeight, 0.0f), Quaternion.identity);

                room.name = "Room_" + i.ToString() + "_" + j.ToString();
                rooms[i, j] = room.GetComponent<MazeRoom>();
                rooms[i, j].Index = new Vector2Int(i, j);
            }
        }

        CreateMaze();
    }
}
