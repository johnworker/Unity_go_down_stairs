using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // 公開在unity調整參數的C#寫法
    // public float moveSpeed = -5f;

    // 保持隱私在unity調整參數的C#寫法
    [SerializeField] float moveSpeed = -5f;
    GameObject currentFloor;
    [SerializeField] int Hp;
    [SerializeField] GameObject HpBar;
    [SerializeField] Text ScoreTextMeshPro;
    [SerializeField] private TextMeshProUGUI ScoreTextElement;

    int score;
    float scoreTime;

    Animator anim;
    SpriteRenderer render;

    AudioSource deathSound;

    [SerializeField] GameObject replayButton;


    // Start is called before the first frame update
    void Start()
    {
       Hp = 10;
       score = 0;
       scoreTime = 0f;
       anim = GetComponent<Animator>();
       render = GetComponent<SpriteRenderer>();
       deathSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 如果(輸入.關鍵鈕(鍵盤.右鍵))
        if(Input.GetKey(KeyCode.D))
        {
        // Time.deltaTime 每秒的間隔時間(為了在每個不同的遊戲上有相同遊戲體驗而做)
            transform.Translate(moveSpeed*Time.deltaTime, 0, 0);
            render.flipX = false;
            anim.SetBool("run", true);
        }

        else if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed*Time.deltaTime, 0, 0);
            render.flipX = true;
            anim.SetBool("run", true);
        }

        else
        {
            anim.SetBool("run", false);
        }

        updateScore();

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
                currentFloor = other.gameObject;
                ModifyHp(1);
                
                other.gameObject.GetComponent<AudioSource>().Play();
            }
            
        }

        else if(other.gameObject.tag == "Nails")
        {
            if(other.contacts[0].normal == new Vector2(0f,1f))
            {
                currentFloor = other.gameObject;
                ModifyHp(-3);
                anim.SetTrigger("hurt");

                other.gameObject.GetComponent<AudioSource>().Play();
            }
        }

        else if(other.gameObject.tag == "Ceiling")
        {
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;
            ModifyHp(-3);
            anim.SetTrigger("hurt");

            other.gameObject.GetComponent<AudioSource>().Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "DeathLine")
        {
            Die();
        }

    }

    void ModifyHp(int num)
    {
        Hp += num;
        if(Hp>10)
        {
            Hp = 10;
        }

        else if(Hp<=0)
        {
            Hp = 0;
            deathSound.Play();
            // 遊戲時間縮放比例
            Time.timeScale = 0f;
            Die();
        }

        UpdateHpBar();
    }

    void UpdateHpBar()
    {
        for(int i=0; i<HpBar.transform.childCount; i++)
        {
            if(Hp>i)
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(true);
            }

            else
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void updateScore()
    {
        scoreTime += Time.deltaTime;
        if(scoreTime>2f)
        {
            score++;
            scoreTime = 0f;
            ScoreTextElement.text = "地下" + score.ToString() + "層";
        }
    }

    void Die()
    {
        deathSound.Play();
        Time.timeScale = 0f;
        replayButton.SetActive(true);
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

}
