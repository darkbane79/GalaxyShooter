using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerUpID;

    private AudioSource _powerUpSound;



    // Start is called before the first frame update
    void Start()
    {
        _powerUpSound = GameObject.Find("PowerUpSound").GetComponent<AudioSource>();
        if (_powerUpSound == null)
        {
            Debug.LogError("Unable to find PowerUpSound");

        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -7f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {


            switch (_powerUpID)
            {
                case 0:
                    Player.Instance.EnableTripleShot();
                    break;

                case 1:
                    Player.Instance.EnableSpeedUp();
                    break;

                case 2:
                    Player.Instance.EnableShields();
                    break;
                case 3:
                    Player.Instance.Reload();
                    break;
                case 4:
                    Player.Instance.RestoreLife();
                    break;
                case 5:
                    Player.Instance.EnableSpreadShot();
                    break;

                default:
                    Debug.Log("Null");
                    break;
            }

            _powerUpSound.Play();
            Destroy(this.gameObject);

        }
         
    }



    



}
