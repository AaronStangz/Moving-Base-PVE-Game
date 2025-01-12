using UnityEngine;

public class ShuttleControl : MonoBehaviour
{
    public float openRange;
    public GameObject Shuttle;
    public GameObject Player;
    public GameObject Head;

    ShuttleDriving SDS;

    public Rigidbody Rb;

    public bool Driving;

    void Start()
    {
        SDS = Shuttle.GetComponent<ShuttleDriving>();
        Rigidbody Rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Driving)
        {
            Exit();
        }
    }

    public void Open()
    {
        if (!Driving) 
        {
            Shuttle.GetComponent<ShuttleDriving>().enabled = true;
            Rb.isKinematic = false;
            Head.SetActive(true);
            Player.SetActive(false);
            Driving = true;
        }
    }

    public void Exit()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Shuttle.GetComponent<ShuttleDriving>().enabled = false;
            Rb.isKinematic = true;
            Head.SetActive(false);
            Player.SetActive(true);
            Driving = false;
        }
    }
}
