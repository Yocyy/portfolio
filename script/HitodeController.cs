using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
public static class Define
{
    // constの場合
    public const int CUBE_VERTEX = 24;
}
public class HitodeController : MonoBehaviour
{
    // 効果音再生用
    public AudioClip jump;
    public AudioClip walk;
    public AudioClip stick;
    public AudioClip water;
    public AudioClip extend;
    public AudioClip division;
    public AudioClip list;
    public AudioClip g_cursor;
    public AudioClip g_cancel;
    public AudioClip Ice;

    // アニメーション
    private Animator anim;
    public bool destroy;
    private float sound_timer;
    private new AudioSource audio;

   private Rigidbody rigid_p;    // 物理演算
   private float jumpForce = 5000000.0f;   // ジャンプ
   private float hitode_size_x = 30.0f; // プレイヤーのサイズ(x)
   private float hitode_size_y = 30.0f; // プレイヤーのサイズ(y)
   private float hitode_size_z = 30.0f; // プレイヤーのサイズ(z)
   private float mass = 10000.0f;  // プレイヤーの重さ(1.0 = 1kg)
   public float vir_mass = 100.0f;      // 重さギミック用の仮想体重
   private Vector3 PlusPlayerPos;  // プレイヤー分裂時の生成位置

    public GameObject mainCamera;     // メインカメラ


    public bool flag_walk = false;  // 歩くフラグ
    public bool flag_jump = false;  // ジャンプフラグ
    private bool jump_con;
    public bool flag_action = false;    // アクションフラグ

    public GameObject Controller;
    public GameObject hitodePrefab;
    public GameObject hitodeTopHandPrefab;  // TopHand
    public GameObject hitodeRightHandPrefab;// RightHand
    public GameObject hitodeRightLegPrefab; // RightLeg
    public GameObject hitodeLeftLegPrefab;  // LeftLeg
    public GameObject hitodeLeftHandPrefab; // LeftHand
    public GameObject hitodeBody;           // Body
    public GameObject hitodeLeg;            // Leg
    private GameObject hitode;  //  複製するヒトデ
    public int ID;              // プレイヤーの管理番号

    // 重心のベクター
    public Vector3 center;
    private float center_y = -0.2f;

    private float Nobasu_Pos_X = 0.4f; // 伸ばすときにずらすpos
    private float Nobasu_Pos_Y = 0.3f; // 伸ばすときにずらすpos
    // Right_Handの変数
    private Mesh mesh_rh;
    private MeshFilter meshFilter_rh;
    private MeshRenderer meshRenderer_rh;
    private MeshCollider meshCollider_rh;
    private Vector3[] VertexData_rh;
    private Material material_rh;
    private PhysicMaterial physicMaterial_rh;
    private float range_rh = 0.5f;
    private bool flag_rh = false;
    
    // Left_Handの変数
    private Mesh mesh_lh;
    private MeshFilter meshFilter_lh;
    private MeshRenderer meshRenderer_lh;
    private MeshCollider meshCollider_lh;
    private Vector3[] VertexData_lh;
    private Material material_lh;
    private PhysicMaterial physicMaterial_lh;
    private float range_lh = 0.5f;
    private bool flag_lh = false;
    
    // BulletWindの変数
    public GameObject BulletWind;
    private Vector3 BulletWind_Pos;
    private float BulletWind_Pos_x = 4.61f;

    // はりつくの変数
    public int max = -1;
    public bool flag_harituku = false;
    bool flag_action_now = false;
    bool flag_nobasu = false;
    int HaritukuSelectCount = -1;
    public bool flag_Harituku_actionselect = false;
    GameObject[] haritukus;
    Vector3[] haritukus_GetPos = new Vector3[10];
    Quaternion[] haritukus_GetRot = new Quaternion[10];

    public float coefficient;   // 空気抵抗係数
    public Vector3 velocity;    // 風速
    public GameObject Arrow;
    Vector3 ArrowPos;
    GameObject SelectArrow;
    Vector3 Player_velocity;

    public bool PlayerChange = false;  // はりつく中のキャラチェンジフラグ

    [SerializeField] private float moveSpeed = 5.0f;        // 移動速度

    private int stop_con;

    bool select_action   = false;
    bool harituku_action = false;
    bool flag_other_action = false;
    bool nobasu_action = false;

    bool XButton;
    bool YButton;
    bool BButton;
    bool RButton;
    bool LButton;
    bool nobasu_now = false;
    bool Flag_BulletWindCount = false;
    int BulletWaitCnt = 0;

    float WalkAddForce =  1000000.0f;   //地上の歩く力
    float JumpMoveForce = 500000.0f;   //空中の歩く力
    //bool flag_Jump= false;
    public bool Stop_Player;
    public  GameObject JumpCollider; //ジャンプの当たり判定
    //public float BulletWindPower_x = 3500.0f; // 風圧
    float BulletWindPower_y = 10.0f; // 風圧
    float BulletWindPower_x = 3500.0f; // 風圧
    int Bullet_Direction;   //弾の向き

    bool tri_Division = false;
    public bool One_Division = false;
    bool RStick_Right = false;
    bool RStick_Left = false;
    bool One_Select = false;

    //凍る竜巻用変数
    public GameObject ice;
    public bool b_ice;
    public bool ice_con;

    private bool LStickUp;
    private bool One;


