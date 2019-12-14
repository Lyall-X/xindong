using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Create time
/// Last revision date 
/// </summary>
public class BloodFollowMother : MonoBehaviour
{
    private GameObject mother;
    private Image image;

    public Vector3 toScreen;
    public Vector3 offset;
    private void Start()
    {
        mother = GameObject.FindGameObjectWithTag("woman");
        //transform.position = Camera.main.ScreenToWorldPoint(mother.transform.position) + offset;
    }
    private void Update()
    {
        Vector3 player3DPosition = Camera.main.WorldToScreenPoint(mother.transform.position);
        transform.position = player3DPosition + offset;
    }
    public void OnValueChange(int number)
    {
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
