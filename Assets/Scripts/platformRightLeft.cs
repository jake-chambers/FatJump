using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformRightLeft : MonoBehaviour
{
    public GameObject player;
    playerScript playerS;

    int scoreThreshold = 0;

    public float rightLimit = 4f;
    public float leftLimit = -4f;

    float scoreLimit1 = 1f;
    float scoreLimit2 = 3f;

    private int direction = 1;
    Vector3 movement;

    // Update is called once per frame

    private void Start()
    {
        player = GameObject.Find("Gordo");
        playerS = player.GetComponent<playerScript>();

    }

    void Update()
    {
        if (playerS.score > scoreThreshold + 30)
        {
            scoreThreshold = playerS.score;
            scoreLimit1++;
            scoreLimit2++;
        }

        if (transform.position.x > rightLimit)
        {
            direction = -1;
        }
        else if (transform.position.x < leftLimit)
        {
            direction = 1;
        }
        float speed = Random.Range(scoreLimit1, scoreLimit2);
        movement = Vector3.right * direction * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
