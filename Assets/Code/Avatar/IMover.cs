using UniRx;
using UnityEngine;

public interface IMover
{
    public ReactiveProperty<bool> IsMoving { get; set; }
    public void Initialize();
    public void MoveTo(Vector3 targetPosition);
    public void Stop();
}
