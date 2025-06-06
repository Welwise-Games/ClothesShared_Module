using System.Collections.Generic;
using UnityEngine;

namespace WelwiseClothesSharedModule.Runtime.Client.Scripts
{
    public class PlayerColorableClothesViewSerializableComponents : MonoBehaviour
    {
        [field: SerializeField] public SkinnedMeshRenderer MainSkinnedMeshRenderer { get; private set; }
        [field: SerializeField] public List<DefaultClothesInstances> DefaultClothesInstances { get; private set; }
    }
}