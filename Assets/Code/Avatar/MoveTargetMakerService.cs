using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MoveTargetMakerService
{
    private class MarkerData
    {
        public GameObject Effect;
        public Renderer[] Renderers;
    }

    private MarkerData markerData;

    public async UniTask Initialize()
    {
        var effect = await Addressables.InstantiateAsync("EffectMoveTarget");
        
        var renderers = effect.GetComponentsInChildren<Renderer>();

        markerData = new MarkerData
        {
            Effect = effect,
            Renderers = renderers
        };

        HideTarget();
    }

    public void SetTarget(Vector3 position)
    {
        position.y += 0.1f;
        markerData.Effect.transform.position = position;
        
        foreach (var renderer in markerData.Renderers)
        {
            renderer.enabled = true;
        }
    }
    
    public void HideTarget()
    {
        foreach (var renderer in markerData.Renderers)
        {
            renderer.enabled = false;
        }
    }
}
