using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GameHub.Core;
using GameHub.Games.TicTacToe2D.Event;
using Zenject;
using TMPro;

namespace GameHub.Games.TicTacToe2D.UI
{
    /// <summary>
    /// Class <c>GameBoardPanel</c> represents the panel where the game board grid
    /// is rendered to.
    /// </summary>
    public class GameBoardPanel : MonoBehaviour, IDisposable
    {
        /// <summary>
        /// Instance variable <c>_gameManager</c> is used to control all game moves and 
        /// game state.
        /// </summary>
        private IGameManager _gameManager;

        /// <summary>
        /// Instance variable <c>_eventBus</c> is used to manage all game events.
        /// </summary>
        private IEventBus _eventBus;

        /// <summary>
        /// Instance variable <c>_playerSettingsService</c> is used to retrieve and persist 
        /// player game settings across game sessions.
        /// </summary>
        private IPlayerSettingsService _playerSettingsService;

        /// <summary>
        /// Instance variable <c>_sceneLoader</c> is used to load scenes.
        /// </summary>
        private ISceneLoader _sceneLoader;

        /// <summary>
        /// Instance variable <c>_boardClickEnabled</c> is a flag that determines when 
        /// board clicks are permitted or blocked. This can be used to block the player
        /// from clicking on the board when its not their turn or to block clicking at
        /// the end of a game.
        /// </summary>
        private bool _boardClickEnabled;

        /// <summary>
        /// Instance variable <c>_boardId</c> is the most recent board iteration. Each
        /// time a board is generated we increment the board id. This ensures that a user
        /// cannot click on any game objects that are still in memory from the previous
        /// game.
        /// </summary>
        private int _boardId;

        /// <summary>
        /// Instance variable <c>_newSeriesModal</c> represents the modal used to
        /// start a new game series.
        /// </summary>
        [SerializeField]
        private PlayAgainModal _playAgainModal;

        /// <summary>
        /// Instance variable <c>_gameObjectMaterial</c> is the material associated with
        /// the player icons which represent x's and o's.
        /// </summary>
        [SerializeField]
        public Material _gameObjectMaterial;

        /// <summary>
        /// Instance variable <c>_audioWinSourc</c> is an audio resource that we play when
        /// a user wins a game. 
        /// </summary>
        [SerializeField]
        private AudioSource _audioWinSource;

        /// <summary>
        /// Instance variable <c>_circleSprite</c> is the circle token for the defensive
        /// player.
        /// </summary>
        [SerializeField]
        private Sprite _circleSprite;

        /// <summary>
        /// Instance variable <c>_crossSprite</c> is the cross token for the offsenvie
        /// player.
        /// </summary>
        [SerializeField]
        private Sprite _crossSprite;

        /// <summary>
        /// Instance variable <c>_message</c> is used to display a game ending message.
        /// </summary>
        [SerializeField]
        private TMP_Text _message;

        /// <summary>
        /// Method <c>Setup</c> is responsible for wiring up depedencies on object creation.
        /// </summary>
        /// <param name="gameManager">
        /// <c>gameManager</c> is used to control all game moves and game state.
        /// </param>
        /// <param name="eventBus">
        /// <c>eventBus</c> is used to manage game event subscription and notification.
        /// </param>
        /// <param name="playerSettingsService">
        /// <c>playerSettingsService</c> is used to retrieve and persist player game settings.
        /// </param>
        /// <param name="playerSettingsService">
        /// <c>playerSettingsService</c> is used to load scenes.
        /// </param>
        [Inject]
        public void Setup(
            IGameManager gameManager,
            IEventBus eventBus,
            IPlayerSettingsService playerSettingsService, 
            ISceneLoader sceneLoader
        )
        {
            _gameManager = gameManager;
            _eventBus = eventBus;
            _playerSettingsService = playerSettingsService;
            _sceneLoader = sceneLoader;

            _eventBus.NewSeriesEvents.AddListener(OnNewGame);
            _eventBus.NewGameEvents.AddListener(OnNewGame);
            _eventBus.TieGameEvents.AddListener(OnTieGame);
            _eventBus.PlayerClaimEvents.AddListener(OnPlayerClaim);
            _eventBus.PlayerWinEvents.AddListener(OnPlayerWin);
        }

