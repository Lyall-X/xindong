using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MapManager : MonoBehaviour {

    public GameObject[] outWallArray;
    public GameObject[] floorArray;
    public GameObject[] wallArray;
    public GameObject[] foodArray;
    public GameObject[] enemyArray;
    public GameObject exitPrefab;
    public GameObject[] storyPanels;

    public GameObject womanPrefab;
    public GameObject outPathPrefab;

    public int rows=10;
    public int cols=10;

    public int minCountWall = 2;
    public int maxCountWall = 8;

    const int nullfloor = 0;
    const int floor = 1;
    const int outwall = 2;
    const int player_born = 3;
    const int exit = 4;
    const int woman = 5;
    const int outpath = 6;

    private Transform mapHolder;
    private List<Vector2> positionList = new List<Vector2>();

    private GameManager gameManager;

    //地图文件

    private List<int[]> mapType = new List<int[]>();
    // Update is called once per frame
    private void Start()
    {
        gameManager = this.GetComponent<GameManager>();
        mapHolder = new GameObject("Map").transform;
        //加载策划配置
        mapType.Clear();
        string[] mapArr = File.ReadAllLines(Application.dataPath + "/Scripts/map/level" + GameManager.Instance.level + ".txt");
        //string[] mapArr = File.ReadAllLines("C:\\Users\\zzn\\Desktop\\RoguelikeProject\\Assets\\Scripts\\map\\level1.txt");
        for (int x = 0; x < mapArr.Length; ++x)
        {
            string[] sArray = mapArr[x].Split(',');
            mapType.Add(System.Array.ConvertAll<string, int>(sArray, s => int.Parse(s)));
        }
        mapType.Reverse();
        //创建地图坐标映射
        rows = mapType.Count;
        cols = mapType[0].Length;
        Debug.Log("rows:" + rows + "  :cols: " + cols);
        positionList.Clear();
        for (int x = 0; x < cols - 1; x++)
        {
            for (int y = 0; y < rows - 1; y++)
            {
                if (getMapType(x, y) != nullfloor && getMapType(x, y) != outwall && getMapType(x, y) != outpath)
                {
                    positionList.Add(new Vector2(x, y));
                }
            }
        }
    }

    void Update () {
	
	}

    //初始化地图
    public void InitMap()
    {

        //下面是创建围墙和地板
        for (int x = 0; x < cols; x++) {
            for (int y = 0; y < rows; y++)
            {
                if (getMapType(x, y) != nullfloor && getMapType(x, y) != outwall)
                {
                    int index = Random.Range(0, floorArray.Length);
                    GameObject go = GameObject.Instantiate(floorArray[index], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mapHolder);
                }
                if (getMapType(x,y) == outwall) {
                    int index = Random.Range(0, outWallArray.Length);
                    GameObject go =  GameObject.Instantiate(outWallArray[index], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mapHolder);
                }
                //创建不可穿越地形
                else if (getMapType(x, y) == outpath)
                {
                    GameObject go = GameObject.Instantiate(outPathPrefab, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(mapHolder);
                }
                //创建母亲
                if (getMapType(x, y) == woman && !GameManager.Instance.isAdd)
                {
                    woman womanPeople = GameObject.FindGameObjectWithTag("woman").GetComponent<woman>();
                    womanPeople.transform.position = new Vector3(x, y, 0);
                }
            }
        }

        //创建障碍物 食物 敌人
        //创建障碍物
        int wallCount = Random.Range(minCountWall, maxCountWall + 1);//障碍物的个数
        InstantiateItems(wallCount,wallArray);
        //创建食物 2 - level*2
        int foodCount = Random.Range(2, gameManager.level*2 + 1);
        InstantiateItems(foodCount,foodArray);
        //创建敌人 // level/2
        int enemyCount = gameManager.level/2;
        InstantiateItems(enemyCount,enemyArray);
        //创建出口
        GameObject go4 = Instantiate(exitPrefab, getplayerBorn(exit), Quaternion.identity) as GameObject;
        go4.transform.SetParent(mapHolder);
    }
    private int getMapType(int x, int y)
    {
        return mapType[y][x];
    }

    public Vector2 getplayerBorn(int type)
    {
        Vector2 vec = new Vector2();
        for (int x = 0; x < cols - 1; x++)
        {
            for (int y = 0; y < rows - 1; y++)
            {
                if (getMapType(x, y) == type)
                {
                    vec.x = x;
                    vec.y = y;
                }
            }
        }
        return vec;
    }

    private void InstantiateItems(int count, GameObject[] prefabs) {
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = RandomPosition();
            GameObject enemyPrefab = RandomPrefab(prefabs);
            GameObject go = Instantiate(enemyPrefab, pos, Quaternion.identity) as GameObject;
            go.transform.SetParent(mapHolder);
        }
    }
    private Vector2 RandomPosition() {
        int positionIndex = Random.Range(0, positionList.Count);
        Vector2 pos = positionList[positionIndex];
        positionList.RemoveAt(positionIndex);
        return pos;
    }

    private GameObject RandomPrefab(GameObject[] prefabs) {
        int index = Random.Range(0, prefabs.Length);
        return prefabs[index];
    }

}
