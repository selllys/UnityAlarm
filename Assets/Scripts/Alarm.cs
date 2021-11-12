using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AlarmAudio))]
public class Alarm : MonoBehaviour
{
    private AlarmAudio _alarmAudio;

    private void Start()
    {
        _alarmAudio = GetComponent<AlarmAudio>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Thief>(out Thief thief))
        {
            Debug.Log("Thief detected. Starting Alarm.");
            _alarmAudio.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Thief>(out Thief thief))
        {
            Debug.Log("Thief left our house. Stopping Alarm.");
            _alarmAudio.Stop();
        }
    }
}