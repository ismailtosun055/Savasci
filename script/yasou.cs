using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public class yasou : MonoBehaviour//yasounun hareketi ,ziplamasi...
{
    public float movespeed;
    bool donme=true;
    Animator anim;
    Rigidbody2D rb2d;
    float yatay_hareket;
    public float ziplama_gucu;
    bool yere_degdimi;
    bool iki_kere_ziplama;
    combo_hasari1 kombo_hasari;

    public bool characterattack;
    public float charactertimer;

    void Start()
    {
        yatay_hareket = UnityEngine.Input.GetAxis("Horizontal");
        anim=GetComponent<Animator>();
        rb2d=GetComponent<Rigidbody2D>();
        kombo_hasari=GetComponent<combo_hasari1>();
        charactertimer = 0.7f;
    }

    void Update()
    {
        CharacterMovement();
        CharacterAnimation();
        CharacterAttack();
        CharacterAttackRun();
        CharacteRjumping();
        CharacterAttackSpacing();
    }

    void CharacterMovement(){//karakterin hareketi
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

    void CharacterFlip(){//donmesi
        donme = !donme;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void CharacterAttack(){//saldirisi
        if(UnityEngine.Input.GetKeyDown(KeyCode.E) && yatay_hareket == 0){
            anim.SetTrigger("attack");
            if(characterattack)
            {
                kombo_hasari.DamageEnemy();
                characterattack = false;
            }
        }
    }

    void CharacterAttackRun(){//koşup saldırmasi
        if(UnityEngine.Input.GetKeyDown(KeyCode.E) && yatay_hareket != 0){
            anim.SetTrigger("attackrun");
               if(characterattack)
            {
                kombo_hasari.DamageEnemy();
                characterattack = false;
            }
        }
    }

    void CharacteRjumping(){//ziplamasi
        if(UnityEngine.Input.GetKeyDown(KeyCode.Space)){
            anim.SetBool("jump",true);

            if(yere_degdimi){
                rb2d.velocity=Vector2.up*ziplama_gucu;
                iki_kere_ziplama=true;
            }
            else if(iki_kere_ziplama){
                ziplama_gucu /= 1.5f;
                rb2d.velocity=Vector2.up*ziplama_gucu;

                iki_kere_ziplama=false;
                ziplama_gucu *= 1.5f;
            }
        }
    }

    void CharacterAttackSpacing()//karakterin hep vuramamasi bir bosluk olmasi
    {
        if(characterattack == false)
        {
            charactertimer -= Time.deltaTime;
        }
        if(charactertimer < 0)
        {
            charactertimer = 0;
        }
        if(charactertimer == 0)
        {
            characterattack = true;
            charactertimer = 0.7f;
        }
    }

    void OnCollisionEnter2D(Collision2D col){//yer kontrolu
        anim.SetBool("jump",false);

        if(col.gameObject.tag =="zemin"){
            yere_degdimi = true;
        }
    }

    void OnCollisionStay2D(Collision2D col){
        anim.SetBool("jump",false);

        if(col.gameObject.tag =="zemin"){
            yere_degdimi = true;
        }
    }

    void OnCollisionExit2D(Collision2D col){
        anim.SetBool("jump",true);

        if(col.gameObject.tag =="zemin"){
            yere_degdimi = false;
        }
    }
}