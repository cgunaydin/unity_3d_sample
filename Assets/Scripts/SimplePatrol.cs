using UnityEngine;

public class SimplePatrol : MonoBehaviour
{
    EnemyAttackController attackController;

    public bool enemySpotted;

    public float speed = 5.0f; // Adjust the speed of movement

    private bool movingForward = true;
    private float timer = 0.0f;
    private float switchDirectionTime = 5.0f; // Time to switch direction

    //TODO: saldýrdýðý objeden vazgeçince(destroy ya da alandan çýkma) burasý bozuluyor.
    void Update()
    {
        if (!enemySpotted)
        {
            timer += Time.deltaTime;

            if (timer >= switchDirectionTime)
            {
                movingForward = !movingForward;
                timer = 0.0f;
            }

            if (movingForward)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
            }
        }
    }
}