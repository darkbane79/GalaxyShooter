using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4.0f;
    private Animator _anim;
    private float _fireRate = 3.0f;
    private float _canFire = -1;
    [SerializeField]
    private GameObject _thruster;

   

    [SerializeField]
    private AudioClip _laserSound;
    private AudioSource _audioSource;

    private AudioSource _explodeSource;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Unable to find Animator");
        }

        _explodeSource = GameObject.Find("ExplosionSound").GetComponent<AudioSource>();
        if (_explodeSource == null)
        {
            Debug.LogError("Unable to find Explosion AudioSource");

        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Unable to find Laser AudioSource");
        }
        else
        {
            _audioSource.clip = _laserSound;
        }



    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        Shoot();

    }
    
    void Shoot()
    {
       
       if (Time.time > _canFire && _speed != 0) //laser fire 
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
           GameObject laser = PoolManager.Instance.RequestEnemyLaser();
           laser.transform.position = transform.position;
           _audioSource.Play();

        }
        
    

    }
    

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -6.5)
        {
            float randomX = Random.Range(-9.2f, 9.2f);
            transform.position = new Vector3(randomX,6.5f,0f);

        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player !=null)
            {
                player.Damage();
            }
            
            UiManager.Instance.UpdateScore(10);
            _speed = 0;
            _anim.SetTrigger("OnEnemyDeath");
            _explodeSource.Play();
            Destroy(_thruster.gameObject);
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
        }

        if (other.tag == "Laser")
        {
            other.gameObject.SetActive(false);

            UiManager.Instance.UpdateScore(10);
            _speed = 0;
            _anim.SetTrigger("OnEnemyDeath");
            _explodeSource.Play();
            Destroy(_thruster.gameObject);
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.2f);

        }

    }


}

