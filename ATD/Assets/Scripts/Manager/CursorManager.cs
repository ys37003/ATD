using UnityEngine;
using System.Collections;

public class CursorManager : MonoBehaviour
{
    private static CursorManager instance;
    public  static CursorManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(CursorManager)) as CursorManager;
            }

            if (instance == null)
            {
                GameObject obj = new GameObject("CursorManager");
                instance = obj.AddComponent<CursorManager>() as CursorManager;
            }

            return instance;
        }
    }

    [SerializeField] private Texture2D  cursorTexture = null;
    [SerializeField] private GameObject goSelectedTower = null;
    [SerializeField] private Transform  tfSelectedTower = null;
    [SerializeField] private UISprite   spSelectedTower = null;
    [SerializeField] private UIAtlas    atlas = null;
    [SerializeField] private Camera     mainCamera = null, uiCamera = null;

    private CursorMode cursorMode = CursorMode.Auto;

    private Ray ray;
    private RaycastHit2D hit2d;

    void Awake()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && mainCamera.orthographicSize > 10)
        {
            mainCamera.orthographicSize--;
            tfSelectedTower.localScale = Vector3.one * 10 / mainCamera.orthographicSize;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && mainCamera.orthographicSize < 16)
        {
            mainCamera.orthographicSize++;
            tfSelectedTower.localScale = Vector3.one * 10 / mainCamera.orthographicSize;
        }
    }

    public void StopSelectCancle()
    {
        StopCoroutine("SelectCancel");

        SetSelectedTower(false);
        TileManager.Instance.TileActiveFalse();
        ShopManager.Instance.InitShop(E_TowerType.BasicTower);

        StopCoroutine("CheckUI");
    }

    public void SetSelectedTower(bool active)
    {
        goSelectedTower.SetActive(active);
    }

    public void OnClickTowerInfo(TowerSimpleData data)
    {
        StopCoroutine("SelectCancel");
        StartCoroutine("SelectCancel");
        StopCoroutine("CheckUI");
        StartCoroutine("CheckUI");

        SetSelectedTower(true);
        spSelectedTower.spriteName = data.TowerType.ToString();
        UICursor.Set(atlas, data.TowerType.ToString());
        tfSelectedTower.localScale = Vector3.one * 10 / mainCamera.orthographicSize;
    }

    private IEnumerator SelectCancel()
    {
        // 1 == mouse right click
        while (!Input.GetMouseButtonDown(1))
        {
            yield return null;
        }

        SetSelectedTower(false);
        TileManager.Instance.TileActiveFalse();
        ShopManager.Instance.InitShop(E_TowerType.BasicTower);

        StopCoroutine("CheckUI");
    }

    private IEnumerator CheckUI()
    {
        while(true)
        {
            ray = uiCamera.ScreenPointToRay(Input.mousePosition);
            yield return null;
        }
    }

    public bool IsUI()
    {
        hit2d = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
        return hit2d.transform != null;
    }
}