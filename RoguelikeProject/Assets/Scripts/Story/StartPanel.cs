using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// Create time
/// Last revision date 
/// </summary>
public class StartPanel : MonoBehaviour
{   

    public Button startBtn;
    public Image bgIma;

    public Image[] fireIma;
    //相对位置
    public Vector3 fireDownLeftPositon;
    public Vector3 fireDownRightPositon;

    public float[] intervalTime;
    public int currentTime = 0;

    public float timeCounter = 0;
    //private Tween tween;
    private void Start()
    {
        startBtn.onClick.AddListener(OnStart);
        //播放CG音乐
        AudioManager.Instance.PlayBgMusic(GameManager.Instance.LevelClip);

        //tween.SetAutoKill(false);
        //tween = bgIma.transform.DOLocalMove(new Vector3(-5663, 0, 0), intervalTime[currentTime++]);
        DOTween.defaultEaseType = Ease.InOutSine;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(bgIma.transform.DOLocalMove(new Vector3(-5663, 0, 0), intervalTime[currentTime++]))
            .Append(bgIma.transform.DOLocalMove(new Vector3(-8926, 0, 0), intervalTime[currentTime++]))
            .Append(bgIma.transform.DOLocalMove(new Vector3(-10213, 0, 0), intervalTime[currentTime++])).OnComplete(Boom)
            .Append(bgIma.transform.DOLocalMove(new Vector3(-13000, 0, 0), intervalTime[currentTime++]));
                       
    }

    private void Boom()
    {
        Vector3 position;
        for (int i = 0; i <= 6; i++)
        {
            position = fireIma[i].transform.localPosition;
            if (i <= 2)
            {
                fireIma[i].transform.DOLocalMove(position + fireDownLeftPositon, 5f);
            }
            else
            {
                fireIma[i].transform.DOLocalMove(position + fireDownRightPositon, 7f);
            }
        }

    }
    private void OnStart()
    {
        //Level声音
        GameManager.Instance.InitGame();     
        AudioManager.Instance.PlayBgMusic(GameManager.Instance.LevelClip);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Level声音
            GameManager.Instance.InitGame();          
            AudioManager.Instance.PlayBgMusic(GameManager.Instance.LevelClip);
            gameObject.SetActive(false);
        }
    }
}
