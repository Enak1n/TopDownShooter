using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int movSpd = 1;
    [SerializeField] private int bounceForce = 1;
    [SerializeField] private int damage = 10;

    [Space]
    public int rewardOnDeath = 10;

    GameObject player;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            return;

        FollowPlayer();
    }

    void FollowPlayer()
    {

        transform.up = player.transform.position - transform.position;

        rb.AddForce(transform.up * movSpd);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            rb.AddForce(transform.up * -bounceForce);
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (Random.Range(0, 100) > 50)
            {
                rb.AddForce(transform.right * Random.Range(0, -bounceForce));
            }
            else
            {
                rb.AddForce(transform.forward * Random.Range(0, -bounceForce));
            }
        }

    }
}
