using System.Collections;
using TMPro;
using UnityEngine;

namespace StylizedMultiplayer
{
    public class ErrorText : MonoBehaviour
    {
        private static ErrorText instance;

        public static ErrorText Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<ErrorText>();
                    if (instance == null)
                    {
                        var obj = new GameObject("NetworkManager");
                        obj.AddComponent<ErrorText>();
                        instance = obj.GetComponent<ErrorText>();

                    }
                }
                return instance;
            }
        }

        [SerializeField] private TMP_Text _errorText;
        [SerializeField] private float _duration;

        public void  DisplayText(string error)
        {
            _errorText.text = error;
            Invoke("ResetText",_duration);
        }

        private void ResetText()
        {
            _errorText.text = "";
        }
    }
}