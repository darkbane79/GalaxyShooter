using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerUpID;  
    // Start is called before the first frame update
    void Start()
    {
        
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
                   
                    break;

                case 2:
                    
                    break;
                default:
                    Debug.Log("Null");
                    break;
            }

            Player.Instance.EnableTripleShot();
            Destroy(this.gameObject);

        }
         
    }



    



}
