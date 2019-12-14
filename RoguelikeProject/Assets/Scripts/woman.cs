using UnityEngine;
using System.Collections;

public class woman : MonoBehaviour
{

    private Vector2 targetPosition;
    private Transform player;
    private Rigidbody2D rigidbody;

    public float smoothing = 3;
    public AudioClip BeattackAudio;

    private BoxCollider2D collider;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
        GameManager.Instance.womanList.Add(this);
    }

    void Update()
    {
        //检测
        collider.enabled = false;
        RaycastHit2D hit = Physics2D.Linecast(this.transform.position, targetPosition);
        collider.enabled = true;
        if (hit.transform == null)
        {
            rigidbody.MovePosition(Vector2.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime));
        }
        else
        {
            switch (hit.collider.tag)
            {
                case "Player":
                    break;
                case "woman":
                    break;
                case "Enemy":
                    break;
            }
        }
    }

    public void Move(Vector3 pos)
    {
        targetPosition = pos;
        GameManager.Instance.ReduceMotherFood(1);
    }
    public void TakeDamage(int lossFood)
    {
        GameManager.Instance.ReduceMotherFood(lossFood);
        animator.SetTrigger("Damage");
    }

}