        /// <summary>
        /// Method <c>Dispose</c> is called when the class is destroyed and we need to 
        /// clean up dependencies.
        /// </summary>
        public void Dispose()
        {
            _eventBus.NewSeriesEvents.RemoveListener(OnNewGame);
            _eventBus.NewGameEvents.RemoveListener(OnNewGame);
            _eventBus.TieGameEvents.RemoveListener(OnTieGame);
            _eventBus.PlayerClaimEvents.RemoveListener(OnPlayerClaim);
            _eventBus.PlayerWinEvents.RemoveListener(OnPlayerWin);
        }

        /// <summary>
        /// Method <c>OnTieGame</c> is a listener that is executed when the game manager
        /// determines that a tie game occurred.
        /// </summary>
        /// <param name="eventType">
        /// <c>eventType</c> is a generic event type with no information.
        /// </param>
        private void OnTieGame(GameEvent eventType)
        {
            StartCoroutine(ShowTie());
        }

        /// <summary>
        /// Method <c>ShowTie</c> is called when a tie occurs. Displays tie game message 
        /// and then displays the play again modal.
        /// </summary>
        /// <returns></returns>
        private IEnumerator ShowTie()
        {
            _boardClickEnabled = false;

            if (_audioWinSource)
            {
                _audioWinSource.Play();
            }

            StartCoroutine(ShowMessage("Tie Game!"));

            yield return new WaitForSeconds(2);

            _boardClickEnabled = true;

            CheckPlayAgain();
        }

        /// <summary>
        /// Method <c>ShowMessage</c> is used to display a message.
        /// </summary>
        /// <returns></returns>
        private IEnumerator ShowMessage(string message)
        {
            _message.text = message;
            _message.gameObject.SetActive(true);
            int index = 0;
            float color = .25f;

            while (color < 1)
            {
                _message.color = new Color(color, color, color);
                index++;
                color = color + (index * .1f);
                yield return new WaitForSeconds(.05f);
            }
        }

        /// <summary>
        /// Method <c>OnPlayerWin</c> is a listener that is executed when the game manager
        /// determines that one of the players won the game. When this event occurs, the
        /// win audio source is played and the winning claim path is highlighted.
        /// </summary>
        /// <param name="eventType">
        /// <c>eventType</c> is an event associated with the winning player.
        /// </param>
        private void OnPlayerWin( PlayerWinEvent eventType )
        {
            StartCoroutine(ShowWin(eventType));
        }

        /// <summary>
        /// Method <c>ShowWin</c> is called when a player wins. Displays pulsing effect on 
        /// winning cells, displays winner text, and after a delay, displays the Play Again
        /// modal.
        /// </summary>
        /// <param name="eventType">
        /// <c>eventType</c> is an event associated with the winning player.
        /// </param>
        /// <returns></returns>
        private IEnumerator ShowWin( PlayerWinEvent eventType )
        {
            _boardClickEnabled = false;

            if (_audioWinSource)
            {
                _audioWinSource.Play();
            }

            if (eventType.Player == _gameManager.GetGameSeries().Player1)
            {
                StartCoroutine(ShowMessage("Playe 1 Wins!"));
            }
            else
            {
                StartCoroutine(ShowMessage("Player 2 Wins!"));
            }
   
            Transform gameBoard = transform.Find($"GameBoard{_boardId}");
 
            Color defaultColor = eventType.Player.Settings.Color;
            Color highlightColor = new Color(
                (float)(defaultColor.r * 1.1),
                (float)(defaultColor.g * 1.1),
                (float)(defaultColor.b * 1.1)
            );

            for (int i = 0; i < 6; i++)
            {
                foreach (GameBoardCell cell in eventType.WinCells)
                {
                    string tileName = $"GameBoard{_boardId}{cell.Row}{cell.Column}";
                    GameObject tile = gameBoard.Find(tileName).gameObject;
                    RectTransform tileRect = tile.GetComponent<RectTransform>();
                    Image tileBackground = tile.GetComponent<Image>();
                    Image iconImage = tileRect.Find($"{tileName}Icon").GetComponent<Image>();

                    if (i % 2 == 0)
                    {
                        tileBackground.color = new Color(0.098f, 0.098f, 0.098f);
                        iconImage.color = defaultColor;
                    }
                    else
                    {
                        tileBackground.color = new Color(.2f, .2f, .2f);
                        iconImage.color = highlightColor;

                    }
                }
                yield return new WaitForSeconds(.35f);
            }

            _boardClickEnabled = true;

            CheckPlayAgain();
        }

