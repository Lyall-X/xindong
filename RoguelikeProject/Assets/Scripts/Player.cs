﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float smoothing = 1;
    public float restTime = 1;
    public AudioClip chop1Audio;
    public AudioClip chop2Audio;
    public AudioClip step1Audio;
    public AudioClip step2Audio;

    public AudioClip soda1Audio;
    public AudioClip soda2Audio;
    public AudioClip fruit1Audio;
    public AudioClip fruit2Audio;

    public AudioClip footAudio;
    public AudioClip attckAudio;
    public AudioClip eatSnowballAudio;
    public AudioClip chopAudio;
    public AudioClip helpAudio;
    public Button AddBtn;

    private float restTimer = 0;
    [HideInInspector] public Vector2 targetPos = new Vector2(1, 1);
    [HideInInspector] public Vector2 oldPos = new Vector2(1, 1);
    private Rigidbody2D rigidbody;
    private BoxCollider2D collider;
    private Animator animator;


    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        AddBtn.onClick.AddListener(OnAdd);
    }
    private void OnAdd()
    {
        GameManager.Instance.AddFood(5);
        AudioManager.Instance.RandomPlay(footAudio);
        AudioManager.Instance.PlayEfxMusic(eatSnowballAudio);
        GameManager.Instance.setAddButtonVisible(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            OnAdd();
        rigidbody.MovePosition(Vector2.Lerp(transform.position, targetPos, smoothing * Time.deltaTime));
        if (GameManager.Instance.food <= 0 || GameManager.Instance.isEnd == true) return;
        restTimer += Time.deltaTime;
        if (restTimer < restTime) return;
        oldPos = transform.position;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h > 0)
        {
            v = 0;
        }

        if (h != 0 || v != 0)
        {
            GameManager.Instance.ReduceFood(1);
            //检测
            GameManager.Instance.setAddButtonVisible(false);
            collider.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(targetPos, targetPos + new Vector2(h, v));
            collider.enabled = true;
            if (hit.transform == null)
            {
                targetPos += new Vector2(h, v);
                AudioManager.Instance.RandomPlay(footAudio);
            }
            else
            {
                switch (hit.collider.tag)
                {
                    case "OutWall":
                        break;
                    case "Wall":
                        animator.SetTrigger("Attack");
                        AudioManager.Instance.PlayEfxMusic(attckAudio, 3);
                        hit.collider.SendMessage("TakeDamage");
                        break;
                    case "Food":
                        targetPos += new Vector2(h, v);
                        GameManager.Instance.setAddButtonVisible(true);
                        Destroy(hit.transform.gameObject);
                        break;
                    case "Soda":
                        targetPos += new Vector2(h, v);
                        GameManager.Instance.setAddButtonVisible(true);
                        Destroy(hit.transform.gameObject);
                        break;
                    case "Enemy":
                    case "Enemy1":
                        break;
                    case "woman":
                        if (!GameManager.Instance.isAdd)
                        {
                            AudioManager.Instance.PlayEfxMusic(helpAudio, 1);
                            GameManager.Instance.isAdd = true;
                        }
                        break;
                }
            }
            GameManager.Instance.OnPlayerMove();

            restTimer = 0;//不管是攻击还是移动 都需要休息
        }
    }

    public void TakeDamage(int lossFood)
    {
        GameManager.Instance.ReduceFood(lossFood);
        animator.SetTrigger("Damage");
    }
}
