using UnityEngine;

public interface IAbility
{
    float ActiveTime();
    float TimeCast();
    void UseAbility(GameObject parent);
}