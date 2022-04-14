using UnityEngine;

public class LoseLine : MonoBehaviour
{
    public event System.Action OnLose;
    private bool lost;

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<Cube>(out Cube cube))
        {
            if (cube.isFlying == false && !lost)
            {
                OnLose?.Invoke();
                lost = true;
            }
        }
    }
}
