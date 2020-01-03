using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoSingleton<Player>
{
    private float _speed = 5;
    [SerializeField]
    private int _lives;

    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;

    [SerializeField]
    private bool _tripleShot = false;
    [SerializeField]
    private int _tripleShotTimeLeft;

    [SerializeField]
    private bool _speedUp = false;
    [SerializeField]
    private int _speedUpTimeLeft;

    private float _speedBoost = 2;

    [SerializeField]
    private GameObject _shields;
    [SerializeField]
    private bool _shieldactive = false;

    [SerializeField]
    private GameObject _leftDamage;
    [SerializeField]
    private GameObject _rightDamage;


    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private AudioClip _laserSound;
    private AudioSource _audioSource;


    void Start()
    {
        transform.position = new Vector3(0,-4.9f,0);
        _lives = 3;
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

    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            Shoot();
        }

    }

    void CalculateMovement()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);


        if (_speedUp == true)
        {
            transform.Translate(direction * _speed * _speedBoost * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }


                

        

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.9f, 0), 0);

        
        if(transform.position.x >= 11f)
        {
            transform.position = new Vector3(-11f, transform.position.y, 0);

        }
        else if (transform.position.x <= -11f)
        {
            transform.position = new Vector3(11f, transform.position.y, 0);

        }


    }

    void Shoot()
    {
        _canFire = Time.time + _fireRate;
        
        if (_tripleShot == true)
        {
            GameObject laser1 = PoolManager.Instance.RequestBullet();
            laser1.transform.position = transform.position + new Vector3(0, 1f, 0);
            laser1.transform.rotation = Quaternion.Euler(0, 0, 0);

            GameObject laser2 = PoolManager.Instance.RequestBullet();
            laser2.transform.position = transform.position + new Vector3(-0.96f, -0.42f, 0);
            laser2.transform.rotation = Quaternion.Euler(0, 0, 30);

            GameObject laser3 = PoolManager.Instance.RequestBullet();
            laser3.transform.position = transform.position + new Vector3(0.96f, -0.42f, 0);
            laser3.transform.rotation = Quaternion.Euler(0, 0, -30);

        }

        else 
        {
            GameObject laser = PoolManager.Instance.RequestBullet();
            laser.transform.position = transform.position + new Vector3(0, 1f, 0);
            laser.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        _audioSource.Play();
    }

    public void Damage()
    {
        if (_shieldactive == true)
        {
            _shieldactive = false;
            _shields.SetActive(false);
            return;
        }
        
        _lives--;

        UiManager.Instance.UpdateCurrentLives(_lives);

        if (_lives == 2)
        {
            _leftDamage.SetActive(true);
        }

        if (_lives == 1)
        {
            _rightDamage.SetActive(true);
        }



        if (_lives <= 0)
        {
            SpawnManager.Instance.OnPlayerDeath();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }
    


    }

    public void EnableTripleShot()
    {
        
        StartCoroutine(TripleShotCoolDown());
       


    }

    IEnumerator TripleShotCoolDown()
    {
        if (_tripleShot == true)
        {
            _tripleShotTimeLeft += 5;
        }



        if (_tripleShot == false)
        {
            _tripleShot = true;
            for (_tripleShotTimeLeft = 5; _tripleShotTimeLeft > 0; _tripleShotTimeLeft--)
            {
                yield return new WaitForSeconds(1f);

            }
            
            _tripleShot = false;
        }

    }
    
    public void EnableSpeedUp()
    {

        StartCoroutine(SpeedUpCoolDown());



    }

    IEnumerator SpeedUpCoolDown()
    {
        if (_speedUp == true)
        {
            _speedUpTimeLeft += 5;
        }

        if (_speedUp == false)
        {
            _speedUp = true;
            for (_speedUpTimeLeft = 5; _speedUpTimeLeft > 0; _speedUpTimeLeft--)
            {
                yield return new WaitForSeconds(1f);

            }
            _speedUp = false;
        }

    }

    public void EnableShields()
    {
        _shieldactive = true;
        _shields.SetActive(true);

                     

    }




}




