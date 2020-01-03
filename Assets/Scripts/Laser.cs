using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    
    [SerializeField]
    private float _speed = 8.0f;
    
    
    private void Start()
    {
    
    }
    void OnEnable()
    {
        StartCoroutine(Hide());

  
    }


    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime, Space.Self);
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(3.0f);
        this.gameObject.SetActive(false);
    }




    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && this.tag == "EnemyLaser")
        {
            //damage player
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            this.gameObject.SetActive(false);
        }

    }
}
