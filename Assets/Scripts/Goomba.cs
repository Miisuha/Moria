using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;
    public AudioSource src;
    public AudioClip die,kill;

     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            
            if (player.starpower) {
                Hit();
                GameManager.Instance.scoreKill();
                src.clip = kill;
                src.Play();
            }
            else if (player.big && collision.transform.DotTest(transform, Vector2.down)) {
                Flatten();
                GameManager.Instance.scoreKill();
            }
            else if (player.big) {
                Hit();
                GameManager.Instance.scoreKill();
                player.Hit();
                src.clip = kill;
                src.Play();
            } 
            else if (collision.transform.DotTest(transform, Vector2.down)) {
                Flatten();
                GameManager.Instance.scoreKill();
                src.clip = kill;
                src.Play();
            } else {
                player.Hit();
                src.clip = die;
                src.Play();
            }
        }
    }
     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell")) {
            Hit();
            GameManager.Instance.scoreKill();
            src.clip = kill;
            src.Play();
        }
    }

   
    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }
     private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

   

}