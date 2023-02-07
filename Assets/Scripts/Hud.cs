using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hud : MonoBehaviour
{

    [SerializeField] Image playerHp;

    [Space]

    public TextMeshProUGUI money_T;
    public TextMeshProUGUI waveCount_T;

    private Health player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health > 0)
        {
            playerHp.fillAmount = (float)player.health / player.maxHealth;
        }
        else
        {
            playerHp.fillAmount = 0;
        }

    }

}
