using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform attackPosition;
    public float attackRange;
    public LayerMask playerLayer;

    public GameObject[] skins;
    public GameObject[] weapones;
    public GameObject[] hats;
    private GameObject playerObject;
    private Animator animator;

    [Header("Movement")]
    public float speed;

    private float time_animation;

    void Start()
    {
        skins[Random.Range(0 , skins.Length)].SetActive(true);
        weapones[Random.Range(0 , weapones.Length)].SetActive(true);
        hats[Random.Range(0 , hats.Length)].SetActive(true);


        playerObject = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(time_animation <= 0)
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
