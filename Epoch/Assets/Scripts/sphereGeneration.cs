using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereGeneration : MonoBehaviour
{
    [SerializeField] private float BASE_SPEED = 20.0f;
    public SpriteRenderer spriteRenderer;
    private GameObject playerObject;
    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("being");
        Debug.Log(Input.mousePosition);
        Debug.Log(Input.mousePosition.normalized);
        Debug.Log(playerObject.transform.localPosition);
        dir = (Input.mousePosition - new Vector3(482.5f, 285.0f, 0)).normalized;
        Debug.Log(dir);
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.spriteRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {   
        transform.position += dir * BASE_SPEED * Time.deltaTime; 
    }
}
