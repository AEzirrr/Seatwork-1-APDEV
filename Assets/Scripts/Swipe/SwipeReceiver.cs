using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeReceiver : MonoBehaviour
{

    void Start()
    {

        GestureManager.Instance.OnSwipe += this.OnSwipe;

    }


    private void OnDisable()
    {
  
        GestureManager.Instance.OnSwipe -= this.OnSwipe;
    }


    public void OnSwipe(object sender, SwipeEventArgs args)
    {
        Debug.Log("SWIPE");
    }
}
