using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    
    public static GestureManager Instance; // Singleton instance of the GestureManager

    
    private Touch _trackedFinger; // Variable to track the touch input
    private float _gestureTime; // Variable to track the duration of the gesture

    // Variables to store the start and end points of the touch
    private Vector2 _startPoint = Vector2.zero;
    private Vector2 _endPoint = Vector2.zero;

    // Tap stuff 
    [SerializeField] 
    private TapProperty _tapProperty;
    public EventHandler<TapEventArgs> OnTap;

    // Swipe stuff
    [SerializeField]
    private SwipeProperty _swipeProperty;
    private ESwipeDirection _swipeDirection;
    public EventHandler<SwipeEventArgs> OnSwipe;

    // Drag stuff
    [SerializeField]
    private DragProperty _dragProperty;
    public EventHandler<DragEventArgs> OnDrag;

    // Card stuff

    



    // Deck Object
    [SerializeField]
    private GameObject deck;

    private void Awake()
    {
        // Singleton Pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private GameObject GetHitObject(Vector2 screenPoint)
    {
        GameObject hitObject = null;
        Ray ray = Camera.main.ScreenPointToRay(this._startPoint);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green, 2f);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

            hitObject = hit.collider.gameObject;
        }

        return hitObject;
    }

    // Method to check if the touch gesture qualifies as a tap
    private void CheckTap()
    {
        // Check if the gesture time is within the allowed tap time and the distance is within the max allowed distance
        if (this._gestureTime <= this._tapProperty.Time &&
           Vector2.Distance(this._startPoint, this._endPoint) <= (Screen.dpi * this._tapProperty.MaxDistance))
        {
            // If conditions are met, fire the tap event
            this.FireTapEvent();
        }

    }

    private void CheckSwipe()
    {
        // Check if the gesture time is within the allowed tap time and the distance is within the max allowed distance
        if (this._gestureTime <= this._swipeProperty.Time && 
           Vector2.Distance(this._startPoint, this._endPoint) >= (Screen.dpi * this._swipeProperty.MinDistance))
        {
            // If conditions are met, fire the tap event
            this.FireSwipeEvent();
        }
    }

    private void CheckDrag()
    {
        if(this._gestureTime >= this._dragProperty.Time)
        {
            this.FireDragEvent();
        }
    }



    // Method to fire the tap event
    private void FireTapEvent()
    {

        GameObject hitObject = null;
        Ray ray = Camera.main.ScreenPointToRay(this._startPoint);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            hitObject = hit.collider.gameObject;
        }

        TapEventArgs args = new TapEventArgs(this._startPoint);


        if (hitObject == deck) //general tap on the deck
        {

            this.OnTap(this, args);
        }


        /*if (hitObject != null) //direct tap
        {
            ITappable handler = hitObject.GetComponent<ITappable>();
            if (handler != null)
            {
                handler.OnTap(args);
            }
        }*/

    }

    private void FireSwipeEvent()
    {

        Debug.Log("Swipe");
        if(this.OnSwipe != null)
        {
            GameObject hitObject = this.GetHitObject(this._startPoint);
            Vector2 rawDirection = this._endPoint - this._startPoint;
            ESwipeDirection direction = this.GetSwipeDirection(rawDirection);

            CardProperty cardProperty = hitObject.GetComponent<CardProperty>();


            string cardColor = cardProperty.CardColor;
            string cardShape = cardProperty.CardShape;
            int cardValue = cardProperty.CardValue;


            Debug.Log(cardColor);
            Debug.Log(cardShape);
            Debug.Log(cardValue);



            SwipeEventArgs args = new SwipeEventArgs(direction, rawDirection, this._startPoint, cardColor, cardShape, cardValue);

            this.OnSwipe(this, args);

            if (hitObject != null)
            {
                ISwipeable handler = hitObject.GetComponent<ISwipeable>();
                if (handler != null)
                {
                    handler.OnSwipe(args);
                }
            }
        }

    }

    private void FireDragEvent()
    {
        Vector2 position = this._trackedFinger.position;
        GameObject hitObject = this.GetHitObject(position);
        DragEventArgs args = new DragEventArgs(this._trackedFinger, hitObject);

        if(this.OnDrag != null) {
            this.OnDrag (this, args);
        }

        if(hitObject != null) {
        
            IDraggable handler = hitObject.GetComponent<IDraggable>();
            if (handler != null)
            {
                handler.OnDrag(args);
            }

        }
    }



    // function to get the swipe direction based on tap startpoint and endpoint
    private ESwipeDirection GetSwipeDirection(Vector2 rawDirection)
    {
        if(Math.Abs(rawDirection.x) > Math.Abs(rawDirection.y))
        {
            if (rawDirection.x > 0)
            {
                _swipeDirection = ESwipeDirection.RIGHT;
            }
            else if (rawDirection.x < 0)
            {
                _swipeDirection = ESwipeDirection.LEFT;
            }
            else if (rawDirection.y > 0)
            {
                _swipeDirection = ESwipeDirection.UP;
            }
            else if (rawDirection.y < 0)
            {
                _swipeDirection = ESwipeDirection.DOWN;
            }
            else
            {
                Debug.Log("no Swipe");
            }
            
        }

        return _swipeDirection;
    }



    // Method called once per frame
    private void Update()
    {
        // Check if there are any touch inputs
        if (Input.touchCount > 0)
        {
            // Track the first touch
            this._trackedFinger = Input.GetTouch(0);
            // Handle different touch phases
            switch (this._trackedFinger.phase)
            {
                case TouchPhase.Began:
                    // When touch begins, store the start position and reset gesture time
                    this._startPoint = this._trackedFinger.position;
                    this._gestureTime = 0;
                    break;

                case TouchPhase.Ended:
                    // When touch ends, store the end position and check if it qualifies as a tap
                    this._endPoint = this._trackedFinger.position;
                    this.CheckTap();
                    this.CheckSwipe();
                    this.CheckDrag();
                    break;

                default:
                    // For other phases, increment the gesture time
                    this._gestureTime += Time.deltaTime;
                    break;
            }
        }
    }
}
