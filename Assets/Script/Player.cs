using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    public float speed;
    public FixedJoystick myJoyStick;
     

    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    void FixedUpdate()
    {
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
    
}
