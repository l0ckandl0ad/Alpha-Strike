using System;
using UnityEngine;

public static class MapEntitySelectionModel
{
    public static event Action<IMapEntity> OnMapEntitySelection = delegate { };
    public static bool IsMapEntitySelectionAllowed { get; private set; } = true;
    public static IMapEntity SelectedMapEntity { get; private set; }    // currently selected IMapEntity
    public static GameObject SelectedGameObject { get; private set; }   // selected IMapEntity's GameObject

    public static void Initialize()
    {
        IsMapEntitySelectionAllowed = true;
    }

    public static void SelectMapEntity(IMapEntity selectee)
    {
        if (IsMapEntitySelectionAllowed)
        {
            SelectedMapEntity = selectee;
            SelectedGameObject = selectee.GameObject;
            OnMapEntitySelection(selectee);    // fire the event: tell everyone that selection happened
        }
    }
    public static void ToggleSelectionAllowed()
    {
        IsMapEntitySelectionAllowed = !IsMapEntitySelectionAllowed;

        /// Refactor notes.
        /// This is toggled from outside, from a coroutine.
        /// This means that if the couroutine ends prematurely (say, the object that runs it is suddenly destroyed)
        /// then this toggle WILL BREAK!
        /// and it WILL BREAK ALL THE UI interactions (see SelectMapEntity bool check!)
    }

}
