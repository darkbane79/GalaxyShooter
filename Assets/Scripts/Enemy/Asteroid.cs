using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private GameObject _explosionPrefab;





    // Start is called before the first frame update
    void Start()
    {

     
    }

    // Update is called once per frame
    void Update()
    {
        //rotat on z axis 3m per second
        _speed = 15.0f;
        transform.Rotate(0, 0, _speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {



        //IF hit LASER -- DESTORY LASER, DESTORY US
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            SpawnManager.Instance. StartSpawn();
            other.gameObject.SetActive(false);
            Destroy(this.gameObject, 0.25f);
 
        }
         


    }







}
