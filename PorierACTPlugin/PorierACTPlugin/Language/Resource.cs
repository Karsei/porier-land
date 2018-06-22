using System.Collections.Generic;
using System.Windows.Forms;

namespace PorierACTPlugin.Language
{
    public static class Resource
    {
        public static string CultureName = "ko-KR";
        private static LanguageDictionary languageDictionary = new LanguageDictionary();

        public static string GetString(string keyword)
        {
            if (!languageDictionary.ContainsKey(CultureName)) return "LOCALE_NOT_FOUND";
            if (!languageDictionary[CultureName].ContainsKey(keyword))
            {
                MessageBox.Show(keyword);
                return "TEXT_NOT_FOUND";
            }
            
            return languageDictionary[CultureName][keyword];
        }
    }

    public class LanguageDictionary : Dictionary<string, Dictionary<string, string>>
    {
        public LanguageDictionary() : base()
        {
            Dictionary<string, string> enGB = new Dictionary<string, string>();
            Dictionary<string, string> koKR = new Dictionary<string, string>();

            string[][] strings = new string[][]
            {
                #region UI Texts
                new string[] { "PLUGIN_NAME", "PorierACTPlugin", "포리에 ACT 플러그인" },
                new string[] { "PLUGIN_STARTED", "PorierACTPlugin started successfully.", "포리에 ACT 플러그인 작동중!" },
                new string[] { "PLUGIN_DISABLED", "PorierACTPlugin disabled.", "포리에 ACT 플러그인 종료됨." },

                new string[] { "OVERLAY_EXCEPTION", "Unexpected exception is thrown while trying to turn on the overlay. Please reset settings and try again. (You can reset settings from the ACT > Plugins > PorierACTPlugin)", "오버레이를 켜는 도중 예상치 못한 오류가 발생했습니다. 문제가 지속되면 설정을 초기화한 후 다시 시도해주세요. (설정 초기화는 ACT > Plugins > 포리에 ACT 플러그인에서 실행할 수 있습니다.)" },
                new string[] { "UPDATE_EXCEPTION", "Unexpected exception was thrown while updating the plugin. Please re-install if the problem persists.", "플러그인을 업데이트하는 도중 예상치 못한 오류가 발생했습니다. 문제가 지속되면 플러그인을 재설치해주세요." },

                new string[] { "UPDATE_QUESTION", "New version of the plugin is found! Current version: {0}, New version: {1}. Would you like to update?", "플러그인 업데이트가 발견되었습니다! 현재 버전: {0}, 새 버전: {1}. 업데이트하시겠습니까?" },
                new string[] { "UPDATE_TITLE", "Update Plugin", "플러그인 업데이트" },

                new string[] { "EDIT_SETTINGS_TEXT", "Edit Settings", "설정 변경" },

                new string[] { "RESET_SETTINGS_TEXT", "Reset Settings", "설정 초기화" },
                new string[] { "RESET_SETTINGS_CONFIRM_TEXT", "Do you really want to reset settings?", "정말로 모든 설정을 초기화하시겠습니까?" },

                new string[] { "APPLY_BUTTON", "Apply", "적용" },

                new string[] { "YUDIE_PANEL_LAST_SKILLS", "Last skills hit", "죽기 직전에 맞은 기술들" },
                new string[] { "YUDIE_PANEL_BUFFS", "Status effects at time of death", "죽는 순간에 걸려있던 상태효과들" },
                #endregion

                #region General Texts
                new string[] { "TRUE", "True", "활성화" },
                new string[] { "FALSE", "False", "비활성화" },

                new string[] { "TRUE_VISIBLE", "True", "보이기" },
                new string[] { "FALSE_VISIBLE", "False", "가리기" },

                new string[] { "LIST", "List", "목록" },
                #endregion

                #region Job Names
                new string[] { "GLA", "Gladiator", "검술사" },
                new string[] { "MRD", "Marauder", "도끼술사" },

                new string[] { "LNC", "Lancer", "창술사" },
                new string[] { "PGL", "Pugilist", "격투사" },
                new string[] { "ARC", "Archer", "궁술사" },
                new string[] { "ROG", "Rogue", "쌍검사" },
                new string[] { "THM", "Thaumaturge", "주술사" },
                new string[] { "ACN", "Arcanist", "비술사" },

                new string[] { "CNJ", "Conjurer", "환술사" },

                new string[] { "PLD", "Paladin", "나이트" },
                new string[] { "WAR", "Warrior", "전사" },
                new string[] { "DRK", "Dark Knight", "암흑기사" },

                new string[] { "DRG", "Dragoon", "용기사" },
                new string[] { "MNK", "Monk", "몽크" },
                new string[] { "SAM", "Samurai", "사무라이" },
                new string[] { "BRD", "Bard", "음유시인" },
                new string[] { "NIN", "Ninja", "닌자" },
                new string[] { "MCH", "Machinist", "기공사" },
                new string[] { "BLM", "Black Mage", "흑마도사" },
                new string[] { "SMN", "Summoner", "소환사" },
                new string[] { "RDM", "Red Mage", "적마도사" },

                new string[] { "WHM", "White Mage", "백마도사" },
                new string[] { "SCH", "Scholar", "학자" },
                new string[] { "AST", "Astrologian", "점성술사" },

                new string[] { "LIMIT", "Limit Break", "리미트 브레이크" },
                new string[] { "PET", "Pet", "소환수" },
                #endregion

                #region Anchor Type
                new string[] { "ANCHOR_TYPE_BOTTOM_LEFT", "Bottom Left", "왼쪽 하단 모서리" },
                new string[] { "ANCHOR_TYPE_BOTTOM_RIGHT", "Bottom Right", "오른쪽 하단 모서리" },
                new string[] { "ANCHOR_TYPE_TOP_LEFT", "Top Left", "왼쪽 상단 모서리" },
                new string[] { "ANCHOR_TYPE_TOP_RIGHT", "Top Right", "오른쪽 상단 모서리" },
                #endregion

                #region Auto Size Wrapper
                new string[] { "AUTO_SIZE_WRAPPER_DISPLAY_NAME_ANCHOR_LOCATION", "Location", "위치" },
                new string[] { "AUTO_SIZE_WRAPPER_DESCRIPTION_ANCHOR_LOCATION", "The location of the overlay's anchor. When auto-sizing, the anchor location will stay fixed and the overlay will resize relative to the anchor type.", "오버레이 창의 위치 좌표를 설정합니다. 이 좌표는 오버레이 창의 기준축 좌표이며 크기 자동 설정 기능이 작동할 때 기준축 좌표가 변경되지 않는 방향으로 크기를 설정하게 됩니다." },
                new string[] { "AUTO_SIZE_WRAPPER_DISPLAY_NAME_ANCHOR_TYPE", "Anchor Type", "기준축 유형" },
                new string[] { "AUTO_SIZE_WRAPPER_DESCRIPTION_ANCHOR_TYPE", "The type of the anchor. This determines what direction the overlay would resize when auto-sizing. For example, if anchor type is set to top-right, the overlay would expand to the direction of left and bottom when it needs to be enlarged.", "기준축 유형을 설정합니다. 기준축 유형은 크기가 자동 설정될 때 확대/축소되는 방향을 결정하게 됩니다. 예) 기준축 유형이 오른쪽 상단 모서리로 설정되었을 경우, 오버레이 창이 확대될 때 왼쪽 아래 방향으로 확대되게 됩니다." },
                new string[] { "AUTO_SIZE_WRAPPER_DISPLAY_NAME_ENABLE_AUTO_SIZE", "Enable Auto Size", "크기 자동 설정 기능 활성화" },
                new string[] { "AUTO_SIZE_WRAPPER_DESCRIPTION_ENABLE_AUTO_SIZE", "True if you want to enable auto size functionality, False otherwise.", "오버레이 크기 자동 설정 기능을 활성화할지 여부를 설정합니다." },
                #endregion

                #region Column Wrapper
                new string[] { "COLUMN_WRAPPER_CATEGORY_ALL", "Column Setting", "열 설정" },

                new string[] { "COLUMN_WRAPPER_DISPLAY_NAME_NAME", "Column name", "열 이름" },
                new string[] { "COLUMN_WRAPPER_DESCRIPTION_NAME", "A name of the column that shows up on the table header. Changing the name only changes the text that shows up on the header and does not change the type of data that is actually displayed under the column.", "테이블 헤더에 표시되는 열의 이름을 설정합니다." },
                new string[] { "COLUMN_WRAPPER_DISPLAY_NAME_VISIBLE", "Show this column?", "열 보이기" },
                new string[] { "COLUMN_WRAPPER_DESCRIPTION_VISIBLE", "True if you want to show this column in the table, False otherwise.", "해당 열을 테이블에 표시할지 여부를 선택합니다." },
                #endregion
                
                #region Job Color Wrapper
                new string[] { "JOB_COLOR_WRAPPER_CATEGORY_ALL", "Job Graph Colors", "그래프 색" },

                new string[] { "JOB_COLOR_WRAPPER_DISPLAY_NAME_COLOR_WRAPPER", "Job Graph Color", "그래프 색" },
                new string[] { "JOB_COLOR_WRAPPER_DESCRIPTION_COLOR_WRAPPER", "The color that will be used to draw graph for this job.", "해당 직업의 그래프를 표시할 때 사용될 색상을 설정합니다." },
                #endregion

                #region Point Wrapper
                new string[] { "POINT_WRAPPER_DISPLAY_NAME_X", "X", "X" },
                new string[] { "POINT_WRAPPER_DESCRIPTION_X", "The x coordinate for the point.", "X 좌표를 설정합니다." },
                new string[] { "POINT_WRAPPER_DISPLAY_NAME_Y", "Y", "Y" },
                new string[] { "POINT_WRAPPER_DESCRIPTION_Y", "The y coordinate for the point.", "Y 좌표를 설정합니다." },
                #endregion

                #region Size Wrapper
                new string[] { "SIZE_WRAPPER_DISPLAY_NAME_HEIGHT", "Height", "높이" },
                new string[] { "SIZE_WRAPPER_DESCRIPTION_HEIGHT", "The height of the designated object.", "지정된 오브젝트의 높이를 설정합니다." },
                new string[] { "SIZE_WRAPPER_DISPLAY_NAME_WIDTH", "Width", "너비" },
                new string[] { "SIZE_WRAPPER_DESCRIPTION_WIDTH", "The width of the designated object.", "지정된 오브젝트의 너비를 설정합니다." },
                #endregion

                #region Text Wrapper
                new string[] { "TEXT_WRAPPER", "Text Setting", "글꼴 설정" },

                new string[] { "TEXT_WRAPPER_DISPLAY_NAME_COLOR_WRAPPER", "Color", "색" },
                new string[] { "TEXT_WRAPPER_DESCRIPTION_COLOR_WRAPPER", "The color of the designated text.", "텍스트를 표시하는데 사용될 색상을 설정합니다." },

                new string[] { "TEXT_WRAPPER_DISPLAY_NAME_FONT_WRAPPER", "Font", "글꼴" },
                new string[] { "TEXT_WRAPPER_DESCRIPTION_FONT_WRAPPER", "The font of the designated text.", "텍스트를 표시하는데 사용될 글꼴을 설정합니다." },
                #endregion

                #region Main Tab Setting
                new string[] { "MAIN_TAB_SETTING_CATEGORY_FUNCTIONALITY", "Functional Settings", "기능 설정" },
                new string[] { "MAIN_TAB_SETTING_CATEGORY_OVERLAYS", "Overlays", "오버레이" },

                new string[] { "MAIN_TAB_SETTING_DISPLAY_NAME_CULTURE_NAME", "Language", "언어" },
                new string[] { "MAIN_TAB_SETTING_DESCRIPTION_CULTURE_NAME", "Display language for PorierACTPlugin.", "포리에 ACT 플러그인의 표시언어를 설정합니다." },

                new string[] { "MAIN_TAB_SETTING_DISPLAY_NAME_GAME_MODE_HOT_KEY", "Game Mode Hotkey", "게임 모드 단축키" },
                new string[] { "MAIN_TAB_SETTING_DESCRIPTION_GAME_MODE_HOT_KEY", "The hotkey shortcut assigned for toggling game mode. (When you enable game mode, all overlays will become unclickable and will only show up when you have focus on FFXIV process.)", "게임 모드를 켜거나 끄는 단축키를 설정합니다. (게임 모드가 켜져 있는 상태에서는 모든 오버레이가 클릭 불가능한 상태가 되며 FFXIV 프로세스가 선택되어 있는 상황에서만 화면에 나타납니다.)" },

                new string[] { "MAIN_TAB_SETTING_DISPLAY_NAME_HIDE_ALL_HOT_KEY", "Hide All Hotkey", "모두 숨기기 단축키" },
                new string[] { "MAIN_TAB_SETTING_DESCRIPTION_HIDE_ALL_HOT_KEY", "The hotkey shortcut assigned for hiding/showing all enabled overlays. (The overlays will still correctly gather necessary game data even when hidden.)", "모든 활성화된 오버레이를 숨기거나 보여주는 단축키를 설정합니다. (활성화된 오버레이들은 숨겨진 상태에서도 필요한 게임 데이터를 수집합니다.)" },

                new string[] { "MAIN_TAB_SETTING_DISPLAY_NAME_SHOW_OVERLAY", "Show DPS/HPS overlay?", "DPS/HPS 오버레이" },
                new string[] { "MAIN_TAB_SETTING_DESCRIPTION_SHOW_OVERLAY", "True if you want to show DPS/HPS overlay, False otherwise.", "DPS/HPS 오버레이를 활성화할지 여부를 설정합니다." },

                new string[] { "MAIN_TAB_SETTING_DISPLAY_NAME_SHOW_YUDIE", "Show Y U Die overlay?", "왜죽었니 오버레이" },
                new string[] { "MAIN_TAB_SETTING_DESCRIPTION_SHOW_YUDIE", "True if you want to show Y U Die overlay, False otherwise.", "왜죽었니 오버레이를 활성화할지 여부를 설정합니다." },
                #endregion

                #region Overlay Setting
                new string[] { "OVERLAY_SETTING_CATEGORY_DPS_TABLE", "Overlay DPS Table Setting", "오버레이 DPS 테이블 설정" },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_DPS_TABLE", "DPS Table Setting", "DPS 테이블 설정" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_DPS_TABLE", "Settings for the DPS table on the overlay.", "오버레이에 보여지는 DPS 테이블의 세부 설정을 변경할 수 있습니다." },



                new string[] { "OVERLAY_SETTING_CATEGORY_FUNCTIONALITY", "Overlay Functional Setting", "오버레이 기능 설정" },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_CAPTURE_HOT_KEY", "Capture Hotkey", "캡쳐 단축키" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_CAPTURE_HOT_KEY", "The hotkey shortcut assigned for capturing current overlay screen. The capture image will be saved under the designated capture save path as PNG image.", "현재 오버레이 스크린을 캡쳐해 저장하는 단축키를 설정합니다. 캡쳐된 이미지는 설정된 저장 경로에 PNG 포맷으로 저장됩니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_CAPTURE_SAVE_PATH", "Capture Save Path", "캡쳐 저장 경로" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_CAPTURE_SAVE_PATH", "The path where the captured overlay images will be saved.", "캡처된 오버레이 이미지가 저장될 경로를 지정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_PUT_CAPTURE_IN_CLIPBOARD", "Copy captured image to clipboard?", "캡처 시 클립보드에 복사" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_PUT_CAPTURE_IN_CLIPBOARD", "True if you want to automatically copy the captured overlay image to the clipboard upon capturing, False otherwise.", "오버레이 캡처 시 캡처된 이미지를 자동으로 클립보드에 복사할지 여부를 설정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_HIDE_NAMES_WHEN_CAPTURING", "Hide names when capturing?", "캡처 시 이름 가리기" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_HIDE_NAMES_WHEN_CAPTURING", "True if you want to automatically hide names of the players when capturing the overlay, False otherwise.", "오버레이 캡처 시 자동으로 플레이어들의 이름을 가릴지 여부를 설정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_END_COMBAT_HOT_KEY", "End Encounter Hotkey", "전투 종료 단축키" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_END_COMBAT_HOT_KEY", "The hotkey shortcut assigned for signaling ACT to end current encounter.", "현재 전투를 종료하고 새 전투 집계를 시작하는 단축키를 설정합니다." },



