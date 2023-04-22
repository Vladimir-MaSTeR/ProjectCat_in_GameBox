using System;
using UnityEngine;
public class FireplacePanel : MonoBehaviour {

    #region ���������� ������

   

    [Tooltip("�������������� ������ ������")]
    [SerializeField]
    private GameObject _fireplacePanel;

    [Tooltip("������ � ������� ����� ������� � ���������� ���")]
    [SerializeField]
    private UpdateQuestsText _updateQuestsTextScript;

    #endregion

    #region ��������� ����������
    private Texture[] _logTextures;
    private Texture[] _neilTextures;
    private Texture[] _stoneTextures;
    private Texture[] _clothTextures;

    #endregion

    private void Start() {
        _fireplacePanel.SetActive(false);

        _logTextures = _updateQuestsTextScript.GetLogTextures();
        _neilTextures = _updateQuestsTextScript.GetNeilTexture();
        _stoneTextures = _updateQuestsTextScript.GetStoneTexture();
        _clothTextures = _updateQuestsTextScript.GetClothTextures();
    }

    private void OnEnable() {
        HomeEvents.onOpenFireplacePanel += OpenFireplacePanel;
    }

    private void OnDisable() {
        HomeEvents.onOpenFireplacePanel -= OpenFireplacePanel;
    }

    private void OpenFireplacePanel(int id) {
        if(HomeConstants.idFireplace == id) {
            _fireplacePanel.SetActive(true);    
        }
    }
}
