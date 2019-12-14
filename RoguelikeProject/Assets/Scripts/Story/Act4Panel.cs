using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// Create time
/// Last revision date 
/// </summary>
public class Act4Panel : MonoBehaviour
{
    public Text text;
    public string[] strings;
    public float time;
    public Font font;

    private int currentCout = 0;
    private Tween currentTween = null;
    private GameManager gameManager;

    private void Start()
    {
        text.color = Color.white;
        text.font = font;
        text.fontSize = 30;

        //DoTween初始化
        currentTween = text.DOText("", 0);
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
                OnSession(text, strings[currentCout - 1], time);
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
    private void OnSession(Text text, string Strings, float time)
    {
        text.transform.parent.gameObject.SetActive(true);
        text.text = "";
        currentTween = text.DOText(Strings, time).OnComplete(() => SessionComplete(text));
        currentTween.Play();
    }

    /// <summary>
    /// 对话结束 OnComplete
    /// </summary>
    /// <param name="text"></param>
    private void SessionComplete(Text text)
    {
    }
}
