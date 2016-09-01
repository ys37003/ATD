using UnityEngine;
using System.Collections.Generic;

public enum E_TileSize
{
    Tile1,
    Tile4,
    Tile9,
}

public class TileManager : MonoBehaviour
{
    private static TileManager instance;
    public  static TileManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(TileManager)) as TileManager;
            }

            if (instance == null)
            {
                GameObject obj = new GameObject("TileManager");
                instance = obj.AddComponent<TileManager>() as TileManager;
            }

            return instance;
        }
    }


    [SerializeField] private UIGrid     grid = null;
    [SerializeField] private Transform  tfNormalTiles = null, tfQuadTiles = null;
    [SerializeField] private GameObject goNormalTiles = null, goQuadTiles = null;

    private Dictionary<Tile.Position, Tile> tileDic;
    private E_TileSize tileSize = E_TileSize.Tile9;

    private List<Tile.Position> hoveredTIlePosList = new List<Tile.Position>();

    void Awake()
    {
        InitTile();
    }

    public Vector3 GetTIleRealPosision(Tile.Position pos)
    {
        if (tileDic.ContainsKey(pos))
            return tileDic[pos].transform.position;

        return Vector3.one * 999;
    }

    public void OnClickTowerInfo(TowerSimpleData data)
    {
        InitHoveredTileList();

        tileSize = data.TowerSize;

        switch (tileSize)
        {
            case E_TileSize.Tile1:
            case E_TileSize.Tile9:
                {
                    goNormalTiles.SetActive(true);
                    goQuadTiles.SetActive(false);
                }
                break;
            case E_TileSize.Tile4:
                {
                    goNormalTiles.SetActive(false);
                    goQuadTiles.SetActive(true);
                }
                break;
        }
    }

    public void TileActiveFalse()
    {
        InitHoveredTileList();

        goNormalTiles.SetActive(false);
        goQuadTiles.SetActive(false);
    }

    private void onHoverTile(Tile.Position pos, bool isHover)
    {
        switch (tileSize)
        {
            case E_TileSize.Tile1:
                {
                    bool built = tileDic[pos].Built;
                    TIleHover(new Tile.Position(pos.x, pos.y), isHover, built);
                }
                break;

            case E_TileSize.Tile4:
                {
                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //sb.AppendLine("hover");
                    //sb.AppendLine(string.Format("[__,__]/[{0:D2},{1:D2}]/[__,__]", pos.x, pos.y - 1));
                    //sb.AppendLine(string.Format("[{0:D2},{1:D2}]/[{2:D2},{3:D2}]/[{4:D2},{5:D2}]", pos.x - 1, pos.y, pos.x, pos.y, pos.x + 1, pos.y));
                    //sb.AppendLine(string.Format("[__,__]/[{0:D2},{1:D2}]/[__,__]", pos.x, pos.y + 1));
                    //Debug.Log(sb.ToString());

                    bool built = tileDic[new Tile.Position(pos.x    , pos.y - 1)].Built ||
                                 tileDic[new Tile.Position(pos.x - 1, pos.y    )].Built ||
                                 tileDic[new Tile.Position(pos.x    , pos.y    )].Built ||
                                 tileDic[new Tile.Position(pos.x + 1, pos.y    )].Built ||
                                 tileDic[new Tile.Position(pos.x    , pos.y + 1)].Built;

                    TIleHover(new Tile.Position(pos.x, pos.y), isHover, built);
                }
                break;

            case E_TileSize.Tile9:
                {
                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //sb.AppendLine("hover");
                    //sb.AppendLine(string.Format("[__,__]/[__,__]/[{0:D2},{1:D2}]/[__,__]/[__,__]", pos.x, pos.y - 2));
                    //sb.AppendLine(string.Format("[__,__]/[{0:D2},{1:D2}]/[{2:D2},{3:D2}]/[{4:D2},{5:D2}]/[__,__]", pos.x - 1, pos.y - 1, pos.x, pos.y - 1, pos.x + 1, pos.y - 1));
                    //sb.AppendLine(string.Format("[{0:D2},{1:D2}]/[{2:D2},{3:D2}]/[{4:D2},{5:D2}]/[{6:D2},{7:D2}]/[{8:D2},{9:D2}]", pos.x - 2, pos.y, pos.x - 1, pos.y, pos.x, pos.y, pos.x + 1, pos.y, pos.x + 2, pos.y));
                    //sb.AppendLine(string.Format("[__,__]/[{0:D2},{1:D2}]/[{2:D2},{3:D2}]/[{4:D2},{5:D2}]/[__,__]", pos.x - 1, pos.y + 1, pos.x, pos.y + 1, pos.x + 1, pos.y + 1));
                    //sb.AppendLine(string.Format("[__,__]/[__,__]/[{0:D2},{1:D2}]/[__,__]/[__,__]", pos.x, pos.y + 2));
                    //Debug.Log(sb.ToString());

                    bool built = tileDic[new Tile.Position(pos.x    , pos.y - 2)].Built ||
                                 tileDic[new Tile.Position(pos.x - 1, pos.y - 1)].Built ||
                                 tileDic[new Tile.Position(pos.x    , pos.y - 1)].Built ||
                                 tileDic[new Tile.Position(pos.x + 1, pos.y - 1)].Built ||
                                 tileDic[new Tile.Position(pos.x - 2, pos.y    )].Built ||
                                 tileDic[new Tile.Position(pos.x - 1, pos.y    )].Built ||
                                 tileDic[new Tile.Position(pos.x    , pos.y    )].Built ||
                                 tileDic[new Tile.Position(pos.x + 1, pos.y    )].Built ||
                                 tileDic[new Tile.Position(pos.x + 2, pos.y    )].Built ||
                                 tileDic[new Tile.Position(pos.x - 1, pos.y + 1)].Built ||
                                 tileDic[new Tile.Position(pos.x    , pos.y + 1)].Built ||
                                 tileDic[new Tile.Position(pos.x + 1, pos.y + 1)].Built ||
                                 tileDic[new Tile.Position(pos.x    , pos.y + 2)].Built;               

                    TIleHover(new Tile.Position(pos.x    , pos.y - 2), isHover, built);
                    TIleHover(new Tile.Position(pos.x - 1, pos.y - 1), isHover, built);
                    TIleHover(new Tile.Position(pos.x + 1, pos.y - 1), isHover, built);
                    TIleHover(new Tile.Position(pos.x - 2, pos.y    ), isHover, built);
                    TIleHover(new Tile.Position(pos.x    , pos.y    ), isHover, built);
                    TIleHover(new Tile.Position(pos.x + 2, pos.y    ), isHover, built);
                    TIleHover(new Tile.Position(pos.x - 1, pos.y + 1), isHover, built);
                    TIleHover(new Tile.Position(pos.x + 1, pos.y + 1), isHover, built);
                    TIleHover(new Tile.Position(pos.x    , pos.y + 2), isHover, built);
                }
                break;
        }
    }

    public bool CanBuild(Tile.Position pos)
    {
        switch (tileSize)
        {
            case E_TileSize.Tile1:
                {
                    if (!tileDic[pos].Built)
                    {
                        tileDic[pos].Built = true;
                        return true;
                    }
                }
                break;
            case E_TileSize.Tile4:
                {
                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //sb.AppendLine(string.Format("[__,__]/[{0:D2},{1:D2}]/[__,__]", pos.x, pos.y - 1));
                    //sb.AppendLine(string.Format("[{0:D2},{1:D2}]/[{2:D2},{3:D2}]/[{4:D2},{5:D2}]", pos.x - 1, pos.y, pos.x, pos.y, pos.x + 1, pos.y));
                    //sb.AppendLine(string.Format("[__,__]/[{0:D2},{1:D2}]/[__,__]", pos.x, pos.y + 1));
                    //Debug.Log(sb.ToString());

                    if (!tileDic[new Tile.Position(pos.x    , pos.y - 1)].Built &&
                        !tileDic[new Tile.Position(pos.x - 1, pos.y    )].Built &&
                        !tileDic[new Tile.Position(pos.x    , pos.y    )].Built &&
                        !tileDic[new Tile.Position(pos.x + 1, pos.y    )].Built &&
                        !tileDic[new Tile.Position(pos.x    , pos.y + 1)].Built)
                    {
                        tileDic[new Tile.Position(pos.x    , pos.y - 1)].Built = true;
                        tileDic[new Tile.Position(pos.x - 1, pos.y    )].Built = true;
                        tileDic[new Tile.Position(pos.x    , pos.y    )].Built = true;
                        tileDic[new Tile.Position(pos.x + 1, pos.y    )].Built = true;
                        tileDic[new Tile.Position(pos.x    , pos.y + 1)].Built = true;
                        return true;
                    }
                }
                break;
            case E_TileSize.Tile9:
                {
                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //sb.AppendLine(string.Format("[__,__]/[__,__]/[{0:D2},{1:D2}]/[__,__]/[__,__]", pos.x, pos.y - 2));
                    //sb.AppendLine(string.Format("[__,__]/[{0:D2},{1:D2}]/[{2:D2},{3:D2}]/[{4:D2},{5:D2}]/[__,__]", pos.x - 1, pos.y - 1, pos.x, pos.y - 1, pos.x + 1, pos.y - 1));
                    //sb.AppendLine(string.Format("[{0:D2},{1:D2}]/[{2:D2},{3:D2}]/[{4:D2},{5:D2}]/[{6:D2},{7:D2}]/[{8:D2},{9:D2}]", pos.x - 2, pos.y, pos.x - 1, pos.y, pos.x, pos.y, pos.x + 1, pos.y, pos.x + 2, pos.y));
                    //sb.AppendLine(string.Format("[__,__]/[{0:D2},{1:D2}]/[{2:D2},{3:D2}]/[{4:D2},{5:D2}]/[__,__]", pos.x - 1, pos.y + 1, pos.x, pos.y + 1, pos.x + 1, pos.y + 1));
                    //sb.AppendLine(string.Format("[__,__]/[__,__]/[{0:D2},{1:D2}]/[__,__]/[__,__]", pos.x, pos.y + 2));
                    //Debug.Log(sb.ToString());

                    if (!tileDic[new Tile.Position(pos.x    , pos.y - 2)].Built &&
                        !tileDic[new Tile.Position(pos.x - 1, pos.y - 1)].Built &&
                        !tileDic[new Tile.Position(pos.x    , pos.y - 1)].Built &&
                        !tileDic[new Tile.Position(pos.x + 1, pos.y - 1)].Built &&
                        !tileDic[new Tile.Position(pos.x - 2, pos.y    )].Built &&
                        !tileDic[new Tile.Position(pos.x - 1, pos.y    )].Built &&
                        !tileDic[new Tile.Position(pos.x    , pos.y    )].Built &&
                        !tileDic[new Tile.Position(pos.x + 1, pos.y    )].Built &&
                        !tileDic[new Tile.Position(pos.x + 2, pos.y    )].Built &&
                        !tileDic[new Tile.Position(pos.x - 1, pos.y + 1)].Built &&
                        !tileDic[new Tile.Position(pos.x    , pos.y + 1)].Built &&
                        !tileDic[new Tile.Position(pos.x + 1, pos.y + 1)].Built &&
                        !tileDic[new Tile.Position(pos.x    , pos.y + 2)].Built)
                    {
                        tileDic[new Tile.Position(pos.x    , pos.y - 2)].Built = true;
                        tileDic[new Tile.Position(pos.x - 1, pos.y - 1)].Built = true;
                        tileDic[new Tile.Position(pos.x    , pos.y - 1)].Built = true;
                        tileDic[new Tile.Position(pos.x + 1, pos.y - 1)].Built = true;
                        tileDic[new Tile.Position(pos.x - 2, pos.y    )].Built = true;
                        tileDic[new Tile.Position(pos.x - 1, pos.y    )].Built = true;
                        tileDic[new Tile.Position(pos.x    , pos.y    )].Built = true;
                        tileDic[new Tile.Position(pos.x + 1, pos.y    )].Built = true;
                        tileDic[new Tile.Position(pos.x + 2, pos.y    )].Built = true;
                        tileDic[new Tile.Position(pos.x - 1, pos.y + 1)].Built = true;
                        tileDic[new Tile.Position(pos.x    , pos.y + 1)].Built = true;
                        tileDic[new Tile.Position(pos.x + 1, pos.y + 1)].Built = true;
                        tileDic[new Tile.Position(pos.x    , pos.y + 2)].Built = true;
                        return true;
                    }
                }
                break;
        }

        Debug.Log("CanBuild is false");
        return false;
    }

    public bool CanUpgrade(E_TileSize before, E_TileSize after, Tile.Position pos)
    {
        if (before == after)
            return true;

        if (before == E_TileSize.Tile1 && after == E_TileSize.Tile4)
        {
            if (!tileDic[new Tile.Position(pos.x - 1, pos.y    )].Built &&
                !tileDic[new Tile.Position(pos.x + 1, pos.y    )].Built &&
                !tileDic[new Tile.Position(pos.x    , pos.y - 1)].Built &&
                !tileDic[new Tile.Position(pos.x    , pos.y + 1)].Built)
            {
                return true;
            }
        }
        else if (before == E_TileSize.Tile1 && after == E_TileSize.Tile9)
        {
            if (!tileDic[new Tile.Position(pos.x - 1, pos.y    )].Built &&
                !tileDic[new Tile.Position(pos.x + 1, pos.y    )].Built &&
                !tileDic[new Tile.Position(pos.x    , pos.y - 1)].Built &&
                !tileDic[new Tile.Position(pos.x    , pos.y + 1)].Built &&
                !tileDic[new Tile.Position(pos.x - 2, pos.y    )].Built &&
                !tileDic[new Tile.Position(pos.x + 2, pos.y    )].Built &&
                !tileDic[new Tile.Position(pos.x    , pos.y - 2)].Built &&
                !tileDic[new Tile.Position(pos.x    , pos.y + 2)].Built &&
                !tileDic[new Tile.Position(pos.x - 1, pos.y + 1)].Built &&
                !tileDic[new Tile.Position(pos.x + 1, pos.y + 1)].Built &&
                !tileDic[new Tile.Position(pos.x + 1, pos.y - 1)].Built &&
                !tileDic[new Tile.Position(pos.x - 1, pos.y - 1)].Built)
            {
                return true;
            }
        }
        else if (before == E_TileSize.Tile4 && after == E_TileSize.Tile9)
        {
            if (!tileDic[new Tile.Position(pos.x - 2, pos.y    )].Built &&
                !tileDic[new Tile.Position(pos.x + 2, pos.y    )].Built &&
                !tileDic[new Tile.Position(pos.x    , pos.y - 2)].Built &&
                !tileDic[new Tile.Position(pos.x    , pos.y + 2)].Built &&
                !tileDic[new Tile.Position(pos.x - 1, pos.y + 1)].Built &&
                !tileDic[new Tile.Position(pos.x + 1, pos.y + 1)].Built &&
                !tileDic[new Tile.Position(pos.x + 1, pos.y - 1)].Built &&
                !tileDic[new Tile.Position(pos.x - 1, pos.y - 1)].Built)
            {
                return true;
            }
        }

        return false;
    }

    public void OnDestroyTower(Tile.Position pos)
    {
        switch (tileSize)
        {
            case E_TileSize.Tile1:
                {
                    tileDic[pos].Built = false;
                }
                break;
            case E_TileSize.Tile4:
                {
                    tileDic[new Tile.Position(pos.x    , pos.y - 1)].Built = false;
                    tileDic[new Tile.Position(pos.x - 1, pos.y    )].Built = false;
                    tileDic[new Tile.Position(pos.x    , pos.y    )].Built = false;
                    tileDic[new Tile.Position(pos.x + 1, pos.y    )].Built = false;
                    tileDic[new Tile.Position(pos.x    , pos.y + 1)].Built = false;
                }
                break;
            case E_TileSize.Tile9:
                {
                    tileDic[new Tile.Position(pos.x    , pos.y - 2)].Built = false;
                    tileDic[new Tile.Position(pos.x - 1, pos.y - 1)].Built = false;
                    tileDic[new Tile.Position(pos.x    , pos.y - 1)].Built = false;
                    tileDic[new Tile.Position(pos.x + 1, pos.y - 1)].Built = false;
                    tileDic[new Tile.Position(pos.x - 2, pos.y    )].Built = false;
                    tileDic[new Tile.Position(pos.x - 1, pos.y    )].Built = false;
                    tileDic[new Tile.Position(pos.x    , pos.y    )].Built = false;
                    tileDic[new Tile.Position(pos.x + 1, pos.y    )].Built = false;
                    tileDic[new Tile.Position(pos.x + 2, pos.y    )].Built = false;
                    tileDic[new Tile.Position(pos.x - 1, pos.y + 1)].Built = false;
                    tileDic[new Tile.Position(pos.x    , pos.y + 1)].Built = false;
                    tileDic[new Tile.Position(pos.x + 1, pos.y + 1)].Built = false;
                    tileDic[new Tile.Position(pos.x    , pos.y + 2)].Built = false;
                }
                break;
        }
    }

    private void TIleHover(Tile.Position pos, bool isHover, bool built)
    {
        if (tileDic.ContainsKey(pos))
        {
            tileDic[pos].GoHoverActive(isHover);
            tileDic[pos].InitHoverColor(built);

            if (isHover)
            {
                hoveredTIlePosList.Add(pos);
            }
            else
            {
                hoveredTIlePosList.Remove(pos);
            }
        }
    }

    private void InitHoveredTileList()
    {
        while(hoveredTIlePosList.Count != 0)
        {
            TIleHover(hoveredTIlePosList[0], false, false);
        }

        hoveredTIlePosList.Clear();
    }

    private void InitTile()
    {
        tileDic = new Dictionary<Tile.Position, Tile>();

        int x = 0, y = -1;
        for (int i = 0; transform.childCount != 0; ++i)
        {
            if (i % grid.maxPerLine == 0)
            {
                x = 0;
                y++;
            }

            string tileName = string.Format("Tile[{0:D2},{1:D2}]", x, y);
            Transform tfChild = transform.FindChild(tileName);

            Tile tile = tfChild.GetComponent<Tile>();
            tile.OnHoverTile += onHoverTile;
            tile.IsUI += CursorManager.Instance.IsUI;
            tile.OnBuildTower += () => 
            {
                if (!ShopManager.Instance.buyTower() || !CanBuild(tile.Pos))
                    return;

                ShopManager.Instance.OnUpdateGold(-ShopManager.Instance.SelectedTSD.Cost);
                TowerManager.Instance.BuildTower(tile.Pos, ShopManager.Instance.SelectedTSD.TowerType);
                CursorManager.Instance.StopSelectCancle();
            };
            tileDic.Add(new Tile.Position(x, y), tile);

            if ((x % 2 == 1 && y % 2 == 0) || (x % 2 == 0 && y % 2 == 1))
            {
                tfChild.parent = tfQuadTiles;
            }
            else
            {
                tfChild.parent = tfNormalTiles;
            }

            x++;
        }
    }

    [ContextMenu("Sort")]
    private void Sort()
    {
        grid.Reposition();
        int x = 0, y = -1;
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (i % grid.maxPerLine == 0)
            {
                x = 0;
                y++;
            }

            Transform tfChild = transform.GetChild(i);
            tfChild.name = string.Format("Tile[{0:D2},{1:D2}]", x, y);
            x++;
        }
    }
}