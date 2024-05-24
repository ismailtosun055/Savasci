using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class enemyAI : MonoBehaviour//bu da dusmanımız sizi gormesi ve takip etmesi ve saldirmasi
{
    public UnityEngine.Vector2 pos1;
    public UnityEngine.Vector2 pos2;
    public float sol_sag_hizi;
    float eski_konum;
    public float mesafe;
    private Transform target;
    public float followspeed;
    private Animator anim;
    enemycombat enemycombat;
    void Start()
    {
        Physics2D.queriesStartInColliders=false;//Çarpıştırıcıları algılamak veya algılamamak için Çarpıştırıcıların içinde başlayan ışın yayınlarını veya çizgi yayınlarını ayarlar.

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();//target=hedef demek.Ve burda biz tag'i player olanlari hedef alıyor.
        anim = GetComponent<Animator>();

        enemycombat=GetComponent<enemycombat>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAi();
    }

    void EnemyMove(){//belli bir konumda gidip gelmesi ve donmesi
        transform.position=UnityEngine.Vector3.Lerp(pos1,pos2,Mathf.PingPong(Time.time*sol_sag_hizi,1.0f));
        if(transform.position.x > eski_konum){
            transform.localRotation = UnityEngine.Quaternion.Euler(0,180,0); 
        }

        if(transform.position.x < eski_konum){
            transform.localRotation = UnityEngine.Quaternion.Euler(0,0,0); 
        }

        eski_konum = transform.position.x;
    }

    void EnemyAi(){//bir raycast ile ısık yollayıp dusmani tespit edip ona saldırmasi
        RaycastHit2D dusmana_saldir=Physics2D.Raycast(transform.position,-transform.right,mesafe);

        if(dusmana_saldir.collider != null){
            Debug.DrawLine(transform.position,dusmana_saldir.point, Color.red);
            anim.SetBool("attack",true);
            EnemyFollow();

            enemycombat.DamagePlayer();
        }
        else{
            Debug.DrawLine(transform.position,transform.position - transform.right*mesafe,Color.green);
            anim.SetBool("attack",false);
            EnemyMove();
        }
    }

    void EnemyFollow(){//dusmanin seni takip etmesi
        UnityEngine.Vector3 targetposition = new UnityEngine.Vector3(target.position.x,gameObject.transform.position.y,target.position.x);
        transform.position = UnityEngine.Vector2.MoveTowards(transform.position,targetposition,followspeed*Time.deltaTime);
    }

}
