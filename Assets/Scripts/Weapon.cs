using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public int damage;
    public float fireRate = 0;

    [Header("Componets")]

    public Transform muzzle;

    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private Transform bullet;

    private Player player;

    float timeToFire = 0;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void Update()
    {
        if (player.disablePlayer)
            return;

        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Fire();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Fire();
            }
        }
    }

    void Fire()
    {

        player.topTorso.Play("Shoot");
        GameController.Instance.GetComponent<AudioManager>().PlaySound("GunShot");

        Vector2 muzzlePos = new Vector2(muzzle.position.x, muzzle.position.y);

        SpawnBullet();

    }

    void SpawnBullet()
    {

        if (bullet == null)
            return;

        Transform shot = Instantiate(bullet, muzzle.position, muzzle.rotation);
        shot.GetComponent<Bullet>().damage = damage;

        Instantiate(muzzleFlash, muzzle.position, muzzle.rotation);

    }

}
