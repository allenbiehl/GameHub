using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameHub.Core.Util;

namespace GameHub.Core.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class GamesPanel : MonoBehaviour
    {
        private List<GameConfig> _gameConfigs;

        public Material gameObjectMaterial;

        //private Texture2D _cursorTexture;

        private void Awake()
        {
            TMP_InputField searchField = ComponentUtil.FindComponent<TMP_InputField>("SearchField", this);

            if (searchField)
            {
                searchField.onValueChanged.AddListener(OnSearchValueChange);
            }

            // TODO Cache resources on app startup
            // dalays scene load
            //cursorTexture = CursorLoader.Load(CursorType.HandPointer);

            // TODO Cache game configs on startup
            // dalays scene load
            _gameConfigs = GameConfigLoader.Load();

            LoadGameList(_gameConfigs);
        }

        private void OnSearchValueChange(string value)
        {
            if (value == "")
            {
                LoadGameList(_gameConfigs);
            }
            else
            {
                List<GameConfig> filteredConfigs = _gameConfigs
                    .Where(config => config.Name.ToUpper().Contains(value.ToUpper()))
                    .ToList();

                LoadGameList(filteredConfigs);
            }

        }

        private void ClearGameList()
        {
            RectTransform gameListBody = ComponentUtil.FindComponent<RectTransform>("GameList/Body", this);

            if (gameListBody && gameListBody.childCount > 0)
            {
                foreach (Transform child in gameListBody.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameConfigs"></param>
        private void LoadGameList(List<GameConfig> gameConfigs)
        {
            RectTransform gameListBody = ComponentUtil.FindComponent<RectTransform>("GameList/Body", this);
            
            if (gameListBody && gameConfigs != null)
            {
                ClearGameList();

                Font arial = Resources.GetBuiltinResource<Font>("Arial.ttf");
                float listWidth = gameListBody.sizeDelta.x;
                float listHeight = gameListBody.sizeDelta.y;
                float rowHeight = 50;

                for (int i = 0; i < gameConfigs.Count; i++)
                {
                    GameConfig config = gameConfigs[i];

                    // Create game row
                    float rowLocX = 0;
                    float rowLocY = (listHeight / 2) - (rowHeight / 2) - (i * rowHeight);

                    GameObject gameListRow = new GameObject($"GameListRow{i}", typeof(RectTransform));
                    gameListRow.transform.SetParent(gameListBody, false);
                    RectTransform row = gameListRow.GetComponent<RectTransform>();
                    row.sizeDelta = new Vector2(listWidth, rowHeight);
                    row.transform.localPosition = new Vector2(rowLocX, rowLocY);
                    row.transform.localScale = Vector2.one;

                    HoverImage image = gameListRow.AddComponent<HoverImage>();
                    image.color = Color.clear;
                    image.hoverColor = new Color(.2f, .2f, .2f, 1);
                    //image.cursorTexture = cursorTexture;

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
                    icon.material = gameObjectMaterial;

                    if (config.Icon != null)
                    {
                        icon.sprite = config.Icon;
                    }
                }
            }
        }
    }
}