using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanketusenBullet : MonoBehaviour
{
    float Bullet_size_x = 0.3f;
    float Bullet_size_y = 0.3f;
    float Bullet_size_z = 0.3f;

    int shoot = 0;

    // 弾の変数
    GameObject bullet;
    Vector3 bulletForce;
    float bulletForce_x = 8.0f;
    float bulletPos_x = -1.1f;
    Vector3 bulletPos;
    float bulletmass = 0.1f;
    public GameObject bulletPrefab;
    Rigidbody rigid_b;
    Vector3 bullet_size;

    // Start is called before the first frame update
    void Start()
    {
        // 弾の初期化
        rigid_b = bulletPrefab.GetComponent<Rigidbody>();
        rigid_b.mass = bulletmass;
        bulletForce = new Vector3(bulletForce_x, 0, 0);
        bulletPrefab.transform.localScale = new Vector3(Bullet_size_x, Bullet_size_y, Bullet_size_z);
        bullet_size = new Vector3(Bullet_size_x, Bullet_size_y, Bullet_size_z);
    }

    // Update is called once per frame
    void Update()
    {
        // 弾の発射
        if (shoot % 4 == 0)
        {
            bulletPos = gameObject.transform.position + new Vector3(bulletPos_x, 0, 0);
            
            bulletForce = new Vector3(-bulletForce_x, Random.Range(-1.0f, 2.0f), 0);
            // 弾の生成
            bullet = Instantiate(bulletPrefab, bulletPos, bulletPrefab.transform.rotation) as GameObject;
            // 弾の重さを適用
            bullet.GetComponent<Rigidbody>().mass = bulletmass;
            // 弾のサイズを適用
            bullet.GetComponent<Transform>().localScale = bullet_size;
            // 弾に加わる力を適用
            bullet.GetComponent<Rigidbody>().AddForce(bulletForce);
        }
        shoot++;
        
    }
}
