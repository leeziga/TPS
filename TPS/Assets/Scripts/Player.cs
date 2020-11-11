using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance { get; set; }

    public enum GunType
    {
        Pistol,
        Riffle,
        Count
    }
    public Gun pistol;
    public Gun riffle;
    private Gun currentGun;
    [SerializeField]
    public float maxHealth = 100;
    [SerializeField]
    public float health = 100;

    public float Health { get { return health; } set { health = value; } }

    private Animator animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentGun = riffle;
        pistol.gameObject.SetActive(false);
        currentGun.UpdateUI();
        health = maxHealth;
        animator = GetComponent<Animator>();
        UpdateUI();
    }

    public void Shoot()
    {
        currentGun.Shoot();
    }

    public void ReloadGun(float animationSpeed)
    {
        animator.SetTrigger("isReloading");
        animator.speed = animationSpeed;
    }

    public void TriggerReload()
    {
        currentGun.Reload();
        animator.speed = 1.0f;
    }

    public void SwitchGun(GunType type)
    {
        currentGun.gameObject.SetActive(false);
        switch (type)
        {
            case GunType.Pistol:
                currentGun = pistol;
                break;
            case GunType.Riffle:
                currentGun = riffle;
                break;
            default:
                currentGun = riffle;
                break;
        }
        currentGun.gameObject.SetActive(true);
        currentGun.UpdateUI();
    }

    public void ModifyHealth(float damage)
    {
        health -= damage;
        UpdateUI();
    }

    private void Update()
    {
        if (health <= 0.0f)
        {
            SceneManager.LoadScene(0);
        }
        Debug.Log("YOU LOSE"); 
    }

    public void UpdateUI()
    {
        ServiceLocator.Get<UIManager>().UpdatePlayerHealth(health / maxHealth);
    }
}
