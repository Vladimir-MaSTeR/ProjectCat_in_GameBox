using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateQuestsText : MonoBehaviour {

    #region Переменные движка
    [Header("Текстовые поля")]

    [Tooltip("Текстовое поле для названия предмета")]
    [SerializeField]
    private TextMeshProUGUI _questText;

    [Tooltip("Текстовое поле для первой руны")]
    [SerializeField]
    private TextMeshProUGUI _oneRuneText;

    [Tooltip("Текстовое поле для второй руны")]
    [SerializeField]
    private TextMeshProUGUI _twoRuneText;

    [Tooltip("Текстовое поле третьей руны")]
    [SerializeField]
    private TextMeshProUGUI _threeRuneText;

    [Tooltip("Текстовое поле четвертой руны")]
    [SerializeField]
    private TextMeshProUGUI _fourRuneText;

    [Space(20)]
    [Header("Картинки рун")]

    [Tooltip("Первая картинка для отображения 3д руны")]
    [SerializeField]
    private RawImage _oneResoucesRawImage;

    [Tooltip("Первая картинка для отображения 3д руны")]
    [SerializeField]
    private RawImage _twoResoucesRawImage;

    [Tooltip("Первая картинка для отображения 3д руны")]
    [SerializeField]
    private RawImage _threeResoucesRawImage;

    [Tooltip("Первая картинка для отображения 3д руны")]
    [SerializeField]
    private RawImage _fourResoucesRawImage;

    [Space(20)]
    [Tooltip("Масив для текстур рун с тегом Log всех уровней")]
    [SerializeField]
    private Texture[] _logTextures;

    [Tooltip("Масив для текстур рун с тегом Neil всех уровней")]
    [SerializeField]
    private Texture[] _neilTextures;

    [Tooltip("Масив для текстур рун с тегом Stone всех уровней")]
    [SerializeField]
    private Texture[] _stoneTextures;

    [Tooltip("Масив для текстур рун с тегом Cloth всех уровней")]
    [SerializeField]
    private Texture[] _clothTextures;
    #endregion

    #region Приватные переменные
    private string defaultValueRunsText = "---/---";
    private string defaultMainText = "Квест не выбран";
    
    #endregion

    #region СЕТТЕРЫ ДЛЯ ТЕКСТА

    /// <summary>
    /// Сеттер для главного текста
    /// </summary>
    /// <param name="text"></param>
    public void SetQuestText(string text) {
        _questText.text = text;
    }

    /// <summary>
    /// Сеттер для текста первой руны
    /// </summary>
    /// <param name="currentValue"> Текущее колличество руны в инвенторе </param>
    /// <param name="targetValue"> Необходимое колличество рун для выполнения квеста </param>
    public void SetOneRuneText(int currentValue, int targetValue) {
        if(targetValue > 0) {
            _oneRuneText.text = $"{currentValue}/{targetValue}";
        } else {
            _oneRuneText.text = defaultValueRunsText;
        }
        
    }

    /// <summary>
    /// Сеттер для текста второй руны
    /// </summary>
    /// <param name="currentValue"> Текущее колличество руны в инвенторе </param>
    /// <param name="targetValue"> Необходимое колличество рун для выполнения квеста </param>
    public void SetTwoRuneText(int currentValue, int targetValue) {
        if(targetValue > 0) {
            _twoRuneText.text = $"{currentValue}/{targetValue}";
        } else {
            _twoRuneText.text = defaultValueRunsText;
        }
    }

    /// <summary>
    /// Сеттер для текста третьей руны
    /// </summary>
    /// <param name="currentValue"> Текущее колличество руны в инвенторе </param>
    /// <param name="targetValue"> Необходимое колличество рун для выполнения квеста </param>
    public void SetThreeRuneText(int currentValue, int targetValue) {
        if(targetValue > 0) {
            _threeRuneText.text = $"{currentValue}/{targetValue}";
        } else {
            _threeRuneText.text = defaultValueRunsText;
        }
    }

    /// <summary>
    /// Сеттер для текста четвертой руны
    /// </summary>
    /// <param name="currentValue"> Текущее колличество руны в инвенторе </param>
    /// <param name="targetValue"> Необходимое колличество рун для выполнения квеста </param>
    public void SetFourRuneText(int currentValue, int targetValue) {
        if(targetValue > 0) {
            _fourRuneText.text = $"{currentValue}/{targetValue}";
        } else {
            _fourRuneText.text = defaultValueRunsText;
        }
    }

    #endregion

    #region СЕТТЕРЫ ДЛЯ КАРТИНОК

    /// <summary>
    /// Сеттер для картинки первого слота рун
    /// </summary>
    /// <param name="tagRune"> тег руны которую нужно отобразить </param>
    /// <param name="levelRune"> уровень руны которую нужно отобразить </param>
    public void SetOneRawImage(string tagRune, int levelRune) {
        if(ResourcesTags.Log_1.ToString() == tagRune || ResourcesTags.Log_2.ToString() == tagRune || 
           ResourcesTags.Log_3.ToString() == tagRune || ResourcesTags.Log_4.ToString() == tagRune) {

            _oneResoucesRawImage.texture = _logTextures[levelRune - 1];
        }
    }

    /// <summary>
    /// Сеттер для картинки второго слота рун
    /// </summary>
    /// <param name="tagRune"> тег руны которую нужно отобразить </param>
    /// <param name="levelRune"> уровень руны которую нужно отобразить </param>
    public void SetTwoRawImage(string tagRune, int levelRune) {
       if(ResourcesTags.Neil_1.ToString() == tagRune || ResourcesTags.Neil_2.ToString() == tagRune ||
           ResourcesTags.Neil_3.ToString() == tagRune || ResourcesTags.Neil_4.ToString() == tagRune) {

                _twoResoucesRawImage.texture = _neilTextures[levelRune - 1];
        }
    }

    /// <summary>
    /// Сеттер для картинки третьего слота рун
    /// </summary>
    /// <param name="tagRune"> тег руны которую нужно отобразить </param>
    /// <param name="levelRune"> уровень руны которую нужно отобразить </param>
    public void SetThreeRawImage(string tagRune, int levelRune) {
        if(ResourcesTags.Stone_1.ToString() == tagRune || ResourcesTags.Stone_2.ToString() == tagRune ||
           ResourcesTags.Stone_3.ToString() == tagRune || ResourcesTags.Stone_4.ToString() == tagRune) {

            _threeResoucesRawImage.texture = _stoneTextures[levelRune - 1];
        }
    }

    /// <summary>
    /// Сеттер для картинки четвертой слота рун
    /// </summary>
    /// <param name="tagRune"> тег руны которую нужно отобразить </param>
    /// <param name="levelRune"> уровень руны которую нужно отобразить </param>
    public void SetFourRawImage(string tagRune, int levelRune) {      
        if(ResourcesTags.Cloth_1.ToString() == tagRune || ResourcesTags.Cloth_2.ToString() == tagRune ||
           ResourcesTags.Cloth_3.ToString() == tagRune || ResourcesTags.Cloth_4.ToString() == tagRune) {

            _fourResoucesRawImage.texture = _clothTextures[levelRune - 1];
        }
    }

    #endregion

    #region ГЕТТЕРЫ ДЛЯ МАССИВОВ КАРТИНОК

    public Texture[] GetLogTextures() {
        return _logTextures;
    }

    public Texture[] GetNeilTexture() {
        return _neilTextures;
    }

    public Texture[] GetStoneTexture() {
        return _stoneTextures;
    }

    public Texture[] GetClothTextures() {
        return _clothTextures;
    }

    #endregion

    public void DefoultValue() {
        _questText.text = defaultMainText;

        SetOneRawImage(ResourcesTags.Log_1.ToString(), 1);
        SetOneRuneText(0, 0);

        SetTwoRawImage(ResourcesTags.Neil_1.ToString(), 1);
        SetTwoRuneText(0, 0);

        SetThreeRawImage(ResourcesTags.Stone_1.ToString(), 1);
        SetThreeRuneText(0, 0);

        SetFourRawImage(ResourcesTags.Cloth_1.ToString(), 1);
        SetFourRuneText(0, 0);
    }

}
