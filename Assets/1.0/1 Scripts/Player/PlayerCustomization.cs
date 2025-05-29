using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomization : MonoBehaviour
{
    private void Start()
    {
        SetPlayer();
    }

    private void SetPlayer()
    {
        var user = DataManager.Instance.user;

        int weaponIndex = user.selectedWeaponIndex;
        int weaponColorIndex = user.selectedWeaponColorIndex;
        int skinIndex = user.selectedSkinIndex;

        var weaponsInfoContainer = WeaponsInfoContainer.Instance;
        var skinInfoContainer = SkinInfoContainer.Instance;

        var playerComponents = this.gameObject.GetComponent<PlayerComponents>(); ;

        var swordObj01 = Instantiate(weaponsInfoContainer.weapons[weaponIndex].prefab, playerComponents.leftHandWeaponSlot);
        var swordObj02 = Instantiate(weaponsInfoContainer.weapons[weaponIndex].prefab, playerComponents.rightHandWeaponSlot);

        swordObj01.transform.localPosition = Vector3.zero;
        swordObj01.transform.localRotation = Quaternion.identity;

        swordObj02.transform.localPosition = Vector3.zero;
        swordObj02.transform.localRotation = Quaternion.identity;

        playerComponents.weapons = new GameObject[2];
        playerComponents.weapons[0] = swordObj01;
        playerComponents.weapons[1] = swordObj02;

        var material = weaponsInfoContainer.weapons[weaponIndex].colorMaterials[weaponColorIndex];

        var meshRenderer01 = swordObj01.GetComponent<MeshRenderer>();
        var materials01 = meshRenderer01.materials;
        materials01[0] = material;
        meshRenderer01.materials = materials01;

        var meshRenderer02 = swordObj02.GetComponent<MeshRenderer>();
        var materials02 = meshRenderer02.materials;
        materials02[0] = material;
        meshRenderer02.materials = materials02;

        Vector2 tilingValues = skinInfoContainer.skins[skinIndex].tilingValues;
        Vector2 offsetValues = skinInfoContainer.skins[skinIndex].offsetValues;

        var materials03 = playerComponents.skinnedMeshRenderer.materials;

        var skinMaterial = playerComponents.skinnedMeshRenderer.materials[0];
        skinMaterial.mainTextureScale = tilingValues;
        skinMaterial.mainTextureOffset = offsetValues;

        materials03[0] = skinMaterial;

        playerComponents.skinnedMeshRenderer.materials = materials03;
    }
}
