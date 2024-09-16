using UnityEngine.Events;
using UnityEngine;

public class BlenderController : MonoBehaviour
{
    public UnityEvent CapDown;
    public UnityEvent SmuziShake;

    [SerializeField] private GameObject ANIM_Smuzi;
    [SerializeField] private GameObject posCapNew;

    private bool isMixing;

    private void Update()
    {
        if(ProductController.itemCount == 3)
        {
            isMixing = true;
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Cap") && isMixing)
                {
                    CapDown.Invoke();
                    ANIM_Smuzi.SetActive(true);
                    GameObject gameOBject = GameObject.Find("Position");
                    Destroy(gameOBject);
                    this.gameObject.SetActive(false);
                    hit.transform.position = posCapNew.transform.position;
                    SmuziShake.Invoke();
                }
            }
        }
    }
}
