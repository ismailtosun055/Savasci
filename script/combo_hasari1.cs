using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combo_hasari1 : MonoBehaviour//yasounun verecegi hasari
{
    public Transform saldiri_yeri;
    public LayerMask enemy_layer;
    public float attack_menzili=0.5f;
    public int attack_hasari=10;

    public void DamageEnemy(){//yasounun nasil hasar verecegini acıklıyor.collider kontrol edilerek hasar verilir. 
        Collider2D[] dusmana_vurmak=Physics2D.OverlapCircleAll(saldiri_yeri.position,attack_menzili,enemy_layer);
        foreach(Collider2D enemy in dusmana_vurmak){
            enemy.GetComponent<enemy>().TakeDamage(attack_hasari);
        }
    }
    
    void OnDrawGizmosSelected(){
        if(saldiri_yeri == null){//yasounun hasari verecegi yer bir alan olarak dusun.
           return; 
        }
        Gizmos.DrawWireSphere(saldiri_yeri.position,attack_menzili);
    }
}
