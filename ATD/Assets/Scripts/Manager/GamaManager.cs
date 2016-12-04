using UnityEngine;
using System.Collections;
using LitJson;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class GamaManager : MonoBehaviour
{
    private static GamaManager instance;
    public  static GamaManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(GamaManager)) as GamaManager;
            }

            if (instance == null)
            {
                GameObject obj = new GameObject("GamaManager");
                instance = obj.AddComponent<GamaManager>() as GamaManager;
            }

            return instance;
        }
    }

    public GameObject goRankRegist;
    public UIInput InputRankRegist;
    public UILabel LabelTitle, LabelScore;
    public UIButton BtnRegist, BtnUnRegist;

    public List<MonsterRespawn> responList = new List<MonsterRespawn>();

    void Awake()
    {
        EventDelegate.Add(BtnRegist.onClick, onClickRegist);
        EventDelegate.Add(BtnUnRegist.onClick, onClickUnRegist);
    }

    void Start()
    {
        StartCoroutine("EndCheck");
    }

    public void GameEnd(bool isSuccess)
    {
        goRankRegist.SetActive(true);

        BtnRegist.isEnabled = isSuccess;

        LabelTitle.text = isSuccess ? "Clear" : "Fail";
        LabelScore.text = string.Format("Score : {0}", ShopManager.Instance.Gold.ToString());
    }

    private void onClickRegist()
    {
        StartCoroutine("RankRegist");
    }

    private void onClickUnRegist()
    {
        SceneManager.LoadScene("Start");
    }

    IEnumerator RankRegist()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", InputRankRegist.value);
        form.AddField("score", ShopManager.Instance.Gold);

        WWW www = new WWW("localhost:8080/rank", form);

        yield return www;

        SceneManager.LoadScene("Start");
    }

    IEnumerator EndCheck()
    {
        while(true)
        {
            bool isEnd = false;
            foreach(MonsterRespawn mr in responList)
            {
                isEnd = mr.isEnd;
                if (!isEnd)
                    break;
            }

            if (isEnd)
                break;

            yield return new WaitForSeconds(1);
        }

        while(true)
        {
            bool isEnd = false;
            foreach (MonsterRespawn mr in responList)
            {
                isEnd = mr.transform.childCount == 0;
                if (!isEnd)
                    break;
            }

            if (isEnd)
                break;

            yield return new WaitForSeconds(1);
        }

        GameEnd(true);
    }
}