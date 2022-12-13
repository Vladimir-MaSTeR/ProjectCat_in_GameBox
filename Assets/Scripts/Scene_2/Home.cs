using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{

    [Header("������ �������� ����")]
    [Tooltip("������ ���� ������")]
    [SerializeField] private GameObject _fireplaceObje;
    [Tooltip("������ ���� ������")]
    [SerializeField] private GameObject _armchairObje;
    [Tooltip("������ ���� �����")]
    [SerializeField] private GameObject _kitchenObje;
    [Tooltip("������ ���� �������� �� 2 ����")]
    [SerializeField] private GameObject _ladderGoTo2Obje;
    [Tooltip("������ ���� �������")]
    [SerializeField] private GameObject _bedObje;
    [Tooltip("������ ���� �������� �� 3 ����")]
    [SerializeField] private GameObject _ladderGoTo3Obje;
    [Tooltip("������ ���� ���� � ������")]
    [SerializeField] private GameObject _cupboardObje;

    [Header("������� ������� ����")]
    [Tooltip("������� ������")]
    private int _fireplaceLv;
    [Tooltip("������� ������")]
    private int _armchairLv;
    [Tooltip("������� �����")]
    private int _kitchenLv;
    [Tooltip("������� �������� �� 2 ����")]
    private int _ladderGoTo2Lv;
    [Tooltip("������� �������")]
    private int _bedLv;
    [Tooltip("������� �������� �� 3 ����")]
    private int _ladderGoTo3Lv;
    [Tooltip("������� ���� � ������")]
    private int _cupboardLv;
    [Tooltip("������� ��� ����")]
    private IDictionary<string, int> _homeDictionary = new Dictionary<string, int>();


    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        _checkQuests();
    }

    private void OnEnable()
    {
         EventsResources.onGetLvObjHome += _checkLvObjHomeNow;

    }

    private void OnDisable()
    {
          EventsResources.onGetLvObjHome -= _checkLvObjHomeNow;

    }

    /// <summary>
    /// �������� ������ �������� � ����
    /// </summary>
    private IDictionary<string, int> _checkLvObjHomeNow(int levelQuests)
    {
        _homeDictionary.Clear();
        AddLvObjHomeDictionary(_homeDictionary);
        return _homeDictionary;
    }

    [Tooltip("����� ���������� ������ �������� ���� � �������")]
    private void AddLvObjHomeDictionary(IDictionary<string, int> homeDictionary)
    {
        if (homeDictionary.Count == 0)
        {
            homeDictionary.Add(_fireplaceObje.GetComponent<ClickFireplace>().NameObject.ToString(),
                                _fireplaceObje.GetComponent<ClickFireplace>().LvObj);
            homeDictionary.Add(_armchairObje.GetComponent<ClickFireplace>().NameObject.ToString(),
                                _armchairObje.GetComponent<ClickFireplace>().LvObj);
            homeDictionary.Add(_kitchenObje.GetComponent<ClickFireplace>().NameObject.ToString(),
                                _kitchenObje.GetComponent<ClickFireplace>().LvObj);
            homeDictionary.Add(_ladderGoTo2Obje.GetComponent<ClickFireplace>().NameObject.ToString(),
                                _ladderGoTo2Obje.GetComponent<ClickFireplace>().LvObj);
            homeDictionary.Add(_bedObje.GetComponent<ClickFireplace>().NameObject.ToString(),
                                _bedObje.GetComponent<ClickFireplace>().LvObj);
            homeDictionary.Add(_ladderGoTo3Obje.GetComponent<ClickFireplace>().NameObject.ToString(),
                                _ladderGoTo3Obje.GetComponent<ClickFireplace>().LvObj);
            homeDictionary.Add(_cupboardObje.GetComponent<ClickFireplace>().NameObject.ToString(),
                               _cupboardObje.GetComponent<ClickFireplace>().LvObj);
        }
    }

    bool _checkMainQuests = false;

    private void _checkQuests()
    { if (_checkMainQuests == false)
        {
            // onGetQuestsDictionary
            bool _fireplaceQuests = false;
            bool _armchairQuests = false;
            bool _kitchenQuests = false;

            if (_fireplaceObje.GetComponent<ClickFireplace>().LvObj > 0)
            {
                _fireplaceQuests = true;
            }
            if (_armchairObje.GetComponent<ClickFireplace>().LvObj > 0)
            {
                _armchairQuests = true;
            }
            if (_kitchenObje.GetComponent<ClickFireplace>().LvObj > 0)
            {
                _kitchenQuests = true;
            }

            if (_kitchenObje == true && _armchairQuests == true && _fireplaceQuests == true)
            {
                Debug.Log("����� ���");
                _checkMainQuests = true;
                EventsResources.onEndMainQuest();

            }
        }
    }
}
