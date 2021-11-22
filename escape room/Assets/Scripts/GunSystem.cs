
using UnityEngine;
using System.Collections;
using TMPro;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    [SerializeField] int damage;
    [SerializeField] float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    [SerializeField] int maxAmmo, magazineSize, bulletsPerTap;
    [SerializeField] bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;
   [SerializeField] bool allowInvoke;

    //Reference
    public Camera fpsCam;
    public Transform shootPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    public Animator anim;
    //public CamShake camShake;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;

    
    private void Awake()
    {
        reloading = false;
        anim.SetBool("reloading", false);
        if (bulletsLeft == -1)
            bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void OnEnable()
    {
        reloading = false;
        anim.SetBool("reloading", false);
    }
    private void Update()
    {
        if (bulletsLeft <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        MyInput();

        //SetText
        text.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
                rayHit.collider.GetComponent<enemy>().TakeDamage(damage);
        }

        //ShakeCamera
        //camShake.Shake(camShakeDuration, camShakeMagnitude);

        //Graphics
        GameObject impactEffect= Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        //Instantiate(muzzleFlash, shootPoint.position, Quaternion.identity);
        Destroy(impactEffect, 2f);

        bulletsLeft--;
        bulletsShot--;
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
        

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
    IEnumerator Reload()
    {
        reloading = true;
        anim.SetBool("reloading", true);
        yield return new WaitForSeconds(reloadTime - .25f);
        anim.SetBool("reloading", false);
        yield return new WaitForSeconds(.25f);
        bulletsLeft = magazineSize;
        
        reloading = false;
    }
    
}
