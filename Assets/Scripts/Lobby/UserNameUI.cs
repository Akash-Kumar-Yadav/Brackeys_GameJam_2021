using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StylizedMultiplayer
{
    public class UserNameUI : Menu
    {
        [SerializeField] private TMP_InputField _userNameInputField;
        [SerializeField] private Button _submitButton;

        private void Start()
        {
            _submitButton.onClick.AddListener(Submit);
        }

        private void Submit()
        {
            if (String.IsNullOrEmpty(_userNameInputField.text)) 
            {
                ErrorText.Instance.DisplayText("User Name Cannot be Empty");
                return;
            }

            NetworkManager.Instance.ConnectToMaster(_userNameInputField.text);
            
        }
    }
}