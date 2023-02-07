using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float maxHealth = 100;
    public float health = 100;

    public float regen = 0;
    public float regenRate = 1;

    bool isDead = false;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        animator = GetComponent<Animator>();

        StartCoroutine(HpRegen());
    }

    public void TakeDamage(int damage)
    {

        health -= damage;

        if (gameObject.tag == "Player")
        {
            GameController.Instance.GetComponent<AudioManager>().PlaySound("Hurt");
        }

        if (health < 0 && !isDead) // can only die one
        {
            isDead = true;

            if (gameObject.tag == "Player")
            {
                GameController.Instance.GameOver();
            }
            else // AI
            {
                GameController.Instance.GetComponent<AudioManager>().PlaySound("BeeDeath");
                animator.Play("Death");
                GameController.Instance.AddMoney(GetComponent<Enemy>().rewardOnDeath);
            }
        }

    }

    void Die()
    {

        Destroy(gameObject);

    }

    IEnumerator HpRegen()
    {

        while (true)
        {
            yield return new WaitForSeconds(regenRate);

            if (health < maxHealth)
            {
                health += regen;
            }

        }
    }
}
