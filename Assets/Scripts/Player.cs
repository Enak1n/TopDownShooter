using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private float _runSpeed;

    public Animator topTorso;
    public Animator botTorso;
    public bool disablePlayer;

    private float horizontal;
    private float vertical;

    private Rigidbody2D body;

    private bool isMoving = false;
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        MovePlayer();
        RotateBody();
        RotateFeet();
    }

    private void FixedUpdate()
    {
        body.AddForce(new Vector2(horizontal * _runSpeed, vertical * _runSpeed));
    }

    void MovePlayer()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    void RotateBody()
    {
        Vector3 difference;
        float distance = Vector3.Distance(gameObject.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        topTorso.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    void RotateFeet()
    {
        if (isMoving)
        {
            botTorso.SetBool("isMoving", isMoving);
            Vector2 v = body.velocity;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            botTorso.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (botTorso.transform.localEulerAngles.z > 90 && botTorso.transform.localEulerAngles.z < 270)
            {
                botTorso.transform.localScale = new Vector3(-1, -1, -1);
            }
            else
            {
                botTorso.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            botTorso.SetBool("isMoving", isMoving);
        }
    }
}
