using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TargetData
{
    public ITargetable TargetableEntity { get; private set; }
    public float Range { get; private set; }

    public TargetData(IMapEntity observer, IMapEntity targetMapEntity, ITargetable targetEntity)
    {
        TargetableEntity = targetEntity;
        Range = Vector2.Distance(observer.Transform.position, targetMapEntity.Transform.position);
    }
}