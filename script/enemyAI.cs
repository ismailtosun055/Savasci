using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    public UnityEngine.Vector2 pos1;
    public UnityEngine.Vector2 pos2;
    public float sol_sag_hizi;
    float eski_konum;
    public float mesafe;
    private Transform target;
    public float followspeed;
    private Animator anim;
    
    void Start()
    {
        Physics2D.queriesStartInColliders=false;

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAi();
    }

    void EnemyMove(){
        transform.position=UnityEngine.Vector3.Lerp(pos1,pos2,Mathf.PingPong(Time.time*sol_sag_hizi,1.0f));
        if(transform.position.x > eski_konum){
            transform.localRotation = UnityEngine.Quaternion.Euler(0,180,0); 
        }

        if(transform.position.x < eski_konum){
            transform.localRotation = UnityEngine.Quaternion.Euler(0,0,0); 
        }

        eski_konum = transform.position.x;
    }

    void EnemyAi(){
        RaycastHit2D dusmana_saldir=Physics2D.Raycast(transform.position,-transform.right,mesafe);

        if(dusmana_saldir.collider != null){
            Debug.DrawLine(transform.position,dusmana_saldir.point, Color.red);
            anim.SetBool("attack",true);
            EnemyFollow();
        }
        else{
            Debug.DrawLine(transform.position,transform.position - transform.right*mesafe,Color.green);
            anim.SetBool("attack",false);
            EnemyMove();
        }
    }

    void EnemyFollow(){
        UnityEngine.Vector3 targetposition = new UnityEngine.Vector3(target.position.x,gameObject.transform.position.y,target.position.x);
        transform.position = UnityEngine.Vector2.MoveTowards(transform.position,targetposition,followspeed*Time.deltaTime);
    }

}