    // ゴールアニメーション
    public bool Goal = false;
    public bool GoalAnimationStart = false;
    float SleepAnimationCnt = 0;
    public GameObject SleepImage;
    int SleepImageCnt = 0;
    float SleepImagePos_x = 2;
    float SleepImagePos_y = 2;
    GameObject sleepImage1;
    GameObject sleepImage2;
    GameObject sleepImage3;
    int AnimationEnd_Cnt = 0;
    public GameObject GoalCamera;
    public bool Jump;
    public bool flag_hayasugi = false;
    public bool flag_jumpar = false;
    bool flag_Onehayasugi = false;
    float hayasugiCnt = 0;

    // N-4ゴールアニメーション
    public bool N_4GoalAnimationStart = false;
    float N_4AnimationEnd_Cnt = 0;
    public GameObject n_4GoalCamera;
    float rotatio_x = 0;
    float rotatio_y = 0;
    float rotatio_z = 0;
    public bool flag_rotatio_x_90 = false;

    // 分裂アニメーション
    public bool Bunretu_Animation = false;
    public bool Bunretu_Animation_1 = false;
    float x;
    float y;
    float z;
    float Bunretu_PlusSize_x;
    float Bunretu_PlusSize_y;
    float Bunretu_PlusSize_z;
    float Bunretu_Pos_x = -2.0f;

    //[SerializeField] private float applySpeed = 1.0f;       // 振り向きの適用速度
    //Vector3 WalkRotation;
    void Start()
    {
        ice_con = false;
        b_ice = false;
        jump_con = false;
        One = false;
        flag_jump = false;

        center = new Vector3(0f, center_y, 0f);
        rigid_p = GetComponent<Rigidbody>();
        rigid_p.mass = mass;
        rigid_p.centerOfMass = center;

        stop_con = 0;

        //Right_Handの初期化
        mesh_rh = hitodeRightHandPrefab.GetComponent<MeshFilter>().mesh;
        meshFilter_rh = hitodeRightHandPrefab.GetComponent<MeshFilter>();
        if (!meshFilter_rh) meshFilter_rh = hitodeRightHandPrefab.AddComponent<MeshFilter>();
        meshRenderer_rh = hitodeRightHandPrefab.GetComponent<MeshRenderer>();
        if (!meshRenderer_rh) meshRenderer_rh = hitodeRightHandPrefab.AddComponent<MeshRenderer>();
        meshCollider_rh = hitodeRightHandPrefab.GetComponent<MeshCollider>();
        if (!meshCollider_rh) meshCollider_rh = hitodeRightHandPrefab.AddComponent<MeshCollider>();
        VertexData_rh = mesh_rh.vertices;

        //Left_Handの初期化      
        mesh_lh = hitodeLeftHandPrefab.GetComponent<MeshFilter>().mesh;
        meshFilter_lh = hitodeLeftHandPrefab.GetComponent<MeshFilter>();
        if (!meshFilter_lh) meshFilter_lh = hitodeLeftHandPrefab.AddComponent<MeshFilter>();
        meshRenderer_lh = hitodeLeftHandPrefab.GetComponent<MeshRenderer>();
        if (!meshRenderer_lh) meshRenderer_lh = hitodeLeftHandPrefab.AddComponent<MeshRenderer>();
        meshCollider_lh = hitodeLeftHandPrefab.GetComponent<MeshCollider>();
        if (!meshCollider_lh) meshCollider_lh = hitodeLeftHandPrefab.AddComponent<MeshCollider>();
        VertexData_lh = mesh_lh.vertices;

        meshCollider_rh.convex = true;
        meshCollider_lh.convex = true;

        // 音関連
        audio = gameObject.GetComponent<AudioSource>();
        sound_timer = 0;

        // アニメーション
        anim = GetComponent<Animator>();
        destroy = false;
        Bunretu_PlusSize_x = hitode_size_x / 30;
        Bunretu_PlusSize_y = hitode_size_y / 30;
        Bunretu_PlusSize_z = hitode_size_z / 30;
    }



