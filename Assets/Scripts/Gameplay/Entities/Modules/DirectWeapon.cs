using System;

[System.Serializable]
public class DirectWeapon : Weapon, IDirectWeapon
{
    protected DateTime canFire = new DateTime(); // the earliest DateTime I can fire

    protected float minDamage = 0f;
    protected float maxDamage = 0f;

    protected float accuracy = 1f; // 1f = 100% accuracy

    protected float minRange = 0f;
    protected float maxRange = 100f;

    protected float rateOfFire = 5f; // rate of fire in seconds

    public override void Fire(TargetData target) // move all this to Weapon?
    {
        // if target is within range and the weapon is operational
        if (target.Range > minRange && target.Range <= maxRange && IsOperational)
        {
            // am I reloading/in cooldown? -- refactor to bool Weapon.IsAbleToFire that checks IsOperational and delay
            if (DateTimeModel.CurrentDateTime >= canFire)
            {
                // calculate chance to hit and FIRE!
                float cth = UnityEngine.Random.Range(0f, 1f);

                // if hit then generate damage
                if (cth <= accuracy)
                {
                    // generate damage and send it via TakeDamage
                    int damage = Convert.ToInt32((UnityEngine.Random.Range(minDamage, maxDamage)));
                    target.TargetableEntity.TakeDamage(damage);
                }

                // when can I fire next time?
                canFire = DateTimeModel.CurrentDateTime.AddSeconds(rateOfFire);
            }
        }
    }
}
