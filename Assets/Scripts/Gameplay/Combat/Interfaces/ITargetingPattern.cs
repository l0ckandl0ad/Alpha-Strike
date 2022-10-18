using System.Collections.Generic;

public interface ITargetingPattern
{
    List<TargetData> SortTargets(List<TargetData> availableTargets);
}
