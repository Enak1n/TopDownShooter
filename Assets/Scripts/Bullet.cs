using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    public int speed = 50;

    public int timer = 1;

    public GameObject hitFX;
    private void Start()
    {
        Destroy(gameObject, timer);
    }

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Instantiate(hitFX, transform.position, transform.rotation);

            collision.GetComponent<Health>().TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
