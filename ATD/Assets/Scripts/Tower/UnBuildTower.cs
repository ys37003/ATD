using UnityEngine;

public class UnBuildTower : MonoBehaviour
{
    [SerializeField] UIButton btn = null;

    public System.Action OnClickBtn;

    public Tower Target { get; set; }

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
        if (Target != null)
            Target.UnBuildTower();

        Target = null;

        if (OnClickBtn != null)
            OnClickBtn();
    }
}