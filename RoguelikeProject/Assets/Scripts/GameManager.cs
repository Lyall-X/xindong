using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager Instance {
        get {
            return _instance;
        }
    }

    public bool isAd { get; internal set; }

    public int level = 1;//当前关卡
    public int food = 100;
    public int mother_food = 100;
    //特效音乐
    public AudioClip dieClip;

    [HideInInspector]public List<Enemy>  enemyList = new List<Enemy>();
    [HideInInspector] public List<woman> womanList = new List<woman>();
    [HideInInspector]public bool isEnd = false;//是否得到终点
    [HideInInspector] public bool isAdd = false;//是否添加老婆
    private bool sleepStep = true;

    private Text foodText;
    private Text failText;
    private Image dayImage;
    private Text dayText;
    private Player player;
    public MapManager mapManager;

    private woman womanPeople;
    public GameObject friendPeople;

    void Awake() {
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
        foodText = GameObject.Find("FoodText").GetComponent<Text>();
        UpdateFoodText(0);
        failText = GameObject.Find("FailText").GetComponent<Text>();
        failText.enabled = false;
        dayImage = GameObject.Find("DayImage").GetComponent<Image>();
        dayText = GameObject.Find("DayText").GetComponent<Text>();
        dayText.text = "Day " + level;
       
        Invoke("HideBlack",1);

        //初始化参数
        isEnd = false;
        enemyList.Clear();
    }

    void UpdateFoodText(int foodChange) {
        if (foodChange == 0) {
            foodText.text = "Food:" + food;
        }
        else {
            string str = "";
            if (foodChange < 0) {
                str = foodChange.ToString();
            }
            else {
                str = "+" + foodChange;
            }
            foodText.text = str + "   Food:" + food;
        }
        
    }

    public void ReduceFood(int count) {
        food -= count;
        UpdateFoodText(-count);
        if (food <= 0) {
            failText.enabled = true;
            AudioManager.Instance.StopBgMusic();
            AudioManager.Instance.RandomPlay(dieClip);
        }
    }

    public void AddFood(int count) {
        food += count;
        UpdateFoodText(count);
    }

    public void OnPlayerMove() {
        if (sleepStep==true) {
            sleepStep = false;
        }
        else {
            foreach (var enemy in enemyList) {
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
        if (player.targetPos.x == exitvec.x && player.targetPos.y == exitvec.y) {
            isEnd = true;
            //加载下一个关卡
            Application.LoadLevel( Application.loadedLevel );//重新加载本关卡
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
    }

    private void HideBlack()
    {
        dayImage.gameObject.SetActive(false);
    }
}
