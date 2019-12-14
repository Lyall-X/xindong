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
    public Image bgIma;
    public GameObject OpeningStroyPanel;

    public Image[] fireIma;
    public float[] intervalTime;

    Sequence sequence;
    private void Start()
    {
        //播放CG音乐
        AudioManager.Instance.PlayBgMusic(GameManager.Instance.LevelClip);
        //bgIma.transform.DOLocalMove(new Vector3(-26447, 0, 0), intervalTime[currentTime++]).SetEase(Ease.Linear);

        sequence = DOTween.Sequence();
        sequence.Append(bgIma.transform.DOLocalMove(new Vector3(-13224, 0, 0), intervalTime[0]).SetEase(Ease.Linear));
        //        .Join(fireIma[count++].transform.DOShakeRotation(1));
        //    .Append(bgIma.transform.DOLocalMove(new Vector3(-8926, 0, 0), intervalTime[currentTime++]))
        //    .Append(bgIma.transform.DOLocalMove(new Vector3(-10213, 0, 0), intervalTime[currentTime++])).OnComplete(Boom)
        //    .Append(bgIma.transform.DOLocalMove(new Vector3(-13000, 0, 0), intervalTime[currentTime++]));

    }

    //private void Boom()
    //{
    //    Vector3 position;
    //    for (int i = 0; i <= 6; i++)
    //    {
    //        position = fireIma[i].transform.localPosition;
    //        if (i <= 2)
    //        {
    //            fireIma[i].transform.DOLocalMove(position + fireDownLeftPositon, 5f);
    //        }
    //        else
    //        {
    //            fireIma[i].transform.DOLocalMove(position + fireDownRightPositon, 7f);
    //        }
    //    }

    //}

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.PlayBgMusic(GameManager.Instance.StoryClip);
            OpeningStroyPanel.SetActive(true);
            gameObject.SetActive(false);
        }
        if (!sequence.IsPlaying())
        {
            bgIma.DOFade(0, 3).OnComplete(()=>
            {
                AudioManager.Instance.PlayBgMusic(GameManager.Instance.StoryClip);
                OpeningStroyPanel.SetActive(true);
                gameObject.SetActive(false);
            });
        }

    }
}
