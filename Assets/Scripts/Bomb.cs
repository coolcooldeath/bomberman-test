using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private bool exploded = false;
    public LayerMask levelMask;
    public GameObject explosion;

    void Start()
    {

        Invoke("Explode", 3f);
    }

    private IEnumerator CreateExplosions(Vector3 direction)
    {
        for (int i = 0; i < 64; i += 16)
        {



            if (Physics2D.OverlapCircle(transform.position + (i * direction) / 100, 0.01f, levelMask))
            {
                break;
            }
            else
            {
                Instantiate(explosion, transform.position + (i * direction) / 100,

                explosion.transform.rotation);

            }
            yield return new WaitForSeconds(.04f);
        }
    }

    void Explode()
    {

        Instantiate(explosion, transform.position, Quaternion.identity);
        StartCoroutine(CreateExplosions(Vector2.down));
        StartCoroutine(CreateExplosions(Vector2.right));
        StartCoroutine(CreateExplosions(Vector2.up));
        StartCoroutine(CreateExplosions(Vector2.left));
        GetComponent<SpriteRenderer>().enabled = false;
        exploded = true;
        Destroy(gameObject, .3f);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!exploded && other.gameObject.CompareTag("explosion"))
        {
            CancelInvoke("Explode");
            Explode();
        }
    }

}
