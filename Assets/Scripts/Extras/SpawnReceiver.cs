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

    private static int _clubsTopCard = 0;
    private static int _spadesTopCard = 0;
    private static int _heartsTopCard = 0;
    private static int _diamondsTopCard = 0;

    private static int _clubsCardCount = 0;
    private static int _spadesCardCount = 0;
    private static int _heartsCardCount = 0;
    private static int _diamondsCardCount = 0;

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

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("PileTrigger1"))
                {
                    _targetPosition = hit.point;
                    transform.position = _targetPosition;
                    CardProperty draggedCard = this.GetComponent<CardProperty>();
                    PileProperty targetPile = hit.transform.GetComponent<PileProperty>();

                    if(CanBePlacedOnPile(targetPile, draggedCard))
                    {
                        _targetPosition = hit.point;
                        transform.position = _targetPosition;
                        AddCardToPile(targetPile, draggedCard);
                    }
                }
                if (hit.collider.CompareTag("PileTrigger2"))
                {
                    _targetPosition = hit.point;
                    transform.position = _targetPosition;
                    CardProperty draggedCard = this.GetComponent<CardProperty>();
                    PileProperty targetPile = hit.transform.GetComponent<PileProperty>();

                    if (CanBePlacedOnPile(targetPile, draggedCard))
                    {
                        _targetPosition = hit.point;
                        transform.position = _targetPosition;
                        AddCardToPile(targetPile, draggedCard);
                    }
                }
                if (hit.collider.CompareTag("PileTrigger3"))
                {
                    _targetPosition = hit.point;
                    transform.position = _targetPosition;
                    CardProperty draggedCard = this.GetComponent<CardProperty>();
                    PileProperty targetPile = hit.transform.GetComponent<PileProperty>();

                    if (CanBePlacedOnPile(targetPile, draggedCard))
                    {
                        _targetPosition = hit.point;
                        transform.position = _targetPosition;
                        AddCardToPile(targetPile, draggedCard);
                    }
                }
                if (hit.collider.CompareTag("PileTrigger4"))
                {
                    _targetPosition = hit.point;
                    transform.position = _targetPosition;
                    CardProperty draggedCard = this.GetComponent<CardProperty>();
                    PileProperty targetPile = hit.transform.GetComponent<PileProperty>();

                    if (CanBePlacedOnPile(targetPile, draggedCard))
                    {
                        _targetPosition = hit.point;
                        transform.position = _targetPosition;
                        AddCardToPile(targetPile, draggedCard);
                    }
                }
                if (hit.collider.CompareTag("PileTrigger5"))
                {
                    _targetPosition = hit.point;
                    transform.position = _targetPosition;
                    CardProperty draggedCard = this.GetComponent<CardProperty>();
                    PileProperty targetPile = hit.transform.GetComponent<PileProperty>();

                    if (CanBePlacedOnPile(targetPile, draggedCard))
                    {
                        _targetPosition = hit.point;
                        transform.position = _targetPosition;
                        AddCardToPile(targetPile, draggedCard);
                    }
                }
                if (hit.collider.CompareTag("PileTrigger6"))
                {
                    _targetPosition = hit.point;
                    transform.position = _targetPosition;
                    CardProperty draggedCard = this.GetComponent<CardProperty>();
                    PileProperty targetPile = hit.transform.GetComponent<PileProperty>();

                    if (CanBePlacedOnPile(targetPile, draggedCard))
                    {
                        _targetPosition = hit.point;
                        transform.position = _targetPosition;
                        AddCardToPile(targetPile, draggedCard);
                    }
                }
                if (hit.collider.CompareTag("PileTrigger7"))
                {
                    _targetPosition = hit.point;
                    transform.position = _targetPosition;
                    CardProperty draggedCard = this.GetComponent<CardProperty>();
                    PileProperty targetPile = hit.transform.GetComponent<PileProperty>();

                    if (CanBePlacedOnPile(targetPile, draggedCard))
                    {
                        _targetPosition = hit.point;
                        transform.position = _targetPosition;
                        AddCardToPile(targetPile, draggedCard);
                    }
                }
            }
        }
    }

    private bool CanBePlacedOnPile(PileProperty pile, CardProperty card)
    {
        if(pile == null)
        {
            return false;
        }

        int topCardValue = pile.GetTopCardValue();
        string topCardColor = pile.GetTopCardColor();

        if(card.CardValue == topCardValue + 1 && card.CardColor != topCardColor)
        {
            return true;
        }

        return false;
    }

    private void AddCardToPile(PileProperty pile, CardProperty card)
    {
        if (pile != null)
        {
            pile.AddCard(card);
        }
    }

    private void MoveToDock(SwipeEventArgs args)
    {
        bool canDock = false;
        Transform targetDock = null;
        float zOffset = 0.01f; 

        if (args.Direction == ESwipeDirection.RIGHT)
        {
            switch (args.CardShape)
            {
                case "clubs":
                    if (args.CardValue == _clubsTopCard + 1)
                    {
                        targetDock = _clubsDock;
                        _clubsTopCard = args.CardValue;
                        _clubsCardCount++;
                        canDock = true;
                    }
                    break;
                case "spades":
                    if (args.CardValue == _spadesTopCard + 1)
                    {
                        targetDock = _spadesDock;
                        _spadesTopCard = args.CardValue;
                        _spadesCardCount++;
                        canDock = true;
                    }
                    break;
                case "hearts":
                    if (args.CardValue == _heartsTopCard + 1)
                    {
                        targetDock = _heartsDock;
                        _heartsTopCard = args.CardValue;
                        _heartsCardCount++;
                        canDock = true;
                    }
                    break;
                case "diamonds":
                    if (args.CardValue == _diamondsTopCard + 1)
                    {
                        targetDock = _diamondsDock;
                        _diamondsTopCard = args.CardValue;
                        _diamondsCardCount++;
                        canDock = true;
                    }
                    break;
            }

            if (canDock)
            {
                _targetPosition = targetDock.position;
                switch (args.CardShape)
                {
                    case "clubs":
                        _targetPosition.z -= _clubsCardCount * zOffset;
                        break;
                    case "spades":
                        _targetPosition.z -= _spadesCardCount * zOffset;
                        break;
                    case "hearts":
                        _targetPosition.z -= _heartsCardCount * zOffset;
                        break;
                    case "diamonds":
                        _targetPosition.z -= _diamondsCardCount * zOffset;
                        break;
                }
                _docked = true;
            }
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
        if (Vector3.Distance(transform.position, _targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        }
        else
        {
            transform.position = _targetPosition;
        }
    }
}
