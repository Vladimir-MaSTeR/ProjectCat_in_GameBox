using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateQuestsText : MonoBehaviour {

    #region ���������� ������
    [Header("��������� ����")]

    [Tooltip("��������� ���� ��� �������� ��������")]
    [SerializeField]
    private TextMeshProUGUI _questText;

    [Tooltip("��������� ���� ��� ������ ����")]
    [SerializeField]
    private TextMeshProUGUI _oneRuneText;

    [Tooltip("��������� ���� ��� ������ ����")]
    [SerializeField]
    private TextMeshProUGUI _twoRuneText;

    [Tooltip("��������� ���� ������� ����")]
    [SerializeField]
    private TextMeshProUGUI _threeRuneText;

    [Tooltip("��������� ���� ��������� ����")]
    [SerializeField]
    private TextMeshProUGUI _fourRuneText;

    [Space(20)]
    [Header("�������� ���")]

    [Tooltip("������ �������� ��� ����������� 3� ����")]
    [SerializeField]
    private RawImage _oneResoucesRawImage;

    [Tooltip("������ �������� ��� ����������� 3� ����")]
    [SerializeField]
    private RawImage _twoResoucesRawImage;

    [Tooltip("������ �������� ��� ����������� 3� ����")]
    [SerializeField]
    private RawImage _threeResoucesRawImage;

    [Tooltip("������ �������� ��� ����������� 3� ����")]
    [SerializeField]
    private RawImage _fourResoucesRawImage;

    [Space(20)]
    [Tooltip("����� ��� ������� ��� � ����� Log ���� �������")]
    [SerializeField]
    private Texture[] _logTextures;

    [Tooltip("����� ��� ������� ��� � ����� Neil ���� �������")]
    [SerializeField]
    private Texture[] _neilTextures;

    [Tooltip("����� ��� ������� ��� � ����� Stone ���� �������")]
    [SerializeField]
    private Texture[] _stoneTextures;

    [Tooltip("����� ��� ������� ��� � ����� Cloth ���� �������")]
    [SerializeField]
    private Texture[] _clothTextures;
    #endregion

    #region ��������� ����������
    private string defaultValueRunsText = "---/---";
    private string defaultMainText = "����� �� ������";
    
    #endregion

    #region ������� ��� ������

    /// <summary>
    /// ������ ��� �������� ������
    /// </summary>
    /// <param name="text"></param>
    public void SetQuestText(string text) {
        _questText.text = text;
    }

    /// <summary>
    /// ������ ��� ������ ������ ����
    /// </summary>
    /// <param name="currentValue"> ������� ����������� ���� � ��������� </param>
    /// <param name="targetValue"> ����������� ����������� ��� ��� ���������� ������ </param>
    public void SetOneRuneText(int currentValue, int targetValue) {
        if(targetValue > 0) {
            _oneRuneText.text = $"{currentValue}/{targetValue}";
        } else {
            _oneRuneText.text = defaultValueRunsText;
        }
        
    }

    /// <summary>
    /// ������ ��� ������ ������ ����
    /// </summary>
    /// <param name="currentValue"> ������� ����������� ���� � ��������� </param>
    /// <param name="targetValue"> ����������� ����������� ��� ��� ���������� ������ </param>
    public void SetTwoRuneText(int currentValue, int targetValue) {
        if(targetValue > 0) {
            _twoRuneText.text = $"{currentValue}/{targetValue}";
        } else {
            _twoRuneText.text = defaultValueRunsText;
        }
    }

    /// <summary>
    /// ������ ��� ������ ������� ����
    /// </summary>
    /// <param name="currentValue"> ������� ����������� ���� � ��������� </param>
    /// <param name="targetValue"> ����������� ����������� ��� ��� ���������� ������ </param>
    public void SetThreeRuneText(int currentValue, int targetValue) {
        if(targetValue > 0) {
            _threeRuneText.text = $"{currentValue}/{targetValue}";
        } else {
            _threeRuneText.text = defaultValueRunsText;
        }
    }

    /// <summary>
    /// ������ ��� ������ ��������� ����
    /// </summary>
    /// <param name="currentValue"> ������� ����������� ���� � ��������� </param>
    /// <param name="targetValue"> ����������� ����������� ��� ��� ���������� ������ </param>
    public void SetFourRuneText(int currentValue, int targetValue) {
        if(targetValue > 0) {
            _fourRuneText.text = $"{currentValue}/{targetValue}";
        } else {
            _fourRuneText.text = defaultValueRunsText;
        }
    }

    #endregion

    #region ������� ��� ��������

    /// <summary>
    /// ������ ��� �������� ������� ����� ���
    /// </summary>
    /// <param name="tagRune"> ��� ���� ������� ����� ���������� </param>
    /// <param name="levelRune"> ������� ���� ������� ����� ���������� </param>
    public void SetOneRawImage(string tagRune, int levelRune) {
        if(ResourcesTags.Log_1.ToString() == tagRune || ResourcesTags.Log_2.ToString() == tagRune || 
           ResourcesTags.Log_3.ToString() == tagRune || ResourcesTags.Log_4.ToString() == tagRune) {

            _oneResoucesRawImage.texture = _logTextures[levelRune - 1];
        }
    }

    /// <summary>
    /// ������ ��� �������� ������� ����� ���
    /// </summary>
    /// <param name="tagRune"> ��� ���� ������� ����� ���������� </param>
    /// <param name="levelRune"> ������� ���� ������� ����� ���������� </param>
    public void SetTwoRawImage(string tagRune, int levelRune) {
       if(ResourcesTags.Neil_1.ToString() == tagRune || ResourcesTags.Neil_2.ToString() == tagRune ||
           ResourcesTags.Neil_3.ToString() == tagRune || ResourcesTags.Neil_4.ToString() == tagRune) {

                _twoResoucesRawImage.texture = _neilTextures[levelRune - 1];
        }
    }

    /// <summary>
    /// ������ ��� �������� �������� ����� ���
    /// </summary>
    /// <param name="tagRune"> ��� ���� ������� ����� ���������� </param>
    /// <param name="levelRune"> ������� ���� ������� ����� ���������� </param>
    public void SetThreeRawImage(string tagRune, int levelRune) {
        if(ResourcesTags.Stone_1.ToString() == tagRune || ResourcesTags.Stone_2.ToString() == tagRune ||
           ResourcesTags.Stone_3.ToString() == tagRune || ResourcesTags.Stone_4.ToString() == tagRune) {

            _threeResoucesRawImage.texture = _stoneTextures[levelRune - 1];
        }
    }

    /// <summary>
    /// ������ ��� �������� ��������� ����� ���
    /// </summary>
    /// <param name="tagRune"> ��� ���� ������� ����� ���������� </param>
    /// <param name="levelRune"> ������� ���� ������� ����� ���������� </param>
    public void SetFourRawImage(string tagRune, int levelRune) {      
        if(ResourcesTags.Cloth_1.ToString() == tagRune || ResourcesTags.Cloth_2.ToString() == tagRune ||
           ResourcesTags.Cloth_3.ToString() == tagRune || ResourcesTags.Cloth_4.ToString() == tagRune) {

            _fourResoucesRawImage.texture = _clothTextures[levelRune - 1];
        }
    }

    #endregion

    #region ������� ��� �������� ��������

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
