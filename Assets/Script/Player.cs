using UnityEngine;

public class Player : MonoBehaviour
{   
    [Header("Movement")]
    private Animator animator;
    public float speed;
    public FixedJoystick myJoyStick;
    [Header("Camera")]
    private bool cameraMove = true;
    public GameObject cameraObject;
    public Vector3 plusVec;
    public float speedCamera;

    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    void FixedUpdate()
    {

        if (cameraMove)
        {
            cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position , new Vector3(transform.position.x + plusVec.x , cameraObject.transform.position.y + plusVec.y , transform.position.z + plusVec.z) , speedCamera * Time.deltaTime);
            cameraObject.transform.LookAt(transform.position);
        }
        else
        {
            cameraObject.transform.LookAt(transform.position);
        }

        if(myJoyStick.Horizontal != 0 || myJoyStick.Vertical != 0)
        {
            transform.Translate(new Vector3(0 , 0 , speed * Time.deltaTime));
        }
    }
    void Update()
    {
        float moveH = myJoyStick.Horizontal;
        float moveV = myJoyStick.Vertical;

        Vector3 direction =new Vector3(moveH , 0 , moveV);

        if(myJoyStick.Horizontal != 0 || myJoyStick.Vertical != 0)
        {
            transform.LookAt(transform.position - direction);
            animator.SetBool("run", true);
        }
        else if(myJoyStick.Horizontal == 0 || myJoyStick.Vertical == 0)
        {
            animator.SetBool("run", false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "camera")
        {
            cameraMove = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "camera")
        {
            cameraMove = false;
        }
    }
}
