using System;
using UnityEngine;

namespace DamageSystem.Weapons
{
    public class WeaponSelect : MonoBehaviour
    {
        [Serializable]
        public struct WeaponsStatus
        {
            public string[] names;
            public bool[] unlocked;

            public void Unlock(string name)
            {
                for (var i = 0; i < names.Length; i++)
                {
                    if (names[i] == name)
                    {
                        unlocked[i] = true;
                    }
                }
                UnlockedWeapons = this;
            }

            public bool IsUnlocked(string name)
            {
                for (var i = 0; i < names.Length; i++)
                {
                    if (names[i] == name)
                    {
                        return unlocked[i];
                    }
                }

                return false;
            }
        }

        private static WeaponsStatus _weapons;
        public static WeaponsStatus UnlockedWeapons
        {
            get
            {
                var data = PlayerPrefs.GetString("unlockedWeapons", "");
                return data == "" ? new WeaponsStatus() : JsonUtility.FromJson<WeaponsStatus>(data);
            }
            set
            {
                var data = JsonUtility.ToJson(value);
                PlayerPrefs.SetString("unlockedWeapons", data);
            }
        }
        public GameObject[] weapons;
        public Transform weaponSlot;
        public GameObject defaultweapon;
        private int _selectedIndex;
        private void Start()
        {
            SelectWeapon(0);
            _weapons = UnlockedWeapons;
            if (_weapons.unlocked == null)
            {
                _weapons.unlocked = new bool[weapons.Length];
                _weapons.names = new string[weapons.Length];
                for (int i = 0; i < weapons.Length; i++)
                {
                    _weapons.unlocked[i] = false;
                    _weapons.names[i] = weapons[i].name;
                }
            }
            _weapons.Unlock(defaultweapon.name);
        }

        private void Update()
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                Select(1);
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                Select(-1);
            }
        }

        private void Select(int dir)
        {
            _selectedIndex += dir;
            if (_selectedIndex < 0)
                _selectedIndex = weapons.Length - 1;
            if (_selectedIndex >= weapons.Length)
                _selectedIndex = 0;
            while (!UnlockedWeapons.unlocked[_selectedIndex])
            {
                Select(dir);            
            }
            SelectWeapon(_selectedIndex);
        }
        private void SelectWeapon(int index)
        {
            _selectedIndex = index;
            for (int i = 0; i < weaponSlot.childCount; i++)
            {
                Destroy(weaponSlot.GetChild(i).gameObject);
            }
            var weapon = Instantiate(weapons[index], weaponSlot).GetComponent<Weapon>();
            GetComponent<PlayerHealth>().weapon = weapon;
            weapon.owner = GetComponent<PlayerHealth>();
        }
    }
}