using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{   
    public static int redFruitPicked = 0;
    public static int yellowFruitPicked = 0;
    public static int orangeFruitPicked = 0;
    public static int RandomRedNeededFruit, RandomYellowNeededFruit, RandomOrangeNeededFruit;
    public Text needOrange, needRed, needYellow;
    public AudioSource audio;
    public AudioClip fruitPickSound, plantGrow, fruitGrow;
    


    private int lastNum;

    private void Awake()
    {
        redFruitPicked = 0;
        yellowFruitPicked = 0; 
        orangeFruitPicked = 0;
        
        audio.GetComponent<AudioSource>();
        RandomRedNeededFruit = GetRandom(1,10);
        RandomYellowNeededFruit = GetRandom(1, 10);
        RandomOrangeNeededFruit = GetRandom(1,10);

        needRed.text = "x" + RandomRedNeededFruit.ToString();
        needYellow.text = "x" + RandomYellowNeededFruit.ToString();
        needOrange.text = "x" + RandomOrangeNeededFruit.ToString();
    }

    int GetRandom(int min,int max)
    {
        int rand = Random.Range(min, max);
        while (rand == lastNum)
            rand = Random.Range(min, max);   
        lastNum = rand;
        return rand;
    }

    public void FruitPicked(){
        audio.PlayOneShot(fruitPickSound, 0.75f);
    }

    public void PlantGrow() {
        audio.PlayOneShot(plantGrow, 0.1f);
    }

    public void FruitGrow() {
        audio.PlayOneShot(fruitGrow, 0.5f);
    }
}
