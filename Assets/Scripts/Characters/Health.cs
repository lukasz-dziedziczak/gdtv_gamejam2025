using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float Amount { get; private set; } = 100;
    [field: SerializeField] public float Max { get; private set; } = 100;

    public float Percentage => Amount / Max;

    public event Action OnDeath;
    public event Action<GameObject> OnDamaged;

    private void Start()
    {
        Amount = Max;
    }

    public void ApplyDamage(float AmountToTake, GameObject damageGiver = null)
    {
        if (Amount <= 0) return;

        //Debug.Log(gameObject.name + " took " + AmountToTake + " damage");
        Amount = Mathf.Clamp(Amount - AmountToTake, 0, Max);

        if (Amount == 0) OnDeath?.Invoke();
        else OnDamaged?.Invoke(damageGiver);
    }
}
