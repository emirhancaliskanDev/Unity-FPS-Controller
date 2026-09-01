using UnityEngine;

public class GeneralHelpers : MonoBehaviour
{
    public static GeneralHelpers Instance;
    private void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    [SerializeField]LayerMask groundLayer;


    void Update()
    {
        
    }
    public bool GroundCheck()
    {
        return Physics.Raycast(transform.position,Vector3.down,2f,groundLayer);
    }
}
