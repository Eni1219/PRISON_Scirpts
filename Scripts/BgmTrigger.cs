using UnityEngine;

public class BgmTrigger : MonoBehaviour
{
    public enum TriggerAction
    {
        Play,
        Stop
    }

    [Header("BGM Settings")]
    [SerializeField] private string bgmName;

    [SerializeField] private TriggerAction action;

    private bool hasBeenTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasBeenTriggered)
        {
            hasBeenTriggered = true; 

            if (AudioManager.instance == null) return;

            switch (action)
            {
                case TriggerAction.Play:
                    AudioManager.instance.Play(bgmName);
                    break;
                case TriggerAction.Stop:
                    AudioManager.instance.Stop(bgmName);
                    break;
            }
        }
    }
}