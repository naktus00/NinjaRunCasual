using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelData _currentLevelData;
    [HideInInspector] public LevelData currentLevelData { get { return _currentLevelData; } }

    [SerializeField] private Transform _parent;
    
    LevelDataContainer _levelDataContainer;
    LevelData.LevelPartData _currentLevelPartData;

    int _levelIndex;
    float _f0;

    public void LoadLevel(int level)
    {
        if(_levelDataContainer == null)
            _levelDataContainer = LevelDataContainer.Instance;

        int levelsLength = _levelDataContainer.levelData.Length;

        _levelIndex = level - 1;
        _levelIndex = _levelIndex % levelsLength; 

        _f0 = 0f;

        _currentLevelData = _levelDataContainer.levelData[_levelIndex];

        int length = _currentLevelData.levelParts.Length;

        LevelPartsPrefabContainer levelPartPrefabs = LevelPartsPrefabContainer.Instance;

        for (int i = 0; i < length; i++)
        {
            _currentLevelPartData = _currentLevelData.levelParts[i];
            LoadPrefabs(levelPartPrefabs);
        }
    }
    private void LoadPrefabs(LevelPartsPrefabContainer levelPartPrefabs)
    {
        Vector3 position = Vector3.zero;
        float posZ = 0f;
        
        switch (_currentLevelPartData.type)
        {
            case LevelPart.LevelPartType.Empty:

                posZ = _f0 + LevelPartEmpty.GetR(_currentLevelPartData.sample);
                position = new Vector3(0f, 0f, posZ);
                _f0 = posZ + LevelPartEmpty.GetR(_currentLevelPartData.sample);

                GameObject emptyObj = Instantiate(levelPartPrefabs.LevelPartEmptyPrefabs[_currentLevelPartData.sample], position, Quaternion.identity, _parent);

                LevelPartEmpty empty = emptyObj.GetComponent<LevelPartEmpty>();
                empty.position = position;

                SetPlatformAxe(empty.axeSlotsParent, _currentLevelPartData.axeSlotIndex, _currentLevelPartData.hasAxe);
                break;

            case LevelPart.LevelPartType.Straight:

                posZ = _f0 + LevelPartStraight.r;
                position = new Vector3(0f, 0f, posZ);
                _f0 = posZ + LevelPartStraight.r;

                GameObject straightObj = Instantiate(levelPartPrefabs.LevelPartStraightPrefabs[_currentLevelPartData.sample], position, Quaternion.identity, _parent);

                LevelPartStraight straight = straightObj.GetComponent<LevelPartStraight>();
                straight.position = position;

                SetPlatformAxe(straight.axeSlotsParent, _currentLevelPartData.axeSlotIndex, _currentLevelPartData.hasAxe);
                SetEnemies(straight.enemiesParent);
                break;

            case LevelPart.LevelPartType.Zigzag:

                posZ = _f0 + LevelPartZigzag.r;
                position = new Vector3(0f, 0f, posZ);
                _f0 = posZ + LevelPartZigzag.r;

                GameObject zigzagObj = Instantiate(levelPartPrefabs.LevelPartZigzagPrefabs[_currentLevelPartData.sample], position, Quaternion.identity, _parent);

                LevelPartZigzag zigzag = zigzagObj.GetComponent<LevelPartZigzag>();
                zigzag.position = position;

                SetPlatformAxe(zigzag.axeSlotsParent, _currentLevelPartData.axeSlotIndex, _currentLevelPartData.hasAxe);
                SetEnemies(zigzag.enemiesParent);
                break;

            case LevelPart.LevelPartType.InLine:

                posZ = _f0 + LevelPartInLine.r;
                position = new Vector3(0f, 0f, posZ);
                _f0 = posZ + LevelPartInLine.r;

                GameObject inLineObj = Instantiate(levelPartPrefabs.LevelPartInLinePrefabs[_currentLevelPartData.sample], position, Quaternion.identity, _parent);

                LevelPartInLine inLine = inLineObj.GetComponent<LevelPartInLine>();
                inLine.position = position;

                SetPlatformAxe(inLine.axeSlotsParent, _currentLevelPartData.axeSlotIndex, _currentLevelPartData.hasAxe);
                SetEnemies(inLine.enemiesParent);
                break;

            case LevelPart.LevelPartType.Hammer:

                posZ = _f0 + LevelPartHammer.r;
                position = new Vector3(0f, 0f, posZ);
                _f0 = posZ + LevelPartHammer.r;

                GameObject hammerObj = Instantiate(levelPartPrefabs.LevelPartHammerPrefabs[_currentLevelPartData.sample], position, Quaternion.identity, _parent);

                LevelPartHammer hammer = hammerObj.GetComponent<LevelPartHammer>();
                hammer.position = position;

                SetPlatformAxe(hammer.axeSlotsParent, _currentLevelPartData.axeSlotIndex, _currentLevelPartData.hasAxe);
                SetEnemies(hammer.enemiesParent);
                break;

            case LevelPart.LevelPartType.Jump:

                posZ = _f0 + LevelPartJump.r;
                position = new Vector3(0f, 0f, posZ);
                _f0 = posZ + LevelPartJump.r;

                GameObject jumpObj = Instantiate(levelPartPrefabs.LevelPartJumpPrefabs[_currentLevelPartData.sample], position, Quaternion.identity, _parent);

                LevelPartJump jump = jumpObj.GetComponent<LevelPartJump>();
                jump.position = position;

                SetPlatformAxe(jump.axeSlotsParent, _currentLevelPartData.axeSlotIndex, _currentLevelPartData.hasAxe);
                break;

            case LevelPart.LevelPartType.Boss:

                posZ = _f0 + LevelPartBoss.r;
                position = new Vector3(0f, 0f, posZ);
                _f0 = posZ + LevelPartBoss.r;

                GameObject bossObj = Instantiate(levelPartPrefabs.LevelPartBossPrefabs, position, Quaternion.identity, _parent);

                EnemyBoss enemyBoss = bossObj.GetComponentInChildren<EnemyBoss>();
                enemyBoss.health = (float)_currentLevelData.totalCollactableAxeNumber * GameManager.Instance.gameData.axeDamage;

                LevelPartBoss boss = bossObj.GetComponent<LevelPartBoss>();
                boss.position = position;
                break;

            //case LevelPart.LevelPartType.FakePlayer:
            //    posZ = _f0 + LevelPartFakePlayer.r;
            //    position = new Vector3(0f, 0f, posZ);
            //    _f0 = posZ + LevelPartBoss.r;

            //    GameObject obj = Instantiate(levelPartPrefabs.levelPartFakePlayer, position, Quaternion.identity, _parent);

            //    LevelPartFakePlayer fakePlayerPart = obj.GetComponent<LevelPartFakePlayer>();
            //    fakePlayerPart.position = position;

            //    CameraManager.Instance.fakePlayerWinCamLocTr = fakePlayerPart.fakePlayerWinCamLocTr;
            //    CameraManager.Instance.playerWinCamLocTr = fakePlayerPart.playerWinCamLocTr;
            //    CameraManager.Instance.fightCamLocTr = fakePlayerPart.fightCamLocTr;

            //    GameManager.Instance.playerFightLocTr = fakePlayerPart.playerLocTr;

            //    EnemyFakePlayer enemyPlayer = obj.GetComponentInChildren<EnemyFakePlayer>();
            //    GameManager.Instance.enemyPlayer = enemyPlayer;

            //    break;
        }
    }
    private void SetEnemies(Transform enemiesParent)
    {
        var enemies = enemiesParent.GetComponentsInChildren<EnemyStandard>();

        int length = enemies.Length;

        int playerLevel = DataManager.Instance.user.playerLevel;

        for (int i = 0; i < length; i++)
        {
            int enemyLevel = playerLevel + _currentLevelPartData.enemiesLevels[i];
            enemies[i].Initialize(enemyLevel);
        }

        Action action = delegate { NinjaLevelUI.CheckAllEnemyUI(); };
        StartCoroutine(AddtionalRequirement.WaitEndofFrameAndDo(action));
    }
    private void SetPlatformAxe(Transform axeSlotsParent, int axeSlotIndex, bool hasAxe)
    {
        if (hasAxe == false)
            return;

        Transform targetSlot = axeSlotsParent.GetChild(axeSlotIndex);
        Instantiate(_levelDataContainer.axePrefab, targetSlot.position, Quaternion.identity, targetSlot);
    }

}
