using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private Player _player;


    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Unable to find Player Animator");
        }
        _player = GetComponent<Player>();
        if (_anim == null)
        {
            Debug.LogError("Unable to find Player");
        }


    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _anim.SetBool("turnleft", true);
            _anim.SetBool("turnright", false);
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _anim.SetBool("turnleft", false);
            _anim.SetBool("turnright", false);

        }


        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _anim.SetBool("turnleft", false);
            _anim.SetBool("turnright", true);
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _anim.SetBool("turnleft", false);
            _anim.SetBool("turnright", false);
                    }




    }
}
