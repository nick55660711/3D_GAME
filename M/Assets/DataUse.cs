using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Extensions;
using Firebase.Database;
using Proyecto26;
public class DataUse : MonoBehaviour
{
    public UnityEvent OnFirebaseitialized = new UnityEvent();
    DatabaseReference reference;
    public InputField UserIDInputField;
    public InputField PasswordInputField;
    public Text ScoreText;
    static public string UserID;
    static public string Password;
    static public int Score;
    public List<string> DatabaseData;


    public InputField SignInUserIDInputField;
    public InputField SignInPasswordInputField;
    public Text ErroeInfo;
    private void Start()
    {
        
        //Firebase 初始化
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.Log("無法初始化" + task.Exception);
            }
            OnFirebaseitialized.Invoke();
        });

        //串接Firebase資料表網址(每個資料庫網址不同)
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://reimjump.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }


    //按下註冊按鈕將使用者資料紀錄到Firebase中
    public void SaveDataToFireBase()
    {
        UserID = UserIDInputField.text;
        Password = PasswordInputField.text;
        Score = 0;
        PostToDatabase();

    }
    //發送給Database

    void PostToDatabase()
    {
        User user = new User();
        RestClient.Put("https://reimjump.firebaseio.com/" + UserID + ".json", user);
    }

    public string[] Datas;
    public string[] PasswordData;
    public void GetDataFormFirebase()
    {
        DatabaseData.Clear();
        RestClient.Get("https://reimjump.firebaseio.com/" +SignInUserIDInputField.text + ".json").Then(Response =>
        {
            if (Response.Text != "null")
            {
                DatabaseData.Add(Response.Text);
                 Datas = DatabaseData[0].Split(',');
                 PasswordData = Datas[0].Split(':');
                string[] PasswordData1 = PasswordData[1].Split('"');

                    print(PasswordData[1].Substring(1, PasswordData[1].Length-2));
                    print(PasswordData1[1]);

                if (SignInPasswordInputField.text == PasswordData[1].Substring(1, PasswordData[1].Length - 2))

                {
                    ErroeInfo.text = "帳號密碼正確，登入";
                    StartCoroutine(StartMenu());

                }
                else { ErroeInfo.text = "密碼錯誤"; }
            }

            else
            {
                ErroeInfo.text = "此帳號尚未註冊";
            }


        });

       

    }

    public GameObject SignUI;
    public GameObject MenuUI;

    IEnumerator StartMenu()
    {

        yield return new WaitForSeconds(3f);
        SignUI.SetActive(false);
        MenuUI.SetActive(true);
    }

}