    void Update()
    {
        ///////////////////////////////////////////////////////////////////////////////////
        ///                 分裂時のアニメーション
        ///////////////////////////////////////////////////////////////////////////////////
        if (Bunretu_Animation)
        {
            Bunretu_Animation = false;
            Bunretu_Animation_1 = true;
            x = hitode_size_x * 0.2f;
            y = hitode_size_y * 0.2f;
            z = hitode_size_z * 0.2f;
            gameObject.transform.localScale = new Vector3(x, y, z);
            rigid_p.AddForce(new Vector3(-1,0,0) * WalkAddForce);
        }

        if (Bunretu_Animation_1)
        {
            if (x < hitode_size_x)
            {
                x += Bunretu_PlusSize_x;
                y += Bunretu_PlusSize_y;
                z += Bunretu_PlusSize_z;
                gameObject.transform.localScale = new Vector3(x, y, z);
            }
        }

        // ジャンプの判定
        Jump = JumpCollider.GetComponent<JumpCollider>().flag_Jump;
        Goal_Animation();
        Goal_Animation_N_4();
        if(Jump)
        {
            // アニメーション
            anim.SetBool("jump", false);
            anim.SetBool("walk", false);
        }
        // メインカメラがアクティブなら入力を受けつける
        if (mainCamera.activeSelf && !Goal && !b_ice && !destroy)
        {
            // 入力された番号とプレイヤーの番号の照合確認
            if (Controller.GetComponent<Cnrl>().mode_hitode == ID && !flag_hayasugi)
            {
                Debug.Log("PlayerChange = " + PlayerChange);
                if (Jump)
                {
                    anim.SetBool("walk", false);
                    anim.SetBool("jump", false);
                    flag_jump = false;
                    jump_con = false;
                    if (Input.GetButtonDown("Jump") && !flag_action_now && !flag_jump)
                    {

                        rigid_p.velocity = new Vector3(rigid_p.velocity.x, 0, rigid_p.velocity.z);
                        rigid_p.AddForce(transform.up * jumpForce);
                        flag_jump = true;
                        if (LStickUp)
                        {
                            One = true;
                        }
                    }
                    else if (LStickUp && !One && !flag_action_now && !flag_jump)
                    {
                        rigid_p.velocity = new Vector3(rigid_p.velocity.x, 0, rigid_p.velocity.z);
                        rigid_p.AddForce(transform.up * jumpForce);
                        flag_jump = true;
                        if (LStickUp)
                        {
                            One = true;
                        }
                    }

                    if (flag_jump && !jump_con)
                    {
                        // 効果音
                        audio.PlayOneShot(jump);
                        jump_con = true;
                    }
                }
                else
                {
                    anim.SetBool("jump", true);
                }

                // 空中移動
                if (!flag_action_now && !Jump)
                {
                    // WASD入力から、XZ平面(水平な地面)を移動する方向(velocity)を得ます
                    velocity = Vector3.zero;
                    if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0f)
                    {
                        velocity.x += Input.GetAxis("Horizontal");
                    }
                    // 速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整します
                    velocity = velocity.normalized * moveSpeed * Time.deltaTime;

                    // いずれかの方向に移動している場合
                    if (velocity.magnitude > 0)
                    {
                        // 移動処理
                        rigid_p.AddForce(velocity * JumpMoveForce);
                    }
                }

                // 地上移動
                if (!flag_action_now && Jump)
                {
                    // WASD入力から、XZ平面(水平な地面)を移動する方向(velocity)を得ます
                    velocity = Vector3.zero;
                    if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0f)
                    {
                        velocity.x += Input.GetAxis("Horizontal");
                    }
                    else
                    {
                        anim.SetBool("walk", false);
                        rigid_p.velocity = new Vector3(0, rigid_p.velocity.y, rigid_p.velocity.z);
                        rigid_p.angularVelocity = new Vector3(0, rigid_p.velocity.y, rigid_p.velocity.z);
                    }

                    // 速度ベクトルの長さを1秒でmoveSpeedだけ進むように調整します
                    velocity = velocity.normalized * moveSpeed * Time.deltaTime;
                    // いずれかの方向に移動している場合
                    if (velocity.magnitude > 0)
                    {
                        //// キャラクターの向き
                        //if(velocity.x >0)
                        //{
                        //    WalkRotation.z = 1.0f;
                        //    Bullet_Direction = 1;
                        //}
                        //else
                        //{
                        //    WalkRotation.z = -1.0f;
                        //    Bullet_Direction = -1;
                        //}

                        //transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(WalkRotation),applySpeed);
                        //Debug.Log(Bullet_Direction);
                        //Debug.Log(BulletWindPower_x);
                        anim.SetBool("walk", true);
                        // 移動処理
                        rigid_p.AddForce(velocity * WalkAddForce);
                    }
                }

                if (One_Division)
                {
                    if ((Input.GetAxis("Division") == 0))
                    {
                        One_Division = false;
                    }
                }

                if (!One_Division)
                {
                    tri_Division = (Input.GetAxis("Division") > 0);
                }

                Stop_Player = (rigid_p.velocity == Vector3.zero);
                if (rigid_p.velocity.x > -0.1f && rigid_p.velocity.x < 0.1f)
                {
                    Stop_Player = true;
                }


                // 分裂
                if (!flag_action_now && tri_Division && Stop_Player && !One_Division)
                {
                    One_Division = true;

                    // 分裂回数の制限(５回)(9.8304は1.0を0.8で５回かけた数)
                    if (hitode_size_x > 12.288 && hitode_size_y > 12.288 && hitode_size_z > 12.288)
                    {
                        // アニメーション
                        anim.SetBool("l_ude", false);
                        anim.SetBool("rude", false);
                        anim.SetBool("jump", false);
                        anim.SetBool("walk", false);
                        anim.SetBool("nkzr", false);
                        anim.SetBool("ouu", false);
                        audio.PlayOneShot(division);

                        Bunretu_Pos_x *= 0.8f;
                        // ジャンプ中の移動の調整
                        JumpMoveForce *= 0.8f;
                        // プレイヤーの重心の調整
                        center_y *= 0.6f;
                        // プレイヤー縮小
                        hitode_size_x *= 0.8f;
                        hitode_size_y *= 0.8f;
                        hitode_size_z *= 0.8f;
                        // ゴール時の画像サイズを調整
                        HitodeMarkImage_Size *= 0.8f;
                        // 弾の風圧の調整
                        BulletWindPower_x *= 0.8f;
                        BulletWindPower_y *= 0.8f;
                        // 重さを縮小
                        rigid_p.mass *= 0.8f;
                        vir_mass *= 0.8f;                       // ジャンプする力を縮小
                        jumpForce *= 0.8f;
                        // 歩く力を縮小
                        WalkAddForce *= 0.8f;
                        // 伸ばす距離の縮小
                        Nobasu_Pos_X *= 0.8f;
                        Nobasu_Pos_Y *= 0.8f;
                        // プレイヤーのサイズを適用
                        hitodePrefab.transform.localScale = new Vector3(hitode_size_x, hitode_size_y, hitode_size_z);
                        // プレイヤーの重心を適用
                        center = new Vector3(0f, center_y, 0f);
                        rigid_p.centerOfMass = center;
                        // 現在の座標 ＋ Y(重なり防止)
                        PlusPlayerPos = gameObject.transform.position + new Vector3(Bunretu_Pos_x, 0, 0);
                        // プレイヤー生成
                        hitode = Instantiate(gameObject, PlusPlayerPos, gameObject.transform.rotation);

                        // サイズを入れる
                        hitode.GetComponent<HitodeController>().hitode_size_x = hitode_size_x;
                        hitode.GetComponent<HitodeController>().hitode_size_y = hitode_size_y;
                        hitode.GetComponent<HitodeController>().hitode_size_z = hitode_size_z;
                        hitode.GetComponent<HitodeController>().center_y = center_y;
                        // 重さを入れる
                        hitode.GetComponent<HitodeController>().mass = rigid_p.mass;
                        hitode.GetComponent<HitodeController>().vir_mass = vir_mass;
                        // ジャンプする力を入れる
                        hitode.GetComponent<HitodeController>().jumpForce = jumpForce;
                        // 歩く力を入れる
                        hitode.GetComponent<HitodeController>().WalkAddForce = WalkAddForce;
                        // IDにプレイヤーの現存数を代入
                        hitode.GetComponent<HitodeController>().ID = Controller.GetComponent<Cnrl>().max_hitode;
                        //　プレイヤーの現存数にプラス１して次に生成されるプレイヤーの番号を用意する
                        Controller.GetComponent<Cnrl>().Hitodeplus();
                        // ヒトデボタン生成
                        GameObject.Find("Cnrl").GetComponent<Cnrl>().ButtonInstance();

                        hitode.GetComponent<HitodeController>().JumpMoveForce = JumpMoveForce;
                        hitode.GetComponent<HitodeController>().BulletWindPower_x = BulletWindPower_x;
                        hitode.GetComponent<HitodeController>().BulletWindPower_y = BulletWindPower_y;
                        hitode.GetComponent<HitodeController>().Nobasu_Pos_X = Nobasu_Pos_X;
                        hitode.GetComponent<HitodeController>().Nobasu_Pos_Y = Nobasu_Pos_Y;
                        hitode.GetComponent<HitodeController>().Bunretu_Animation = true;
                        hitode.GetComponent<HitodeController>().HitodeMarkImage_Size = HitodeMarkImage_Size;
                    }
                }



                ///////////////////////////////////////////////////////////////////////////////////
                ///                 アクション
                ///////////////////////////////////////////////////////////////////////////////////

                if (One_Select)
                {
                    RStick_Right = false;
                    RStick_Left = false;
                    if ((Input.GetAxis("RStickRight") == 0) && (Input.GetAxis("RStickLeft") == 0))
                    {
                        One_Select = false;
                    }
                }

                if (!One_Select)
                {
                    RStick_Right = (Input.GetAxis("RStickRight") > 0);
                    RStick_Left = (Input.GetAxis("RStickLeft") < 0);
                }

                XButton = Input.GetButtonDown("act_Harituku");
                YButton = Input.GetButtonDown("act_Nobasu");
                BButton = Input.GetButtonDown("act_Bullet");
                RButton = Input.GetButtonDown("RB");
                LButton = Input.GetButtonDown("LB");
                flag_other_action = false;

                // 貼りつくアクション
                if (!select_action && !harituku_action && !flag_other_action && XButton && !nobasu_action && !Flag_BulletWindCount && Stop_Player)
                {
                    flag_action_now = true;
                    flag_other_action = true;
                    haritukus = GameObject.FindGameObjectsWithTag("Harituku");

                    foreach (GameObject HaritukuObj in haritukus)
                    {
                        if (HaritukuObj.GetComponent<HaritukuController>().flag_harituku == true && gameObject.GetComponent<PlayerColliderController>().flag_Harituku == true && !HaritukuObj.GetComponent<HaritukuController>().Get_Flag())
                        {
                            max++;
                            haritukus_GetPos[max] = HaritukuObj.GetComponent<HaritukuController>().tmpPos;
                            haritukus_GetRot[max] = HaritukuObj.GetComponent<HaritukuController>().tmpRot;
                            if (max > 0)
                            {
                                for (int i = 0; i < max; i++)
                                {
                                    if (haritukus_GetPos[i].x > haritukus_GetPos[i + 1].x)
                                    {
                                        Vector3 change = haritukus_GetPos[i];
                                        haritukus_GetPos[i] = haritukus_GetPos[i + 1];
                                        haritukus_GetPos[i + 1] = change;
                                    }
                                    Debug.Log("i = " + i);
                                    Debug.Log("haritukus_GetPos[i].x = " + haritukus_GetPos[i].x);
                                }
                            }
                        }
                    }


                    if (max != -1 && haritukus_GetPos[0].x != 0)
                    {
                        select_action = true;
                        SelectArrow = Instantiate(Arrow, new Vector3(5, 5, 0), Arrow.transform.rotation);

                        HaritukuSelectCount++;
                        Debug.Log("HaritukuSelectCount" + HaritukuSelectCount);
                        ArrowPos = haritukus_GetPos[HaritukuSelectCount] + new Vector3(0, 2, 0);
                        SelectArrow.transform.position = ArrowPos;
                        Debug.Log("選択");
                        Debug.Log("max" + max);
                        PlayerChange = true;
                    }
                    else
                    {
                        Reset_Action();
                    }
                }

                // 貼りつく先を選択

                if (select_action && RStick_Left && !One_Select && !harituku_action && !nobasu_action && !flag_other_action && HaritukuSelectCount > 0)
                {
                    One_Select = true;
                    HaritukuSelectCount--;
                    Debug.Log(HaritukuSelectCount);
                    ArrowPos = haritukus_GetPos[HaritukuSelectCount] + new Vector3(0, 2, 0);
                    SelectArrow.transform.position = ArrowPos;
                }

                if (select_action && RStick_Right && !One_Select && !harituku_action && !nobasu_action && !flag_other_action && HaritukuSelectCount < 1)
                {
                    One_Select = true;
                    HaritukuSelectCount++;
                    Debug.Log(HaritukuSelectCount);
                    ArrowPos = haritukus_GetPos[HaritukuSelectCount] + new Vector3(0, 2, 0);
                    SelectArrow.transform.position = ArrowPos;
                }


                // 選択先にはりつく
                if (select_action && !harituku_action && !flag_other_action && XButton && !nobasu_action)
                {
                    harituku_action = true;
                    flag_other_action = true;
                    PlayerChange = false;
                    if (HaritukuSelectCount != -1)
                        flag_Harituku_actionselect = true;
                    if (flag_Harituku_actionselect && max != -1)
                    {
                        flag_harituku = true;
                        anim.SetBool("nkzr", true);
                        audio.PlayOneShot(stick);
                        Destroy(SelectArrow);
                        rigid_p.isKinematic = true;
                        Vector3 Harituku_SetPos = haritukus_GetPos[HaritukuSelectCount];
                        Quaternion Harituku_SetRot = haritukus_GetRot[HaritukuSelectCount];
                        gameObject.transform.position = Harituku_SetPos;
                        gameObject.transform.rotation = Harituku_SetRot;
                    }

                    Debug.Log("はりついた！");
                }

                // はりつく解除
                if ((((select_action || harituku_action) && (YButton || BButton)) || (select_action && harituku_action && XButton)) && !flag_other_action)
                {
                    Reset_Action();
                }


                // 水を吹く
                if (!select_action && !harituku_action && BButton && !nobasu_action && !flag_other_action && !Flag_BulletWindCount && Stop_Player)
                {
                    //action_n = true;
                    Flag_BulletWindCount = true;
                    flag_action_now = true;
                    Debug.Log("発射！");
                    //select_n = true;
                    anim.SetBool("ouu", true);
                    audio.PlayOneShot(water);
                    BulletWind_Pos = gameObject.transform.position + new Vector3(BulletWind_Pos_x, 0, 0);
                    // 弾の生成
                    GameObject bulletWind = Instantiate(BulletWind, BulletWind_Pos, BulletWind.transform.rotation);
                    bulletWind.GetComponent<BulletWind>().Setvelocity_x = BulletWindPower_x;
                    bulletWind.GetComponent<BulletWind>().Setvelocity_y = BulletWindPower_y;
                }
                // 水を吹いてから2秒間停止
                if (Flag_BulletWindCount)
                {
                    BulletWaitCnt++;
                    Debug.Log(BulletWaitCnt);
                    if (BulletWaitCnt > 90)
                    {
                        Reset_Action();
                    }
                }


                // 手を伸ばす選択モードに移行
                if (!select_action && !harituku_action && !flag_other_action && YButton && !nobasu_action && !Flag_BulletWindCount && Stop_Player)
                {
                    flag_action_now = true;
                    nobasu_action = true;
                    flag_other_action = true;
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    Debug.Log("伸ばすモード");
                }

                if (nobasu_action)
                {
                    // 右手を伸ばす
                    if (RStick_Right && !One_Select && !flag_nobasu && Stop_Player)
                    {
                        One_Select = true;
                        Debug.Log("nobasu");
                        anim.SetBool("l_ude", true);
                        audio.PlayOneShot(extend);
                        //ローテーションの解除
                        rigid_p.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                        // プレイヤーの位置を伸ばした分だけ浮かせる
                        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + Nobasu_Pos_Y, transform.position.z);
                        ;                        //transform.rotation = Quaternion.Euler(0, 90, 0);
                        hitodeRightLegPrefab.transform.position = new Vector3(hitodeRightLegPrefab.transform.position.x + Nobasu_Pos_X, hitodeRightLegPrefab.transform.position.y - Nobasu_Pos_Y, hitodeRightLegPrefab.transform.position.z);
                        hitodeLeftLegPrefab.transform.position = new Vector3(hitodeLeftLegPrefab.transform.position.x - Nobasu_Pos_X, hitodeLeftLegPrefab.transform.position.y - Nobasu_Pos_Y, hitodeLeftLegPrefab.transform.position.z);
                        hitodeRightLegPrefab.transform.rotation = Quaternion.Euler(hitodeRightLegPrefab.transform.rotation.x, hitodeRightLegPrefab.transform.rotation.y, hitodeRightLegPrefab.transform.rotation.z + 45);
                        hitodeLeftLegPrefab.transform.rotation = Quaternion.Euler(hitodeLeftLegPrefab.transform.rotation.x, hitodeLeftLegPrefab.transform.rotation.y, hitodeLeftLegPrefab.transform.rotation.z - 45);
                        flag_nobasu = true;
                        //flag_action_now = true;
                        flag_rh ^= true;
                        if (flag_rh)
                        {
                            range_rh = 2.0f;
                        }
                        if (!flag_rh)
                        {
                            range_rh = 0.5f;
                        }

                        for (int i = 0; i < 24; i++)
                        {
                            if (flag_rh && VertexData_rh[i].y == 0.5f)
                            {
                                VertexData_rh[i].y = range_rh;
                            }
                            else if (!flag_rh && VertexData_rh[i].y == 2.0f)
                            {
                                VertexData_rh[i].y = range_rh;
                            }
                        }

                        mesh_rh.vertices = VertexData_rh;
                        meshFilter_rh.mesh = mesh_rh;
                        meshRenderer_rh.sharedMaterial = material_rh;
                        meshCollider_rh.sharedMesh = mesh_rh;
                        meshCollider_rh.sharedMaterial = physicMaterial_rh;
                        meshCollider_rh.convex = true;
                        Debug.Log("右のび太！");
                    }

                    // 左手を伸ばす
                    if (RStick_Left && !One_Select && !flag_nobasu && Stop_Player)
                    {
                        One_Select = true;
                        Debug.Log("nobasu");
                        anim.SetBool("rude", true);
                        audio.PlayOneShot(extend);
                        //ローテーションの解除
                        rigid_p.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                        // プレイヤーの位置を伸ばした分だけ浮かせる
                        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + Nobasu_Pos_Y, transform.position.z);
                        //transform.rotation = Quaternion.Euler(0, 90, 0);
                        hitodeRightLegPrefab.transform.position = new Vector3(hitodeRightLegPrefab.transform.position.x + Nobasu_Pos_X, hitodeRightLegPrefab.transform.position.y - Nobasu_Pos_Y, hitodeRightLegPrefab.transform.position.z);
                        hitodeLeftLegPrefab.transform.position = new Vector3(hitodeLeftLegPrefab.transform.position.x - Nobasu_Pos_X, hitodeLeftLegPrefab.transform.position.y - Nobasu_Pos_Y, hitodeLeftLegPrefab.transform.position.z);
                        hitodeRightLegPrefab.transform.rotation = Quaternion.Euler(hitodeRightLegPrefab.transform.rotation.x, hitodeRightLegPrefab.transform.rotation.y, hitodeRightLegPrefab.transform.rotation.z + 45);
                        hitodeLeftLegPrefab.transform.rotation = Quaternion.Euler(hitodeLeftLegPrefab.transform.rotation.x, hitodeLeftLegPrefab.transform.rotation.y, hitodeLeftLegPrefab.transform.rotation.z - 45);
                        flag_nobasu = true;
                        //flag_action_now = true;
                        flag_lh ^= true;
                        if (flag_lh)
                        {
                            range_lh = 2.0f;
                        }
                        if (!flag_lh)
                        {
                            range_lh = 0.5f;
                        }

                        for (int i = 0; i < 24; i++)
                        {
                            if (flag_lh && VertexData_lh[i].y == 0.5f)
                            {
                                VertexData_lh[i].y = range_lh;
                            }
                            else if (!flag_lh && VertexData_lh[i].y == 2.0f)
                            {
                                VertexData_lh[i].y = range_lh;
                            }
                        }

                        mesh_lh.vertices = VertexData_lh;
                        meshFilter_lh.mesh = mesh_lh;
                        meshRenderer_lh.sharedMaterial = material_lh;
                        meshCollider_lh.sharedMesh = mesh_lh;
                        meshCollider_lh.sharedMaterial = physicMaterial_lh;
                        meshCollider_lh.convex = true;
                        Debug.Log("左のび太！");
                    }


                }
                // 伸ばす解除
                if ((nobasu_action && (YButton || XButton || BButton)) && !flag_other_action)
                {
                    Reset_Action();
                }
            }
        }

        // 凍ったときの処理
        if (b_ice && !ice_con)
        {
            GameObject.Find("Cnrl").GetComponent<Cnrl>().ButtonDestroy();
            ice_con = true;
            ice.SetActive(true);
            audio.PlayOneShot(Ice);
            if (!flag_harituku)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
        }

        // 死んだときの処理
        if (destroy)
        {
            GameObject.Find("Cnrl").GetComponent<Cnrl>().ButtonDestroy();
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.GetComponent<HitodeController>().hitodeTopHandPrefab.SetActive(false);
            gameObject.GetComponent<HitodeController>().hitodeRightHandPrefab.SetActive(false);
            gameObject.GetComponent<HitodeController>().hitodeLeftHandPrefab.SetActive(false);
            gameObject.GetComponent<HitodeController>().hitodeRightLegPrefab.SetActive(false);
            gameObject.GetComponent<HitodeController>().hitodeLeftLegPrefab.SetActive(false);
            gameObject.GetComponent<HitodeController>().JumpCollider.SetActive(false);
            gameObject.GetComponent<HitodeController>().hitodeBody.SetActive(false);
            gameObject.GetComponent<HitodeController>().hitodeLeg.SetActive(false);
        }

        if (One)
        {
            LStickUp = false;
            if ((Input.GetAxis("Vertical") == 0f))
            {
                One = false;
            }
        }

        if (!One)
        {
            LStickUp = (Input.GetAxis("Vertical") > 0.5f);
        }

    }

    void Reset_Action()
    {
        PlayerChange = false;
        BulletWaitCnt = 0;
        Flag_BulletWindCount = false;
        select_action = false;
        harituku_action = false;
        nobasu_action = false;
        flag_action_now = false;
        Debug.Log("reset");
        flag_other_action = true;
        
        Destroy(SelectArrow);

        // アニメーション
        anim.SetBool("l_ude", false);
        anim.SetBool("rude", false);
        anim.SetBool("jump", false);
        anim.SetBool("walk", false);
        anim.SetBool("nkzr", false);
        anim.SetBool("ouu", false);

        // めり込み防止
        // transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        audio.PlayOneShot(g_cancel);

        if (flag_nobasu)
        {
            hitodeRightLegPrefab.transform.position = new Vector3(hitodeRightLegPrefab.transform.position.x - Nobasu_Pos_X, hitodeRightLegPrefab.transform.position.y + Nobasu_Pos_Y, hitodeRightLegPrefab.transform.position.z);
            hitodeLeftLegPrefab.transform.position = new Vector3(hitodeLeftLegPrefab.transform.position.x + Nobasu_Pos_X, hitodeLeftLegPrefab.transform.position.y + Nobasu_Pos_Y, hitodeLeftLegPrefab.transform.position.z);
            hitodeRightLegPrefab.transform.rotation = Quaternion.Euler(0, 0, 0);
            hitodeLeftLegPrefab.transform.rotation = Quaternion.Euler(0, 0, 0);
            flag_nobasu = false;
        }

        for (int i = 0; i > 10; i++)
        {
            haritukus_GetPos[i] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
        max = -1;
        HaritukuSelectCount = -1;

        // はりつくの初期化
        flag_harituku = false;
        flag_Harituku_actionselect = false;
        rigid_p.isKinematic = false;
        flag_harituku = false;
        //ローテーションの固定
        rigid_p.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        //プレイヤーの回転を0に
        transform.rotation = Quaternion.Euler(0, 0, 0);
        //transform.Rotate(new Vector3(0, 0, 0));

        // Right_Handの初期化
        flag_rh = false;
        for (int i = 0; i < 24; i++)
        {
            if (VertexData_rh[i].y == 2.0f)
            {
                VertexData_rh[i].y = 0.5f;
            }
        }
        mesh_rh.vertices = VertexData_rh;
        meshFilter_rh.mesh = mesh_rh;
        meshRenderer_rh.sharedMaterial = material_rh;
        meshCollider_rh.sharedMesh = mesh_rh;
        meshCollider_rh.sharedMaterial = physicMaterial_rh;
        meshCollider_rh.convex = true;

        // Left_Handの初期化
        flag_lh = false;
        for (int i = 0; i < 24; i++)
        {
            if (VertexData_lh[i].y == 2.0f)
            {
                VertexData_lh[i].y = 0.5f;
            }
        }
        mesh_lh.vertices = VertexData_lh;
        meshFilter_lh.mesh = mesh_lh;
        meshRenderer_lh.sharedMaterial = material_lh;
        meshCollider_lh.sharedMesh = mesh_lh;
        meshCollider_lh.sharedMaterial = physicMaterial_lh;
        meshCollider_lh.convex = true;

    }

    ///////////////////////////////////////////////////////////
    ///          N-1 ～ N-3のゴールアニメーション関数       ///
    ///////////////////////////////////////////////////////////
    void Goal_Animation()
    {
        // GoalAnimationCubeに触れたら
        if (gameObject.GetComponent<PlayerColliderController>().flag_Enter && !gameObject.GetComponent<PlayerColliderController>().flag_Exit)
        {    // 一定の速さ以上なら
            if (flag_hayasugi)
            {
                hayasugiCnt += Time.deltaTime;
            }
            if (hayasugiCnt > 1.5)
            {
                rigid_p.isKinematic = false;
                rigid_p.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }
            // 一定の速さ以上なら
            if (rigid_p.velocity.x > 1 && !flag_Onehayasugi)
            {
                rigid_p.velocity = Vector3.zero;
                rigid_p.collisionDetectionMode = CollisionDetectionMode.Discrete;
                rigid_p.isKinematic = true;

                flag_hayasugi = true;
                flag_Onehayasugi = true;
            }

            // 地面に触れているなら
            if (Jump && rigid_p.velocity.x <= 5)
            {
                flag_hayasugi = true;
                flag_jumpar = true;
            }
            //Debug.Log("rigid_p.velocity =" + rigid_p.velocity);
            // playerの動きを止める
            if (flag_hayasugi && flag_jumpar)
            {
                Goal = true;
                mainCamera.SetActive(false);
                GoalCamera.SetActive(true);
                anim.SetBool("walk", true);
                gameObject.transform.position += new Vector3(0.1f, 0, 0);
            }
        }


        // Cubeを出たら寝るアニメーション
        if (gameObject.GetComponent<PlayerColliderController>().flag_Exit)
        {
            if (AnimationEnd_Cnt == 2)
            {
                anim.SetBool("walk", true);
                rigid_p.collisionDetectionMode = CollisionDetectionMode.Continuous;
                rigid_p.isKinematic = false;
                Destroy(sleepImage1);
                Destroy(sleepImage2);
                Destroy(sleepImage3);
                GoalAnimationStart = false;
            }
            else
            {
                // playerの動きを止める
                rigid_p.collisionDetectionMode = CollisionDetectionMode.Discrete;
                rigid_p.isKinematic = false;
                GoalAnimationStart = true;
            }
        }
        // アニメーション開始
        if (GoalAnimationStart)
        {
            SleepAnimationCnt += Time.deltaTime;
        }
        if (SleepAnimationCnt > 1)
        {
            SleepImageCnt++;
            SleepAnimationCnt = 0;
            switch (SleepImageCnt)
            {
                case 1:
                    // アニメーション
                    anim.SetBool("l_ude", false);
                    anim.SetBool("rude", false);
                    anim.SetBool("jump", false);
                    anim.SetBool("walk", false);
                    anim.SetBool("nkzr", false);
                    anim.SetBool("ouu", false);
                    SleepImage.transform.localScale = new Vector3(1f, 1f, 0);
                    sleepImage1 = Instantiate(SleepImage, new Vector3(gameObject.transform.position.x - SleepImagePos_x - 2f, gameObject.transform.position.y, 0), SleepImage.transform.rotation);
                    SleepImagePos_x += 3f;
                    Debug.Log("1");
                    break;
                case 2:
                    SleepImage.transform.localScale = new Vector3(2f, 2f, 0);
                    sleepImage2 = Instantiate(SleepImage, new Vector3(gameObject.transform.position.x - SleepImagePos_x, gameObject.transform.position.y + SleepImagePos_y, 0), SleepImage.transform.rotation);
                    SleepImagePos_x += 2f;
                    SleepImagePos_y += 2f;
                    Debug.Log("2");
                    break;
                case 3:
                    SleepImage.transform.localScale = new Vector3(3f, 3f, 0);
                    sleepImage3 = Instantiate(SleepImage, new Vector3(gameObject.transform.position.x - SleepImagePos_x, gameObject.transform.position.y + SleepImagePos_y, 0), SleepImage.transform.rotation);
                    SleepImagePos_x = 2f;
                    SleepImagePos_y = 2f;
                    Debug.Log("3");
                    break;
                case 4:
                    Debug.Log("4");
                    break;
                case 5:
                    Debug.Log("5");
                    Destroy(sleepImage1);
                    Destroy(sleepImage2);
                    Destroy(sleepImage3);
                    SleepImageCnt = 0;
                    AnimationEnd_Cnt++;
                    Debug.Log("AnimationEnd_Cnt = " + AnimationEnd_Cnt);
                    break;
                default:
                    break;
            }
        }

        if (AnimationEnd_Cnt >= 2)
        {
            rigid_p.AddForce(new Vector3(0.1f, 0, 0) * WalkAddForce);
        }

    }




    ///////////////////////////////////////////////////////////
    ///             N-4のゴールアニメーション関数           ///
    ///////////////////////////////////////////////////////////
    ///
    public GameObject HitodeMarkImage;
    bool flag_HitodeMarkImage = false;
    bool flag_HitodeMarkImage_One = false;
    public Vector3 HitodeMarkImage_Size = new Vector3(0.5f, 0.5f, 1);
    void Goal_Animation_N_4()
    {
        // N_4GoalAnimationCubeに触れたら
        if (gameObject.GetComponent<PlayerColliderController>().flag_N_4_Enter && !gameObject.GetComponent<PlayerColliderController>().flag_N_4_Exit)
        {
            // 一定の速さ以上なら
            if (flag_hayasugi)
            {
                hayasugiCnt += Time.deltaTime;
            }
            if (hayasugiCnt > 1.5)
            {
                rigid_p.isKinematic = false;
                rigid_p.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }
            // 一定の速さ以上なら
            if (rigid_p.velocity.x > 1 && !flag_Onehayasugi)
            {
                rigid_p.velocity = Vector3.zero;
                rigid_p.collisionDetectionMode = CollisionDetectionMode.Discrete;
                rigid_p.isKinematic = true;

                flag_hayasugi = true;
                flag_Onehayasugi = true;
            }

            // 地面に触れているなら
            if (Jump && rigid_p.velocity.x <= 5)
            {
                flag_hayasugi = true;
                flag_jumpar = true;
                anim.SetBool("jump", false);
                anim.SetBool("walk", true);
            }
            //Debug.Log("rigid_p.velocity =" + rigid_p.velocity);
            // playerの動きを止める
            if (flag_hayasugi && flag_jumpar)
            {
                Goal = true;
                mainCamera.SetActive(false);
                n_4GoalCamera.SetActive(true);
                gameObject.transform.position += new Vector3(0.1f, 0, 0);
            }
        }

        if(gameObject.GetComponent<PlayerColliderController>().flag_N_4_Exit)
        {
            // アニメーションの停止
            anim.SetBool("walk", false);

            if (rotatio_z < 90 && !flag_rotatio_x_90)
            {
                rotatio_z += 2f;
                transform.rotation = Quaternion.Euler(0, 0, -rotatio_z);
            }
            else if(rotatio_z == 90)
            {
                flag_rotatio_x_90 = true;
            }

            if(flag_rotatio_x_90)
            {
                flag_HitodeMarkImage = true;
                N_4AnimationEnd_Cnt += Time.deltaTime;
                anim.SetBool("ouu", true);
            }


            if (flag_HitodeMarkImage && !flag_HitodeMarkImage_One)
            {
                GameObject hitodeMarkImage = Instantiate(HitodeMarkImage, gameObject.transform.position, HitodeMarkImage.transform.rotation);
                // 画像サイズ
                hitodeMarkImage.transform.localScale = HitodeMarkImage_Size;
                flag_HitodeMarkImage_One = true;
            }
            if(rotatio_z > 0 && flag_rotatio_x_90 && N_4AnimationEnd_Cnt > 3)
            {
                anim.SetBool("ouu", false);
                anim.SetBool("nkzr", true);
                rotatio_z -= 2f;
                transform.rotation = Quaternion.Euler(0, 0, -rotatio_z);
            }
            if(rotatio_z == 0)
            {
                anim.SetBool("nkzr", false);
                anim.SetBool("ouu", false);
                anim.SetBool("walk", true);
                rigid_p.AddForce(new Vector3(0.1f, 0, 0) * WalkAddForce);
            }
        }
    }
}