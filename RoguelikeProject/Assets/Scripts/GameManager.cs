using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public bool isAd { get; internal set; }

    public int level = 1;//当前关卡
    public int food = 100;
    public int mother_food = 100;
    public string[] actName;
    private int cout = 0;
    //特效音乐
    public AudioClip dieClip;
    //背景音乐
    public AudioClip LevelClip;
    public AudioClip StoryClip;

    [HideInInspector] public List<Enemy> enemyList = new List<Enemy>();
    [HideInInspector] public List<woman> womanList = new List<woman>();
    [HideInInspector] public bool isEnd = false;//是否得到终点
    [HideInInspector] public bool isAdd = false;//是否添加老婆
    private bool sleepStep = true;

    private Image dayImage;
    private Text dayText;
    private Player player;
    public MapManager mapManager;
    private GameObject player_blood;
    private GameObject mother_blood;

    private woman womanPeople;

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
        //初始化地图
        mapManager = GetComponent<MapManager>();
    }

    //改
    public void InitGame()
    {
        mapManager.InitMap();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        womanPeople = GameObject.FindGameObjectWithTag("woman").GetComponent<woman>();
        if (player && GameManager.Instance.mapManager)
        {
            player.targetPos = GameManager.Instance.mapManager.getplayerBorn(3);
            if (isAdd)
            {
                womanPeople.transform.position = new Vector3(player.targetPos.x - 1, player.targetPos.y, 0);
            }
        }
        //初始化UI
        UpdateFoodText();
        //dayImage = GameObject.Find("DayImage").GetComponent<Image>();
        //dayText = GameObject.Find("DayText").GetComponent<Text>();
        //dayText.text = "Day " + level;
        //Invoke("HideBlack", 1);

        //初始化参数
        isEnd = false;
        enemyList.Clear();
        setAddButtonVisible(false);
    }

    void UpdateFoodText()
    {
        player_blood = GameObject.FindGameObjectWithTag("blood");
        player_blood.GetComponent<BloodFollow>().OnValueChange();
        mother_blood = GameObject.FindGameObjectWithTag("motherblood");
        mother_blood.GetComponent<BloodFollowMother>().OnValueChange();
    }

    public void ReduceFood(int count)
    {
        food -= count;
        UpdateFoodText();
        if (food <= 0)
        {
            AudioManager.Instance.StopBgMusic();
            AudioManager.Instance.RandomPlay(dieClip);
        }
    }
    public void ReduceMotherFood(int count)
    {
        mother_food -= count;
        UpdateFoodText();
        if (mother_food <= 0)
        {
            AudioManager.Instance.RandomPlay(dieClip);
        }
    }


    public void AddFood(int count)
    {
        setAddButtonVisible(false);
        food += count;
        UpdateFoodText();
    }
    public void AddMontherFood(int count)
    {
        setAddButtonVisible(false);
        mother_food += count;
        UpdateFoodText();
    }

    public void OnPlayerMove()
    {
        if (sleepStep == true)
        {
            sleepStep = false;
        }
        else
        {
            foreach (var enemy in enemyList)
            {
                enemy.Move();
            }
            sleepStep = true;
        }
        if (isAdd)
        {
            womanPeople.Move(player.oldPos);
        }
        //检测有没有到达终点
        Vector2 exitvec = mapManager.getplayerBorn(4);
        if (player.targetPos.x == exitvec.x && player.targetPos.y == exitvec.y)
        {
            isEnd = true;
            //加载下一个关卡
            Application.LoadLevel(Application.loadedLevel);//重新加载本关卡
        }
    }

    void OnLevelWasLoaded(int sceneLevel)
    {
        level++;

        //改
        //InitGame();//初始化游戏 
        GameObject.Find("StartPanel").SetActive(false);
        mapManager = GetComponent<MapManager>();
        LoadStoryPanel(level);
    }

    //改
    private void LoadStoryPanel(int level)
    {
        Instantiate(mapManager.storyPanels[level-1]).transform.SetParent(GameObject.Find("Canvas").transform, false);
        AudioManager.Instance.PlayBgMusic(StoryClip);
    }

    private void HideBlack()
    {
        dayImage.gameObject.SetActive(false);
    }

    //1 是主角 2是母亲
    public void setAddButtonVisible(bool type)
    {
        mother_blood = GameObject.FindGameObjectWithTag("motherblood");
        mother_blood.GetComponent<BloodFollowMother>().setBtnVisible(type);
        if (type == true)
        {
            Vector3 player3DPosition = Camera.main.WorldToScreenPoint(player.transform.position);
            player.GetComponentInChildren<Button>().gameObject.transform.position = player3DPosition + new Vector3(0, -80, 0);
        }
        else
        {
            player.GetComponentInChildren<Button>().gameObject.transform.position = new Vector3(-100, -100, 0);
        }

    }
}
