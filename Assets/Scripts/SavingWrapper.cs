using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.Period))
            {
                Save();
            }
        }

        private void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        private void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
    }
}