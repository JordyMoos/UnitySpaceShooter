using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject DeathParticlesPrefab = null;
    private Transform ThisTransform = null;
    public bool ShouldRestroyOnDeath = true;

    [SerializeField]
    private float _HealthPoints = 100f;

    void Start()
    {
        ThisTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HealthPoints = 0;
        }
    }

    public float HealthPoints
    {
        get
        {
            return _HealthPoints;
        }

        set
        {
            _HealthPoints = value;

            if (_HealthPoints <= 0)
            {
                SendMessage("Die", SendMessageOptions.DontRequireReceiver);

                if (DeathParticlesPrefab != null)
                {
                    Instantiate(DeathParticlesPrefab, ThisTransform.position, ThisTransform.rotation);
                }

                if (ShouldRestroyOnDeath)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
