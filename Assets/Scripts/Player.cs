using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    // Define the scale factor for resizing the player in the second scene
    public float resizeFactor = 0.1f;
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;
    public LayerMask solidObjectsLayer;
    public LayerMask interactablesLayer;
    private Rigidbody2D rb;
    private bool canMove = true;

    // Awake is called before Start
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        // Subscribe to the sceneLoaded event
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // Check if the current scene is the second scene ("Game2")
        if (scene.name == "Game2")
        {
            // Resize the player in the second scene
            ResizePlayer();
        }
    }

    // Function to resize the player
    private void ResizePlayer()
    {
        // Reduce the size of the player by the specified factor
        transform.localScale = new Vector3(resizeFactor, resizeFactor, resizeFactor);
    }

    public void HandleUpdate()
    {
        if (!isMoving)
        {

            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // Reset the horizontal input if moving vertically
            if (input.y != 0)
            {
                input.x = 0;
            }

            if (input != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                /* // Check if the target position is blocked by an obstacle
                 Collider2D[] colliders = Physics2D.OverlapCircleAll(targetPos, 0.1f, solidObjectsLayer);
                 Debug.Log("Number of colliders detected: " + colliders.Length);

                 bool canMoveToTarget = true;

                 foreach (var collider in colliders)
                 {
                     if (collider.CompareTag("obstacle"))
                     {
                         canMoveToTarget = false;
                         break;
                     }
                 }

                 if (canMoveToTarget)
                 {
                     StartCoroutine(Move(targetPos));
                 }*/ // Check for obstacle collision using raycasting
                if (!IsBlocked(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Vector3 adjustedStart = transform.position + new Vector3(input.x, input.y, 0) * 0.1f;
            Vector3 adjustedEnd = transform.position + new Vector3(input.x, input.y, 0) * 0.19f;

            if (CheckInteractableCollision(adjustedStart, adjustedEnd))
            {
                Debug.Log("Hello");
                // Handle interaction with the environment or display a message
            }

        }


    }


   



    private bool IsBlocked(Vector3 targetPos)
    {
        Vector3 adjustedStart = transform.position + new Vector3(input.x, input.y, 0) * 0.1f; // Adjust start a bit ahead
        Vector3 adjustedEnd = targetPos + new Vector3(input.x, input.y, 0) * 0.19f; // Adjust end a bit ahead

        bool blockedByObstacle = CheckObstacleCollision(adjustedStart, adjustedEnd);
        bool blockedByInteractable = CheckInteractableCollision(adjustedStart, adjustedEnd);

        return blockedByObstacle || blockedByInteractable;
    }

    private bool CheckObstacleCollision(Vector2 start, Vector2 end)
    {
        Vector2 direction = end - start;
        RaycastHit2D hit = Physics2D.Raycast(start, direction.normalized, direction.magnitude, solidObjectsLayer);

        if (hit.collider != null && hit.collider.CompareTag("obstacle"))
        {
            return true;
           
        }

        return false;
    }
   
     private bool CheckInteractableCollision(Vector2 start, Vector2 end)
     {
         Vector2 direction = end - start;
         RaycastHit2D hit = Physics2D.Raycast(start, direction.normalized, direction.magnitude, interactablesLayer);

         if (hit.collider != null && hit.collider.CompareTag("interactable"))
         {
             Debug.Log("yes");
            hit.collider.GetComponent<Interactable>()?.Interact();

             return true;
         }

         return false;
     }
    /*private bool CheckObstacleCollision(Vector2 start, Vector2 end)
    {
        Vector2 direction = end - start;
        RaycastHit2D hit1 = Physics2D.Raycast(start, direction.normalized, direction.magnitude, solidObjectsLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(start, direction.normalized, direction.magnitude, interactablesLayer);

        if (hit1.collider != null && hit1.collider.CompareTag("obstacle") || hit2.collider != null && hit2.collider.CompareTag("obstacle"))
        {
            return true;
        }

        return false;
    }*/

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }




    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exited collision with an obstacle");

    }


}
