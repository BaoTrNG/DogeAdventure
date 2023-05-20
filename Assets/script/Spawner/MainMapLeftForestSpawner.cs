using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMapLeftForestSpawner : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    [SerializeField] Transform SpawnPoints;
    [SerializeField] int NumberOfEnemies;
    private List<Vector3> location = new List<Vector3>();
    private List<GameObject> enemies = new List<GameObject>();

    private float counter = 0f;
    private float timer = 2f;

    public Vector3 rotationForSpawner;

    void Start()
    {
        ModifiedLocation();
        SpawnEnemy();

    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= timer)
        {
            counter = 0f;
            CheckForRespawn();
        }
    }
    void ModifiedLocation()
    {
        for (int i = 0; i < NumberOfEnemies; i++)
        {
            location.Add(SpawnPoints.position);
        }
        if (NumberOfEnemies > 1) // only = 3 now not improved yet
        {
            location[1] = new Vector3(location[1].x - 3f, location[1].y, location[1].z - 3f);
            location[2] = new Vector3(location[2].x + 3f, location[2].y, location[2].z - 3f);
        }


    }
    void SpawnEnemy()
    {

        for (int i = 0; i < NumberOfEnemies; i++)
        {
            // enemies.Add(Instantiate(Enemy, location[i], Quaternion.Euler(new Vector3(0, 180, 0))));
            enemies.Add(Instantiate(Enemy, location[i], Quaternion.Euler(rotationForSpawner)));
        }
    }
    private void CheckForRespawn()
    {
        int count = 0;
        foreach (GameObject a in enemies)
        {
            if (a == null)
            {
                count++;
            }
        }
        if (count == NumberOfEnemies)
        {
            enemies.Clear();
            SpawnEnemy();
        }
        else return;
    }

    void EnemyTimer()
    {
        counter += Time.deltaTime;
        if (counter >= timer)
        {

            counter = 0f;
        }
    }
}
