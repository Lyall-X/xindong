using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Create time
/// Last revision date 
/// </summary>
public class InteractStoryPanel : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(false);
        }
    }
}
