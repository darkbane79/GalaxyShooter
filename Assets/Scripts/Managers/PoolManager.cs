using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private GameObject _bulletcontainer;

    [SerializeField]
    private List<GameObject> _bulletpool;
    



    [SerializeField]
    private GameObject _enemyLaserPrefab;

    [SerializeField]
    private List<GameObject> _enemyLaserpool;
    
    void Start()
    {
        _bulletpool = GenerateBullets(15);
        _enemyLaserpool = GenerateEnemyLaser(20);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //player laser
    List<GameObject> GenerateBullets(int amountBullets)
    {

        for (int i = 0; i < amountBullets; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab);
            bullet.transform.parent = _bulletcontainer.transform;
            bullet.SetActive(false);
            _bulletpool.Add(bullet);
        }

        return _bulletpool;
    }

    public GameObject RequestBullet()
    {
        foreach (GameObject bullet in _bulletpool)
        {
            if (bullet.activeInHierarchy == false)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }

        GameObject newbullet = Instantiate(_bulletPrefab);
        newbullet.transform.parent = _bulletcontainer.transform;

        _bulletpool.Add(newbullet);
        return newbullet;
    }
    
    List<GameObject> GenerateEnemyLaser(int amountBullets)
    {

        for (int i = 0; i < amountBullets; i++)
        {
            GameObject laser = Instantiate(_enemyLaserPrefab);
            laser.transform.parent = _bulletcontainer.transform;
            laser.SetActive(false);
            _enemyLaserpool.Add(laser);
        }

        return _enemyLaserpool;
    }

    public GameObject RequestEnemyLaser()
    {
        foreach (GameObject laser in _enemyLaserpool)
        {
            if (laser.activeInHierarchy == false)
            {
                laser.SetActive(true);
                return laser;
            }
        }

        GameObject newlaser = Instantiate(_enemyLaserPrefab);
        newlaser.transform.parent = _bulletcontainer.transform;

        _enemyLaserpool.Add(newlaser);
        return newlaser;
    }

    


}