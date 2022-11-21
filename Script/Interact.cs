using UnityEngine;

public class Interact : MonoBehaviour
{
    public float radius =0.2f;
    Transform player;
    bool isFocus=false;
    public bool hasInteracted = false;
    PlayerControl playerControl;

    private void Start()
    {
        playerControl=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public void Update()
    {
        if (isFocus)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if(distance < radius)
            {
                Debug.Log("INTERACT");
                hasInteracted = true;
                playerControl.SitDown();
                player.transform.rotation=this.transform.rotation;
            }
        }
    }
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }
    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
}
