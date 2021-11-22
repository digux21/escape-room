
using UnityEngine;

public class gunSwitch : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int selectedGun=0;
    void Start()
    {
        SelectedGun();
    }

    // Update is called once per frame
    void Update()
    {
        int prevSelGun= selectedGun;
        if(Input.GetAxis("Mouse ScrollWheel")> 0f)
        {
            if (selectedGun >= transform.childCount - 1)
                selectedGun = 0;
            else
                selectedGun++;

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedGun <= 0)
                selectedGun = transform.childCount - 1;
            else
                selectedGun--;

        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && transform.childCount >= 1)
        {
            selectedGun = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)&& transform.childCount >= 2)
        {
            selectedGun = 1;
        }

        if (prevSelGun != selectedGun)
        {
            SelectedGun();
        }
    }
    void SelectedGun()
    {
        int i = 0;
        foreach(Transform gun in transform)
        {
            if (i == selectedGun)
                gun.gameObject.SetActive(true);
            else
                gun.gameObject.SetActive(false);
            i++;
        }
    }
}
