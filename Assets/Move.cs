using UnityEngine;

public class Move : MonoBehaviour
{
    // Speed of the movement
    private const float OscillationsPerSecond = 1f;
    private const float Length = 5f;

    // Update is called once per frame
    void Update()
    {
        // Ping pong between -Length and +Length
        var pingPong = Mathf.PingPong(Time.time * OscillationsPerSecond * Length * 2, Length*2) - Length;
        transform.position = new Vector3(pingPong, transform.position.y, transform.position.z);
    }

}
