using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Create time
/// Last revision date 
/// </summary>
public class StartPanel : MonoBehaviour
{
    public Button startBtn;
    public GameObject OpeningStroyPanel;
    //背景音乐
    public AudioClip levelClip;
    private void Start()
    {
        startBtn.onClick.AddListener(OnStart);
        AudioManager.Instance.PlayBgMusic(levelClip);
    }
    private void OnStart()
    {
        OpeningStroyPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
