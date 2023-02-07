using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [SerializeField] private float timeBetweenWaves = 3f;
    [SerializeField] private float enemyMult; // this is to spawn more or less enemies per wave

    [Space]

    [SerializeField] private float radius = 5f;
    [SerializeField] private float innerRadius = 4f;
    [SerializeField] private Vector3 originPoint = Vector3.zero;

    [Header("Components")]

    [SerializeField] GameObject enemy;
    [SerializeField] Animator waveAnim;

    [Header("Debug")]

    [SerializeField] public int waveCount = 1;
    [SerializeField] private float checkInterval = 2;
    [SerializeField] private bool drawGizmos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {

        while (true)
        {

            yield return new WaitForSeconds(checkInterval); // wait abit so it isnt called every frame

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            // if 0 enemies spawn wave
            if (enemies.Length == 0)
            {

                waveAnim.Play("CountDown");
                GameController.Instance.WaveCount(waveCount);

                yield return new WaitForSeconds(timeBetweenWaves);

                SpawnEnemies();

            }

        }

    }

    private void SpawnEnemies()
    {

        for (int i = 0; i < waveCount * enemyMult; i++)
        {

            // create a spawn
            Vector2 spawnpos = new Vector2(Random.Range(0, radius), Random.Range(0, radius));

            while (Vector2.Distance(Vector2.zero, spawnpos) < innerRadius)
            {
                spawnpos = new Vector2(Random.Range(0, radius), Random.Range(0, radius));
            }

            // have a 50% chance to be below or to the left
            if (Random.Range(0, 100) > 50)
            {
                spawnpos = -spawnpos;
            }

            Instantiate(enemy, spawnpos, new Quaternion(0, 0, 0, 0));
        }

        waveCount++;

    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(originPoint, new Vector3(radius * 2, radius * 2, 0)); // outer

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(originPoint, new Vector3(innerRadius * 2, innerRadius * 2, 0)); // inner
        }
    }

}
