using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    private GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("being");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position  = playerObject.transform.position + new Vector3(0, 0, -10);
    }
}
