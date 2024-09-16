using UnityEngine;
using UnityEngine.Events;

public class ProductController : MonoBehaviour
{
    public UnityEvent BananaDown;
    public UnityEvent StrawberryDown;
    public UnityEvent MilkDown;

    [SerializeField] private Color highlightColor = Color.white;
    [SerializeField] private Color originalColor;
    [SerializeField] private LayerMask dropZoneLayer;
    [SerializeField] private GameObject newSpritePrefab;
    [SerializeField] private GameObject posUp;
    [SerializeField] private GameObject posDown;
    [SerializeField] private GameObject posMilk;
    [SerializeField] private GameObject MilkProduct;
    [SerializeField] private GameObject UpStrawberry;
    [SerializeField] private GameObject DownStrawberry;
    [SerializeField] private GameObject UpBanana;
    [SerializeField] private GameObject DownBanana;

    private bool isDragging = false;
    private Vector3 originalPosition;
    private Vector3 newSpriteSpawnPoint;
    private SpriteRenderer spriteRenderer;

    public static int itemCount = 0;
    private static int itemCount2 = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
        newSpriteSpawnPoint = transform.position;
        originalColor = spriteRenderer.color;
    }

    void OnMouseEnter()
    {
        spriteRenderer.color = highlightColor;
    }

    void OnMouseExit()
    {
        if (!isDragging)
        {
            spriteRenderer.color = originalColor;
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }
    }
    void OnMouseUp()
    {
        isDragging = false;
        spriteRenderer.color = originalColor;

        if (IsValidDropPosition())
        {
            Destroy(gameObject);
            SpawnNewSprite();
        }
        else
        {
            transform.position = originalPosition;
        }
    }

    bool IsValidDropPosition()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, dropZoneLayer);
        return hit.collider != null;
    }
    void SpawnNewSprite()
    {
        if (newSpritePrefab != null && newSpriteSpawnPoint != null)
        {
            Instantiate(newSpritePrefab, newSpriteSpawnPoint, Quaternion.identity);
        }
        if (gameObject.CompareTag("Milk"))
        {
            GameObject milkObject = Instantiate(MilkProduct, posMilk.transform.position, Quaternion.identity);
            MilkDown.Invoke();
            milkObject.transform.SetParent(posMilk.transform);
            itemCount++;
        }

        if (gameObject.CompareTag("Banana") && itemCount2 < 1)
        {
            GameObject downBanana = Instantiate(DownBanana, posDown.transform.position, Quaternion.identity);
            downBanana.transform.SetParent(posDown.transform);
            BananaDown.Invoke();
            itemCount2++;
            itemCount++;
        }
        else if (gameObject.CompareTag("Banana"))
        {
            GameObject upBanana = Instantiate(UpBanana, posUp.transform.position, Quaternion.identity);
            upBanana.transform.SetParent(posUp.transform);
            BananaDown.Invoke();
            itemCount2++;
            itemCount++;
        }
        if (gameObject.CompareTag("Strawberry") && itemCount2 < 1)
        {
            GameObject downStrawberry = Instantiate(DownStrawberry, posDown.transform.position, Quaternion.identity);
            downStrawberry.transform.SetParent(posDown.transform);
            StrawberryDown.Invoke();
            itemCount2++;
            itemCount++;
        }
        else if (gameObject.CompareTag("Strawberry"))
        {
            GameObject upStrawberry = Instantiate(UpStrawberry, posUp.transform.position, Quaternion.identity);
            upStrawberry.transform.SetParent(posUp.transform);
            StrawberryDown.Invoke();
            itemCount2++;
            itemCount++;
        }


    }
}
