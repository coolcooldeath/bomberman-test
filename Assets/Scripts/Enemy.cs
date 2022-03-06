using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private RaycastHit2D hitY;
    private RaycastHit2D hitX;

    float directionX = -1;
    float directionY = -1;
    Vector3 pos;
    public Animator animator;
    private BoxCollider2D boxCollider2D;
    const float speedMove = 0.5f;
    GameObject player;
    private SpriteRenderer spriteRenderer;
    private System.Random random;
    private IEnumerator coroutine;
    public LayerMask layerMask;


    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        random = new System.Random();
        boxCollider2D = GetComponent<BoxCollider2D>();
        player = GameObject.FindWithTag("player");

    }




    void FixedUpdate()
    {

        if (directionY > 0)
            animator.SetBool("VerticalUpRun", true);
        else if (directionY < 0)
            animator.SetBool("VerticalDownRun", true);

        if (directionX > 0)
        {

            animator.SetBool("HorizontalRun", true);
        }

        else if (directionX < 0)
        {

            animator.SetBool("HorizontalRun", true);
        }





        pos = transform.position;
        hitY = Physics2D.Raycast(transform.position, new Vector2(0, directionY), 0.08f, layerMask);
        if (hitY.collider == null)
        {
            pos.y += Mathf.Sign(directionY) * speedMove * Time.deltaTime;

        }
        if (hitY.collider != null)
        {
            int x = random.Next(-1, 1);
            if (x != 0)
                directionY = x * directionY;



        }

        hitX = Physics2D.BoxCast(transform.position, new Vector2(0.08f, 0.08f), 0, new Vector2(directionX, 0), Mathf.Abs(directionX), layerMask);
        if (hitX.collider == null)

        {
            pos.x += Mathf.Sign(directionX) * speedMove * Time.deltaTime;

        }
        if (hitX.collider != null)
        {
            int x = random.Next(-1, 1);
            if (x != 0)
                directionX = x * directionX;

        }

        transform.position = pos;




    }

    IEnumerator Resurection()
    {


        yield return new WaitForSeconds(1f);
        pos.x = 1.36f;
        pos.y = 0.4f;
        transform.position = pos;
        spriteRenderer.enabled = true;
        coroutine = null;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("explosion"))
        {
            coroutine = Resurection();
            spriteRenderer.enabled = false;
            StartCoroutine(coroutine);


        }
    }
}
