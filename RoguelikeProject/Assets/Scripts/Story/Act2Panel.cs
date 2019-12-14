using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// Create time
/// Last revision date 
/// </summary>
public class Act2Panel : MonoBehaviour
{
    public Text[] texts;
    public string[] strings;
    public float[] times;
    public Font font;

    public bool nestSonSpeak = false;

    private int currentCout = 0;
    private Tween currentTween = null;
    private GameManager gameManager;

    private void Start()
    {
        foreach (Text item in texts)
        {
            item.color = Color.white;
            item.font = font;
            item.fontSize = 30;
        }

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
            if (nestSonSpeak)
            {
                texts[0].transform.parent.gameObject.SetActive(true);
                texts[1].transform.parent.gameObject.SetActive(false);
            }
            else
            {
                texts[0].transform.parent.gameObject.SetActive(false);
                texts[1].transform.parent.gameObject.SetActive(true);
            }
            if (currentCout <= strings.Length)
            {
                switch (currentCout)
                {
                    case 1:
                    case 2:
                    case 5:
                    case 9:
                        OnSession(texts[0], strings[currentCout - 1], times[currentCout - 1]);
                        
                        break;
                    case 3:
                    case 4:
                    case 6:
                    case 7:
                    case 8:
                        OnSession(texts[1], strings[currentCout - 1], times[currentCout - 1]);
                        break;
                }
                if (currentCout + 1 == 2 || currentCout + 1 == 5 || currentCout + 1 == 9)
                {
                    nestSonSpeak = true;
                }
                else
                {
                    nestSonSpeak = false;
                }
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
        currentTween = text.DOText(Strings, time);
        currentTween.Play();
    }

}
