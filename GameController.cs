using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Unity.VisualScripting;

public class GameController : Singleton<GameController>
{
    public int money;

    [Header("Upgrades")]

    [SerializeField] int maxHPPrice = 50;
    [SerializeField] int RegenPrice = 50;
    [SerializeField] int PistolDmgPrice = 50;
    [SerializeField] int PistolFRPrice = 50;

    [Header("Components")]

    [SerializeField] Hud hud;
    [SerializeField] GameObject shopMenu;
    [SerializeField] GameObject gameOverMenu;

    [Space]

    [SerializeField] TextMeshProUGUI MaxHpDesc;
    [SerializeField] TextMeshProUGUI RegenDesc;
    [SerializeField] TextMeshProUGUI DmgDesc;
    [SerializeField] TextMeshProUGUI FRDesc;

    [Header("Debug")]
    [SerializeField] private bool pause = false;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<AudioManager>().PlayerSound("BG");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            shopMenu.SetActive(!shopMenu.activeInHierarchy);

            UpdateShop();
            PauseGame();
        }

        hud.money_T.text = "$" + money;

    }

    public void GameOver()
    {
        PauseGame();
        gameOverMenu.GetComponent<Animator>().Play("Death");
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public void WaveCount(int number)
    {
        hud.waveCount_T.text = "Wave:" + number;
    }

    void PauseGame()
    {
        pause = !pause;

        if (pause)
        {
            Time.timeScale = 0;
            player.GetComponent<Player>().disablePlayer = true;
        }
        else
        {
            Time.timeScale = 1;
            player.GetComponent<Player>().disablePlayer = false;
        }
    }

    public bool CheckMoney(int price)
    {
        if (price > money)
            return false;
        money -= 50;

        return true;
    }

    public void UpdateShop()
    {
        MaxHpDesc.text = "Max HP: " + player.GetComponent<Health>().maxHealth;
        RegenDesc.text = "HP Regen: " + player.GetComponent<Health>().regen;
        DmgDesc.text = "Damage: " + player.GetComponent<Weapon>().damage;
        FRDesc.text = "FireRate: " + Math.Round(player.GetComponent<Weapon>().firerate, 2);
    }
    public void BuyMaxHealth()
    {
        if (!CheckMoney(maxHPPrice))
            return;
        player.GetComponent<Health>().maxHealth += 5;
        UpdateShop();
    }

    public void BuyRegen()
    {
        if (!CheckMoney(RegenPrice))
            return;
        player.GetComponent<Health>().regen += .1f;
        UpdateShop();
    }

    public void BuyPistolDmg()
    {
        if (!CheckMoney(PistolDmgPrice))
            return;
        player.GetComponent<Weapon>().damage += 1;
        UpdateShop();
    }

    public void BuyPistolFR()
    {
        if (!CheckMoney(PistolFRPrice))
            return;
        if (player.GetComponent<Weapon>().firerate > 0)
            player.GetComponent<Weapon>().firerate += .1f;
        else
            player.GetComponent<Weapon>().firerate += 4;

        UpdateShop();
    }

    public void Restart()
    {
        PauseGame();
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
