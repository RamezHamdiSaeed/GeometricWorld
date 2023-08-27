using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Start is called before the first frame update
    public float minX, maxX;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject,10f);
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.gravityScale = GameManager._inst.globalSpeed;
        
    }
}
