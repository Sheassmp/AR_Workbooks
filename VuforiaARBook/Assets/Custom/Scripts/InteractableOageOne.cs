using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableOageOne : Interactable
{
    public GameObject pie;

    List<string> pieList;
    private Vector3 initialTransform;
    public AnimationCurve valueOverTime;
    private Coroutine obTransform;

    public void Awake()
    {
        pieList = new List<string>();
        initialTransform = transform.position;
    }

    protected override void OnMouseDown()
    {
        Respond();
    }

    protected override void Respond() 
    {
        if(obTransform == null)
            {
                obTransform = StartCoroutine(Divide());
            }
    }
    private IEnumerator Divide()
    {   
        float distance = 0;
        float duration = 0.5f;

        Vector3 movementV = initialTransform * 2;
        while (distance < duration){
            transform.position = Vector3.Lerp(initialTransform, movementV, valueOverTime.Evaluate(distance / duration));

            distance += Time.deltaTime;
            yield return null;
        }

        transform.position = initialTransform;
        obTransform = null;

    }
}

