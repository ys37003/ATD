using UnityEngine;
using System.Collections;

public class BackTowerList : MonoBehaviour
{
    [SerializeField] UIButton btn = null;

    public System.Action OnClickBtn;

    void Awake()
    {
        EventDelegate.Add(btn.onClick, onClickBtn);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    private void onClickBtn()
    {
        if (OnClickBtn != null)
            OnClickBtn();
    }
}
