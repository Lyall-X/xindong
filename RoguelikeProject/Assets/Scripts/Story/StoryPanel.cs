using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// Create time
/// Last revision date 
/// </summary>
public class StoryPanel : MonoBehaviour
{
    public Text[] texts;
    public string[] strings;
    public float[] times;

    public int currentCout = 0;
    private Tween currentTween = null;
    private GameManager gameManager;

    private void Start()
    {
        //DoTween初始化
        currentTween = texts[0].DOText("", 0);
        DOTween.defaultAutoPlay = AutoPlay.None;
        currentTween.SetAutoKill(false);
        //获得gameManager
        gameManager = GameManager.Instance;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !currentTween.IsPlaying())
        {
            currentCout++;
            if (currentCout <= strings.Length)
            {
                OnSession(texts[(currentCout - 1) % 2], strings[currentCout - 1], times[currentCout - 1]);
            }
            else
            {
                gameManager.InitGame();
                AudioManager.Instance.PlayBgMusic(GameManager.Instance.LevelClip);
                gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 对话
    /// </summary>
    /// <param name="text"></param>
    /// <param name="Strings"></param>
    /// <param name="time"></param>
    private void OnSession(Text text ,string Strings,float time)
    {
        text.enabled = true;
        currentTween = text.DOText(Strings, time).OnComplete(()=>SessionComplete(text));
        currentTween.Play();
    }

    /// <summary>
    /// 对话结束 OnComplete
    /// </summary>
    /// <param name="text"></param>
    private void SessionComplete(Text text)
    {
        text.text = "";
        text.enabled = false;
    }
}
