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
    private void Start()
    {
        startBtn.onClick.AddListener(OnStart);
    }
    private void OnStart()
    {
        OpeningStroyPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
