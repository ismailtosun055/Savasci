using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public class yasou : MonoBehaviour
{
    public float movespeed;
    bool donme=true;
    Animator anim;
    Rigidbody2D rb2d;
    float yatay_hareket;
    void Start()
    {
        yatay_hareket = UnityEngine.Input.GetAxis("Horizontal");
        anim=GetComponent<Animator>();
        rb2d=GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CharacterMovement();
        CharacterAnimation();
        CharacterAttack();
        CharacterAttackRun();
    }

    void CharacterMovement(){
        yatay_hareket = UnityEngine.Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(yatay_hareket*movespeed,rb2d.velocity.y);
    }

    void CharacterAnimation(){
        if(yatay_hareket > 0){
            anim.SetBool("run",true);
        }
        else if(yatay_hareket < 0){
            anim.SetBool("run",true);
        }
        else anim.SetBool("run",false);

        if(donme==false && yatay_hareket > 0){
            CharacterFlip();
        }
        if(donme==true && yatay_hareket < 0){
            CharacterFlip();
        }
    }

    void CharacterFlip(){
        donme = !donme;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void CharacterAttack(){
        if(UnityEngine.Input.GetKeyDown(KeyCode.E) && yatay_hareket == 0){
            anim.SetTrigger("attack");
        }
    }

    void CharacterAttackRun(){
        if(UnityEngine.Input.GetKeyDown(KeyCode.E) && yatay_hareket != 0){
            anim.SetTrigger("attackrun");
        }
    }
}
