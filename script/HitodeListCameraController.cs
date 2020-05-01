using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

public class HitodeListCameraController : MonoBehaviour
{
    public AudioClip list;
    public AudioClip g_select;
    public AudioClip g_cursor;
    public AudioClip g_cancel;
    public AudioClip g_change;
    private AudioSource audios;

    public bool bList;
    public int cursor;
    public int now_cursor;

    public GameObject mainCamera;
    public GameObject worldCamera;
    public GameObject hitodeListCamera;
    public GameObject Allow;
    public GameObject[] Hitodes;
    bool RButton;
    bool LButton;
    bool LStickUp;
    bool LStickDown;
    bool One = false;
    public bool tri_LTButton = false;
    bool One_LTButton = false;

    public int hitodeMax;
    
    // ゴール管理用
    public GoalAnimationCube n_goal;
    public N_4GoalAnimationCube s_goal;

    [SerializeField] private PlayerColliderController playerColliderController;
    [SerializeField] private HitodeController hitodeController;
    bool flag_SleepAction = false;

    /// <summary>
    /// ScrollRectコンポーネント
    /// </summary>
    [SerializeField]
    private ScrollRect _scrollRect;

    /// <summary>
    /// スクロールエリアのRectTransform
    /// </summary>
    [SerializeField]
    private RectTransform _viewportRectransform;

    /// <summary>
    /// Nodeを格納するTransform
    /// </summary>
    [SerializeField]
    private Transform _contentTransform;

    /// <summary>
    /// NodeのRectTransform
    /// </summary>
    [SerializeField]
    private RectTransform _nodePrefab;

    /// <summary>
    /// VerticalLayoutGroup(Spacing取得用)
    /// </summary>
    [SerializeField]
    private VerticalLayoutGroup _verticalLayoutGroup;



    void Start()
    {
        bList = false;
        Allow.SetActive(true);
        cursor = 0;
        now_cursor = 0;
        // 音関連
        audios = gameObject.GetComponent<AudioSource>();

        EventSystem.current
                             .ObserveEveryValueChanged(x => x.currentSelectedGameObject)
                             .Select(x => x != null ? x.GetComponent<SelectButton>() : null)
                             .Where(x => x != null)
                             .Subscribe(x => Scroll(x.ID))
                             .AddTo(this);
    }

