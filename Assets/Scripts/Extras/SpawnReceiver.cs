using UnityEngine;

public class SpawnReceiver : MonoBehaviour, ITappable, ISwipeable, IDraggable
{
    [SerializeField]
    private float _speed = 10f;
    private Vector3 _targetPosition;
    [SerializeField] private Transform _clubsDock;
    [SerializeField] private Transform _spadesDock;
    [SerializeField] private Transform _heartsDock;
    [SerializeField] private Transform _diamondsDock;
    
    [SerializeField] private int _type;

    private bool _docked;

    public bool IsDocked()
    {
        return _docked;
    }

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
                Debug.Log(args.Direction);
                MoveToDock(args);
                
                break;
            case 1:
                MoveDiagonal(args);
                break;
        }
        Debug.Log("SWIPE");
    }

    public void OnDrag(DragEventArgs args)
    {
        if (args.HitObject == this.gameObject)
        {
            Vector2 position = args.TrackedFinger.position;
            Ray ray = Camera.main.ScreenPointToRay(position);
            Vector2 worldPosition = ray.GetPoint(10);

            this._targetPosition = worldPosition;
            this.transform.position = worldPosition;
        }
        Debug.Log("DRAG");
    }

    private void MoveToDock(SwipeEventArgs args)
    {
        if(args.Direction == ESwipeDirection.RIGHT)
        {
            if (args.CardShape == "clubs" && args.CardValue == 1)
            {
                _targetPosition = _clubsDock.position;
                _docked = true;
            }
            else if (args.CardShape == "spades" && args.CardValue == 1)
            {
                _targetPosition = _spadesDock.position;
                _docked = true;
            }
            else if (args.CardShape == "hearts" && args.CardValue == 1)
            {
                _targetPosition = _heartsDock.position;
                _docked = true;
            }
            else if (args.CardShape == "diamonds" && args.CardValue == 1)
            {
                _targetPosition = _diamondsDock.position;
                _docked = true;
            }

            _targetPosition.z -= .01f;
           

        }
        
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

        _targetPosition += (direction * 5);
    }

    private void MoveDiagonal(SwipeEventArgs args)
    {
        Debug.Log("MoveDiagonal called");
    }

    private void OnEnable()
    {
        _targetPosition = this.transform.position;
    }

    private void Update()
    {
        // Move towards the target position
        if (Vector3.Distance(transform.position, _targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        }
        else
        {
            // Ensure the card stops exactly at the target position
            transform.position = _targetPosition;
        }
    }
}
