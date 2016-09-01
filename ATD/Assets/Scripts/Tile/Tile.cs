using UnityEngine;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
    public struct Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Position(Position pos)
        {
            x = pos.x;
            y = pos.y;
        }
    }

    [SerializeField] private List<SpriteRenderer> srHoverList = new List<SpriteRenderer>();
    [SerializeField] private GameObject goHover = null;

    public System.Action<Position, bool> OnHoverTile;
    public System.Action OnBuildTower;

    public delegate bool OnUICheck();
    public OnUICheck IsUI;

    private Position pos = new Position();
    public  Position Pos
    {
        get { return pos; }
        private set { pos = value; }
    }

    public bool Built { get; set; }

    void Start()
    {
        string[] strPos = name.Substring(5, 5).Split(',');
        pos.x = int.Parse(strPos[0]);
        pos.y = int.Parse(strPos[1]);
    }

    public void GoHoverActive(bool active)
    {
        goHover.SetActive(active);
    }

    public void InitHoverColor(bool built)
    {
        foreach(SpriteRenderer sr in srHoverList)
        {
            sr.color = built ? new Color32(255, 31, 31, 155) : new Color32(31, 220, 31, 155);
        }
    }

    private void OnMouseEnter()
    {
        if (IsUI == null || IsUI())
            return;

        if (OnHoverTile != null)
            OnHoverTile(Pos, true);
    }

    private void OnMouseExit()
    {
        if (OnHoverTile != null)
            OnHoverTile(Pos, false);
    }

    public void OnMouseUp()
    {
        if (IsUI == null || IsUI())
        {
            Debug.Log("IsUI == null || IsUI()");
            return;
        }

        if (OnBuildTower != null)
            OnBuildTower();
    }
}