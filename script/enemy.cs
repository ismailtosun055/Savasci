using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Animator anim;
    public int dusman_cani=100;
    int mevcut_saglik;
    enemyAI enemyai;

    void Start()
    {
        mevcut_saglik= dusman_cani;
        enemyai = GetComponent<enemyAI>();
        
    }

    public void TakeDamage(int damage){
        mevcut_saglik -= damage;

        anim.SetTrigger("hasar");
        if(mevcut_saglik <= 0){
            Die();
        }


    }
    
    void Die(){
        anim.SetBool("die",true);

        this.enabled=false;
        GetComponent<Collider2D>().enabled = false;
        enemyai.followspeed = 0;
        Destroy(gameObject,2f);
    }
}
