using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public Animator animator;
    public GameObject bulletPrefab;
    public GameObject endGame;
    public int speed = 5;
    public int bulletSpeed = 7;
    public float h_move;
    public int jump = 18;
    public int health = 3;
    public TextMeshProUGUI value;
    public bool isGrounded;
    public bool isOnFloor;
    public bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        endGame.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //horizontal move
        h_move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(speed*h_move, rb.velocity.y);
        if (isFacingRight == true && h_move == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isFacingRight = false;
        }
        if (isFacingRight == false && h_move == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            isFacingRight = true;
        }

        //jump
        if ((isGrounded || isOnFloor) && Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            isGrounded = false;
            isOnFloor = false;
            bc.isTrigger = true;
            StartCoroutine(wait());
        }

        //go down
        if (isOnFloor && Input.GetKeyDown(KeyCode.DownArrow))
        {
            bc.isTrigger = true;
            StartCoroutine(wait());
        }

        //shoot
        if(Input.GetKeyDown(KeyCode.Space)) {
            if (isFacingRight == true)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position + Vector3.right * 0.4f + Vector3.up * 0.1f, transform.rotation);
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                bulletRb.isKinematic = true;
                bulletRb.velocity = transform.rotation * Vector2.right * bulletSpeed;
            } else {
                GameObject bullet = Instantiate(bulletPrefab, transform.position + Vector3.left * 0.4f + Vector3.up * 0.1f, transform.rotation);
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                bulletRb.isKinematic = true;
                bulletRb.velocity = transform.rotation * Vector2.left * bulletSpeed;
            }
        }

        //animation
        animator.SetFloat("move", Mathf.Abs(h_move));

        //health
        value.text = health.ToString();
        if (health == 0)
        {
            Time.timeScale = 0;
            endGame.SetActive(true);
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.7f);
        bc.isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Floor")
        {
            isOnFloor = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Floor")
        {
            isOnFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
        if (collision.gameObject.tag == "Floor")
        {
            isOnFloor = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Enemy")
        {
            health -= 1;
        }
        if (collision.gameObject.tag == "Heart")
        {
            health += 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Portal")
        {
            Time.timeScale = 0;
            endGame.SetActive(true);
        }
    }    
}
