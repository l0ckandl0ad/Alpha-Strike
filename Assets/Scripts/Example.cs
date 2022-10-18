using System;
using UnityEngine;
using AlphaStrike.Gameplay.Utils;

namespace AlphaStrike.Examples
{
    /// <summary>
    /// This class is an example of proper syntax usage
    /// </summary>
    /// <remarks>
    /// Please abide by it!
    /// For more information look here https://docs.microsoft.com/en-us/dotnet/csharp/codedoc
    /// </remarks>

    // On naming stuff.
    // Manager - manages multiple things. Controller - controlls one thing.
    // MVC pattern. Model is the simulated, live part of what's happening. View gives visual output. 
    public class Example : MonoBehaviour
    {
        //Example Properties
        public Example ExampleProperty { get; private set; }
        public bool IsExampleProperty { get; private set; } = true;
        public Transform ExampleForeignTransformReferenceProperty { get; private set; }

        [field: NonSerialized]// Monobehaviours and events should not be serialized!
        public event Action OnStatusChange = delegate { };

        [Header("ExampleFields")]
        public string ExampleFieldOne = "";
        [SerializeField] private string exampleFieldTwo;

        private void Awake()
        {
            if (gameObject.TryGetComponent<Transform>(out Transform transform))
            {
                ExampleForeignTransformReferenceProperty = transform;
            }
        }

        public void ExampleDebugLogMessages()
        {
            exampleFieldTwo = "sceneName";
            // using "this" to provide the name of the script and the gameObject it is attached to
            Debug.Log(this + ": " + exampleFieldTwo + " scene loaded"); // routine messages for the debug log
            Debug.LogWarning(this + ": Loading is not yet implemented!"); // warning about important but not critical things
        }
    }
}