using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestination
{
    void Enter(Transform player);
    string Id { get; }
}
