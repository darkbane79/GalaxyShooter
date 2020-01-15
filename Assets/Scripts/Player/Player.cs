using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



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
    private bool _spreadShot = false;
    [SerializeField]
    private int _spreadShotTimeLeft;


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
    private int _shieldStrength;
    [SerializeField]
    private SpriteRenderer _shieldColor;

    [SerializeField]
    private int _currentAmmo;
    [SerializeField]
    private int _maxAmmo = 15;



    [SerializeField]
    private GameObject _leftDamage;
    [SerializeField]
    private GameObject _rightDamage;


    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private AudioClip _laserSound;
    private AudioSource _audioSource;


    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private GameObject _thrusters;
    private bool _isOverHeat;
    [SerializeField]
    private GameObject _overHeatText;
    private bool _isThrustersOn = false;

    [SerializeField]
    private Transform camTransform;
    private Vector3 _originalPos;
    private float _shakeDuration = 0f;
    private float _shakeAmount = 0.2f;
    private float _decreaseFactor = 1.0f;


    void Start()
    {
        transform.position = new Vector3(0,-4.9f,0);
        _lives = 3;
        _originalPos = camTransform.localPosition;
        _currentAmmo = _maxAmmo;
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Unable to find Laser AudioSource");
        }
        else
        {
            _audioSource.clip = _laserSound;
        }

        _slider.value = 0;
        _isOverHeat = false;

    }

    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            Shoot();
        }

        if (_slider.value < 5f && _isOverHeat == false && _isThrustersOn ==false)
        {
            _slider.value += 1 * Time.deltaTime;
        }

        CamShake();



    }


    void CamShake()
    {
        if (_shakeDuration > 0)
        {
            camTransform.localPosition = _originalPos + Random.insideUnitSphere * _shakeAmount;

            _shakeDuration -= Time.deltaTime * _decreaseFactor;
        }
        else
        {
            _shakeDuration = 0f;
            camTransform.localPosition = _originalPos;
        }
    }


    IEnumerator OverHeat()
    {
        _isOverHeat = true;
        _overHeatText.SetActive(true);
        _speed = 5.0f;
        _isThrustersOn = false;
        _thrusters.SetActive(false);
        yield return new WaitForSeconds(5f);
        _isOverHeat = false;
        _overHeatText.SetActive(false);
    }
    

    void CalculateMovement()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);


        if (Input.GetKey(KeyCode.LeftShift) && _isOverHeat==false)
        {
            _speed = 8.0f;
            _slider.value -= 1 * Time.deltaTime;
            _isThrustersOn = true;
            _thrusters.SetActive(true);
            if (_slider.value == 0)
            {
                StartCoroutine(OverHeat());
            }


        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _speed = 5.0f;
            _isThrustersOn = false;
            _thrusters.SetActive(false);
        }

        


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
        _currentAmmo--;
        if (_tripleShot == true && _currentAmmo > 0)
        {
            GameObject laser1 = PoolManager.Instance.RequestBullet();
            laser1.transform.position = transform.position + new Vector3(0, 1f, 0);
            laser1.transform.rotation = Quaternion.Euler(0, 0, 0);

            GameObject laser2 = PoolManager.Instance.RequestBullet();
            laser2.transform.position = transform.position + new Vector3(-0.96f, -0.42f, 0);
            laser2.transform.rotation = Quaternion.Euler(0, 0, 0);

            GameObject laser3 = PoolManager.Instance.RequestBullet();
            laser3.transform.position = transform.position + new Vector3(0.96f, -0.42f, 0);
            laser3.transform.rotation = Quaternion.Euler(0, 0, 0);
            _audioSource.Play();

        }

        if (_spreadShot == true && _currentAmmo > 0)
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
            _audioSource.Play();

        }
        else if (_currentAmmo > 0)
        {
            GameObject laser = PoolManager.Instance.RequestBullet();
            laser.transform.position = transform.position + new Vector3(0, 1f, 0);
            laser.transform.rotation = Quaternion.Euler(0, 0, 0);
            _audioSource.Play();
        }

        else
        {
            _currentAmmo = 0;
        }

        UiManager.Instance.UpdateAmmo(_currentAmmo);


    }

    public void Damage()
    {
        if (_shieldactive == true)
        {
            _shieldStrength--;
            if (_shieldStrength == 2)
            {
                _shieldColor.color = Color.yellow;


            }

            if (_shieldStrength == 1)
            {
                _shieldColor.color = Color.red;
            }
            if (_shieldStrength == 0)
            {
                _shieldactive = false;
                _shields.SetActive(false);

            }

            return;
        }
        
        _lives--;
        _shakeDuration = 1f;
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
        _shieldColor.color = Color.white;
        _shieldStrength = 3;

    }

    public void Reload()
    {
        _currentAmmo = _maxAmmo;
        UiManager.Instance.UpdateAmmo(_currentAmmo);

    }

    public void RestoreLife()
    {
        _lives++;
        if (_lives > 3)
        {
            _lives = 3;
        }
        if (_lives == 3)
        {
            _leftDamage.SetActive(false);
            _rightDamage.SetActive(false);
        }

        if (_lives == 2)
        {
            _leftDamage.SetActive(true);
            _rightDamage.SetActive(false);
        }

        if (_lives == 1)
        {
            _leftDamage.SetActive(true);
            _rightDamage.SetActive(true);
        }

        UiManager.Instance.UpdateCurrentLives(_lives);

    }

    public void EnableSpreadShot()
    {

        StartCoroutine(SpreadShotCoolDown());



    }

    IEnumerator SpreadShotCoolDown()
    {
        if (_spreadShot == true)
        {
            _spreadShotTimeLeft += 5;
        }



        if (_spreadShot == false)
        {
            _spreadShot = true;
            for (_spreadShotTimeLeft = 5; _spreadShotTimeLeft > 0; _spreadShotTimeLeft--)
            {
                yield return new WaitForSeconds(1f);

            }

            _spreadShot = false;
        }

    }



}




