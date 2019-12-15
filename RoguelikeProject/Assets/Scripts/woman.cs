using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class woman : MonoBehaviour
{

    private Vector2 targetPosition;
    private Transform player;
    private Rigidbody2D rigidbody;

    public float smoothing = 3;

    private BoxCollider2D collider;
    private Animator animator;
    public Button AddBtn;

    public AudioClip footAudio;
    public AudioClip eatSnowballAudio;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
        GameManager.Instance.womanList.Add(this);
        AddBtn.onClick.AddListener(OnAdd);
    }
    private void OnAdd()
    {
        GameManager.Instance.AddMontherFood(5);
        AudioManager.Instance.RandomPlay(footAudio);
        AudioManager.Instance.PlayEfxMusic(eatSnowballAudio);
        GameManager.Instance.setAddButtonVisible(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            OnAdd();
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
                case "Enemy1":
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
        animator.SetTrigger("motherDamage");
    }

}
