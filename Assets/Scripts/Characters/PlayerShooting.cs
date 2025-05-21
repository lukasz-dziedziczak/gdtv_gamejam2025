using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] LayerMask raycastLayers;
    [SerializeField] float weaponCooldown = 0.1f;
    [SerializeField] Camera cam;
    [SerializeField] float projectileSpeed = 100.0f;
    [SerializeField] Transform muzzle;
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] A_RifleMuzzle muzzleAudio;
    [SerializeField] float weaponDamage = 10.0f;
    [SerializeField] float aimVariation = 1.0f;
    [field: SerializeField] public int CurrentAmmo {  get; private set; }
    [field: SerializeField] public int MaxAmmo { get; private set; }
    [field: SerializeField] public int StorageAmmo { get; private set; }

    float cooldownTimer;
    Player player;
    [field: SerializeField] public bool IsReloading { get; private set; }

    [SerializeField] bool autoFire;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        CurrentAmmo = MaxAmmo;
        UI.Ammo.UpdateAmmoText();
    }

    private void OnEnable()
    {
        player.Input.Attack += OnAttackStart;
        player.Input.AttackComplete += OnAttackComplete;
        player.Input.Reload += BeginReload;
    }

    private void OnDisable()
    {
        player.Input.Attack -= OnAttackStart;
        player.Input.AttackComplete -= OnAttackComplete;
        player.Input.Reload -= BeginReload;
    }

    private void Update()
    {
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;

        if (!autoFire && player.Input.IsAttacking) FireWeapon();

        //if (IsReloading && !player.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Reload")) Reload();
    }

    private void FireWeapon()
    {
        if (!player.IsAlive || Time.timeScale == 0) return;
        if (cooldownTimer > 0 || CurrentAmmo == 0) return;

        if (!autoFire)
        {
            if (player.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Fire")) return;
            cooldownTimer = weaponCooldown;
        }
        
        Fire();
    }

    public void Fire()
    {
        if (!player.IsAlive || Time.timeScale == 0) return;
        if (IsReloading) return;
        if (CurrentAmmo <= 0)
        {
            BeginReload();
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, cam.transform.forward, out hit, 1000.0f, raycastLayers))
        {
            // Rotate to match camera's horizontal forward
            Vector3 lookDirection = cam.transform.forward;
            lookDirection.y = 0f;
            lookDirection.Normalize();
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = targetRotation;

            Projectile.ProjectilePayload payload = new Projectile.ProjectilePayload();
            payload.Target = hit.point;
            payload.Damage = weaponDamage;
            payload.Speed = projectileSpeed;
            payload.Health = hit.collider.GetComponent<Health>();
            payload.DamageGiver = player.gameObject;

            Projectile projectile = Instantiate<Projectile>(projectilePrefab, muzzle.position, muzzle.rotation);
            projectile.Initilize(payload);

            player.Animator.SetTrigger("Fire");

            muzzleFlash.Play();

            muzzleAudio.PlayFireSound();

            //print("hit " + hit.collider.name);
        }
        else
        {
            print("miss");
        }

        CurrentAmmo--;
        UI.Ammo.UpdateAmmoText();
    }

    private Vector3 raycastOrigin
    {
        get
        {
            Vector3 origin = cam.transform.position;
            origin += cam.transform.up * Random.Range(-aimVariation, aimVariation);
            origin += cam.transform.right * Random.Range(-aimVariation, aimVariation);
            return origin;
        }
    }

    public void Reload()
    {
        int ammoToReload = Mathf.Min(MaxAmmo - CurrentAmmo, StorageAmmo);

        CurrentAmmo += ammoToReload;
        StorageAmmo -= ammoToReload;
        UI.Ammo.UpdateAmmoText();

        IsReloading = false;
        UI.Crosshair.ShowCrosshair();
    }

    public void BeginReload()
    {
        if (!player.IsAlive || Time.timeScale == 0) return;
        if (IsReloading || StorageAmmo <= 0) return;
        IsReloading = true;
        player.Animator.SetTrigger("Reload");
        UI.Crosshair.HideCrosshair();
    }

    public void AddToAmmoStorage(int amount)
    {
        StorageAmmo += amount;
        UI.Ammo.UpdateAmmoText();
        UI.Audio.PlayAmmoPickupSound();
    }

    private void OnAttackStart()
    {
        if (!player.IsAlive || Time.timeScale == 0) return;
        if (IsReloading) return;
        if (cooldownTimer > 0) return;
        if (CurrentAmmo <= 0)
        {
            BeginReload();
            return;
        }

        if (!autoFire)
        {
            if (player.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Fire")) return;
            cooldownTimer = weaponCooldown;
        }

        player.Animator.SetBool("Fireing", true);
        Fire();
    }

    private void OnAttackComplete()
    {
        player.Animator.SetBool("Fireing", false);
    }
}
