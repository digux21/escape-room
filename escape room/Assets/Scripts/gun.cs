
using UnityEngine;

public class gun : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float damage=10f;
    [SerializeField] float range= 100f;
    public ParticleSystem muzzelFlash;
    public GameObject impactEffect;
    public Camera fpsCam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            enemy Enemy = hit.transform.GetComponent<enemy>();
            if(Enemy != null)
            {
                Enemy.TakeDamage(damage);
            }
            GameObject impactGO= Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
