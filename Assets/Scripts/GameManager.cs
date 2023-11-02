using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private GameObject enemyZombie;
    [SerializeField] private bool waiting;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // invocar zombies
        if (Input.GetKeyDown(KeyCode.F)) {
            Instantiate( enemyZombie, 
                new Vector3(
                    this.transform.position.x, 
                    this.transform.position.y, 
                    this.transform.position.z
                ),
                Quaternion.identity
            );
        }
    }
    // HITSTOP
    public void Stop(float duration) {
        if (waiting == true)
            return;
        Time.timeScale = 0.0f;
        StartCoroutine(Wait(duration));
    }
    
    IEnumerator Wait(float duration) {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waiting = false;
    }
}
