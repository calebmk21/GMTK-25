using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class SpreadsheetPlayerController : MonoBehaviour
{
    // these will be inited each new level
    private Tilemap floor;
    private Tilemap walls;
    private TileBase hasBeenTile;
    private float gridSize;
    private LayerMask wallLayer;
    private int score;
    private bool hasWon;
    // player input!
    private SpreadsheetInput controls;

    void Awake()
    {
        controls = new SpreadsheetInput();
        controls.Player.Enable();
        controls.Player.Move.performed += OnMove;
    }

    void OnDestroy()
    {
        controls.Dispose();                              // Destroy asset object.
    }

    void OnEnable()
    {
        controls.Enable();                                // Enable all actions within map.
    }
    void OnDisable()
    {
        controls.Disable();                               // Disable all actions within map.
    }

    public void EnableInput()
    {
        controls.Enable();
    }

    public void DisableInput()
    {
        controls.Disable();
    }

    public void InitializeLevel(Tilemap floorMap, Tilemap wallMap, TileBase tileSprite, LayerMask layerWall, float sizeGrid)
    {
        hasWon = false;
        floor = floorMap;
        walls = wallMap;
        hasBeenTile = tileSprite;
        wallLayer = layerWall;
        gridSize = sizeGrid;
    }

    public void PositionPlayer(Vector3 position)
    {
        transform.position = position; 
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int newScore)
    {
        score = newScore; 
    }

    public bool GetHasWon()
    {
        return hasWon;
    }

    // Invoked when "Move" action is either started, performed or canceled.
    void OnMove(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Vector2 input = context.ReadValue<Vector2>();
        Vector3 direction = Vector3.zero;

        if (input.y > 0) direction = Vector3.up;
        else if (input.y < 0) direction = Vector3.down;
        else if (input.x < 0) direction = Vector3.left;
        else if (input.x > 0) direction = Vector3.right;

        direction = transform.position + (direction * gridSize);
        if (CanMove(direction))
        {
            if (floor != null && walls != null)
            {
                Vector3Int fromPos = floor.WorldToCell(transform.position);
                TileBase fromTile = floor.GetTile(fromPos);
                if (fromTile)
                {
                    score++;
                    walls.SetTile(fromPos, hasBeenTile);
                    floor.SetTile(fromPos, null);
                }
            }
            transform.position = direction;
        }
    }

    bool CanMove(Vector3 pos)
    {
        Collider2D overlap = Physics2D.OverlapBox(new Vector2(pos.x, pos.y), new Vector2(gridSize * 0.5f, gridSize * 0.5f), 0f, wallLayer);
        Debug.Log(overlap == null);
        return overlap == null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("End"))
        {
            Debug.Log("You win!");
            Debug.Log(score);
            hasWon = true; 
            // end game logic
        }
    }
}