using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "ApartmentsListSO", menuName = "ScriptableObjects/ApartmentsList")]
public class ApartmentsListSO : ScriptableObject
{
    public ApartmentData[] Apartments;
}

[Serializable]
public class ApartmentData
{
    public string ApartmentName;
    public AssetReference ApartmentScene;
}
