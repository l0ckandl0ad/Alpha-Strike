using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DefaultTargetingPattern : ITargetingPattern
{
    public List<TargetData> SortTargets(List<TargetData> availableTargets)
    {
        // sort them by size and range
        // refactor note - introduce THREAT or some other kind of priority other than size?
        // missiles? carriers? transports? destroyers?
        // currently it would be -> carrier > transport > destroyer > missile and by range within size
        // ie missiles won't be engaged before carriers because they are smaller targets, even if they're closer
        List<TargetData> sortedTargets = availableTargets.OrderByDescending(target => target.TargetableEntity.MinSize)
            .ThenBy(target => target.Range).ToList();

        return sortedTargets;
    }
}
