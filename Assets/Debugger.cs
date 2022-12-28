using UnityEngine;
using UnchordMetroidvania;

public class Debugger : MonoBehaviour
{
    /*
    #region Test Variables
    public float moveSpeed = 3.0f;
    #endregion

    public IEntityPlayerConfigurationBT eConfig;

    public ITerrainRecognizationConfigurationBT fConfig;
    public ITerrainRecognizationConfigurationBT cConfig;
    public ITerrainRecognizationConfigurationBT wConfigB;
    public ITerrainRecognizationConfigurationBT wConfigT;
    public ITerrainRecognizationConfigurationBT lConfig;

    private FloorRecognizationTaskBT fRecogTask;
    private CeilRecognizationTaskBT cRecogTask;
    private WallRecognizationTaskBT wRecogTaskB;
    private WallRecognizationTaskBT wRecogTaskT;
    private LedgeRecognizationTaskBT lRecogTask;
    private ParallelNodeBT<ITerrainRecognizationConfigurationBT> terrainParallelRoot;

    public Transform fOrigin;
    public Transform cOrigin;
    public Transform wOriginB;
    public Transform wOriginT;
    public Transform lOrigin;

    void Start()
    {
        int terrainLayerMask = 1 << LayerMask.NameToLayer("Terrain");

        eConfig = new EntityPlayerConfigurationBT();

        fConfig = new TerrainConfigurationBT();
            fConfig.tOrigin = fOrigin;
            fConfig.dLength = 0.5f;
            fConfig.hLength = 0.04f;
            fConfig.layerMask = terrainLayerMask;
        cConfig = new TerrainConfigurationBT();
            cConfig.tOrigin = cOrigin;
            cConfig.dLength = 0.5f;
            cConfig.hLength = 0.04f;
            cConfig.layerMask = terrainLayerMask;
        wConfigB = new TerrainConfigurationBT();
            wConfigB.tOrigin = wOriginB;
            wConfigB.dLength = 0.1f;
            wConfigB.hLength = 0.02f;
            wConfigB.layerMask = terrainLayerMask;
        wConfigT = new TerrainConfigurationBT();
            wConfigT.tOrigin = wOriginT;
            wConfigT.dLength = 0.1f;
            wConfigT.hLength = 0.02f;
            wConfigT.layerMask = terrainLayerMask;
        lConfig = new TerrainConfigurationBT();
            lConfig.tOrigin = lOrigin;
            lConfig.dLength = 0.5f;
            lConfig.hLength = 0.04f;
            lConfig.height = 0.3f;
            lConfig.ledgerp = 0.04f;
            lConfig.layerMask = terrainLayerMask;

        fRecogTask = new FloorRecognizationTaskBT(fConfig, eConfig);
        cRecogTask = new CeilRecognizationTaskBT(cConfig, eConfig);
        wRecogTaskB = new WallRecognizationTaskBT(wConfigB, eConfig);
        wRecogTaskT = new WallRecognizationTaskBT(wConfigT, eConfig);
        lRecogTask = new LedgeRecognizationTaskBT(lConfig, eConfig);

        terrainParallelRoot = BehaviorTree.Parallel<ITerrainRecognizationConfigurationBT>(fConfig, 5);
        terrainParallelRoot.Set(0, fRecogTask);
        terrainParallelRoot.Set(1, cRecogTask);
        terrainParallelRoot.Set(2, wRecogTaskB);
        terrainParallelRoot.Set(3, wRecogTaskT);
        terrainParallelRoot.Set(4, lRecogTask);
    }

    void FixedUpdate()
    {
        fConfig.AddFps();
        cConfig.AddFps();
        wConfigB.AddFps();
        wConfigT.AddFps();
        lConfig.AddFps();
        eConfig.AddFps();

        fOrigin.localPosition = Vector2.down;
        cOrigin.localPosition = Vector2.up;
        wOriginB.localPosition = new Vector2(0.5f * eConfig.lookDirX, -0.7f);
        wOriginT.localPosition = new Vector2(0.5f * eConfig.lookDirX, 0.7f);
        lOrigin.localPosition = new Vector2(0.5f * eConfig.lookDirX, 1.0f);

        terrainParallelRoot.Invoke();

        transform.position += new Vector3(eConfig.xInput, eConfig.yInput, 0.0f) * moveSpeed * Time.fixedDeltaTime;
    }

    void Update()
    {
        eConfig.xNegative = Input.GetKey(KeyCode.LeftArrow) ? 1.0f : 0.0f;
        eConfig.xPositive = Input.GetKey(KeyCode.RightArrow) ? 1.0f : 0.0f;
        eConfig.yNegative = Input.GetKey(KeyCode.DownArrow) ? 1.0f : 0.0f;
        eConfig.yPositive = Input.GetKey(KeyCode.UpArrow) ? 1.0f : 0.0f;

        eConfig.UpdateLookDir();
    }*/
}
