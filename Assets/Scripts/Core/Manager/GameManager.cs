using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using TopDownRogueLike.Commons;

namespace TopDownRogueLike.Core.Manager
{
    /// <summary>
    /// Manages the game state and provides access to important components and data.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _coinText;

        [SerializeField]
        private GameObject _gameOverUI;

        private static GameManager _instance;
        private PlayerController _playerController;
        private int _currentCoin;

        /// <summary>
        /// Gets the player controller component.
        /// </summary>
        public PlayerController PlayerController { get => _playerController; }

        /// <summary>
        /// The instance of the GameManager.
        /// </summary>
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                    if (_instance == null)
                    {
                        Debug.LogError(ConstantVariable.GameManagerNotFoundErr);
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Gets or sets the current coin count.
        /// </summary>
        public int CurrentCoin
        {
            get => _currentCoin;
            set
            {
                _currentCoin = value;
                UpdateCoinText();
            }
        }

        private void Awake()
        {
            _playerController = GameObject.FindGameObjectWithTag(ConstantVariable.PLAYER_TAG).GetComponent<PlayerController>();
            MakeInstance();
        }

        private void MakeInstance()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        /// <summary>
        /// Updates the coin text UI element with the current coin count.
        /// </summary>
        private void UpdateCoinText()
        {
            if (_coinText != null)
            {
                _coinText.SetText(_currentCoin.ToString());
            }
        }

        public void GameOver()
        {
            _gameOverUI.SetActive(true);
        }

        public void GoToScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}

