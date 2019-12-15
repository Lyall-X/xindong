using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private Vector2 targetPosition;
    private Transform player;
    private Transform mother;
    private Rigidbody2D rigidbody;
    
    public float smoothing = 3;
    public int lossFood = 3;
    public AudioClip attackAudio;

    private BoxCollider2D collider;
    private Animator animator;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mother = GameObject.FindGameObjectWithTag("woman").transform;
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
        GameManager.Instance.enemyList.Add(this);
    }

    void Update() {
         rigidbody.MovePosition( Vector2.Lerp(transform.position, targetPosition, smoothing*Time.deltaTime));
    }
    private void disObj()
    {
        Destroy(this.gameObject);
    }

    public void Move() {
        Vector2 offset_player = player.position - transform.position;
        Vector2 offset_mother = mother.position - transform.position;
        if (offset_player.magnitude < 1.1f || offset_mother.magnitude < 1.1f) {
            //攻击
            animator.SetTrigger("Attack");
            AudioManager.Instance.RandomPlay(attackAudio);
            if (offset_player.magnitude < 1.1f)
                player.SendMessage("TakeDamage",lossFood);
            else if (offset_mother.magnitude < 1.1f)
                mother.SendMessage("TakeDamage", lossFood);

            if (this.gameObject.tag == "Enemy1")
            {
                Invoke("disObj", 1);

            }
        }
        else {
            double dis_player = System.Math.Pow(offset_player.x, 2.0) + System.Math.Pow(offset_player.y, 2.0);
            double dis_mother = System.Math.Pow(offset_mother.x, 2.0) + System.Math.Pow(offset_mother.y, 2.0);
            Vector2 tar = new Vector2();
            if (dis_player > dis_mother)
                tar = offset_mother;
            else
                tar = offset_player;
            float x = 0, y = 0;
            if (Mathf.Abs(tar.y) > Mathf.Abs(tar.x)) {
                //按照y轴移动
                if (tar.y < 0) {
                    y = -1;
                }
                else {
                    y = 1;
                }
            }
            else {
                //按照x轴移动
                if (tar.x > 0) {
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
