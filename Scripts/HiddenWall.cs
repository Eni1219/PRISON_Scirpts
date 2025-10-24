using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenWall : MonoBehaviour,IBreakable
{
    [SerializeField] private int hp = 3;
    [SerializeField] private Collider2D col;
    [SerializeField] private Tilemap tilemap;

    [Header("SE")]
    [SerializeField]private AudioSource audioSource;
    [SerializeField] private AudioClip hitSE;
    [SerializeField] private AudioClip breakSE;

    private void Awake()
    {
        if(!col)
            col = GetComponent<Collider2D>();
        if(!tilemap)
            tilemap = GetComponent<Tilemap>();
        if(!audioSource)
        {
            audioSource = GetComponent<AudioSource>();
            if(!audioSource)audioSource=gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }
    public void TakeHit(int damage,Vector2 hitDir)
    {
        hp-=Mathf.Max(1,damage);
        if (hp <= 0)
            Break();
            else
            HitFeedBack();
    }
    private void Break()
    {
        
        if (breakSE) audioSource.PlayOneShot(breakSE);
        Destroy(gameObject, breakSE ? breakSE.length : .01f);
    }
    private void HitFeedBack()
    {
        StartCoroutine(ShakeRoutine(.3f,.1f));
        StartCoroutine(FlashRoutine());
        if (hitSE) audioSource.PlayOneShot(hitSE);
    }
    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
    private IEnumerator FlashRoutine()
    {
        if (tilemap == null) yield break;
        Color original = tilemap.color;

        tilemap.color = Color.white;      
        yield return new WaitForSeconds(0.3f);

        tilemap.color = original;  
    }
}
