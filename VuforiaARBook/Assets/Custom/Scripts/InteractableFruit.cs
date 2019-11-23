using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InteractableFruit : InteractablePlantBase
{
    private const string red = "RED";
    private const string yellow = "YELLOW";    
    private const string orange = "ORANGE";
    public GameObject AudioTrigger;

    public AnimationCurve valOverTime;
    protected override void OnMouseDown()
    {
        Respond();
    }

    protected override void Respond()
    {
        StartCoroutine(PickFruit());
    }

    private IEnumerator PickFruit()
    {
       
        if (gameObject.tag == red){
            print("<color=red>pickedRed</color>");
            ++ScoreCount.redFruitPicked;
            AudioTrigger.GetComponent<ScoreCount>().FruitPicked();
            fruitB.SetActive(false);
        }
        
        if (gameObject.tag == yellow){
            print("<color=yellow>pickedYellow</color>");
            ++ScoreCount.yellowFruitPicked;
            AudioTrigger.GetComponent<ScoreCount>().FruitPicked();
            fruitA.SetActive(false);
        } 
        
        if (gameObject.tag == orange){
            print("<color=orange>pickedOrange</color>");
            ++ScoreCount.orangeFruitPicked;
            AudioTrigger.GetComponent<ScoreCount>().FruitPicked();
            fruitC.SetActive(false);
        }

        //Check for win
        if (ScoreCount.RandomRedNeededFruit == ScoreCount.redFruitPicked
        && ScoreCount.RandomYellowNeededFruit == ScoreCount.yellowFruitPicked
        && ScoreCount.RandomOrangeNeededFruit == ScoreCount.orangeFruitPicked
        ) {
            GetMixing();
        //Check for lose
        } else if (ScoreCount.RandomRedNeededFruit < ScoreCount.redFruitPicked
        || ScoreCount.RandomYellowNeededFruit < ScoreCount.yellowFruitPicked
        || ScoreCount.RandomOrangeNeededFruit < ScoreCount.orangeFruitPicked) {
            TryAgain();
        }
        yield return null;
    }

    private void TryAgain()
    {
        print("you lose");
        SceneManager.LoadScene(2);
    }

    private void GetMixing()    
    {
        print("you win");
        SceneManager.LoadScene(3);
    }
}
