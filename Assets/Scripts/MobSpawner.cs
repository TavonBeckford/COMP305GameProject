using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{

    [SerializeField]

    private GameObject[] monsterReference;

    private GameObject spawnedMonster;

    [SerializeField]
    private int randomIndex;

    [SerializeField]
    private int spawnCount;
    private bool hasCollided = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&& !hasCollided)
        {
            StartCoroutine(SpawnMonsters());
            hasCollided = true;
        }
    }


    IEnumerator SpawnMonsters()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            yield return new WaitForSeconds(1);
            randomIndex = 0;
       
            spawnedMonster = Instantiate(monsterReference[randomIndex]);
            spawnedMonster.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); // to flip the monster orientation
        }
        //spawnedMonster = Instantiate(monsterReference[randomIndex]);
        //spawnedMonster.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); // to flip the monster orientation
        

    }
}
