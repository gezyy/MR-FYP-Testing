using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigRookGames.Weapons
{
    public class ProjectileController : MonoBehaviour
    {
        // --- Config ---
        public float speed = 100f;  // 子弹的基本速度
        public LayerMask collisionLayerMask;

        // --- Explosion VFX ---
        public GameObject rocketExplosion;

        // --- Projectile Mesh ---
        public MeshRenderer projectileMesh;

        // --- Script Variables ---
        private bool targetHit;
        private Transform target;  // 目标对象
        private bool isTracking;   // 是否开始追踪

        // --- Audio ---
        public AudioSource inFlightAudioSource;

        // --- VFX ---
        public ParticleSystem disableOnHit;

        private void Start()
        {
            // 尝试检测是否有目标可以追踪
            CheckForTarget();
        }

        private void Update()
        {
            // 如果已经命中目标，停止更新位置
            if (targetHit) return;

            // 如果正在追踪目标，进行平滑移动
            if (isTracking && target != null)
            {
                // 计算目标的方向
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                // 计算平滑转向
                transform.forward = Vector3.Lerp(transform.forward, directionToTarget, 0.1f);

                // 更新位置，向目标移动
                transform.position += transform.forward * (speed * Time.deltaTime);
            }
            else
            {
                // 如果没有追踪目标，则按初始方向飞行
                transform.position += transform.forward * (speed * Time.deltaTime);
            }
        }

        private void CheckForTarget()
        {
            // 查找标签为 "Stone" 的目标
            GameObject potentialTarget = GameObject.FindWithTag("Stone");
            if(potentialTarget == null)
            {
                Debug.Log("No Targets!");
            }
            if (potentialTarget != null)
            {
                // 检查目标与子弹的距离是否在 50f 内
                float distanceToTarget = Vector3.Distance(transform.position, potentialTarget.transform.position);
                if (distanceToTarget <= 30f)
                {
                    // 如果满足条件，开始追踪目标
                    target = potentialTarget.transform;
                    isTracking = true;
                    Debug.Log("Target acquired: " + target.name);
                }
            }
        }

        /// <summary>
        /// Explodes on contact.
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            // return if not enabled because OnCollision is still called if component is disabled
            if (!enabled) return;

            // Explode when hitting an object and disable the projectile mesh
            Explode();
            projectileMesh.enabled = false;
            targetHit = true;
            inFlightAudioSource.Stop();
            foreach (Collider col in GetComponents<Collider>())
            {
                col.enabled = false;
            }
            disableOnHit.Stop();

            // Destroy this object after 2 seconds. Using a delay because the particle system needs to finish
            Destroy(gameObject, 5f);
        }

        /// <summary>
        /// Instantiates an explode object.
        /// </summary>
        private void Explode()
        {
            // Instantiate new explosion effect. Object pooling is recommended here
            GameObject newExplosion = Instantiate(rocketExplosion, transform.position, rocketExplosion.transform.rotation, null);
        }
    }
}
