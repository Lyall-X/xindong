using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Create time
/// Last revision date 
/// </summary>
public class ActBlackstoryPanel : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.InitGame();
        AudioManager.Instance.PlayBgMusic(GameManager.Instance.LevelClip);
        gameObject.SetActive(false);
    }
}
