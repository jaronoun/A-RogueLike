using Character.PlayerStateMachine;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DebugSettings : MonoBehaviour
    {
        [Header("Player State Manager")]
        [SerializeField] private PlayerStateManager playerStateManager;
        [SerializeField] private TextMeshProUGUI stateText;
        private bool _debugState;
    
        
        void Start()
        {
            stateText.gameObject.SetActive(false);
            stateText.text = "State: Loading...";
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                ToggleDebugState();
            }
            stateText.text = $"State: {playerStateManager.GetCurrentState()}";
        }

        private void ToggleDebugState()
        {
            _debugState = !_debugState;
            stateText.gameObject.SetActive(_debugState);
        }
    }
}
