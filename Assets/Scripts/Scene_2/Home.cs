using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{

    [Header("объект предмета дома")]
    [Tooltip("объект дома камина")]
    [SerializeField] private GameObject _fireplaceObje;
    [Tooltip("объект дома кресло")]
    [SerializeField] private GameObject _armchairObje;
    [Tooltip("объект дома Кухни")]
    [SerializeField] private GameObject _kitchenObje;
    [Tooltip("объект дома Лестница на 2 этаж")]
    [SerializeField] private GameObject _ladderGoTo2Obje;
    [Tooltip("объект дома кровати")]
    [SerializeField] private GameObject _bedObje;
    [Tooltip("объект дома Лестница на 3 этаж")]
    [SerializeField] private GameObject _ladderGoTo3Obje;
    [Tooltip("объект дома Стол с картой")]
    [SerializeField] private GameObject _cupboardObje;

    [Header("Уровень предмет дома")]
    [Tooltip("Уровень камина")]
    private int _fireplaceLv;
    [Tooltip("Уровень кресло")]
    private int _armchairLv;
    [Tooltip("Уровень Кухни")]
    private int _kitchenLv;
    [Tooltip("Уровень Лестница на 2 этаж")]
    private int _ladderGoTo2Lv;
    [Tooltip("Уровень кровати")]
    private int _bedLv;
    [Tooltip("Уровень Лестница на 3 этаж")]
    private int _ladderGoTo3Lv;
    [Tooltip("Уровень Стол с картой")]
    private int _cupboardLv;
    [Tooltip("Словарь для дома")]
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
    /// Проверка уронвя обьектов в доме
    /// </summary>
    private IDictionary<string, int> _checkLvObjHomeNow(int levelQuests)
    {
        _homeDictionary.Clear();
        AddLvObjHomeDictionary(_homeDictionary);
        return _homeDictionary;
    }

    [Tooltip("Метод добавления уровня объектов дома в словарь")]
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
                Debug.Log("Квест Вып");
                _checkMainQuests = true;
                EventsResources.onEndMainQuest();

            }
        }
    }
}
