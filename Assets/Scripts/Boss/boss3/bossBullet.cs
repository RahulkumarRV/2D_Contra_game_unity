using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBullet : MonoBehaviour
{

    [SerializeField] float speed, offset, bulletSize = 1;
    private Rigidbody rb;
    [SerializeField] private GameObject projectile, destroyEffect;
    private Transform shotPoint; // assuming shoting point is the child object of my object and it should be at 0th index
    private GameObject player;
    private float timeBtwShots;
    [SerializeField] private float startTimeBtwShots;
    private int count;
    [SerializeField] private int lifes = 1;
    [SerializeField] AudioSource mainBossDeadAudioSource;

    // set the reference to the object component's
    void Start()
    {
        count = 0;
        rb = GetComponent<Rigidbody>();
        shotPoint = transform.GetChild(0);
        player = GameObject.Find("Player");
        timeBtwShots = startTimeBtwShots;
    }

    // check the time to shoot bullets and shoot
    void Update()
    {
        if (timeBtwShots <= 0)
        {
            fireProjectile();
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    // create bullet and fire it toword the player
    void fireProjectile()
    {
        if (projectile == null || shotPoint == null || player == null)
        {
            print("Problem in EnemyFireForword_S Object shoting projectile");
            return;
        }
        Vector3 difference = player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        GameObject bullet = Instantiate(projectile, shotPoint.position, Quaternion.Euler(0f, 0f, rotZ + offset));

        if(bullet != null ){
            bullet.transform.localScale *= bulletSize;
        }

    }
    // if touches the player bullet the destroyed
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            count++;
            if (count == lifes)
            {
                if(mainBossDeadAudioSource != null) mainBossDeadAudioSource.Play();
                StartCoroutine(playDestroyEffect());
            }
        }
    }
    // play the destroy effect
    IEnumerator playDestroyEffect()
    {
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.AddForce(Vector2.up * 300);
        yield return new WaitForSeconds(0.5f);
        if (destroyEffect != null) Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(transform.parent.gameObject);
    }
}


