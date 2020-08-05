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

    #region 大招
    public Image Magic_Bar;
    public float MagicMax;
    public float MagicSC;
    #endregion

    #region 關卡
    public string LevelIDstring;
    int LevelID;
    //關卡圖片
    public Image[] LevelImage;
    public Image[] GameOverLevelImage;
    #endregion

    #region GameOver
    string RewardScoreString;
    int GameOverScore;
    string GameOverScoreString;
    public GameObject RewardScoreObject;
    public GameObject RewardScoreGridObject;
    public GameObject GameOverScoreObject;
    public GameObject GameOverScoreGridObject;
    public List<Image> RewardScoreImage;
    public List<Image> GameOverScoreImage;


    public Button NextGame_B;


    #endregion


    #region 分數
    int score;
    string scoreString;
    //分數圖片
    public List<Image> ScoreImage;

    string SaveTotalScore = "SaveTotalScore";
    //分數物件
    public GameObject ScoreObject;
    //放置分數位置
    public GameObject ScoreGridObject;

    #endregion

    public bool isWin;

    public GameObject GameOverUI;

    public Sprite WinSprite, LosrSpirte;
    public Image ResultImage;

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

    
    public void FinalScore(int AddScore) 
    {
        score += AddScore;

        scoreString = score.ToString();

        PlayerPrefs.SetInt(SaveTotalScore, score);

       

        for (int i = ScoreImage.Count; i < scoreString.Length; i++)
        {
            GameObject ScoreObjectPrefab = Instantiate(ScoreObject) as GameObject;
            ScoreObjectPrefab.transform.SetParent(ScoreGridObject.transform);
            ScoreImage.Add(ScoreObjectPrefab.GetComponent<Image>());
        }

        
        for (int i = 1; i < scoreString.Length; i++)
        {
            ScoreImage[scoreString.Length-i].sprite = NumberSprite[int.Parse(scoreString.Substring(i-1, 1))];
        }
        
        /*
        for (int i = 0; i < scoreString.Length; i++)
        {
            ScoreImage[i].sprite = NumberSprite[int.Parse(scoreString.Substring(i, 1))];
        }
        */


    }



   

    public void PauseGame()
    {

        Time.timeScale = 0;
        PauseObject.SetActive(true);
    }

    public void GameOver(int Credit)
    {
        GameOverUI.SetActive(true);
        Time.timeScale = 0;

        if (isWin)
        {
            ResultImage.sprite = WinSprite;
            NextGame_B.interactable = true;
        }
        else
        {
            ResultImage.sprite = LosrSpirte;
            NextGame_B.interactable = false;
        }

        GameOverLevelImage[0].sprite = NumberSprite[int.Parse(LevelIDstring.Substring(0, 1))];
        GameOverLevelImage[1].sprite = NumberSprite[int.Parse(LevelIDstring.Substring(1, 1))];

        GameOverScore = score + Credit;
        GameOverScoreString = GameOverScore.ToString();
        RewardScoreString = Credit.ToString();

        for (int i =RewardScoreImage.Count; i < RewardScoreString.Length; i++)
        {
            GameObject RewardScorePrefab = Instantiate(ScoreObject) ;

            RewardScorePrefab.transform.SetParent(RewardScoreGridObject.transform);

            RewardScoreImage.Add(RewardScorePrefab.GetComponent<Image>());

        }
        for (int i = 0; i < RewardScoreString.Length; i++)
        {
            RewardScoreImage[i].sprite = NumberSprite[int.Parse(RewardScoreString.Substring(i, 1))];

        }



        for (int i = GameOverScoreImage.Count; i < GameOverScoreString.Length; i++)
        {
            GameObject GameOverScorePrefab = Instantiate(ScoreObject);

            GameOverScorePrefab.transform.SetParent(GameOverScoreGridObject.transform);

            GameOverScoreImage.Add(GameOverScorePrefab.GetComponent<Image>());

        }



        for (int i = 0; i < GameOverScoreString.Length; i++)
        {
            GameOverScoreImage[i].sprite = NumberSprite[int.Parse(GameOverScoreString.Substring(i, 1))];

        }



        //將資料寫入Excel表單
        ExcelWritter.ansList.Add(LevelIDstring);
        ExcelWritter.ansList.Add(GameOverScoreString);
        ExcelWritter.WriteExcel("SaveData", "Data");

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
    
    public void ReStart()
    {

        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
    
    public void NextGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }


   

    private void Start()
    {
        Time.timeScale = 1;
        InvokeRepeating("CreateEnemy", 0.2f, CreateTime);

        LevelID = int.Parse(LevelIDstring);
        FinalScore(0);

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

      
    }



    private void Update()
    {
        MagicSC += Time.deltaTime;
        MagicSC = Mathf.Clamp(MagicSC, 0, MagicMax);
        Magic_Bar.fillAmount = MagicSC / MagicMax;



      
    }









}
