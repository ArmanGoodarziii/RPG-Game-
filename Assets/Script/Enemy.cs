using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject[] canvasImages;
    public float health = 50;
    public float saveHealth;
    public Image healthBar;
    public Transform attackPosition;
    public float attackRange;
    public LayerMask playerLayer;

    public GameObject[] skins;
    public GameObject[] weapones;
    public GameObject[] hats;
    private GameObject playerObject;
    [HideInInspector] public Animator animator;

    [Header("Movement")]
    public float speed;

    [HideInInspector] public float time_animation;

    void Start()
    {
        health = Random.Range(50 , 100);
        saveHealth = health;

        skins[Random.Range(0 , skins.Length)].SetActive(true);
        weapones[Random.Range(0 , weapones.Length)].SetActive(true);
        hats[Random.Range(0 , hats.Length)].SetActive(true);


        playerObject = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        healthBar.fillAmount = health / saveHealth;

        if(health <= 30)
        {
            healthBar.color = Color.red;
        }
        else
        {
            healthBar.color = Color.green;  
        }
        if(health <= 0)
        {
            for(int i = 0 ; i < canvasImages.Length ; i++)
            {
                canvasImages[i].SetActive(false);
            }

            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            animator.Play("die");
            Destroy(gameObject , 5);
        }
        else
        {
            for(int i = 0 ; i < canvasImages.Length ; i++)
            {
                canvasImages[i].transform.LookAt(playerObject.GetComponent<Player>().cameraObject.transform.position);
            }
        }
        if(time_animation <= 0 && health > 0)
        {
            Vector3 playerVec = new Vector3(playerObject.transform.position.x , playerObject.transform.position.y , playerObject.transform.position.z);
            transform.LookAt(playerVec);
            if(Vector3.Distance(transform.position , playerVec) > 1.7f)
            {
                transform.position = Vector3.MoveTowards(transform.position , playerVec , speed * Time.deltaTime);
                animator.SetBool("attack" , false);
            }
            else
            {
                animator.SetBool("attack" , true);
            }
        }

        if(time_animation > 0)
        {
            time_animation -= 1 * Time.deltaTime;
            animator.SetBool("attack" , false);
        }
        else if(time_animation <= 0)
        {
            time_animation = 0;
        }
    }

    public void EventAttack()
    {
        Collider[] collider = Physics.OverlapSphere(attackPosition.position , attackRange , playerLayer);

        if(collider != null)
        {
            if(playerObject.GetComponent<Player>().waitAnimation <= 0 && playerObject.GetComponent<Player>().health > 0)
            {
                playerObject.GetComponent<Player>().waitAnimation = 0.5f;
                playerObject.GetComponent<Player>().animator.Play("hit");
            }
            
            playerObject.GetComponent<Player>().health -= 5f;
            playerObject.GetComponent<Player>().audioSource.PlayOneShot(playerObject.GetComponent<Player>().hits[Random.Range(0 , playerObject.GetComponent<Player>().hits.Length)]);
        }
    }
}
