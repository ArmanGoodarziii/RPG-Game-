using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject[] effects;

    [Header("Button's images")]
    public Image StrongAttack_1_img;
    public Image StrongAttack_2_img;
    public Image StrongAttack_3_img;
    // Button's field time 
    private float StrongAttack_1_time;
    private float StrongAttack_2_time;
    private float StrongAttack_3_time;
    [Header("Movement")]
    private Animator animator;
    public float speed;
    public FixedJoystick myJoyStick;
    [Header("Camera")]
    private bool cameraMove = true;
    public GameObject cameraObject;
    public Vector3 plusVec;
    public float speedCamera;
    private float waitAnimation;

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

        if(waitAnimation <= 0)
        {
            if(myJoyStick.Horizontal != 0 || myJoyStick.Vertical != 0)
            {
                transform.Translate(new Vector3(0 , 0 , speed * Time.deltaTime));
            }
        }
    }
    void Update()
    {
        if(StrongAttack_1_time > 0)
        {
            StrongAttack_1_img.enabled = true;
            StrongAttack_1_time -= 1 * Time.deltaTime;
            StrongAttack_1_img.fillAmount = StrongAttack_1_time / 9;
        }
        else if(StrongAttack_1_time <= 0)
        {
            StrongAttack_1_img.enabled = false;
            StrongAttack_1_time = 0;
        }

        if(StrongAttack_2_time > 0)
        {
            StrongAttack_2_img.enabled = true;
            StrongAttack_2_time -= 1 * Time.deltaTime;
            StrongAttack_2_img.fillAmount = StrongAttack_2_time / 8;
        }
        else if(StrongAttack_2_time <= 0)
        {
            StrongAttack_2_img.enabled = false;
            StrongAttack_2_time = 0;
        }

        if(StrongAttack_3_time > 0)
        {
            StrongAttack_3_img.enabled = true;
            StrongAttack_3_time -= 1 * Time.deltaTime;
            StrongAttack_3_img.fillAmount = StrongAttack_3_time / 15;
        }
        else if(StrongAttack_1_time <= 0)
        {
            StrongAttack_3_img.enabled = false;
            StrongAttack_3_time = 0;
        }

        if(waitAnimation <= 0)
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

        if(waitAnimation > 0)
        {
            waitAnimation -= 1 * Time.deltaTime;
        }
        else if(waitAnimation <= 0)
        {
            waitAnimation = 0;
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

    public void Attack()
    {
        if(waitAnimation <= 0)
        {
            animator.Play("attack");
            waitAnimation = 1.2f;
        }
    }
    public void StrongAttack_1()
    {
        if(waitAnimation <= 0)
        {
            animator.Play("great-attack-1");
            waitAnimation = 5f;
            StrongAttack_1_time = 9;
        }
    }
    public void StrongAttack_2()
    {
        if(waitAnimation <= 0)
        {
            animator.Play("great-attack-2");
            waitAnimation = 2f;
            StrongAttack_2_time = 8;
        }
    }
    public void StrongAttack_3()
    {
        if(waitAnimation <= 0)
        {
            animator.Play("great-attack-3");
            waitAnimation = 2f;
            StrongAttack_3_time = 15;
        }
    }
    
    public void Event_Attack()
    {
        Instantiate(effects[3], new Vector3(transform.position.x, 1.1f, transform.position.z), Quaternion.identity);
        Instantiate(effects[4], new Vector3(transform.position.x, 1.1f, transform.position.z), Quaternion.identity);
    }
    public void Event_Charge()
    {
        Instantiate(effects[0], new Vector3(transform.position.x, 1.1f, transform.position.z), Quaternion.identity);
    }
    public void Event_StrongAttack_1()
    {
        Instantiate(effects[1], new Vector3(transform.position.x, 1.1f, transform.position.z), Quaternion.identity);
    }
    public void Event_StrongAttack_2()
    {
        Instantiate(effects[2], new Vector3(transform.position.x, 1.1f, transform.position.z), Quaternion.identity);
    }
    public void Event_StrongAttack_3()
    {
        Instantiate(effects[5], new Vector3(transform.position.x, 1.1f, transform.position.z), Quaternion.identity);
    }
}
