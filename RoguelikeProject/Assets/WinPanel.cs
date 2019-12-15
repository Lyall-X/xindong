using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// Create time
/// Last revision date 
/// </summary>
public class WinPanel : MonoBehaviour
{
    public string str;
    public float time;
    void Start()
    {
        GetComponent<Image>().DOFade(0, 3).From().OnComplete(() =>
        {
            GetComponentInChildren<Text>().DOText(str, time);
        }
        );
    }
}
