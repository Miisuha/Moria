using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DeathBarrier : MonoBehaviour

{   
    public AudioSource src;
    public AudioClip sfx1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            //other.gameObject.SetActive(false);
            player.Hit();
            src.clip = sfx1;
            src.Play();
            GameManager.Instance.ResetLevel(3f);
            
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
    private void Hit()
    {
        //GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

}