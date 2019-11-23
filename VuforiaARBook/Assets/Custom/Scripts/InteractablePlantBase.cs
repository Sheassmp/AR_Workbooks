using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InteractablePlantBase : Interactable
{

    public AnimationCurve saplingGrowth;
    public Vector3 saplingOnPlanted;
    public bool growing = false;
    public bool hasGrown;
    public GameObject fruitA, fruitB, fruitC, sparkle;
    public Vector3 onBranchRed, inBasket;
  
    public Text redPickText, yellowPickText, orangePickText;
    private GameObject[] fruitsList;
    private const string TAG = "Water";
    private String grow = "GROOOOOOOWW";

    // private List<GameObject> fruitsList = new List<GameObject>();
    private void Awake()
    {
        saplingOnPlanted = transform.localScale;
        sparkle.SetActive(false);
        fruitA.SetActive(false);
        fruitB.SetActive(false);
        fruitC.SetActive(false);

        // Add fruit to array and set inactive till the plant growth
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(redPickText != null){
        redPickText.text = "x " + ScoreCount.redFruitPicked.ToString();
        }
        if(yellowPickText != null){
        yellowPickText.text = "x " + ScoreCount.yellowFruitPicked.ToString();
        }
        if(orangePickText != null){
        orangePickText.text = "x " + ScoreCount.orangeFruitPicked.ToString();
        }
    }
    protected override void OnMouseDown()
    {
        Respond();
    }

    protected override void Respond()
    {
        if (hasGrown)
        {

            StartCoroutine(RipeFruit());
        }
    }

    private IEnumerator RipeFruit()
    {
        print("ripen");
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TAG)
        {
            hasGrown = true;
            StartCoroutine(Growth());
            sparkle.SetActive(true);   
            fruitA.SetActive(true);
            fruitB.SetActive(true);
            fruitC.SetActive(true);
        }
        GetComponentInChildren<InteractableFruit>()?.AudioTrigger.GetComponent<ScoreCount>().FruitGrow();
    }

    private IEnumerator Growth()
    {
        float time = 0;
        float duration = 1f;

        Vector3 saplingScaled = saplingOnPlanted * 1.5f;
        GetComponentInChildren<InteractableFruit>()?.AudioTrigger.GetComponent<ScoreCount>().PlantGrow();
        while (time < duration)
        {
            transform.localScale =
            Vector3.Lerp(
                saplingOnPlanted,
                saplingScaled,
                saplingGrowth.Evaluate(time / duration)
            );
            time += Time.deltaTime;

            yield return null;
        }
        growing = true;
    }
    // private void OnGUI()
    // {
    //     if (growing)
    //         GUI.Label(new Rect(0, 0, 100, 100), grow);
    // }
}
