using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 公開在unity調整參數的C#寫法
    // public float moveSpeed = -5f;

    // 保持隱私在unity調整參數的C#寫法
    [SerializeField] float moveSpeed = -5f;

    // Start is called before the first frame update
    void Start()
    {
        // transform.Translate(-10,0,0);
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
            Debug.Log("撞到了第一種階梯");
        }

        else if(other.gameObject.tag == "Nails")
        {
            Debug.Log("撞到了第二種階梯");
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "DeathLine")
        {
            Debug.Log("你輸了");
        }

    }

}
