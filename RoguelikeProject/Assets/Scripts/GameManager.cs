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

    public int level = 1;//当前关卡
    public int food = 100;
    public AudioClip dieClip;

    [HideInInspector]public List<Enemy>  enemyList = new List<Enemy>();
    [HideInInspector] public List<woman> womanList = new List<woman>();
    [HideInInspector]public bool isEnd = false;//是否得到终点
    private bool sleepStep = true;

    private Text foodText;
    private Text failText;
    private Image dayImage;
    private Text dayText;
    private Player player;
    public MapManager mapManager;

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
        if (player && GameManager.Instance.mapManager)
            player.targetPos = GameManager.Instance.mapManager.getplayerBorn(3);

        //初始化UI
        foodText = GameObject.Find("FoodText").GetComponent<Text>();
        UpdateFoodText(0);
        failText = GameObject.Find("FailText").GetComponent<Text>();
        failText.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
        for(int i =0;i < womanList.Count;i++ )
        {
            woman people = womanList[i];
            if (people)
            {
                if (i == 0)
                {
                    people.Move(player.oldPos);
                }
                else
                {
                    people.Move(womanList[i - 1].transform.position);
                }

            }
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
