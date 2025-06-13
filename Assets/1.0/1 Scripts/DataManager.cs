using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;
    public static DataManager Instance { get { return _instance; } }

    [SerializeField] private User _user = null;
    [HideInInspector] public User user { get { return _user; } }

#if UNITY_EDITOR
    // There is a "Serialization" bug in displaying the class "User".
    [Space(10)]
    [Header("USER VALUES")]
    [SerializeField] private bool _showUserValues;

    [Space(5)]
    [SerializeField] private int _playerLevel;
    [SerializeField] private int _nextLevel;

    [Space(3)]
    [SerializeField] private int _coin;

    [Space(5)]
    [SerializeField] private int _selectedWeaponIndex;
    [SerializeField] private int _selectedWeaponColorIndex;
    [SerializeField] private int _selectedSkinIndex;

    [Space(3)]
    [SerializeField] private bool[] _purchasedWeapons;
    [SerializeField] private bool[][] _purchasedWeaponColors;
    [SerializeField] private bool[] _purchasedSkins;   
#endif

    private void Awake()
    {
        #region Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        _instance = this;
        #endregion
    }
    private void Start()
    {
        _user = new User();
    }
    private void Update()
    {
#if UNITY_EDITOR
        if (_showUserValues)
        {
            if (_user != null)
            {
                _nextLevel = user.nextLevel;
                _playerLevel = user.playerLevel;

                _coin = _user.coin;

                _selectedWeaponIndex = _user.selectedWeaponIndex;
                _selectedWeaponColorIndex = _user.selectedWeaponColorIndex;
                _selectedSkinIndex = _user.selectedSkinIndex;

                _purchasedWeapons = _user.purchasedWeapons;
                _purchasedWeaponColors = _user.purchasedWeaponColors;
                _purchasedSkins = _user.purchasedSkins;
            }
        }
#endif

    }
    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }

}

//[Serializable]
public class User
{
    public User()
    {
        playerLevel = 1;
        nextLevel = 1;

        selectedWeaponIndex = 0;
        selectedWeaponColorIndex = 0;
        selectedSkinIndex = 0;

        var weaponInfoContainer = WeaponsInfoContainer.Instance;
        var skinInfoContainer = SkinInfoContainer.Instance;

        int weaponsNumber = weaponInfoContainer.weapons.Length;

        purchasedWeapons = new bool[weaponsNumber];
        purchasedWeapons[0] = true;

        purchasedWeaponColors = new bool[weaponsNumber][];

        int skinNumber = skinInfoContainer.skins.Length;

        purchasedSkins = new bool[skinNumber];
        purchasedSkins[0] = true;

        for (int i = 0; i < purchasedWeaponColors.Length; i++)
        {
            int colorsNumber = weaponInfoContainer.weapons[i].colorMaterials.Length;
            purchasedWeaponColors[i] = new bool[colorsNumber];
        }

        purchasedWeaponColors[0][0] = true;

        coin = 250;
    }

    //[Header("Player")]
    //public string nickname;
    //public int avatarIndex;

    [Header("Game")]
    public int playerLevel;
    public int nextLevel;

    [Space(10)]
    [Header("Currencies")]
    public int coin;

    [Space(10)]
    [Header("Custom")]
    public int selectedWeaponIndex;
    public int selectedWeaponColorIndex;
    public int selectedSkinIndex;

    public bool[] purchasedWeapons;
    public bool[][] purchasedWeaponColors;
    public bool[] purchasedSkins;

}
