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

    [Tooltip("туман на 2 этаже")]
    [SerializeField] private GameObject _mist2storey;
    [SerializeField] Animation _anim_Open2storey;
    [Tooltip("туман на 3 этаже")]
    [SerializeField] private GameObject _mist3storey;
    [SerializeField] Animation _anim_Open3storey;

    [SerializeField]
    private int _openStorey = 0;


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
        LoadResouces();

        if (_openStorey >= 1)
        {
            offMist2Stroery();
        }
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
            homeDictionary.Add(_fireplaceObje.GetComponent<ClickRepair>().NameObject.ToString(),
                                _fireplaceObje.GetComponent<ClickRepair>().LvObj);
            homeDictionary.Add(_armchairObje.GetComponent<ClickRepair>().NameObject.ToString(),
                                _armchairObje.GetComponent<ClickRepair>().LvObj);
            homeDictionary.Add(_kitchenObje.GetComponent<ClickRepair>().NameObject.ToString(),
                                _kitchenObje.GetComponent<ClickRepair>().LvObj);
            homeDictionary.Add(_ladderGoTo2Obje.GetComponent<ClickRepair>().NameObject.ToString(),
                                _ladderGoTo2Obje.GetComponent<ClickRepair>().LvObj);
            homeDictionary.Add(_bedObje.GetComponent<ClickRepair>().NameObject.ToString(),
                                _bedObje.GetComponent<ClickRepair>().LvObj);
            homeDictionary.Add(_ladderGoTo3Obje.GetComponent<ClickRepair>().NameObject.ToString(),
                                _ladderGoTo3Obje.GetComponent<ClickRepair>().LvObj);
            homeDictionary.Add(_cupboardObje.GetComponent<ClickRepair>().NameObject.ToString(),
                               _cupboardObje.GetComponent<ClickRepair>().LvObj);
        }
    }


    private void _checkQuests()
    {
        {
            // onGetQuestsDictionary
            bool _fireplaceQuests = false;
            bool _armchairQuests = false;
            bool _kitchenQuests = false;
            if (_openStorey == 0) /// Квест - открыть 2 этаж
            {
                if (_fireplaceObje.GetComponent<ClickRepair>().LvObj > 0)
                {
                    _fireplaceQuests = true;
                }
                if (_armchairObje.GetComponent<ClickRepair>().LvObj > 0)
                {
                    _armchairQuests = true;
                }
                if (_kitchenObje.GetComponent<ClickRepair>().LvObj > 0)
                {
                    _kitchenQuests = true;
                }

                if (_kitchenQuests == true && _armchairQuests == true && _fireplaceQuests == true)
                {
                    ///Debug.Log("Квест Вып");
                    EventsResources.onEndMainQuest?.Invoke();
                    //_mist2storey.GetComponent<Animator>().enabled = false;
                    _mist2storey.GetComponent<Animator>().SetBool("OperStorey", true);
                    _mist2storey.GetComponent<Animator>().SetTrigger("Oper2storey");

                    //   _anim_Open2storey; //m_Controller
                    _openStorey = 1;
                    SaveResources();
                    Debug.Log("Этаж открыт" + _openStorey + 1);
                    Invoke("offMist2Stroery", 10f);
                    Debug.Log("Убрать туман 2 этаж");

                }
            }
        }
    }



    private void offMist2Stroery()
    {
        _mist2storey.SetActive(false);
    }




    /// <summary>
    /// Готовность к улучшению (анимация)
    /// </summary>
    private void _readyUp()
    {


    }


    
    private void SaveResources()
    {
        PlayerPrefs.SetInt("OpenHomeStorey", _openStorey);


        
        PlayerPrefs.Save();
    }

    private void LoadResouces()
    {
        
        {
            if (PlayerPrefs.HasKey("OpenHomeStorey"))
            {
                _openStorey = PlayerPrefs.GetInt("OpenHomeStorey");
            }



        }
    }
}