        /// <summary>
        /// Method <c>OnPlayerClaim</c> is a listener that is executed when the game manager
        /// determines that the user claimed an available game board position. We always check
        /// with the game manager before claiming a position on the board.
        /// </summary>
        /// <param name="eventType">
        /// <c>eventType</c> is an event associated with the completed player move.
        /// </param>
        public void OnPlayerClaim( PlayerClaimEvent eventType )
        {
            PlayerMove playerMove = eventType.PlayerMove;
            GameState gameState = _gameManager.GetCurrentGame();

            Transform gameBoard = transform.Find($"GameBoard{_boardId}");
            string tileName = $"GameBoard{_boardId}{playerMove.Row}{playerMove.Column}";
            RectTransform tileRect = gameBoard.Find(tileName).GetComponent<RectTransform>();
            Image iconImage = tileRect.Find($"{tileName}Icon").GetComponent<Image>();
            iconImage.color = playerMove.Player.Settings.Color;

            if (gameState.InitialPlayer == playerMove.Player)
            {
                iconImage.sprite = _crossSprite;
            }
            else
            {
                iconImage.sprite = _circleSprite;
            }
        }

        /// <summary>
        /// Method <c>OnNewGame</c> is a listener that is executed when the current game is over
        /// and the player 1 chooses to player another game via the Play New Game modal.
        /// </summary>
        /// <param name="eventType">
        /// <c>eventType</c> is a generic event with no additional information.
        /// </param>
        private void OnNewGame( GameEvent eventType )
        {
            NewGame();
        }

        /// <summary>
        /// Method <c>NewGame</c> is used to initialize and start a new game. First, the game manager
        /// initializes a new game, then we build a new game board, and then we start the game.
        /// </summary>
        private void NewGame()
        {
            PlayerSettings settings = _playerSettingsService.GetSettings();

            GameBoard gameBoard = new GameBoard(settings.BoardSize);
            GameState gameState = _gameManager.InitializeGame(gameBoard, settings.LengthToWin);

            if (_message)
            {
                _message.gameObject.SetActive(false);
                _message.text = "";
            }

            BuildGameBoard(gameState);

            _gameManager.StartGame();

            _boardClickEnabled = true;
        }

