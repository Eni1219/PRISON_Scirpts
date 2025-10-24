using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealable
{
   int maxHealCount { get; }
   int currentHealCount { get; } 
   event System.Action<int,int> OnHealCountChanged;
}
