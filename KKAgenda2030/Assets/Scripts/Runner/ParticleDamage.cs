using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour {

    ParticleSystem ps;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();


    void Awake() {
        ps = GetComponent<ParticleSystem>();
        ps.trigger.SetCollider(0, GameObject.Find("Player").GetComponent<BoxCollider>());

    }

    private void OnParticleTrigger() {
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        if (numEnter > 0) {
            RunnerGameManager.instance.LoseLife();
        }
    }
}
