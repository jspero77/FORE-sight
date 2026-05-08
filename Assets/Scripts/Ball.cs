using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private CircleCollider2D cc;

    [Header("Attributes")]
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;
    [SerializeField] private float maxGoalSpeed = 4f;

    private bool isDragging;
    [SerializeField] public bool inHole;
    [SerializeField] public bool turn = false;
    [SerializeField] public bool shot = false;
    [SerializeField] public float distances = 0;

    private void Update() {

        if (shot == true && rb.linearVelocity.magnitude < 0.01f)
        {
            Debug.Log("Player 1 Has Shot");
            turn = false;
            shot = false;
            


        }
        if (turn == true && shot == false)
        {
            PlayerInput();
        }
    }

    private bool IsReady()
    {
        return rb.linearVelocity.magnitude < 0.2f;
    }

    private void PlayerInput()
    {
        if (!IsReady()) return;
        Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(transform.position, inputPos);

        if (Input.GetMouseButtonDown(0) && distance <= 0.5f) DragStart();
        if (Input.GetMouseButton(0) && isDragging) DragChange(inputPos);
        if (Input.GetMouseButtonUp(0) && isDragging) DragRelease(inputPos);


    }
    private void DragStart()
    {
        
        isDragging = true;
        lr.positionCount = 2;

    }
    private void DragChange(Vector2 pos)
    {
        Vector2 dir = (Vector2)transform.position - pos;

        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, (Vector2)transform.position + Vector2.ClampMagnitude(dir * power / 4, maxPower / 4));
        float distance = Vector2.Distance((Vector2)transform.position, pos);
        if (distance < 1f)
        {
            lr.startColor = Color.red;
            lr.endColor = Color.red;
        }
        else
        {
            lr.startColor = Color.white;
            lr.endColor = Color.white;
        }
    }
    private void DragRelease(Vector2 pos)
    {
        float distance = Vector2.Distance((Vector2)transform.position, pos);
        isDragging = false;
        lr.positionCount = 0;

        if (distance < 1f)
        {
            return;
        }
        
            Vector2 dir = (Vector2)transform.position - pos;
            rb.linearVelocity = Vector2.ClampMagnitude(dir * power, maxPower);
            shot = true;
        cc.enabled = true;
        
    }

    private void CheckWinState()
    {
        if (inHole)
        {
            return;
            
        }
        if (rb.linearVelocity.magnitude < maxGoalSpeed)
        {
            inHole = true;
            rb.linearVelocity = Vector2.zero;
            distances = 0;
            Debug.Log("Player 1 Has Shot");
            turn = false;
            shot = false;
            gameObject.SetActive(false);

            //Eliminate Player

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Goal")
        {
            CheckWinState();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Goal")
        {
            CheckWinState();
        }
    }
}
