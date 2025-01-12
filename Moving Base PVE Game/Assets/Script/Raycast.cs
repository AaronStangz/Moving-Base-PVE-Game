using UnityEngine;

public class Raycast : MonoBehaviour
{

    [SerializeField] private LayerMask Interactable;
    [SerializeField] private LayerMask Collectable;
    [SerializeField] private LayerMask Built;

    MainManager IM;
    public GameObject mainManager;
    public GameObject player;

    void Start()
    {
        IM = mainManager.GetComponent<MainManager>();

    }

    void Update()
    {
        if (Camera.main == null) return;

        RaycastHit hit;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 1000);
        if (Physics.SphereCast(ray, 0.5f, out hit, 10, Interactable + Collectable + Built))
        {
            if (Interactable.value == (1 << hit.collider.gameObject.layer))
            {

                ShuttleControl SL = hit.collider.GetComponent<ShuttleControl>();
                if (SL != null)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (hit.distance < SL.openRange)
                        {
                            Debug.Log("Open");
                            SL.Open();
                        }
                    }
                }

            }
        }
    }
}
