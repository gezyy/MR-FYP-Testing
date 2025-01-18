using System.Collections;
using UnityEngine;

public class StoneController: MonoBehaviour
{
    // --- Config ---
    public GameObject stonePrefab;         // Stone 物体预制体
    public float spawnInterval = 5f;       // 生成间隔时间
    public float moveSpeed = 3f;           // Stone 向飞船移动的速度
    public Transform spaceship;            // 飞船 Transform，用于确定生成范围和移动方向

    // --- Spawn Range ---
    public Vector3 spawnOffsetMin = new Vector3(-50f, -30f, 80f);  // 生成范围的最小偏移
    public Vector3 spawnOffsetMax = new Vector3(50f, 30f, 100f);   // 生成范围的最大偏移

    // --- Internal Variables ---
    private Transform outsideEnv;          // 存放所有 Stone 物体的父物体
    private bool canSpawn = true;          // 是否可以生成 Stone

    private void Start()
    {
        // 获取场景中名为 "OutsideEnv" 的父物体
        outsideEnv = GameObject.Find("StoneSpawner").transform;
        // 如果找不到 "OutsideEnv" 对象，则输出错误信息
        if (outsideEnv == null)
        {
            Debug.LogError("OutsideEnv object not found in the scene. Please ensure the object exists.");
            return;
        }

        // 开始定时生成 Stone
        StartCoroutine(SpawnStoneCoroutine());
    }

    private void Update()
    {
        // 每帧检查并移动所有的 Stone
        foreach (Transform child in outsideEnv)
        {
            if (child.CompareTag("Stone"))
            {
                // 如果该物体是 Stone，移动它
                Stone stoneScript = child.GetComponent<Stone>();
                if (stoneScript != null)
                {
                    // 传递飞船的位置给 Stone 进行移动
                    stoneScript.MoveStone(spaceship.position);
                }
            }
        }
    }

    // 定时生成 Stone 的协程
    private IEnumerator SpawnStoneCoroutine()
    {
        while (true)
        {
            Debug.Log("Checking if more stones can be spawned...");
            // 检查场景中 Stone 的数量
            if (outsideEnv.childCount >= 5)
            {
                canSpawn = false;  // 如果有 5 个或更多 Stone，则暂停生成
                Debug.Log("Maximum number of stones (5) reached. Waiting for removal...");
            }
            else
            {
                canSpawn = true;   // 如果 Stone 数量小于 5，则恢复生成
                Debug.Log("Can spawn stones, less than 5 in the scene.");
            }

            // 如果可以生成 Stone，开始生成
            if (canSpawn)
            {
                // 随机生成 1 到 3 个 Stone
                int stoneCount = Random.Range(1, 4);
                Debug.Log($"Spawning {stoneCount} stone(s)...");
                for (int i = 0; i < stoneCount; i++)
                {
                    SpawnStone();
                }
            }

            // 等待 5 秒再生成
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // 生成一个 Stone
    private void SpawnStone()
    {
        if (spaceship == null)
        {
            Debug.LogError("Spaceship reference is missing. Please assign the spaceship in the inspector.");
            return;
        }

        // 随机生成 Stone 的位置
        Vector3 randomPos = spaceship.position + new Vector3(
            Random.Range(spawnOffsetMin.x, spawnOffsetMax.x),
            Random.Range(spawnOffsetMin.y, spawnOffsetMax.y),
            Random.Range(spawnOffsetMin.z, spawnOffsetMax.z)
        );
        Debug.Log($"Trying to spawn stone at position: {randomPos}");
        // 确保生成位置不与其他 Stone 重叠
        foreach (Transform child in outsideEnv)
        {
            if (child.CompareTag("Stone"))
            {
                if (Vector3.Distance(randomPos, child.position) < 5f)  // 假设 5f 为重叠的最小距离
                {
                    Debug.Log("Position too close to another stone, skipping spawn.");
                    return; // 如果位置与现有的 Stone 物体太近，则不生成
                }
            }
        }

        // 实例化新的 Stone，并设置父物体
        GameObject newStone = Instantiate(stonePrefab, randomPos, Quaternion.identity, outsideEnv);
        Debug.Log("Stone spawned successfully!");
        // 为新生成的 Stone 设置移动速度
        Stone stoneScript = newStone.GetComponent<Stone>();
        if (stoneScript != null)
        {
            stoneScript.moveSpeed = moveSpeed;
            // 将飞船位置传递给 Stone
            stoneScript.SetSpaceshipPosition(spaceship.position);
        }
    }
}
