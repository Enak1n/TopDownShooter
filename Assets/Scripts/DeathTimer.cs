using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    [SerializeField] private float timer = 0.04f;
    private void Update()
    {
        Destroy(gameObject, timer);
    }
}
