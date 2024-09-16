using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SmuziController : MonoBehaviour
{
    public UnityEvent CapUp;
    public UnityEvent SmuziDown;

    [SerializeField] private GameObject posCapOld;
    [SerializeField] private Color highlightColor = Color.white;
    [SerializeField] private Color originalColor;
    [SerializeField] private LayerMask dropZoneLayer;
    [SerializeField] private GameObject newSpritePrefab;
    [SerializeField] private Vector3 newSpriteSpawnPoint;
    [SerializeField] private GameObject WinPanel;

    private bool MixingEND;
    private bool isMovement;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalPosition;

    private void Start()
    {
        StartCoroutine(SmuziEND());
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Smuzi") && MixingEND)
                {
                    CapUp.Invoke();
                    GameObject Smuzi = GameObject.Find("Крышка Блендера");
                    Smuzi.transform.position = posCapOld.transform.position;
                    isMovement = true;
                    MixingEND = false;
                }
            }
        }
    }
    public IEnumerator SmuziEND()
    {
        yield return new WaitForSeconds(3.1f);
        MixingEND = true;
    }

    void OnMouseEnter()
    {
        if (isMovement)
        {
        spriteRenderer.color = highlightColor;
        }
    }
    void OnMouseExit()
    {
        if (isMovement)
        {
            spriteRenderer.color = originalColor;
        }
    }

    void OnMouseDrag()
    {
        if (isMovement)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Smuzi"))
                {
                    GameObject Smuzi = hit.collider.gameObject;
                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = 10f;
                    Smuzi.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
                }
            }
        }
    }
    void OnMouseUp()
    {
        if (isMovement)
        {
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
    }

    bool IsValidDropPosition()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, dropZoneLayer);
        return hit.collider != null;
    }

    void SpawnNewSprite()
    {
        if (newSpritePrefab != null)
        {
            Instantiate(newSpritePrefab, newSpriteSpawnPoint, Quaternion.identity);
            SmuziDown.Invoke();
        }
    }


}
