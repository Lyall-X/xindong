using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private Vector2 targetPosition;
    private Transform player;
    private Rigidbody2D rigidbody;
    
    public float smoothing = 3;
    public int lossFood = 10;
    public AudioClip attackAudio;

    private BoxCollider2D collider;
    private Animator animator;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
        GameManager.Instance.enemyList.Add(this);
    }

    void Update() {
         rigidbody.MovePosition( Vector2.Lerp(transform.position, targetPosition, smoothing*Time.deltaTime));
    }
    
    public void Move() {
        Vector2 offset = player.position - transform.position;
        if (offset.magnitude < 1.1f) {
            //攻击
            animator.SetTrigger("Attack");
            AudioManager.Instance.RandomPlay(attackAudio);
            player.SendMessage("TakeDamage",lossFood);
        }
        else {
            float x = 0, y = 0;
            if (Mathf.Abs(offset.y) > Mathf.Abs(offset.x)) {
                //按照y轴移动
                if (offset.y < 0) {
                    y = -1;
                }
                else {
                    y = 1;
                }
            }
            else {
                //按照x轴移动
                if (offset.x > 0) {
                    x = 1;
                }
                else {
                    x = -1;
                }
            }
            //设置目标位置之前 先做检测
            collider.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(targetPosition, targetPosition + new Vector2(x, y));
            collider.enabled = true;
            if (hit.transform == null) {
                targetPosition += new Vector2(x, y);
            }
            else {
                if (hit.collider.tag == "Food" || hit.collider.tag == "Soda")
                {
                    targetPosition += new Vector2(x, y);
                }
            }
        }
    }

}
