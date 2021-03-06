﻿using System.Collections.Generic;
using UnityEngine;

namespace EZPZ
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class EZ_Particlez : MonoBehaviour
    {
        public static EZ_Particlez Instance { get; private set; }
        public Dictionary<string, GameObject> ParticleEffects = new Dictionary<string, GameObject>();

        private EZ_Particlez()
        {
            Instance = this;
        }

        public void Start()
        {
            DontDestroyOnLoad(Instance);

            InitializeParticles();
        }

        private void InitializeParticles()
        {
            ConfigNode[] particleConfigNodes = GameDatabase.Instance.GetConfigNodes("EZ_Particle");

            foreach (ConfigNode node in particleConfigNodes)
            {
                GameObject particle = (GameObject)Instantiate(Resources.Load(node.GetValue("Resource")));

                DontDestroyOnLoad(particle);

                ParticleEmitter particleEmitter = particle.GetComponent<ParticleEmitter>();

                particleEmitter.emit = false;
                particleEmitter.localVelocity = Vector3.zero;
                particleEmitter.minEnergy = 0;
                particleEmitter.minEmission = 0;
                particleEmitter.angularVelocity = 0;
                particleEmitter.rndVelocity = Vector3.zero;
                particle.transform.eulerAngles = Vector3.zero;
                particle.transform.position = Vector3.zero;

                ParticleEffects.Add(node.GetValue("Name"), particle);
            }

        }
    }
}
