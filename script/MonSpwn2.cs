using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonSpwn2 : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monsterReference;

    private GameObject spawnedMonster;

    [SerializeField]
    private Transform leftPos, rightPos;

    private int randomIndex;
    private int randomSide;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(25,30));

            randomIndex = Random.Range(0, monsterReference.Length);
            randomSide = Random.Range(0, 2);

            spawnedMonster = Instantiate(monsterReference[randomIndex]);

            //left side
            if (randomSide == 0)
            {
                spawnedMonster.transform.position = leftPos.position;
                spawnedMonster.GetComponent<monster>().speed = 0.6f;
               // spawnedMonster.transform.localScale = new Vector3(-0.169278f, 0.17538f, 1f);
            }
          /*  else
            {
                //right side
                spawnedMonster.transform.position = rightPos.position;
                spawnedMonster.GetComponent<monster>().speed = -Random.Range(4, 10);

            }*/

        }
    }


}
