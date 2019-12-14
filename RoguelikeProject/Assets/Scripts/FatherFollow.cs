using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FatherFollow : MonoBehaviour
{
    public float heigh = 100;
    public int finshLevel = 6;
    private Image image;
    private void Start()
    {
        image = GetComponent<Image>();
        Vector3 imagePosition = image.rectTransform.position;
        image.rectTransform.position = new Vector3(imagePosition.x, imagePosition.y - heigh/finshLevel*GameManager.Instance.level, imagePosition.z);
    }
}
