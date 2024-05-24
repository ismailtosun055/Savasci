using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour//burada dusmanin canı nasil azaltılıyor ve oldurulmesi.
{
    public Animator anim;
    public int dusman_cani=100;
    int mevcut_saglik;
    enemyAI enemyai;

    void Start()
    {
        mevcut_saglik = dusman_cani;
        enemyai = GetComponent<enemyAI>();
        
    }

    public void TakeDamage(int damage){//dusmanin hasar almasından bahsediyor
        mevcut_saglik -= damage;

        anim.SetTrigger("hasar");

        if(mevcut_saglik <= 0){
            Die();
        }


    }
    
    void Die(){//dusmanin olmesi ve bedenin kaybolmasi
        anim.SetBool("die",true);
        enemyai.followspeed = 0.0f;

        this.enabled=false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject,2f);
    }
}
