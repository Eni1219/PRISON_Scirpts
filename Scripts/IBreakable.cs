using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBreakable
{
    void TakeHit(int damage, Vector2 hirDir);
}
