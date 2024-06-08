using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeReceiver : MonoBehaviour
{

    [SerializeField] private Transform _clubsDock;
    [SerializeField] private Transform _spadesDock;
    [SerializeField] private Transform _heartsDock;
    [SerializeField] private Transform _diamondsDock;

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
        if (args.CardShape == "clubs" && args.CardValue == 1)
        {
            args.HitObject.transform.position = _clubsDock.position;
        }
        else if (args.CardShape == "spades" && args.CardValue == 1)
        {
            args.HitObject.transform.position = _spadesDock.position;
        }
        else if (args.CardShape == "hearts" && args.CardValue == 1)
        {
            args.HitObject.transform.position = _heartsDock.position;
        }
        else if (args.CardShape == "diamonds" && args.CardValue == 1)
        {
            args.HitObject.transform.position = _diamondsDock.position;
        }

        Debug.Log(this);
    }
}
