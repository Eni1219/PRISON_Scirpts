using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    static readonly Dictionary<string, IDestination> _destination = new();
    public static void Register(IDestination dest)
    {
        if (dest == null || string.IsNullOrEmpty(dest.Id)) return;
        _destination[dest.Id] = dest;
    }
   public static void Unregister(IDestination dest)
    {
        if(dest==null)return;
        _destination.Remove(dest.Id);
    }
    public static bool Go(string destinationId,Transform player)
    {
        if(destinationId != null&&_destination.TryGetValue(destinationId,out var dest))
        {
            dest.Enter(player);
            return true;
        }
        Debug.LogWarning($"[DoorSystem] Destination not found: {destinationId}");
        return false;
    }
}
