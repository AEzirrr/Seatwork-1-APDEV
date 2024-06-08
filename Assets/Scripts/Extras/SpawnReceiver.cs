using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnReceiver : MonoBehaviour, ITappable, ISwipeable, IDraggable
{

    [SerializeField] private float _speed = 10f;
    [SerializeField] private Vector3 _targetPosition;


    [SerializeField] private int _type;


    public void OnTap(TapEventArgs args)
    {
        Destroy(this.gameObject);
        Debug.Log("DESTROY");
    }

    public void OnSwipe(SwipeEventArgs args)
    {
        switch (this._type)
        {
            case 0:
                MovePerpendicular(args);
                break;
            case 1:
                MoveDiagonal(args);
                break;
        }                                                                               
        Debug.Log("SWIPE");
    }

    public void OnDrag(DragEventArgs args)
    {
        if(args.HitObject == this.gameObject)
        {
            Vector2 position = args.TrackedFinger.position;
            Ray ray = Camera.main.ScreenPointToRay(position);
            Vector2 worldPosition = ray.GetPoint(10);

            this._targetPosition = worldPosition;
            this.transform.position = worldPosition;
        }
        Debug.Log("DRAG");
    }


    private void MovePerpendicular(SwipeEventArgs args)
    {

        Vector3 direction = Vector3.zero;

        switch (args.Direction)
        {
            case ESwipeDirection.UP:
                direction.y = 1;
                break;

            case ESwipeDirection.DOWN:
                direction.y = -1;
                break;

            case ESwipeDirection.LEFT:
                direction.x = -1;
                break;

            case ESwipeDirection.RIGHT:
                direction.x = 1;
                break;
        }

        this._targetPosition += (direction * 5);
    }

    private void MoveDiagonal(SwipeEventArgs args)
    {
      
        Debug.Log("MoveDiagonal called");
    
    }

    private void OnEnable()
    {
        this._targetPosition = this.transform.position;
    }

    private void Update()
    {
        if(this.transform.position != this._targetPosition)
        {
            this .transform.position = Vector3.MoveTowards(this.transform.position, this._targetPosition, _speed * Time.deltaTime);
        }
    }


}


