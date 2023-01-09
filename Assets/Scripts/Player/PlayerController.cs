using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float rotateSpeed = 100, bulletSpeed = 100;

    public int ammo = 4;

    private Transform handPos;
    private Transform Fire1, Fire2;
    private LineRenderer lineRenderer;
    public GameObject bullet;

    private GameObject crosshair;

    public AudioClip gunShoot;


    void Awake()
    {
        crosshair = GameObject.Find("Crosshair");
        crosshair.SetActive(false);
        handPos = GameObject.Find("ArmLeft").transform;
        Fire1 = GameObject.Find("Fire1").transform;
        Fire2 = GameObject.Find("Fire2").transform;
        lineRenderer = GameObject.Find("Gun").GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

    }

    void Update()
    {
        if (!IsMouseOverUI())
        {
            if (Input.GetMouseButton(0))
            {
                Aim();
                
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (ammo > 0)
                {
                    Shoot();
                }
                else
                {
                    lineRenderer.enabled = false;
                    crosshair.SetActive(false);

                }
                
            }
        }
    }
    void Aim()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - handPos.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        handPos.rotation = Quaternion.Slerp(transform.rotation,rot,rotateSpeed*Time.fixedDeltaTime);

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, Fire1.position);
        lineRenderer.SetPosition(1, Fire2.position);
        crosshair.SetActive(true);
        crosshair.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition)+(Vector3.forward*10);
    }
    void Shoot()
    {
        crosshair.SetActive(false);
        lineRenderer.enabled = false;
        GameObject b = Instantiate(bullet, Fire1.position, Quaternion.identity);
        if (transform.localScale.x>0)
        {
            b.GetComponent<Rigidbody2D>().AddForce(Fire1.right*bulletSpeed*Time.fixedDeltaTime,ForceMode2D.Impulse);
        }
        else
        {
            b.GetComponent<Rigidbody2D>().AddForce(-Fire1.right * bulletSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        ammo--;
        FindObjectOfType<GameManager>().CheckBullet();
        SoundManager.instance.PlaySoundFX(gunShoot,0.3f);
        Destroy(b, 2f);
    }

    bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
