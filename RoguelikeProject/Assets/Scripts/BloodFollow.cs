using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodFollow : MonoBehaviour
{
    public Vector3 offset = new Vector3(0,0,0);
    private GameObject player;
    private Image image;
    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        image = GetComponent<Image>();
        transform.position += offset;
    }
    public void OnValueChange()
    {
        int number = GameManager.Instance.food;
        if (number <= 0)
        {
            image.transform.localScale = new Vector3(0, 0.1f, 1);
        }
        else if (number <= 100)
        {
            image.transform.localScale = new Vector3((float)GameManager.Instance.food / 100, 0.1f, 1);
        }
        else
        {
            image.transform.localScale = new Vector3(1, 0.1f, 1);
        }
    }
}
