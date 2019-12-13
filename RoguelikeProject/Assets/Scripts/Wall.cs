using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

    public int hp = 2;

    public Sprite damageSprite;//受到攻击的图片

    //自身受到攻击的时候
    public void TakeDamage() {
        hp -= 1;
        GetComponent<SpriteRenderer>().sprite = damageSprite;
        if (hp <= 0) {
            Destroy(this.gameObject);
        }
    }

}
