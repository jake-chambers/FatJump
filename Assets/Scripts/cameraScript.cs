using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{

    public GameObject player;
    public GameObject platform;

    public float x;

    public float spawnY;
    public float lastSpawnLocation;
    public float lastPlayerHeight;



    private void Start()
    {

        player = GameObject.Find("Gordo");
        platformSpawner(4f);

    }
    void Update()
    {
        transform.position = new Vector3(x, player.transform.position.y + 2, -20);

        if (Input.GetKeyDown(KeyCode.UpArrow) && player.transform.position.y > lastSpawnLocation - 2)
        {
            platformSpawner(player.transform.position.y + 4);
        }

    }

    void platformSpawner(float spawn)
    {
        lastSpawnLocation = spawn;

        float randX = Random.Range(-5, 5);
        float randY = spawn;

        Vector2 spawnLocation = new Vector2(randX, randY);

        Instantiate(platform, spawnLocation, Quaternion.identity);

    }


}
