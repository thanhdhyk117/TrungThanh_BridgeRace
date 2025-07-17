using UnityEngine;

public interface IState<T>
{
    void OnEnter(T owner);
    void OnExit(T owner);
    void OnExcute(T owner);
}