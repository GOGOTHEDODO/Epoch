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
        TOPLEFT,
        TOPRIGHT,
        BOTTOMLEFT,
        BOTTOMRIGHT,
        NONE,
    }

    [SerializeField] GameObject topWall;
    [SerializeField] GameObject rightWall;
    [SerializeField] GameObject bottomWall;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject topLeftCorner;
    [SerializeField] GameObject topRightCorner;
    [SerializeField] GameObject bottomLeftCorner;
    [SerializeField] GameObject bottomRightCorner;
    [SerializeField] GameObject brazier;
    [SerializeField] GameObject brazierSprite;
    [SerializeField] GameObject topLantern;
    [SerializeField] GameObject bottomLantern;
    [SerializeField] GameObject leftLantern;
    [SerializeField] GameObject rightLantern;
    [SerializeField] GameObject brazierLight;
    int lanternRatio = 10;

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

    public void SetBrazierActive(bool flag)
    {
        brazier.SetActive(flag);
        brazierSprite.SetActive(flag);
        brazierLight.SetActive(flag);
    }

    public void SetLanternActive(bool flag)
    {
        topLantern.SetActive(flag);
        bottomLantern.SetActive(flag);
        leftLantern.SetActive(flag);
        rightLantern.SetActive(flag);
    }

    public void SetLanternActiveRand(bool flag)
    {
        if (walls[Directions.TOP].activeSelf)
        {
            if (Random.Range(0, lanternRatio) == 1)
            {
                topLantern.SetActive(flag);  // Set lantern active or inactive depending on the flag.
            }
            else
            {
                topLantern.SetActive(false);  // Ensure lantern is deactivated if the wall is inactive.
            }
        }
        else
        {
            topLantern.SetActive(false);  // Ensure lantern is deactivated if the wall is inactive.
        }

        if (walls[Directions.BOTTOM].activeSelf)
        {
            if (Random.Range(0, lanternRatio) == 1)
            {
                bottomLantern.SetActive(flag);  // Set lantern active or inactive depending on the flag.
            }
            {
                bottomLantern.SetActive(false);  // Ensure lantern is deactivated if the wall is inactive.
            }
        }
        else
        {
            bottomLantern.SetActive(false);  // Ensure lantern is deactivated if the wall is inactive.
        }

        if (walls[Directions.LEFT].activeSelf)
        {
            if (Random.Range(0, lanternRatio) == 1)
            {
                leftLantern.SetActive(flag);  // Set lantern active or inactive depending on the flag.
            }
            else
            {
                leftLantern.SetActive(false);  // Ensure lantern is deactivated if the wall is inactive.
            }
        }
        else
        {
            leftLantern.SetActive(false);  // Ensure lantern is deactivated if the wall is inactive.
        }

        if (walls[Directions.RIGHT].activeSelf)
        {
            if (Random.Range(0, lanternRatio) == 1)
            {
                rightLantern.SetActive(flag);  // Set lantern active or inactive depending on the flag.
            }
            else
            {
                rightLantern.SetActive(false);  // Ensure lantern is deactivated if the wall is inactive.
            }
        }
        else
        {
            rightLantern.SetActive(false);  // Ensure lantern is deactivated if the wall is inactive.
        }
    }

    public void SetDirFlag(Directions dir, bool flag)
    {
        dirflags[dir] = flag;
        SetActive(dir, flag);
    }

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

    // Start is called before the first frame update
    void Awake()
    {
        walls[Directions.TOP] = topWall;
        walls[Directions.RIGHT] = rightWall;
        walls[Directions.BOTTOM] = bottomWall;
        walls[Directions.LEFT] = leftWall;
        walls[Directions.TOPLEFT] = topLeftCorner;
        walls[Directions.TOPRIGHT] = topRightCorner;
        walls[Directions.BOTTOMLEFT] = bottomLeftCorner;
        walls[Directions.BOTTOMRIGHT] = bottomRightCorner;
    }
    void Start()
    {
       // brazier.SetActive(false);
    }

}
