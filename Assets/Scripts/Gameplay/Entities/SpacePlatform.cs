using System;

[Serializable]
public abstract class SpacePlatform : ISpacePlatform
{
    public string Name { get; protected set; }
    public IFF IFF { get; protected set; }
    public bool IsAlive { get; protected set; } = true;
    public int MinSize { get; protected set; } = 1;
    public int MaxSize { get; protected set; } = 1;
    public int Structure { get; protected set; } = 100;
    public int VPCost { get; protected set; } = 0;
    public SpacePlatformType PlatformType { get; protected set; } = SpacePlatformType.BASE;
    [field: NonSerialized]
    public IMapEntity MapEntity { get; protected set; } = null;
    [field:NonSerialized]
    public event Action OnStatusChange = delegate { };
    [field: NonSerialized]
    public event Action<ISpacePlatform> OnSpacePlatformHit = delegate { };
    [field: NonSerialized]
    public event Action<ISpacePlatform> OnSpacePlatformDestroyed = delegate { };
    public virtual void TakeDamage(int damage)
    {
        if (!IsAlive)
        {
            return;
        }
        else
        {
            if (damage > 0)
            {
                if (Structure >= 1)
                {
                    GetDamaged(damage);
                }
                if (Structure <= 0)
                {
                    Destroy();
                }
                InternalUpdateOnTakeDamage();
            }
            
        }
    }

    private void GetDamaged(int damage)
    {
        Structure -= damage;

        MessageLog.SendMessage("HIT! " + Name + " gets " + damage + " points of damage! STR: "
    + Structure.ToString() + ".", MessagePrecedence.IMMEDIATE);

        OnSpacePlatformHit?.Invoke(this);
        OnStatusChange?.Invoke();
    }

    /// <summary>
    /// Taking care of events that must happen when this entity takes damage.
    /// </summary>
    protected virtual void InternalUpdateOnTakeDamage()
    {

    }

    public void BindToMapEntity(IMapEntity mapEntity)
    {
        MapEntity = mapEntity;
    }
    public virtual void Destroy()
    {
        IsAlive = false;
        MessageLog.SendMessage(Name + " EXPLODES!", MessagePrecedence.FLASH);
        OnSpacePlatformDestroyed?.Invoke(this);
        OnStatusChange?.Invoke();
    }
}
