using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public float speed = 2.0f;
    public float rightLimit = 0.0f;
    public float leftLimit = -6.0f;
    private Vector2 direction = Vector2.left;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        if (transform.position.x > rightLimit)
        {
            direction = Vector2.left;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (transform.position.x < leftLimit)
        {
            direction = Vector2.right;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
