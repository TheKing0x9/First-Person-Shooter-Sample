using UnityEngine;
using System.Collections;

using FPS.Utilities;

namespace FPS.Managers
{
    public class SpawnManager : MonoBehaviour, IVoidEventListener
    {
        [SerializeField] private GameObject enemy;
        [SerializeField] private float minSpawnTime = 2f;
        [SerializeField] private float maxSpawnTime = 3f;

        [SerializeField] private VoidEvent gameOverEvent;
        [SerializeField] private VoidEvent gameStartEvent;
        [SerializeField] private Transform spawnParent;

        [SerializeField, ReadOnly] private bool spawn = true;
        [SerializeField] private Transform[] spawnPoints;

        private void Awake()
        {
            gameOverEvent.Register(this);
            gameStartEvent.Register(this);

        }

        private void OnDestroy() 
        {
            gameOverEvent.Unregister(this);
            gameStartEvent.Unregister(this);
        }

        public void OnEventRaised(VoidEvent e)
        {
            if (e == gameOverEvent) spawn = false;
            else StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            while(true)
            {
                if (!spawn) break;

                SpawnEnemy();
                yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            }
        }

        private void SpawnEnemy()
        {
            int index = Random.Range(0, spawnPoints.Length);
            Instantiate(enemy, spawnPoints[index].position, spawnPoints[index].rotation, spawnParent);
        }
    }   
}