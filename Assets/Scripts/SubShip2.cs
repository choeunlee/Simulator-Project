using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubShip2 : MonoBehaviour
{
  [SerializeField]
  private float moveSpeed = 10;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += (Vector3.back + new Vector3(0.2f,0,0)) * moveSpeed * Time.deltaTime;
    }
}
