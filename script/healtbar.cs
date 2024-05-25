using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healtbar : MonoBehaviour
{
    public Slider slider;//max ve min deger alarakarsında gecis yapar..
    public Gradient gradient;//Renkleri canlandırmak için kullanılır.
    public Image fill;

    public void SetMaxHealth(int health){
        slider.maxValue = health;
        slider.value = health; 

       fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health){
        slider.value = health; 

       fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    void Uptade(){
    }
}