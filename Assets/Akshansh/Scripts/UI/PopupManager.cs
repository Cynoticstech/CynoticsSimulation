using AkshanshKanojia.Animations;
using TMPro;
using UnityEngine;
using System.Collections;

namespace Simulations.UI
{
    public class PopupManager : MonoBehaviour
    {
        #region Public Fields
        //expand popup variations here
        public enum PopupTypes { CenterFill,SubmitPopup,IncorrectPopup}
        #endregion


        #region Serialized Properties
        //Holds all the reference for different types of popups
        [System.Serializable]
        class PopupHolder
        {
            public GameObject PopupObj;
            public TMP_Text PopupText, PopupHeaderText;
            public TransformSequencer TransSeq;
            public PopupTypes PopupType;
        }

        [SerializeField] PopupHolder[] AvailablePopups;
        #endregion

        #region Private Fields
        PopupHolder activePopup;
        #endregion

        #region Inbuilt methods

        #endregion

        #region Private Methods

        /// <summary>
        /// Overrides to set text inside a popup in desired format
        /// </summary>
        /// <param name="_txt"></param>
        void SetPopupContent(string _txt)
        {
            CheckActivePopup();
            var _tempPopup = activePopup;
            _tempPopup.PopupText.text = _txt;
        }
        void SetPopupContent(string _head, string _txt)
        {
            CheckActivePopup();
            var _tempPopup = activePopup;
            if (_tempPopup.PopupHeaderText != null)
            {
                _tempPopup.PopupHeaderText.text = _head;
                _tempPopup.PopupText.text = _txt;
            }
            else//if no header txt assigned merge header into body by using header as first value.
            {
                _tempPopup.PopupText.text = _head + "\n" + _txt;
            }
        }

        /// <summary>
        /// Checks if a default value is assigned to active popup
        /// </summary>
        private void CheckActivePopup()
        {
            //check for active popup
            if (activePopup == null)
            {
                Debug.LogWarning("Active popup is not specified, use SetActivePopup() " +
                    "to set it manually. Using first popup as default!");
                activePopup = AvailablePopups[0];
            }
        }

        /// <summary>
        /// use to disable popups(gameobject) after a certain delay
        /// </summary>
        /// <param name="_delay"></param>
        /// <param name="_popup"></param>
        /// <returns>_dealy in seconds</returns>
        IEnumerator DisablePopup(float _delay,PopupHolder _popup)
        {
            yield return new WaitForSeconds(_delay);
            _popup.PopupObj.SetActive(false);
            var _temp = _popup.PopupObj.transform;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Displays a popup with custom body text
        /// </summary>
        /// <param name="_text"></param>
        public GameObject ShowPopup(string _text)
        {
            var _tempPopup = activePopup;
            SetPopupContent(_text);
            _tempPopup.PopupObj.SetActive(true);
            return _tempPopup.PopupObj;

        }

        /// <summary>
        /// displays popup at a specified position
        /// </summary>
        /// <param name="_text"></param>
        /// <param name="_pos"></param>
        public GameObject ShowPopup(string _text,Vector3 _pos)
        {
            var _tempPopup = activePopup;
            SetPopupContent(_text);
            _tempPopup.PopupObj.transform.position = _pos;
            _tempPopup.PopupObj.SetActive(true);
            return _tempPopup.PopupObj;

        }
        //animation override
        public GameObject ShowPopup(string _text,bool _useAnim)
        {
            var _tempPopup = activePopup;
            SetPopupContent(_text);
            _tempPopup.PopupObj.SetActive(true);
            if(_useAnim)
            {
                _tempPopup.TransSeq.Play();
            }
            return _tempPopup.PopupObj;

        }
        //disable overrides
        public GameObject ShowPopup(string _text,float _duration)
        {
            var _tempPopup = activePopup;
            SetPopupContent(_text);
            _tempPopup.PopupObj.SetActive(true);
            StartCoroutine(DisablePopup(_duration, _tempPopup));
            return _tempPopup.PopupObj;

        }
        public GameObject ShowPopup(string _text, Vector3 _pos,float _duration)
        {
            var _tempPopup = activePopup;
            SetPopupContent(_text);
            _tempPopup.PopupObj.transform.position = _pos;
            _tempPopup.PopupObj.SetActive(true);
            StartCoroutine(DisablePopup(_duration, _tempPopup));
            return _tempPopup.PopupObj;

        }
        public GameObject ShowPopup(string _text, bool _useAnim,float _duration)
        {
            var _tempPopup = activePopup;
            SetPopupContent(_text);
            _tempPopup.PopupObj.SetActive(true);
            if (_useAnim)
            {
                _tempPopup.TransSeq.Play();
            }
            StartCoroutine(DisablePopup(_duration, _tempPopup));
            return _tempPopup.PopupObj;

        }
        //header overrides
        public GameObject ShowPopup(string _head,string _text)
        {
            var _tempPopup = activePopup;
            SetPopupContent(_head,_text);
            _tempPopup.PopupObj.SetActive(true);
            return _tempPopup.PopupObj;

        }
        public GameObject ShowPopup(string _head, string _text,Vector3 _pos)
        {
            var _tempPopup = activePopup;
            SetPopupContent(_head, _text);
            _tempPopup.PopupObj.transform.position = _pos;
            _tempPopup.PopupObj.SetActive(true);
            return _tempPopup.PopupObj;

        }

        public GameObject ShowPopup(string _head, string _text,bool _useAnim)
        {
            var _tempPopup = activePopup;
            SetPopupContent(_head, _text);
            _tempPopup.PopupObj.SetActive(true);
            if (_useAnim)
            {
                _tempPopup.TransSeq.Play();
            }
            return _tempPopup.PopupObj;

        }
        public GameObject ShowPopup(string _head, string _text,float _duration)
        {
            var _tempPopup = activePopup;
            SetPopupContent(_head, _text);
            _tempPopup.PopupObj.SetActive(true);
            StartCoroutine(DisablePopup(_duration, _tempPopup));
            return _tempPopup.PopupObj;

        }
        public GameObject ShowPopup(string _head, string _text, Vector3 _pos,float _duration)
        {
            var _tempPopup = activePopup;
            SetPopupContent(_head, _text);
            _tempPopup.PopupObj.transform.position = _pos;
            _tempPopup.PopupObj.SetActive(true);
            StartCoroutine(DisablePopup(_duration, _tempPopup));
            return _tempPopup.PopupObj;

        }

        public GameObject ShowPopup(string _head, string _text, bool _useAnim,float _duration)
        {
            var _tempPopup = activePopup;
            SetPopupContent(_head, _text);
            _tempPopup.PopupObj.SetActive(true);
            if (_useAnim)
            {
                _tempPopup.TransSeq.Play();
            }
            StartCoroutine(DisablePopup(_duration, _tempPopup));
            return _tempPopup.PopupObj;
        }
        //POPUP OVERRIDES END//

        /// <summary>
        /// sets current active popup to a specific type.
        /// </summary>
        /// <param name="_type"></param>
        public void SetActivePopup(PopupTypes _type)
        {
            foreach(var v in AvailablePopups)
            {
                if(v.PopupType == _type)
                {
                    activePopup = v;
                    return;
                }
            }
            Debug.LogWarning("No popup type available with value +" +
                " "+_type.ToString());
        }
        #endregion
    }
}
