using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRoom : MonoBehaviour
{
    public enum Directions
    {
        TOP,
        RIGHT,
        BOTTOM,
        LEFT,
<<<<<<< Updated upstream
=======
        TOPLEFT,
        TOPRIGHT,
        BOTTOMLEFT,
        BOTTOMRIGHT,
>>>>>>> Stashed changes
        NONE,
    }

    [SerializeField] GameObject topWall;
    [SerializeField] GameObject rightWall;
    [SerializeField] GameObject bottomWall;
    [SerializeField] GameObject leftWall;
<<<<<<< Updated upstream
=======
    [SerializeField] GameObject topLeftCorner;
    [SerializeField] GameObject topRightCorner;
    [SerializeField] GameObject bottomLeftCorner;
    [SerializeField] GameObject bottomRightCorner;
    [SerializeField] GameObject brazier;
>>>>>>> Stashed changes

    Dictionary<Directions, GameObject> walls = new Dictionary<Directions, GameObject>();

    public Vector2Int Index
    {
        get;
        set;
    }

    public bool visited { get; set; } = false;

    Dictionary<Directions, bool> dirflags = new Dictionary<Directions, bool>();

    private void SetActive(Directions dir, bool flag)
    {
        walls[dir].SetActive(flag);
    }

<<<<<<< Updated upstream
=======
    public void SetBrazierActive(bool flag)
    {
        brazier.SetActive(flag);
    }

>>>>>>> Stashed changes
    public void SetDirFlag(Directions dir, bool flag)
    {
        dirflags[dir] = flag;
        SetActive(dir, flag);
    }

<<<<<<< Updated upstream
=======
    public void SetAllWallsInactive()
    {
        walls[Directions.TOP].SetActive(false);
        walls[Directions.BOTTOM].SetActive(false);
        walls[Directions.LEFT].SetActive(false);
        walls[Directions.RIGHT].SetActive(false);
        walls[Directions.TOPLEFT].SetActive(false);
        walls[Directions.TOPRIGHT].SetActive(false);
        walls[Directions.BOTTOMLEFT].SetActive(false);
        walls[Directions.BOTTOMRIGHT].SetActive(false);
    }

>>>>>>> Stashed changes
    // Start is called before the first frame update
    void Awake()
    {
        walls[Directions.TOP] = topWall;
        walls[Directions.RIGHT] = rightWall;
        walls[Directions.BOTTOM] = bottomWall;
        walls[Directions.LEFT] = leftWall;
<<<<<<< Updated upstream
=======
        walls[Directions.TOPLEFT] = topLeftCorner;
        walls[Directions.TOPRIGHT] = topRightCorner;
        walls[Directions.BOTTOMLEFT] = bottomLeftCorner;
        walls[Directions.BOTTOMRIGHT] = bottomRightCorner;
    }
    void Start()
    {
       // brazier.SetActive(false);
>>>>>>> Stashed changes
    }

}