        /// <summary>
        /// Method <c>BuildGameboard</c> is used to discard the current game board and pieces and 
        /// create a new game board.
        /// </summary>
        /// <param name="game">
        /// <c>game</c> is the new initialized game state.
        /// </param>
        private void BuildGameBoard( GameState game )
        {
            DestroyExistingGameBoard();

            // Increment board id to ensure game objects are unique across games
            _boardId++;

            RectTransform contentPanel = GetComponent<RectTransform>();
            float parentWidth = contentPanel.sizeDelta.x;
            float parentHeight = contentPanel.sizeDelta.y;

            GameObject board = new GameObject($"GameBoard{_boardId}", typeof(RectTransform));
            board.transform.SetParent(transform, false);

            Image boardImage = board.AddComponent<Image>();
            boardImage.color = Color.white;

            int boardSize = game.GameBoard.Size;
            int gutter = GetGutterSize(boardSize);

            int tileSize = (int) parentHeight / boardSize;
            tileSize = tileSize - ((gutter * (boardSize - 1)) / boardSize);

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    float posX = col * tileSize + (col * gutter);
                    float posY = row * -tileSize - (row * gutter);

                    string tileName = $"GameBoard{_boardId}{row}{col}";

                    // Add cell
                    GameObject tile = new GameObject(tileName, typeof(RectTransform));
                    tile.transform.SetParent(board.transform, false);

                    RectTransform tileRect = tile.GetComponent<RectTransform>();
                    tileRect.sizeDelta = new Vector2(tileSize, tileSize);
                    tileRect.localPosition = new Vector3(posX, posY);
                    tileRect.localScale = Vector2.one;

                    tileRect.sizeDelta = new Vector2(tileSize, tileSize);
                    tileRect.anchorMax = new Vector2(0, 1);
                    tileRect.anchorMin = new Vector2(0, 1);
                    tileRect.pivot = new Vector2(0, 1);

                    Image tileImage = tile.AddComponent<Image>();
                    tileImage.color = new Color(.098f, .098f, .098f, 255);

                    // Add icon
                    GameObject icon = new GameObject($"{tileName}Icon", typeof(RectTransform));
                    icon.transform.SetParent(tile.transform, false);
                    RectTransform iconRect = icon.GetComponent<RectTransform>();
                    iconRect.sizeDelta = tileRect.sizeDelta;

                    Image iconImage = icon.AddComponent<Image>();
                    iconImage.color = Color.clear;
                    iconImage.material = _gameObjectMaterial;
                    iconImage.transform.localScale = new Vector2(.8f, .8f);

                    Vector2 rowCoordinates = new Vector2(row, col);

                    Button button = tile.AddComponent<Button>();
                    button.onClick.AddListener(() =>
                    {
                        if (_boardClickEnabled)
                        {
                            int row = (int) rowCoordinates.x;
                            int col = (int) rowCoordinates.y;
                            _eventBus.BoardClickEvents
                                .Notify(new BoardClickEvent(row, col));
                        }
                    });
                }
            }

            float gridWidth = (boardSize * tileSize) + ((boardSize - 1) * gutter);
            float gridHeight = (boardSize * tileSize) + ((boardSize - 1) * gutter);

            RectTransform boardRect = board.GetComponent<RectTransform>();
            boardRect.localPosition = Vector2.zero;
            boardRect.anchorMax = new Vector2(.5f, .5f);
            boardRect.anchorMin = new Vector2(.5f, .5f);
            boardRect.pivot = new Vector2(.5f, .5f);
            boardRect.sizeDelta = new Vector2(gridWidth, gridHeight);
        }

        /// <summary>
        /// Method <c>DestroyExistingGameBoard</c> is used to destroy the current game
        /// board before building a new one.
        /// </summary>
        private void DestroyExistingGameBoard()
        {
            Transform boardTransform = transform.Find($"GameBoard{_boardId}");

            if (boardTransform)
            {
                Destroy(boardTransform.gameObject);
            }
        }

        /// <summary>
        /// Method <c>GetGutterSize</c> is used to dynamically determine the gutter 
        /// size based on the game board dimensions. The larger the game board, the 
        /// smaller the gutter size.
        /// </summary>
        /// <param name="boardSize"></param>
        /// <returns></returns>
        private int GetGutterSize( int boardSize )
        {
            if (boardSize <= 3)
            {
                return 5;
            }
            return 1;
        }

        /// <summary>
        /// Method <c>CheckPlayerAgain</c> is called after a game completes and we
        /// need to check with player 1 if they wish to continue the series and play
        /// a new game with the current opponent. If player 1 wishes to start a new
        /// game against another opponent, then they need to click the Start Series
        /// button which allows the user to choose a different opponent.
        /// </summary>
        private void CheckPlayAgain()
        {
            _playAgainModal.Open(NewGame, BackToMain);
        }

        /// <summary>
        /// Method <c>BackToMain</c> is called when the user chooses not to play again
        /// and the system redirects the user back to the main menu.
        /// </summary>
        private void BackToMain()
        {
            _sceneLoader.LoadMainMenu();
        }
    }
}
