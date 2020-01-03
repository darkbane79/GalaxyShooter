using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] _powerUp;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _powerUpContainer;


    [SerializeField]
    private bool _stopspawn = false;


    void Start()
    {
        
    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (_stopspawn == false)
        {
            float randomX = Random.Range(-9.2f, 9.2f);
            Vector3 spawnPosition = new Vector3(randomX, 6.5f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(4.0f);
        }

    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopspawn == false)
        {
            float randomX = Random.Range(-9.2f, 9.2f);
            float RandomTime = Random.Range(3, 8);
            int randomPU = Random.Range(0, 3);

            Vector3 spawnPosition = new Vector3(randomX, 6.5f, 0);
            


            GameObject newPowerUp = Instantiate(_powerUp[randomPU], new Vector3(randomX, 6.5f, 0), Quaternion.identity);
            newPowerUp.transform.parent = _powerUpContainer.transform;
            
            yield return new WaitForSeconds(RandomTime);
        }

    }


    public void OnPlayerDeath()
    {
        _stopspawn = true;
        UiManager.Instance.GameOver();
        
        
        var clones = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var clone in clones)
        {

            Destroy(clone, 2.2f);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"), 2.2f);

        }
    }




}
