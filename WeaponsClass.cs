using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class WeaponsClass : MonoBehaviour
{

    public AudioSource fire;
    public bool burst;
    public float barrelSpin;
    private Renderer muzzleRenderer;
    public AudioClip pistolClip, rocketClip, smgClip, shotgunClip, plasmaClip, bfgClip, chainGunClip;
    public float timeElapsed, machineGunROF, forwardAmt;
    public int bulletCount;
    public GameObject pistol, machineGun, rocketLauncher, plasmaGun, bfg,
        shotgun, chaingun, chainGunBarrel, bulletHole, grenade, bfgMissile, muzzleFlash, gunPos;
    public enum WeaponState
    {
        PISTOL,
        MACHINE_GUN,
        ROCKET_LAUNCHER,
        PLASMA_GUN,
        BFG,
        SHOTGUN,
        CHAINGUN
    }

    public WeaponState weaponState;
    // Use this for initialization
    void Start()
    {
        burst = true;
        fire = gameObject.GetComponent<AudioSource>();
        StartCoroutine(BFG());
        StartCoroutine(Burst());
        StartCoroutine(ChainGun());
        muzzleRenderer = muzzleFlash.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        timeElapsed += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponState = WeaponState.PISTOL;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponState = WeaponState.SHOTGUN;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponState = WeaponState.MACHINE_GUN;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            weaponState = WeaponState.ROCKET_LAUNCHER;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            weaponState = WeaponState.PLASMA_GUN;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            weaponState = WeaponState.BFG;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            weaponState = WeaponState.CHAINGUN;
        }


        if (Input.GetButtonDown("Fire1") && weaponState == WeaponState.PISTOL)
        {

            fire.clip = pistolClip;
            fire.Play();
            muzzleRenderer.enabled = Mathf.Repeat(Time.time * 10.0f, 1.0f) < 0.5;
            RaycastHit hit;
            if (Physics.Raycast(gunPos.transform.position, gunPos.transform.forward, out hit, 100f))
            {

                Debug.Log(hit.transform.tag);
                if (hit.transform.tag == "Barrel")
                {
                    hit.transform.SendMessage("IsShot", SendMessageOptions.DontRequireReceiver);
                }

                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.SendMessage("Damage", 5f);
                }
                else
                {
                    GameObject hole = (GameObject)GameObject.Instantiate(bulletHole, hit.point + hit.normal * .0004f, Quaternion.FromToRotation(-Vector3.forward, hit.normal));
                    hole.transform.parent = hit.transform;
                }
               // hit.transform.BroadcastMessage("OnDamageTaken", 10);
            }
            Debug.DrawRay(gunPos.transform.position, gunPos.transform.forward * 100f, Color.green);


        }

        if (Input.GetButtonDown("Fire1") && weaponState == WeaponState.SHOTGUN)
        {
            fire.clip = shotgunClip;
            fire.Play();

            RaycastHit hit;
            int amnt = 4;
            for (int i = 0; i <= amnt; i++)
            {
                Ray r = new Ray(gunPos.transform.position, gunPos.transform.forward);
                r.direction += new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), Random.Range(-.1f, .1f));
                if (Physics.Raycast(r, out hit, 10f))
                {

                    Debug.Log(hit.transform.tag);
                    if (hit.transform.tag == "Barrel")
                    {
                        hit.transform.SendMessage("IsShot", SendMessageOptions.DontRequireReceiver);
                    }
                    if (hit.transform.tag == "Enemy")
                    {
                        hit.transform.SendMessage("Damage", 15f);
                    }
                    else
                    {
                        GameObject hole = (GameObject)GameObject.Instantiate(bulletHole, hit.point + hit.normal * .0004f, Quaternion.FromToRotation(-Vector3.forward, hit.normal));
                        hole.transform.parent = hit.transform;
                    }
                   // hit.transform.BroadcastMessage("OnDamageTaken", 5);
                    Debug.DrawRay(r.origin, r.direction * 10f, Color.green);
                }

            }
        }

        if (Input.GetButton("Fire1") && weaponState == WeaponState.MACHINE_GUN && timeElapsed >= 1 / machineGunROF)
        {
            fire.clip = smgClip;
            RaycastHit hit;
            muzzleFlash.SetActive(Mathf.Repeat(Time.time * 10.0f, 1.0f) < 0.5);
            if (Physics.Raycast(gunPos.transform.position, gunPos.transform.forward, out hit, 10f))
            {

                Debug.Log(hit.transform.tag);
                if (hit.transform.tag == "Barrel")
                {
                    hit.transform.SendMessage("IsShot", SendMessageOptions.DontRequireReceiver);
                }
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.SendMessage("Damage", 2f);
                }
                else
                {
                    GameObject hole = (GameObject)GameObject.Instantiate(bulletHole, hit.point + hit.normal * .0004f, Quaternion.FromToRotation(-Vector3.forward, hit.normal));
                    hole.transform.parent = hit.transform;
                }
            }
            Debug.DrawRay(gunPos.transform.position, gunPos.transform.forward * 10f, Color.green);
            timeElapsed = 0;
            //hit.transform.BroadcastMessage("OnDamageTaken", 1);

            fire.Play();

        }
        else
        {
            muzzleFlash.SetActive(false);
        }

        if (Input.GetButtonDown("Fire1") && weaponState == WeaponState.ROCKET_LAUNCHER)
        {
            fire.clip = rocketClip;
            fire.Play();
            GameObject grenadeC = (GameObject)GameObject.Instantiate(grenade, gameObject.transform.position + gameObject.transform.forward * 2f, gameObject.transform.rotation);
            grenadeC.GetComponent<Rigidbody>().AddForce(gunPos.transform.forward * 1000f);
        }

        if (Input.GetButton("Fire1") && weaponState == WeaponState.CHAINGUN)
        {
            chainGunBarrel.transform.Rotate(new Vector3(0f, 10f, 0f));
        }









        switch (weaponState)
        {
            case WeaponState.PISTOL:
                SetRenderPistol();
                break;
            case WeaponState.SHOTGUN:
                SetRenderShotgun();
                break;
            case WeaponState.MACHINE_GUN:
                SetRenderMachineGun();
                break;
            case WeaponState.ROCKET_LAUNCHER:
                SetRenderRL();
                break;
            case WeaponState.PLASMA_GUN:
                SetRenderPlasmaGun();
                break;
            case WeaponState.BFG:
                SetRenderBFG();
                break;
            case WeaponState.CHAINGUN:
                SetRenderChaingun();
                break;
        }
    }


    void SetRenderPistol()
    {
        pistol.SetActive(true);
        shotgun.SetActive(false);
        machineGun.SetActive(false);
        rocketLauncher.SetActive(false);
        plasmaGun.SetActive(false);
        bfg.SetActive(false);
        chaingun.SetActive(false);
    }


    void SetRenderShotgun()
    {
        pistol.SetActive(false);
        shotgun.SetActive(true);
        machineGun.SetActive(false);
        rocketLauncher.SetActive(false);
        plasmaGun.SetActive(false);
        bfg.SetActive(false);
        chaingun.SetActive(false);
    }
    void SetRenderMachineGun()
    {
        pistol.SetActive(false);
        shotgun.SetActive(false);
        machineGun.SetActive(true);
        rocketLauncher.SetActive(false);
        plasmaGun.SetActive(false);
        bfg.SetActive(false);
        chaingun.SetActive(false);
    }

    void SetRenderRL()
    {
        pistol.SetActive(false);
        shotgun.SetActive(false);
        machineGun.SetActive(false);
        rocketLauncher.SetActive(true);
        plasmaGun.SetActive(false);
        bfg.SetActive(false);
        chaingun.SetActive(false);
    }
    void SetRenderPlasmaGun()
    {
        pistol.SetActive(false);
        shotgun.SetActive(false);
        machineGun.SetActive(false);
        rocketLauncher.SetActive(false);
        plasmaGun.SetActive(true);
        bfg.SetActive(false);
        chaingun.SetActive(false);
    }


    void SetRenderBFG()
    {
        pistol.SetActive(false);
        shotgun.SetActive(false);
        machineGun.SetActive(false);
        rocketLauncher.SetActive(false);
        plasmaGun.SetActive(false);
        bfg.SetActive(true);
        chaingun.SetActive(false);
    }


    void SetRenderChaingun()
    {
        pistol.SetActive(false);
        shotgun.SetActive(false);
        machineGun.SetActive(false);
        rocketLauncher.SetActive(false);
        plasmaGun.SetActive(false);
        bfg.SetActive(false);
        chaingun.SetActive(true);
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(gunPos.transform.position, gunPos.transform.forward, out hit, 60f))
        {

            Debug.Log(hit.transform.tag);
            if (hit.transform.tag == "Barrel")
            {
                hit.transform.SendMessage("IsShot", SendMessageOptions.DontRequireReceiver);
            }
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.SendMessage("OnDamageTaken", 5);
            }
            else
            {
                GameObject hole = (GameObject)GameObject.Instantiate(bulletHole, hit.point + hit.normal * .0004f, Quaternion.FromToRotation(-Vector3.forward, hit.normal));
                hole.transform.parent = hit.transform;
            }
           // hit.transform.BroadcastMessage("OnDamageTaken", 5);
        }
        Debug.DrawRay(gunPos.transform.position, gunPos.transform.forward * 60f, Color.green);
    }



    public void Pickup(string s)
    {
        switch (s)
        {
            case "BFG":
                weaponState = WeaponState.BFG;
                break;
            case "Shotgun":
                weaponState = WeaponState.SHOTGUN;
                break;
            case "Rocket Launcher":
                weaponState = WeaponState.ROCKET_LAUNCHER;
                break;
            case "SMG":
                weaponState = WeaponState.MACHINE_GUN;
                break;
            case "Battle Rifle":
                weaponState = WeaponState.PLASMA_GUN;
                break;
            case "Pistol":
                weaponState = WeaponState.PISTOL;
                break;
            case "Chain Gun":
                weaponState = WeaponState.CHAINGUN;
                break;
        }
    }
    IEnumerator Burst()
    {
        if (Input.GetButton("Fire1") && weaponState == WeaponState.PLASMA_GUN)
        {
            fire.clip = plasmaClip;
            fire.Play();



            Shoot();
            Shoot();
            Shoot();


        }
        yield return new WaitForSeconds(.5f);
        StartCoroutine(Burst());
    }

    IEnumerator BFG()
    {
        if (Input.GetButton("Fire1") && weaponState == WeaponState.BFG)
        {
            fire.clip = bfgClip;
            fire.Play();
            GameObject bfgM = (GameObject)GameObject.Instantiate(bfgMissile, gunPos.transform.position + gunPos.transform.forward * forwardAmt, gunPos.transform.rotation);

            bfgM.GetComponent<Rigidbody>().AddForce(gunPos.transform.forward * 5000f);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(BFG());
    }

    IEnumerator ChainGun()
    {
        if (Input.GetButton("Fire1") && weaponState == WeaponState.CHAINGUN)
        {
            //chainGunBarrel.transform.Rotate(new Vector3(1f,0f,0f)* barrelSpin);
            Ray r = new Ray(gunPos.transform.position, gunPos.transform.forward);
            r.direction += new Vector3(Random.Range(-.05f, .05f), Random.Range(-.05f, .05f), Random.Range(-.05f, .05f));
            RaycastHit hit;
            fire.clip = chainGunClip;
            fire.Play();
            if (Physics.Raycast(r, out hit, 60f))
            {

                Debug.Log(hit.transform.tag);
                if (hit.transform.tag == "Barrel")
                {
                    hit.transform.SendMessage("IsShot", SendMessageOptions.DontRequireReceiver);
                }
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.SendMessage("OnDamageTaken", 15);
                }
                else
                {
                    GameObject hole = (GameObject)GameObject.Instantiate(bulletHole, hit.point + hit.normal * .0004f, Quaternion.FromToRotation(-Vector3.forward, hit.normal));
                    hole.transform.parent = hit.transform;
                }
                hit.transform.BroadcastMessage("OnDamageTaken", 3);
                Debug.DrawRay(r.origin, r.direction * 60f, Color.green);
            }
        }
        yield return new WaitForSeconds(.15f);
        StartCoroutine(ChainGun());

    }
}