                new string[] { "OVERLAY_SETTING_CATEGORY_GLOBAL", "Overlay Global Setting", "오버레이 전역 설정" },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_LOCATION", "Location", "위치" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_LOCATION", "The location of the overlay window. (Will get overridden if auto sizing is set to true.)", "오버레이 창의 위치 좌표를 설정합니다. (오버레이 크기 자동 설정 기능이 켜져 있을 경우 이 설정은 적용되지 않습니다.)" },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_SIZE", "Size", "크기" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_SIZE", "The size of the overlay window. (Will get overridden if auto sizing is set to true.)", "오버레이 창의 크기를 설정합니다. (오버레이 크기 자동 설정 기능이 켜져 있을 경우 이 설정은 적용되지 않습니다.)" },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_AUTO_SIZE", "Overlay Auto Size Setting", "오버레이 크기 자동 설정 기능" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_AUTO_SIZE", "Settings for overlay's auto size functionality. If auto size functionality is turned on, overlay's size and location settings will not be applied and the overlay's size will be automatically adjusted based on data shown on the overlay.", "오버레이 크기 자동 설정 기능의 세부 설정을 수정할 수 있습니다.오버레이 크기 자동 설정 기능이 활성화되어있을 경우, 기존 오버레이 위치 좌표 설정과 오버레이 크기 설정은 적용되지 않으며 오버레이 창의 크기는 표시되는 데이터에 따라 자동으로 조절됩니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_AUTO_SIZE_TABLE_DISTANCE", "Distance Between Tables When Auto Sizing", "크기 자동 설정 시 테이블 간격" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_AUTO_SIZE_TABLE_DISTANCE", "Distance between DPS and HPS tables in px when auto sizing the overlay.", "크기 자동 설정이 활성화되어 있을 시 DPS 테이블과 HPS 테이블 사이의 거리를 설정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_BACK_COLOR", "Boundary Color", "테두리 색" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_BACK_COLOR", "The color of the boundary of the overlay window when it is not in a game mode.", "오버레이 창의 테두리 색깔을 설정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_BOUNDARY", "Boundary Width", "테두리 두께" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_BOUNDARY", "The width of the boundary of the overlay window when it is not in a game mode.", "오버레이 창의 테두리 두께를 설정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_GAME_MODE_BACK_COLOR", "Game Mode Boundary Color", "게임 모드 테두리 색" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_GAME_MODE_BACK_COLOR", "The color of the boundary of the overlay window when it is in game mode.", "오버레이 창이 게임 모드일 때의 테두리 색깔을 설정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_GAME_MODE_BOUNDARY", "Game Mode Boundary Width", "게임 모드 테두리 두께" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_GAME_MODE_BOUNDARY", "The width of the boundary of the overlay window when it is in game mode.", "오버레이 창이 게임 모드일 때의 테두리 두께를 설정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_BACKGROUND_FORM_BACK_COLOR", "Background Color", "배경색" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_BACKGROUND_FORM_BACK_COLOR", "The background color of the overlay window.", "오버레이 창의 배경 색깔을 설정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_BACKGROUND_FORM_OPACITY", "Background Opacity", "배경 투명도" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_BACKGROUND_FORM_OPACITY", "The opacity of the background of the overlay window. (1.0 means completely opaque and 0.0 means completely transparent.)", "오버레이 창의 배경 투명도를 설정합니다. (1.0은 완전히 불투명한 값, 0.0은 완전히 투명한 값입니다.)" },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_OPACITY", "Overlay Opacity", "오버레이 투명도" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_OPACITY", "The opacity of the overlay window. (1.0 means completely opaque and 0.0 means completely transparent.)", "오버레이 창의 투명도를 설정합니다. (1.0은 완전히 불투명한 값, 0.0은 완전히 투명한 값입니다.)" },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_ROUND_CORNER", "Corner Roundness", "모서리 둥글기" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_ROUND_CORNER", "The roundness of the overlay window corners.", "오버레이 창 모서리의 둥글기를 설정합니다." },



                new string[] { "OVERLAY_SETTING_CATEGORY_HEADER", "Overlay Header Panel Setting", "오버레이 헤더 설정" },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_HEADER_BACK_COLOR", "Background Color", "배경색" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_HEADER_BACK_COLOR", "The background color of the header panel.", "오버레이 헤더의 배경 색깔을 설정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_HEADER_HEIGHT", "Full Height", "기본 높이" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_HEADER_HEIGHT", "The height (in px) of the header panel when it is not collapsed.", "오버레이 헤더가 간소화되지 않았을 때의 px 단위 높이를 설정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_HEADER_COLLAPSED_HEIGHT", "Collapsed Height", "간소화 높이" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_HEADER_COLLAPSED_HEIGHT", "The height (in px) of the header panel when it is collapsed.", "오버레이 헤더가 간소화되었을 때의 px 단위 높이를 설정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_HEADER_COLLAPSED_TEXT", "Collapsed Header Text", "간소화 텍스트" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_HEADER_COLLAPSED_TEXT", "The font and color settings for the text that shows up on the header when it is collapsed.", "헤더가 간소화되었을 때 보여지는 텍스트의 글꼴과 색상을 설정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_HEADER_SHOW_DURATION", "Show encounter duration time?", "전투 시간 보이기" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_HEADER_SHOW_DURATION", "True if you want encounter duration time to be shown on the header panel, False otherwise.", "오버레이 헤더에 전투 시간을 보여줄지 여부를 설정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_HEADER_DURATION_TEXT", "Encounter Duration Text", "전투 시간 텍스트" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_HEADER_DURATION_TEXT", "The font and color settings for the text that shows the encounter duration time on the header.", "헤더에 표시되는 전투 시간의 글꼴과 색상을 설정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_HEADER_SHOW_ENCOUNTER_NAME", "Show encounter name?", "전투 이름 보이기" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_HEADER_SHOW_ENCOUNTER_NAME", "True if you want encounter name to be shown on the header panel, False otherwise.", "오버레이 헤더에 전투 이름을 보여줄지 여부를 설정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_HEADER_ENCOUNTER_NAME_TEXT", "Encounter Name Text", "전투 이름 텍스트" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_HEADER_ENCOUNTER_NAME_TEXT", "The font and color settings for the text that shows the encounter name on the header.", "헤더에 표시되는 전투 이름의 글꼴과 색상을 설정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_HEADER_SHOW_RDPS", "Show RDPS?", "RDPS 보이기" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_HEADER_SHOW_RDPS", "True if you want RDPS to be shown on the header panel, False otherwise.", "오버레이 헤더에 RDPS를 보여줄지 여부를 설정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_HEADER_RDPS_TEXT", "RDPS Text", "RDPS 텍스트" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_HEADER_RDPS_TEXT", "The font and color settings for the text that shows RDPS on the header.", "헤더에 표시되는 RDPS의 글꼴과 색상을 설정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_HEADER_SHOW_RHPS", "Show RHPS?", "RHPS 보이기" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_HEADER_SHOW_RHPS", "True if you want RHPS to be shown on the header panel, False otherwise.", "오버레이 헤더에 RHPS를 보여줄지 여부를 설정합니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_HEADER_RHPS_TEXT", "RHPS Text", "RHPS 텍스트" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_HEADER_RHPS_TEXT", "The font and color settings for the text that shows RHPS on the header.", "헤더에 표시되는 RHPS의 글꼴과 색상을 설정합니다." },



                new string[] { "OVERLAY_SETTING_CATEGORY_HPS_TABLE", "Overlay HPS Table Setting", "오버레이 HPS 테이블 설정" },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_HPS_TABLE", "HPS Table Setting", "HPS 테이블 설정" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_HPS_TABLE", "Settings for the HPS table on the overlay.", "오버레이에 보여지는 HPS 테이블의 세부 설정을 변경할 수 있습니다." },

                new string[] { "OVERLAY_SETTING_DISPLAY_NAME_HPS_OVER_HEAL_COLOR", "HPS Graph Overheal Color", "HPS 그래프 오버힐 색" },
                new string[] { "OVERLAY_SETTING_DESCRIPTION_HPS_OVER_HEAL_COLOR", "The color that will be used in the HPS table graph for showing the amount of overheal.", "HPS 그래프에서 오버힐을 나타내는 색상을 설정합니다." },
                #endregion

                #region Overlay Table Setting
                new string[] { "OVERLAY_TABLE_SETTING_CATEGORY_ALL", "Table Setting", "테이블 설정" },

                new string[] { "OVERLAY_TABLE_SETTING_DISPLAY_NAME_ANTI_ALIASING", "Anti-Aliasing", "앤티에일리어싱" },
                new string[] { "OVERLAY_TABLE_SETTING_DESCRIPTION_ANTI_ALIASING", "True if you want to enable anti-aliasing for displaying fonts, False otherwise. Enable anti-aliasing will make font appear smoother, but it may affect CPU performance.", "테이블 글꼴을 표시할 때 앤티에일리어싱을 사용할지 여부를 설정합니다. 앤티에일리어싱이 적용되면 글꼴이 더 부드럽게 보이지만 성능이 저하될 수 있습니다." },

                new string[] { "OVERLAY_TABLE_SETTING_DISPLAY_NAME_CELL_PADDING", "Cell Padding", "셀 간격" },
                new string[] { "OVERLAY_TABLE_SETTING_DESCRIPTION_CELL_PADDING", "The horizontal padding between table cells.", "테이블 셀 사이의 간격을 설정합니다." },

                new string[] { "OVERLAY_TABLE_SETTING_DISPLAY_NAME_COLUMN_HEADER_BACK_COLOR", "Column Header Background Color", "열 헤더 배경색" },
                new string[] { "OVERLAY_TABLE_SETTING_DESCRIPTION_COLUMN_HEADER_BACK_COLOR", "The background color of the columns' headers.", "열 헤더의 배경 색깔을 설정합니다." },

                new string[] { "OVERLAY_TABLE_SETTING_DISPLAY_NAME_COLUMN_HEADER_TEXT", "Column Header Text", "열 헤더 텍스트" },
                new string[] { "OVERLAY_TABLE_SETTING_DESCRIPTION_COLUMN_HEADER_TEXT", "The font and color settings for the columns' headers.", "열 헤더의 글꼴과 색상을 설정합니다." },

                new string[] { "OVERLAY_TABLE_SETTING_DISPLAY_NAME_COLUMNS", "Columns", "열" },
                new string[] { "OVERLAY_TABLE_SETTING_DESCRIPTION_COLUMNS", "Settings for the columns that will be used by the table to show data.", "테이블에서 보여질 열들을 설정합니다." },

                new string[] { "OVERLAY_TABLE_SETTING_DISPLAY_NAME_COMBINE_PETS", "Combine pet's DPS with owner?", "소환수 DPS 합치기" },
                new string[] { "OVERLAY_TABLE_SETTING_DESCRIPTION_COMBINE_PETS", "True if you want to combine pet's DPS with owner, False otherwise.", "소환수의 DPS를 소환자와 합칠지 여부를 설정합니다." },

                new string[] { "OVERLAY_TABLE_SETTING_DISPLAY_NAME_CURRENT_GRAPH_DATA_PROPERTY_NAME", "Graph Column", "그래프 열" },
                new string[] { "OVERLAY_TABLE_SETTING_DESCRIPTION_CURRENT_GRAPH_DATA_PROPERTY_NAME", "A name of the column that will be used for drawing graph.", "어떤 열의 데이터를 사용해 그래프를 그릴지 설정합니다." },

                new string[] { "OVERLAY_TABLE_SETTING_DISPLAY_NAME_GRID_COLOR", "Grid Color", "눈금선 색" },
                new string[] { "OVERLAY_TABLE_SETTING_DESCRIPTION_GRID_COLOR", "The color of the grid that divides each cell in the table.", "테이블의 각 셀을 나누는 눈금선의 색상을 설정합니다." },

                new string[] { "OVERLAY_TABLE_SETTING_DISPLAY_NAME_INCLUDE_LIMIT_IN_SORTING", "Include Limit Break when sorting?", "테이블 정렬 대상에 리미트 브레이크를 포함" },
                new string[] { "OVERLAY_TABLE_SETTING_DESCRIPTION_INCLUDE_LIMIT_IN_SORTING", "True if you want to include Limit Break when sorting the table, False otherwise. If this feature is set to False, the Limit Break will always show up at the bottom of the table.", "테이블 정렬 대상에 리미트 브레이크를 포함할지 여부를 설정합니다.이 기능이 비활성화 되어 있을 경우 리미트 브레이크는 언제나 테이블의 최하단에 표시되게 됩니다." },

                new string[] { "OVERLAY_TABLE_SETTING_DISPLAY_NAME_JOB_COLORS", "Job Colors", "그래프 색" },
                new string[] { "OVERLAY_TABLE_SETTING_DESCRIPTION_JOB_COLORS", "Graph colors for each job.", "각 직업별로 그래프를 표시할 때 사용될 색상을 설정합니다." },

                new string[] { "OVERLAY_TABLE_SETTING_DISPLAY_NAME_SHOW_GRAPH", "Show graph?", "그래프 표시하기" },
                new string[] { "OVERLAY_TABLE_SETTING_DESCRIPTION_SHOW_GRAPH", "True if you want to show graph in the table's background, False otherwise.", "테이블 뒷배경에 그래프를 표시할지 여부를 설정합니다." },

                new string[] { "OVERLAY_TABLE_SETTING_DISPLAY_NAME_SHOW_LIMIT", "Show Limit Break?", "리미트 브레이크 보이기" },
                new string[] { "OVERLAY_TABLE_SETTING_DESCRIPTION_SHOW_LIMIT", "True if you want to show Limit Break in the table, False otherwise.", "테이블에 리미트 브레이크를 보여줄지 여부를 설정합니다." },

                new string[] { "OVERLAY_TABLE_SETTING_DISPLAY_NAME_SHOW_ONLY_HEALER", "Show only healers?", "힐러만 보여주기" },
                new string[] { "OVERLAY_TABLE_SETTING_DESCRIPTION_SHOW_ONLY_HEALER", "True if you want to show only healers on the table, False otherwise.", "테이블에 힐러만 보여줄지 여부를 설정합니다. 이 기능이 활성화 되어 있을 경우 테이블에 직업이 힐러인 개체만 보여지게 됩니다." },

                new string[] { "OVERLAY_TABLE_SETTING_DISPLAY_NAME_SHOW_TABLE", "Show this table?", "테이블 보이기" },
                new string[] { "OVERLAY_TABLE_SETTING_DESCRIPTION_SHOW_TABLE", "True if you want to display this table on the overlay, False otherwise.", "해당 테이블을 오버레이에 표시할지 여부를 설정합니다." },

                new string[] { "OVERLAY_TABLE_SETTING_DISPLAY_NAME_TEXT", "Table Text", "테이블 텍스트" },
                new string[] { "OVERLAY_TABLE_SETTING_DESCRIPTION_TEXT", "The font and color settings for remaining texts that show up in the table.", "테이블에서 데이터를 표시하는데 사용되는 글꼴과 색상을 설정합니다." },
                #endregion

                #region YUDIE Setting
                new string[] { "YUDIE_SETTING_CATEGORY_GENERAL", "General Setting", "기본 설정" },

                new string[] { "YUDIE_SETTING_DISPLAY_NAME_LOCATION", "Location", "위치" },
                new string[] { "YUDIE_SETTING_DESCRIPTION_LOCATION", "The location of the overlay window. (Will get overridden if auto sizing is set to true.)", "오버레이 창의 위치 좌표를 설정합니다. (오버레이 크기 자동 설정 기능이 켜져 있을 경우 이 설정은 적용되지 않습니다.)" },

                new string[] { "YUDIE_SETTING_DISPLAY_NAME_SIZE", "Size", "크기" },
                new string[] { "YUDIE_SETTING_DESCRIPTION_SIZE", "The size of the overlay window. (Will get overridden if auto sizing is set to true.)", "오버레이 창의 크기를 설정합니다. (오버레이 크기 자동 설정 기능이 켜져 있을 경우 이 설정은 적용되지 않습니다.)" },

                new string[] { "YUDIE_SETTING_DISPLAY_NAME_AUTO_SIZE", "Overlay Auto Size Setting", "오버레이 크기 자동 설정 기능" },
                new string[] { "YUDIE_SETTING_DESCRIPTION_AUTO_SIZE", "Settings for overlay's auto size functionality. If auto size functionality is turned on, overlay's size and location settings will not be applied and the overlay's size will be automatically adjusted based on data shown on the overlay.", "오버레이 크기 자동 설정 기능의 세부 설정을 수정할 수 있습니다.오버레이 크기 자동 설정 기능이 활성화되어있을 경우, 기존 오버레이 위치 좌표 설정과 오버레이 크기 설정은 적용되지 않으며 오버레이 창의 크기는 표시되는 데이터에 따라 자동으로 조절됩니다." },
                
                new string[] { "YUDIE_SETTING_DISPLAY_NAME_BACK_COLOR", "Boundary Color", "테두리 색" },
                new string[] { "YUDIE_SETTING_DESCRIPTION_BACK_COLOR", "The color of the boundary of the overlay window when it is not in a game mode.", "오버레이 창의 테두리 색깔을 설정합니다." },

                new string[] { "YUDIE_SETTING_DISPLAY_NAME_BOUNDARY", "Boundary Width", "테두리 두께" },
                new string[] { "YUDIE_SETTING_DESCRIPTION_BOUNDARY", "The width of the boundary of the overlay window when it is not in a game mode.", "오버레이 창의 테두리 두께를 설정합니다." },

                new string[] { "YUDIE_SETTING_DISPLAY_NAME_GAME_MODE_BACK_COLOR", "Game Mode Boundary Color", "게임 모드 테두리 색" },
                new string[] { "YUDIE_SETTING_DESCRIPTION_GAME_MODE_BACK_COLOR", "The color of the boundary of the overlay window when it is in game mode.", "오버레이 창이 게임 모드일 때의 테두리 색깔을 설정합니다." },

                new string[] { "YUDIE_SETTING_DISPLAY_NAME_GAME_MODE_BOUNDARY", "Game Mode Boundary Width", "게임 모드 테두리 두께" },
                new string[] { "YUDIE_SETTING_DESCRIPTION_GAME_MODE_BOUNDARY", "The width of the boundary of the overlay window when it is in game mode.", "오버레이 창이 게임 모드일 때의 테두리 두께를 설정합니다." },

                new string[] { "YUDIE_SETTING_DISPLAY_NAME_BACKGROUND_FORM_BACK_COLOR", "Background Color", "배경색" },
                new string[] { "YUDIE_SETTING_DESCRIPTION_BACKGROUND_FORM_BACK_COLOR", "The background color of the overlay window.", "오버레이 창의 배경 색깔을 설정합니다." },

                new string[] { "YUDIE_SETTING_DISPLAY_NAME_BACKGROUND_FORM_OPACITY", "Background Opacity", "배경 투명도" },
                new string[] { "YUDIE_SETTING_DESCRIPTION_BACKGROUND_FORM_OPACITY", "The opacity of the background of the overlay window. (1.0 means completely opaque and 0.0 means completely transparent.)", "오버레이 창의 배경 투명도를 설정합니다. (1.0은 완전히 불투명한 값, 0.0은 완전히 투명한 값입니다.)" },

                new string[] { "YUDIE_SETTING_DISPLAY_NAME_OPACITY", "Overlay Opacity", "오버레이 투명도" },
                new string[] { "YUDIE_SETTING_DESCRIPTION_OPACITY", "The opacity of the overlay window. (1.0 means completely opaque and 0.0 means completely transparent.)", "오버레이 창의 투명도를 설정합니다. (1.0은 완전히 불투명한 값, 0.0은 완전히 투명한 값입니다.)" },

                new string[] { "YUDIE_SETTING_DISPLAY_NAME_ROUND_CORNER", "Corner Roundness", "모서리 둥글기" },
                new string[] { "YUDIE_SETTING_DESCRIPTION_ROUND_CORNER", "The roundness of the overlay window corners.", "오버레이 창 모서리의 둥글기를 설정합니다." },


                
                new string[] { "YUDIE_SETTING_CATEGORY_HEADER", "Header Setting", "헤더 설정" },

                new string[] { "YUDIE_SETTING_DISPLAY_NAME_HEADER_BACK_COLOR", "Background Color", "배경색" },
                new string[] { "YUDIE_SETTING_DESCRIPTION_HEADER_BACK_COLOR", "The background color of the header panel.", "오버레이 헤더의 배경 색깔을 설정합니다." },

                new string[] { "YUDIE_SETTING_DISPLAY_NAME_HEADER_HEIGHT", "Height", "높이" },
                new string[] { "YUDIE_SETTING_DESCRIPTION_HEADER_HEIGHT", "The height (in px) of the header panel.", "오버레이 헤더의 px 단위 높이를 설정합니다." },

                new string[] { "YUDIE_SETTING_DISPLAY_NAME_HEADER_TEXT", "Text", "텍스트" },
                new string[] { "YUDIE_SETTING_DESCRIPTION_HEADER_TEXT", "The font and color settings for the text that shows up on the header.", "헤더에 보여지는 텍스트의 글꼴과 색상을 설정합니다." },



                new string[] { "YUDIE_SETTING_CATEGORY_PANEL", "Panel Setting", "패널 설정" },
                
                new string[] { "YUDIE_SETTING_DISPLAY_NAME_PANEL_TEXT", "Panel Text", "패널 텍스트" },
                new string[] { "YUDIE_SETTING_DESCRIPTION_PANEL_TEXT", "The font and color settings for the text that shows up in the data panel.", "데이터 패널에 보여지는 텍스트의 글꼴과 색상을 설정합니다." }
                #endregion
            };
            
            foreach (string[] str in strings)
            {
                enGB.Add(str[0], str[1]);
                koKR.Add(str[0], str[2]);
            }

            Add("en-GB", enGB);
            Add("ko-KR", koKR);
        }
    }
}