using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 公開在unity調整參數的C#寫法
    // public float moveSpeed = -5f;

    // 保持隱私在unity調整參數的C#寫法
    [SerializeField] float moveSpeed = -5f;
    GameObject currentFloor;
    [SerializeField] int Hp;

    // Start is called before the first frame update
    void Start()
    {
       Hp = 10;
    }

    // Update is called once per frame
    void Update()
    {
        // 如果(輸入.關鍵鈕(鍵盤.右鍵))
        if(Input.GetKey(KeyCode.D))
        {
        // Time.deltaTime 每秒的間隔時間(為了在每個不同的遊戲上有相同遊戲體驗而做)
            transform.Translate(moveSpeed*Time.deltaTime, 0, 0);
        }

        else if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed*Time.deltaTime, 0, 0);
        }

    }

    // private 可省略，因為是電腦的預設值
    // OnCollisionEnter (會一直重複的被執行直到遊戲結束為止)
    // other 就是碰撞到的物件
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Normal")
        {
            if(other.contacts[0].normal == new Vector2(0f,1f))
            {
                Debug.Log("撞到了第一種階梯");
                currentFloor = other.gameObject;
                ModifyHp(1);
            }
            
        }

        else if(other.gameObject.tag == "Nails")
        {
            if(other.contacts[0].normal == new Vector2(0f,1f))
            {
                Debug.Log("撞到了第二種階梯");
                currentFloor = other.gameObject;
                ModifyHp(-3);
            }
        }

        else if(other.gameObject.tag == "Ceiling")
        {
            Debug.Log("撞到天花板");
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;
            ModifyHp(-3);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "DeathLine")
        {
            Debug.Log("你輸了");
        }

    }

    void ModifyHp(int num)
    {
        Hp += num;
        if(Hp>10)
        {
            Hp = 10;
        }

        else if(Hp<0)
        {
            Hp = 0;
        }
    }

}
