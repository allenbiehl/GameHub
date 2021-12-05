using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameHub.Core.UI
{
    /// <summary>
    /// Class <c>GamesPanel</c> represents the main menu, UI panel that displays all 
    /// game configurations available to play in game hub.
    /// </summary>
    public class GamesPanel : MonoBehaviour
    {
        /// <summary>
        /// Instance variable <c>_gameConfigs</c> is the list of game configurations 
        /// available to play and is cached to speed up search performance.
        /// </summary>
        private List<GameConfig> _gameConfigs = new List<GameConfig>();

        /// <summary>
        /// Instance variable <c>_gameObjectMaterial</c> is the game material 
        /// associated with the game icon.
        /// </summary>
        [SerializeField]
        private Material _gameObjectMaterial;

        /// <summary>
        /// Instance variable <c>_gameListBody</c> is the content area where we render
        /// the list of available game configurations.
        /// </summary>
        [SerializeField]
        private RectTransform _gameListBody;

        /// <summary>
        /// Method <c>Start</c> is used to intialize the <c>GamePanel</c>.
        /// </summary>
        public void Start()
        {
            List<GameConfig> gameSource = GameConfigLoader.Instance.Load();

            if (gameSource != null)
            {
                _gameConfigs = gameSource;  
                LoadGameList(_gameConfigs);
            }
        }

        /// <summary>
        /// Method <c>OnSearchValueChanged</c> is executed when the user enters search criteria in 
        /// the search field. If the search field is empty, then we display all game configurations,
        /// otherwise we performance an uppercase pattern match that matches if the game config name
        /// contains the search criteria.
        /// </summary>
        /// <param name="value">
        /// <c>input</c> is search field that contains the value to match against.
        /// </param>
        public void OnSearchValueChange(TMP_InputField input)
        {
            if (input == null || input.text == "")
            {
                LoadGameList(_gameConfigs);
            }
            else
            {
                List<GameConfig> filteredConfigs = _gameConfigs
                    .Where(config => config.Name.ToUpper().Contains(input.text.ToUpper()))
                    .ToList();

                LoadGameList(filteredConfigs);
            }
        }

        /// <summary>
        /// Method <c>ClearGameList</c> is used to clear all game configuration rows from the game 
        /// list content area.
        /// </summary>
        private void ClearGameList()
        {
            if (_gameListBody && _gameListBody.childCount > 0)
            {
                foreach (Transform child in _gameListBody.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        /// <summary>
        /// Method <c>LoadGameList</c> is used to load the specified list of game configurations.
        /// When this method is executed, the existing game list is first cleared and then the 
        /// new list of game configs is rendered.
        /// </summary>
        /// <param name="gameConfigs"></param>
        private void LoadGameList(List<GameConfig> gameConfigs)
        {
            if (_gameListBody && gameConfigs != null)
            {
                ClearGameList();

                Font arial = Resources.GetBuiltinResource<Font>("Arial.ttf");
                float listWidth = _gameListBody.sizeDelta.x;
                float listHeight = _gameListBody.sizeDelta.y;
                float rowHeight = 50;

                for (int i = 0; i < gameConfigs.Count; i++)
                {
                    GameConfig config = gameConfigs[i];

                    // Create game row
                    float rowLocX = 0;
                    float rowLocY = (listHeight / 2) - (rowHeight / 2) - (i * rowHeight);

                    GameObject gameListRow = new GameObject($"GameListRow{i}", typeof(RectTransform));
                    gameListRow.transform.SetParent(_gameListBody, false);
                    RectTransform row = gameListRow.GetComponent<RectTransform>();
                    row.sizeDelta = new Vector2(listWidth, rowHeight);
                    row.transform.localPosition = new Vector2(rowLocX, rowLocY);
                    row.transform.localScale = Vector2.one;

                    HoverImage image = gameListRow.AddComponent<HoverImage>();
                    image.color = Color.clear;
                    image.HoverColor = new Color(.2f, .2f, .2f, 1);

                    Button menuBtn = gameListRow.AddComponent<Button>();
                    menuBtn.onClick.AddListener(() => SceneLoader.Instance.Load(config.Scene, true));

                    // Add Name cell
                    GameObject nameCell = new GameObject($"GameListRow{i}Name");
                    nameCell.transform.SetParent(gameListRow.transform, false);

                    Text txt = nameCell.AddComponent<Text>();
                    txt.text = config.Name;
                    txt.font = arial;
                    txt.fontSize = 12;
                    txt.lineSpacing = 1;
                    Color white = Color.white;
                    txt.color = white;
                    txt.alignment = TextAnchor.MiddleCenter;

                    // TODO calculate position based off top left corner parent versus center vertex
                    // Calculate the offset based on the name cell header.
                    txt.transform.localPosition = new Vector2(-175, 0);
                    txt.rectTransform.sizeDelta = new Vector2(500, rowHeight);

                    // Add Icon cell
                    GameObject iconCell = new GameObject($"GameListRow{i}Icon");
                    iconCell.transform.SetParent(gameListRow.transform, false);

                    Image icon = iconCell.AddComponent<Image>();
                    icon.color = Color.white;

                    // TODO calculate position based off top left corner parent versus center vertex
                    // Calculate the offset based on the icon cell header.
                    icon.transform.localPosition = new Vector2(-250, 0);
                    icon.rectTransform.sizeDelta = new Vector2(25, 25);
                    if (_gameObjectMaterial)
                    {
                        icon.material = _gameObjectMaterial;
                    }
                    if (config.Icon != null)
                    {
                        icon.sprite = config.Icon;
                    }
                }
            }
        }
    }
}