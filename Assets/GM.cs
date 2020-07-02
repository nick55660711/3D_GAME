using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public GameObject Enemy1;
    public GameObject Boss; 
    public GameObject EnemyCreatePoint;
    public float CreateTime;
    public float MaxNumber;
    public float DeadNumber;
    int Number;
    public GameObject PauseObject;
    public Image MonsterL_Bar;
    public Image Magic_Bar;
    public float MagicMax;
    public float MagicSC;

    public string LevelIDstring;
    int LevelID;
    public Image[] LevelImage;
    public Sprite[] NumberSprite;

    public void CreateEnemy()
    {
        if (Number < MaxNumber)
        {
            Vector3 MaxValue = EnemyCreatePoint.GetComponent<Collider>().bounds.max;
            Vector3 MinValue = EnemyCreatePoint.GetComponent<Collider>().bounds.min;
            Vector3 RandomPox = new Vector3(Random.Range(MinValue.x, MaxValue.x), MinValue.y, MinValue.z);


            Instantiate(Enemy1, RandomPox - Vector3.right, EnemyCreatePoint.transform.rotation);

            Number++;
        }
        else if (Number == MaxNumber && GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            Instantiate(Boss, EnemyCreatePoint.transform.position, EnemyCreatePoint.transform.rotation);
            CancelInvoke();
        }

    }

    public void DeadCount()
    {
        DeadNumber ++;
        MonsterL_Bar.fillAmount = (MaxNumber - DeadNumber) / MaxNumber;

    }


    public void CreateBoss()
    {
       



    }

    public void PauseGame()
    {

        Time.timeScale = 0;
        PauseObject.SetActive(true);
    }


    public void Return()
    {

        Time.timeScale = 1;
        PauseObject.SetActive(false);
    }

    public void BackMenu()
    {

        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");

    }

    private void Start()
    {
        Time.timeScale = 1;
        InvokeRepeating("CreateEnemy", 0.2f, CreateTime);

        LevelID = int.Parse(LevelIDstring);


        // 寫法1
        /*
        int a = LevelID / 10;
        int b = LevelID % 10;
        LevelImage[0].sprite = NumberSprite[a]; // 十位數
        LevelImage[1].sprite = NumberSprite[b]; // 個位數
        */

        //寫法2
        /*
        string[] TotalLevelString = LevelIDstring.Split('_'); //切割有_的字串  4_8 會轉變成 4,8 兩個字串
        print(TotalLevelString[0]);
        LevelImage[0].sprite = NumberSprite[int.Parse(TotalLevelString[0])]; // 十位數
        LevelImage[1].sprite = NumberSprite[int.Parse(TotalLevelString[1])];
        */
        //寫法3
        LevelImage[0].sprite = NumberSprite[int.Parse(LevelIDstring.Substring(0,1))]; // 十位數   抓取子字串 SubString(字串開始位置,長度)  0是字串開頭的位置
        LevelImage[1].sprite = NumberSprite[int.Parse(LevelIDstring.Substring(1,1))];

        print(LevelIDstring.Substring(0, 1));
        print(LevelIDstring.Substring(1, 1));
        print(LevelIDstring.Substring(0, 2));

    }



    private void Update()
    {
        MagicSC += Time.deltaTime;
        MagicSC = Mathf.Clamp(MagicSC, 0, MagicMax);
        Magic_Bar.fillAmount = MagicSC / MagicMax;

    }









}
