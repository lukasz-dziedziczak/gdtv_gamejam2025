using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] float weaponCooldown = 0.1f;
    [SerializeField] Camera cam;
    [SerializeField] float projectileSpeed = 100.0f;
    [SerializeField] Transform muzzle;
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] A_RifleMuzzle muzzleAudio;
    [SerializeField] float weaponDamage = 10.0f;
    [field: SerializeField] public int CurrentAmmo {  get; private set; }
    [field: SerializeField] public int MaxAmmo { get; private set; }
    [field: SerializeField] public int StorageAmmo { get; private set; }

    float cooldownTimer;
    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        CurrentAmmo = MaxAmmo;
        UI.Ammo.UpdateAmmoText();
    }

    private void Update()
    {
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;

        if (player.Input.IsAttacking) FireWeapon();
    }

    private void FireWeapon()
    {
        if (cooldownTimer > 0 || CurrentAmmo == 0) return;

        if (player.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Fire")) return;


        cooldownTimer = weaponCooldown;

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000.0f))
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

            print("hit " + hit.collider.name);
        }
        else
        {
            print("miss");
        }

        CurrentAmmo--;
        UI.Ammo.UpdateAmmoText();
    }

    public void Reload()
    {
        int ammoToReload = Mathf.Min(MaxAmmo - CurrentAmmo, StorageAmmo);

        CurrentAmmo += ammoToReload;
        StorageAmmo -= ammoToReload;
        UI.Ammo.UpdateAmmoText();
    }

    public void AddToAmmoStorage(int amount)
    {
        StorageAmmo += amount;
        UI.Ammo.UpdateAmmoText();
    }
}
