using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float Force = 5f;
    [SerializeField] float forceMultiplier = 100f;
    [SerializeField] RectTransform playerArea;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        KeepInBoundries();
    }

    public void GoUp()
    {
        rb.velocity = new Vector2(rb.velocity.x, Force * forceMultiplier * Time.deltaTime);
    }

    public void GoDown()
    {
        rb.velocity = new Vector2(rb.velocity.x, -Force * forceMultiplier * Time.deltaTime);
    }

    public void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }

    public void KeepInBoundries()
    {
        Vector2 position = transform.position;
        Vector2 minBoundary = (Vector2)playerArea.rect.min + new Vector2(playerArea.position.x, playerArea.position.y);
        Vector2 maxBoundary = (Vector2)playerArea.rect.max + new Vector2(playerArea.position.x, playerArea.position.y);
        position.y = Mathf.Clamp(position.y, minBoundary.y, maxBoundary.y);
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }
}
