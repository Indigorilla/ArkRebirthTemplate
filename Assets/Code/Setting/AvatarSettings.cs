using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class NavmeshMoverSettings
{
    public float Speed = 5f;
    public float BaseOffset = -0.05f;
    public float AngularSpeed = 360f;
    public float Acceleration = 1000f;
    public ObstacleAvoidanceType ObstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
    public bool AutoBraking = false;
    public float StoppingDistance = 0f;
}

[CreateAssetMenu(fileName = "AvatarSettings", menuName = "Settings/AvatarSettings")]
public class AvatarSettings : ScriptableObject
{
    [SerializeField] public string ModelName;
    [SerializeField] public NavmeshMoverSettings NavmeshMoverSettings;
}
