using System;
using UnityEngine;

public class StaminaHandler : MonoBehaviour
{

    public static StaminaHandler Instance;

    public event Action OnStaminaZero;
    public event Action CanJump;
    public event Action CanSprint;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    float maxStamina = 100f;
    [SerializeField]float stamina;
    [SerializeField]float reduceAmount = 3;
    [SerializeField]float refreshAmount = 2;
    [SerializeField]bool isUsingStamina;
    [SerializeField]bool isRefreshingStamina;
    bool staminaRefreshCooldown;

    void Start()
    {
        stamina = maxStamina;
    }

    void Update()
    {
        if (isUsingStamina == true)
        {
            if (stamina >= 0)
            {
                stamina -= Time.deltaTime * reduceAmount;    
            }
            
        }
        
        if (isRefreshingStamina == true)
        {
            if (stamina <= 100)
            {
                stamina += Time.deltaTime * refreshAmount;
            }
            
        }

        if (stamina >= 25)
        {
            CanSprint?.Invoke();
        }
        if (stamina >= 10)
        {
            CanJump?.Invoke();
        }
        if (stamina <= 0)
        {
            OnStaminaZero?.Invoke();
        }
    }


    public void SetRefreshMode(bool mode)
    {
        isRefreshingStamina = mode;
    }
    public void SetUsingMode(bool mode)
    {
        isUsingStamina = mode;
    }
    public void ReduceStamina(float amount)
    {
        stamina -= amount;
    }
    void RefreshStamina()
    {
        isRefreshingStamina = true;
    }
    public float GetStamina => stamina;
    public float GetReduceAmount => reduceAmount;
 
}
