using System;

public interface  IHealth
{
    int maxHealth {  get; }
    int currentHealth {  get; }

    event Action<int,int> OnHealthChanged;
    event Action OnDied;


}
