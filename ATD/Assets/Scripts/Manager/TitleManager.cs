using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Text;

public class TitleManager : MonoBehaviour
{
    private static TitleManager instance;
    public  static TitleManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(TitleManager)) as TitleManager;
            }

            if (instance == null)
            {
                GameObject obj = new GameObject("TitleManager");
                instance = obj.AddComponent<TitleManager>() as TitleManager;
            }

            return instance;
        }
    }

    public UIButton BtnStart, BtnRank, BtnExit, BtnRankExit;
    public GameObject goRank;
    public UILabel LabelDescriptionName, LabelDescriptionScore;

    void Awake()
    {
        EventDelegate.Add(BtnStart.onClick, onClickStart);
        EventDelegate.Add(BtnRank.onClick,  onClickRank);
        EventDelegate.Add(BtnExit.onClick,  onClickExit);
        EventDelegate.Add(BtnRankExit.onClick, onClickRankExit);

        NetworkManager.Instance.Init();
    }

    void onClickStart()
    {
        SceneManager.LoadScene("Play");
    }

    void onClickRank()
    {
        goRank.SetActive(true);

        StringBuilder sbName = new StringBuilder();
        StringBuilder sbScore = new StringBuilder();

        List<ScoreData> list = NetworkManager.Instance.GetScoreDataList();
        list.Sort((a, b) =>
        {
            return b.Score.CompareTo(a.Score);
        });

        foreach (ScoreData data in list)
        {
            sbName.AppendLine(data.Name);
            sbScore.AppendLine(data.Score.ToString());
        }

        LabelDescriptionName.text = sbName.ToString();
        LabelDescriptionScore.text = sbScore.ToString();
    }

    void onClickExit()
    {
        Application.Quit();
    }

    private void onClickRankExit()
    {
        goRank.SetActive(false);
    }
}