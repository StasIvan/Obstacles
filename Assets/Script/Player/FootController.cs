using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootController : MonoBehaviour
{
    [SerializeField] FootAnimation Controller;
    [SerializeField] FootAnimation Controller2;
    
    private void Start()
    {
        StartCoroutine(FootStart());
    }
    IEnumerator FootStart()
    {
        //yield return new WaitForSeconds(0.5f);
        StartCoroutine(Controller.AnimationController());
        //yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(FootAnimation._forwardDistance / FootAnimation._footSpeed);
        StartCoroutine(Controller2.AnimationController());
    }
    
}