    void LateUpdate()
    {
        if(n_goal.StageClear || s_goal.N_4StageClear)
        {
            flag_SleepAction = true;
            bList = false;
            Allow.SetActive(false);
        }

        // ボタンとスティック設定
        RButton = Input.GetButtonDown("HitodeSelectDown");
        LButton = Input.GetButtonDown("HitodeSelectUp");
        if (One_LTButton)
        {
            if ((Input.GetAxis("HitodeListLT") == 0))
            {
                One_LTButton = false;
                now_cursor = cursor;
            }
        }

        if (!One_LTButton)
        {
            tri_LTButton = (Input.GetAxis("HitodeListLT") < 0);
        }

        hitodeMax = GameObject.Find("Cnrl").GetComponent<Cnrl>().max_hitode;

        if (!bList && (tri_LTButton && !One_LTButton) && !flag_SleepAction &&!hitodeController.PlayerChange)
        {
            One_LTButton = true;
            bList = true;
            audios.PlayOneShot(list);
            int now = GameObject.Find("Cnrl").gameObject.GetComponent<Cnrl>().mode_hitode;

            foreach (GameObject obs in GameObject.FindGameObjectsWithTag("HitodeButton"))
            {

                cursor = now;
                if (now == obs.gameObject.GetComponent<SelectButton>().ID)
                {
                    obs.GetComponent<Button>().Select();
                    break;
                }
            }

            
        }
        // UI閉じ
        else if (bList && (tri_LTButton && !One_LTButton))
        {
            Debug.Log("UI閉じやす");
            One_LTButton = true;
            bList = false;
            cursor = now_cursor;
            audios.PlayOneShot(g_cancel);
        }

        // 決定
        else if (bList && Input.GetButtonDown("HitodeListDeside"))
        {
            bList = false;
            audios.PlayOneShot(g_select);
            GameObject[] Buttons = GameObject.Find("Cnrl").GetComponent<Cnrl>().Buttons;
            GameObject.Find("Cnrl").gameObject.GetComponent<Cnrl>().HitodeChange(cursor);

        }

        if (bList)
        {
            mainCamera.SetActive(false);
            worldCamera.SetActive(true);
            hitodeListCamera.SetActive(true);

            // カーソル移動
            if (LStickDown && !One)
            {
                Debug.Log("LStickDown = " + LStickDown);
                audios.PlayOneShot(g_cursor);
                //int hitodeMax = GameObject.Find("Cnrl").GetComponent<Cnrl>().max_hitode;

                cursor++ ;
                if (cursor >= (hitodeMax - 1))
                {
                    
                    cursor = (hitodeMax - 1);
                    //_scrollRect.verticalNormalizedPosition = 0;  // スクロールレクトを強制的に一番下に
                   
                }

                // ボタンハイライト
                GameObject[] Buttons = GameObject.Find("Cnrl").GetComponent<Cnrl>().Buttons;

                foreach (GameObject obj in Buttons)
                {
                    if (cursor == obj.GetComponent<SelectButton>().ID)
                    {
                        obj.GetComponent<Button>().Select();
                        break;
                    }
                }

                if (LStickDown)
                One = true;
            }

            if (LStickUp && !One)
            {
                Debug.Log("LStickUp = " + LStickUp);
                audios.PlayOneShot(g_cursor);
                //int hitodeMax = GameObject.Find("Cnrl").GetComponent<Cnrl>().max_hitode;

                cursor--;
                if (cursor <= 0)
                {
                    
                    cursor = 0;
                    //_scrollRect.verticalNormalizedPosition = 1;    // スクロールレクトを強制的に一番上に
                   
                }

                // ボタンハイライト
                GameObject[] Buttons = GameObject.Find("Cnrl").GetComponent<Cnrl>().Buttons;

                foreach (GameObject obj in Buttons)
                {
                    if (cursor == obj.GetComponent<SelectButton>().ID)
                    {
                        obj.GetComponent<Button>().Select();
                        break;
                    }
                }

                if (LStickUp)
                    One = true;
            }

        }

        if (!bList)
        {
            worldCamera.SetActive(false);
            hitodeListCamera.SetActive(false);
        }


        if (!bList && !flag_SleepAction && !hitodeController.PlayerChange)
        {
            mainCamera.SetActive(true);
            worldCamera.SetActive(false);
            hitodeListCamera.SetActive(false);

            if(hitodeMax > 1)
            {
                if (RButton)
                {
                    Debug.Log("次のヒトデ");
                    audios.PlayOneShot(g_change);
                    int hitodeMax = GameObject.Find("Cnrl").GetComponent<Cnrl>().max_hitode;
                    cursor = (cursor + 1) % hitodeMax;
                    GameObject.Find("Cnrl").gameObject.GetComponent<Cnrl>().HitodeChange(cursor);
                }

                if (LButton)
                {
                    Debug.Log("前のヒトデ");
                    audios.PlayOneShot(g_change);
                    int hitodeMax = GameObject.Find("Cnrl").GetComponent<Cnrl>().max_hitode;
                    cursor = (cursor + (hitodeMax - 1)) % hitodeMax;
                    GameObject.Find("Cnrl").gameObject.GetComponent<Cnrl>().HitodeChange(cursor);
                }
            } 
        }

        if (One)
        {
            LStickUp = false;
            LStickDown = false;
            if ((Input.GetAxis("HitodeSelectLStickDown") == 0f) && (Input.GetAxis("HitodeSelectLStickUp") == 0f))
            {
                One = false;
            }
        }

        if (!One)
        {
            LStickUp = (Input.GetAxis("HitodeSelectLStickUp") < 0f);
            LStickDown = (Input.GetAxis("HitodeSelectLStickDown") > 0f);
        }
    }

    public void Scroll(int nodeIndex)
    {
        //要素間の間隔
        var spacing = _verticalLayoutGroup.spacing;
        //現在のスクロール範囲の数値を計算しやすい様に上下反転
        var p = 1.0f - _scrollRect.verticalNormalizedPosition;
        //現在の要素数
        var nodeCount = hitodeMax;
        //描画範囲のサイズ
        var viewportSize = _viewportRectransform.sizeDelta.y;
        //描画範囲のサイズの半分
        var harlViewport = viewportSize * 0.5f;

        //１要素のサイズ
        var nodeSize = _nodePrefab.sizeDelta.y + spacing;

        //現在の描画範囲の中心座標
        var centerPosition = (nodeSize * nodeCount - viewportSize) * p + harlViewport;
        //現在の描画範囲の上端座標
        var topPosition = centerPosition - harlViewport;
        //現在の現在描画の下端座標
        var bottomPosition = centerPosition + harlViewport;

        // 現在選択中の要素の中心座標
        var nodeCenterPosition = nodeSize * nodeIndex + nodeSize / 2.0f;

        //選択した要素が上側にはみ出ている
        if (topPosition > nodeCenterPosition)
        {
            //選択要素が描画範囲に収まるようにスクロール
            var newP = (nodeSize * nodeIndex) / (nodeSize * nodeCount - viewportSize);
            _scrollRect.verticalNormalizedPosition = 1.0f - newP; //反転していたので戻す
            return;
        }

        //選択した要素が下側にはみ出ている
        if (nodeCenterPosition > bottomPosition)
        {
            //選択要素が描画範囲に収まるようにスクロール
            var newP = (nodeSize * (nodeIndex + 1) + spacing - viewportSize) / (nodeSize * nodeCount - viewportSize);
            _scrollRect.verticalNormalizedPosition = 1.0f - newP; //反転していたので戻す
            return;
        }
    }

}

