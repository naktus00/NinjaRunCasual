using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMarket : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

    [Space(10)]
    [SerializeField] private Transform _swordsParent;

    [Space(10)]
    [SerializeField] private Transform _rightHandSlot;
    [SerializeField] private Transform _leftHandSlot;

    [Space(10)]
    [SerializeField] private GameObject[][] _weaponsObjs;

    private WeaponsInfoContainer _weaponsInfoContainer;
    private SkinInfoContainer _skinInfoContainer;
    private GameObject[] _selectedSwords;

    private int _weaponIndex;
    private int _weaponColorIndex;
    private int _skinIndex;

    public void Initialize(int weaponIndex, int weaponColorIndex, int skinIndex)
    {
        if (_weaponsInfoContainer == null)
            _weaponsInfoContainer = WeaponsInfoContainer.Instance;

        if (_skinInfoContainer == null)
            _skinInfoContainer = SkinInfoContainer.Instance;

        _weaponIndex = weaponIndex;
        _weaponColorIndex = weaponColorIndex;
        _skinIndex = skinIndex;

        if (_weaponsObjs == null)
        {
            var weaponNumber = _weaponsInfoContainer.weapons.Length;

            _weaponsObjs = new GameObject[weaponNumber][];

            for (int i = 0; i < _weaponsObjs.Length; i++)
            {
                _weaponsObjs[i] = new GameObject[2];

                for (int j = 0; j < _weaponsObjs[i].Length; j++)
                {
                    GameObject obj = Instantiate(_weaponsInfoContainer.weapons[i].prefab, _swordsParent);
                    _weaponsObjs[i][j] = obj;
                }
            }
        }

        ChangeWeapons(weaponIndex);
        ChangeWeaponsMaterial(weaponColorIndex);
        ChangeSkin(skinIndex);

    }

    public void ChangeWeapons(int weaponIndex)
    {
        for (int i = 0; i < _weaponsObjs.Length; i++)
        {
            for (int j = 0; j < _weaponsObjs[i].Length; j++)
            {
                _weaponsObjs[i][j].gameObject.SetActive(false);
                _weaponsObjs[i][j].transform.parent = _swordsParent;
            }
                
        }

        _weaponIndex = weaponIndex;

        _selectedSwords = _weaponsObjs[weaponIndex];

        _selectedSwords[0].gameObject.SetActive(true);
        _selectedSwords[0].transform.parent = _rightHandSlot;
        _selectedSwords[0].transform.localPosition = Vector3.zero;
        _selectedSwords[0].transform.localEulerAngles = Vector3.zero;

        _selectedSwords[1].gameObject.SetActive(true);
        _selectedSwords[1].transform.parent = _leftHandSlot;
        _selectedSwords[1].transform.localPosition = Vector3.zero;
        _selectedSwords[1].transform.localEulerAngles = Vector3.zero;

    }
    public void ChangeWeaponsMaterial(int matIndex)
    {
        _weaponColorIndex = matIndex;

        var material = _weaponsInfoContainer.weapons[_weaponIndex].colorMaterials[matIndex];

        for (int i = 0; i < _selectedSwords.Length; i++)
        {
            MeshRenderer renderer = _selectedSwords[i].GetComponent<MeshRenderer>();
            var materials = _selectedSwords[i].GetComponent<MeshRenderer>().materials;
            materials[0] = material;
            renderer.materials = materials;
        }
    }
    public void ChangeSkin(int skinIndex)
    {
        _skinIndex = skinIndex;

        Vector2 tilingValues = _skinInfoContainer.skins[skinIndex].tilingValues;
        Vector2 offsetValues = _skinInfoContainer.skins[skinIndex].offsetValues;

        var materials = _skinnedMeshRenderer.materials;

        var mat = _skinnedMeshRenderer.materials[0];
        mat.mainTextureScale = tilingValues;
        mat.mainTextureOffset = offsetValues;

        materials[0] = mat;

        _skinnedMeshRenderer.materials = materials;
    }
    public void PlayAnimation(int animIndex)
    {
        switch (animIndex)
        {
            case 0:
                _animator.SetTrigger("ReturnIdle");
                break;
            case 1:
                _animator.SetTrigger("BuySword");
                break;
            case 2:
                _animator.SetTrigger("BuySwordColor");
                break;
            case 3:
                _animator.SetTrigger("BuySkin");
                break;
        }
    }
}
