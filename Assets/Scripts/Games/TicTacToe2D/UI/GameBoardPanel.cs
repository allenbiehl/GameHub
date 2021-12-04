using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using GameHub.Core.Util;
using GameHub.Games.TicTacToe2D.Event;

namespace GameHub.Games.TicTacToe2D.UI
{
    public class GameBoardPanel : MonoBehaviour
    {
        public Material gameObjectMaterial;
        private Sprite _circleSprite;
        private Sprite _crossSprite;
        private bool _boardClickEnabled;
        public AudioSource audioSource;

        private int _boardId;

        private void Start()
        {
            GameManager.Instance.EventBus.NewSeriesEvents.AddListener(OnNewGame);
            GameManager.Instance.EventBus.NewGameEvents.AddListener(OnNewGame);
            GameManager.Instance.EventBus.TieGameEvents.AddListener(OnTieGame);
            GameManager.Instance.EventBus.PlayerClaimEvents.AddListener(OnPlayerClaim);
            GameManager.Instance.EventBus.PlayerWinEvents.AddListener(OnPlayerWin);

            _circleSprite = SpriteLoader.Load("cell-circle");
            _crossSprite = SpriteLoader.Load("cell-cross");
        }

        private void OnTieGame(GameEvent eventType)
        {
            CheckPlayAgain();
        }

        private void OnPlayerWin( PlayerWinEvent eventType )
        {
            if (audioSource)
            {
                audioSource.Play();
            }
            StartCoroutine(HighlightWinCells(eventType, 6, .35f));
        }

        private IEnumerator HighlightWinCells( PlayerWinEvent eventType, int count, float delay )
        {
            _boardClickEnabled = false;

            Transform gameBoard = transform.Find($"GameBoard{_boardId}");
 
            Color defaultColor = eventType.Player.Settings.Color;
            Color highlightColor = new Color(
                (float)(defaultColor.r * 1.1),
                (float)(defaultColor.g * 1.1),
                (float)(defaultColor.b * 1.1)
            );

            for (int i = 0; i < count; i++)
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
                yield return new WaitForSeconds(delay);
            }
            _boardClickEnabled = true;

            CheckPlayAgain();
        }

        public void OnPlayerClaim( PlayerClaimEvent eventType )
        {
            PlayerMove playerMove = eventType.PlayerMove;
            GameState gameState = GameManager.Instance.CurrentGame;

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

        private void OnNewGame( GameEvent eventType )
        {
            NewGame();
        }

        private void NewGame()
        {
            GameManager manager = GameManager.Instance;
            PlayerSettings settings = PlayerSettingsManager.Instance.GetSettings();

            GameBoard gameBoard = new GameBoard(settings.BoardSize);
            GameState gameState = manager.InitializeGame(gameBoard, settings.LengthToWin);

            BuildGameBoard(gameState);

            manager.StartGame();

            _boardClickEnabled = true;
        }

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
                    iconImage.material = gameObjectMaterial;
                    iconImage.transform.localScale = new Vector2(.8f, .8f);

                    Vector2 rowCoordinates = new Vector2(row, col);

                    Button button = tile.AddComponent<Button>();
                    button.onClick.AddListener(() =>
                    {
                        if (_boardClickEnabled)
                        {
                            int row = (int) rowCoordinates.x;
                            int col = (int) rowCoordinates.y;
                            GameManager.Instance.EventBus.BoardClickEvents
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

        private void DestroyExistingGameBoard()
        {
            Transform boardTransform = transform.Find($"GameBoard{_boardId}");

            if (boardTransform)
            {
                Destroy(boardTransform.gameObject);
            }
        }

        private int GetGutterSize( int boardSize )
        {
            if (boardSize <= 3)
            {
                return 5;
            }
            return 1;
        }

        private void CheckPlayAgain()
        {
            PlayAgainModal.Instance.Open(new UnityAction(NewGame));
        }
    }
}
