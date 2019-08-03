using UnityEngine;

namespace NPC
{
    public class NPC_Manager : MonoBehaviour
    {
        public NPC_Controller NPC_ZombieGO;
        public Transform[] SpawnPoints;

        private void Start()
        {
            GenerateWave();
        }

        public void GenerateWave()
        {
            for (int i = 0; i < 10; i++)
            {
                int spawnPointID = Random.Range(0, 5);
                NPC_Controller NPC = Instantiate(NPC_ZombieGO, SpawnPoints[spawnPointID].transform.position,
                    Quaternion.identity);
                NPC.Init();
            }
        }
    }
}