using System.Collections.Generic;
using UnityEngine;

public class QuestsResources : MonoBehaviour
{
    [Header("Камин 1ур")]
    [SerializeField] private int _fireplaceStone_1lv = 6;
    [SerializeField] private int _fireplaceLog_1lv = 3;
    [SerializeField] private int _fireplaceNeil_1lv = -1; // - 1 значит что ресурс не требуется для починки предмета.
    [SerializeField] private int _fireplaceCloth_1lv = 3;

    [Header("Камин 2ур")]
    [SerializeField] private int _fireplaceStone_2lv = 10;
    [SerializeField] private int _fireplaceLog_2lv = 5;
    [SerializeField] private int _fireplaceNeil_2lv = -1; // - 1 значит что ресурс не требуется для починки предмета.
    [SerializeField] private int _fireplaceCloth_2lv = 5;

    [Header("Камин 3ур")]
    [SerializeField] private int _fireplaceStone_3lv = 14;
    [SerializeField] private int _fireplaceLog_3lv = 8;
    [SerializeField] private int _fireplaceNeil_3lv = -1; // - 1 значит что ресурс не требуется для починки предмета.
    [SerializeField] private int _fireplaceCloth_3lv = 8;

    [Header("Стул 1ур")]
    [SerializeField] private int _chairStone_1lv = 3;
    [SerializeField] private int _chairLog_1lv = 4;
    [SerializeField] private int _chairNeil_1lv = 5;
    [SerializeField] private int _chairCloth_1lv = -1;
    [Header("Стул 2ур")]
    [SerializeField] private int _chairStone_2lv = 5;
    [SerializeField] private int _chairLog_2lv = 6;
    [SerializeField] private int _chairNeil_2lv = 9;
    [SerializeField] private int _chairCloth_2lv = -1;
    [Header("Стул 3ур")]
    [SerializeField] private int _chairStone_3lv = 8;
    [SerializeField] private int _chairLog_3lv = 9;
    [SerializeField] private int _chairNeil_3lv = 13;
    [SerializeField] private int _chairCloth_3lv = -1;

    [Header("Стол 1ур")]
    [SerializeField] private int _tableStone_1lv = -1;
    [SerializeField] private int _tableLog_1lv = 2;
    [SerializeField] private int _tableNeil_1lv = 4;
    [SerializeField] private int _tableCloth_1lv = 6;
    [Header("Стол 2ур")]
    [SerializeField] private int _tableStone_2lv = -1;
    [SerializeField] private int _tableLog_2lv = 4;
    [SerializeField] private int _tableNeil_2lv = 6;
    [SerializeField] private int _tableCloth_2lv = 10;
    [Header("Стол 3ур")]
    [SerializeField] private int _tableStone_3lv = -1;
    [SerializeField] private int _tableLog_3lv = 7;
    [SerializeField] private int _tableNeil_3lv = 9;
    [SerializeField] private int _tableCloth_3lv = 14;


    [Tooltip("Словарь для камина")]
    private IDictionary<string, int> _fireplaceDictionary_1lv = new Dictionary<string, int>();
    private IDictionary<string, int> _fireplaceDictionary_2lv = new Dictionary<string, int>();
    private IDictionary<string, int> _fireplaceDictionary_3lv = new Dictionary<string, int>();

    [Tooltip("Словарь для стула")]
    private IDictionary<string, int> _chairDictionary_1lv = new Dictionary<string, int>();
    private IDictionary<string, int> _chairDictionary_2lv = new Dictionary<string, int>();
    private IDictionary<string, int> _chairDictionary_3lv = new Dictionary<string, int>();

    [Tooltip("Словарь для стола")]
    private IDictionary<string, int> _tableDictionary_1lv = new Dictionary<string, int>();
    private IDictionary<string, int> _tableDictionary_2lv = new Dictionary<string, int>();
    private IDictionary<string, int> _tableDictionary_3lv = new Dictionary<string, int>();


    private void Start()
    {
        AddResourcesFireplaceDictionary_1lv(_fireplaceDictionary_1lv);
        AddResourcesChairDictionary_1lv(_chairDictionary_1lv);
        AddResourcesTableDictionary_1lv(_tableDictionary_1lv);

        AddResourcesFireplaceDictionary_2lv(_fireplaceDictionary_2lv);
        AddResourcesChairDictionary_2lv(_chairDictionary_2lv);
        AddResourcesTableDictionary_2lv(_tableDictionary_2lv);

        AddResourcesFireplaceDictionary_3lv(_fireplaceDictionary_3lv);
        AddResourcesChairDictionary_3lv(_chairDictionary_3lv);
        AddResourcesTableDictionary_3lv(_tableDictionary_3lv);
    }

    private void OnEnable()
    {
        EventsResources.onGetFireplaceDictionary += GetFireplaceDictionary;
        EventsResources.onGetChairDictionary += GetChairDictionary;
        EventsResources.onGetTableDictionary += GetTableDictionary;
    }

    private void OnDisable()
    {
        EventsResources.onGetFireplaceDictionary -= GetFireplaceDictionary;
        EventsResources.onGetChairDictionary -= GetChairDictionary;
        EventsResources.onGetTableDictionary -= GetTableDictionary;
    }

    [Tooltip("Метод добавления ресурсов для камина 1 уровня в словарь")]
    private void AddResourcesFireplaceDictionary_1lv(IDictionary<string, int> fireplaceDictionary)
    {
        if (fireplaceDictionary.Count == 0)
        {
            fireplaceDictionary.Add(ResourcesTags.Stone_1.ToString(), _fireplaceStone_1lv);
            fireplaceDictionary.Add(ResourcesTags.Log_1.ToString(), _fireplaceLog_1lv);
            fireplaceDictionary.Add(ResourcesTags.Neil_1.ToString(), _fireplaceNeil_1lv);
            fireplaceDictionary.Add(ResourcesTags.Cloth_1.ToString(), _fireplaceCloth_1lv);
        }       
    }

    [Tooltip("Метод добавления ресурсов для камина 2 уровня в словарь")]
    private void AddResourcesFireplaceDictionary_2lv(IDictionary<string, int> fireplaceDictionary)
    {
        if (fireplaceDictionary.Count == 0)
        {
            fireplaceDictionary.Add(ResourcesTags.Stone_2.ToString(), _fireplaceStone_2lv);
            fireplaceDictionary.Add(ResourcesTags.Log_2.ToString(), _fireplaceLog_2lv);
            fireplaceDictionary.Add(ResourcesTags.Neil_2.ToString(), _fireplaceNeil_2lv);
            fireplaceDictionary.Add(ResourcesTags.Cloth_2.ToString(), _fireplaceCloth_2lv);
        }
    }

    [Tooltip("Метод добавления ресурсов для камина 3 уровня в словарь")]
    private void AddResourcesFireplaceDictionary_3lv(IDictionary<string, int> fireplaceDictionary)
    {
        if (fireplaceDictionary.Count == 0)
        {
            fireplaceDictionary.Add(ResourcesTags.Stone_3.ToString(), _fireplaceStone_3lv);
            fireplaceDictionary.Add(ResourcesTags.Log_3.ToString(), _fireplaceLog_3lv);
            fireplaceDictionary.Add(ResourcesTags.Neil_3.ToString(), _fireplaceNeil_3lv);
            fireplaceDictionary.Add(ResourcesTags.Cloth_3.ToString(), _fireplaceCloth_3lv);
        }
    }

    [Tooltip("Метод добавления ресурсов для стула 1 уровня в словарь")]
    private void AddResourcesChairDictionary_1lv(IDictionary<string, int> chairDictionary)
    {
        if (chairDictionary.Count == 0)
        {
            chairDictionary.Add(ResourcesTags.Stone_1.ToString(), _chairStone_1lv);
            chairDictionary.Add(ResourcesTags.Log_1.ToString(), _chairLog_1lv);
            chairDictionary.Add(ResourcesTags.Neil_1.ToString(), _chairNeil_1lv);
            chairDictionary.Add(ResourcesTags.Cloth_1.ToString(), _chairCloth_1lv);
        }
    }
    [Tooltip("Метод добавления ресурсов для стула 2 уровня в словарь")]
    private void AddResourcesChairDictionary_2lv(IDictionary<string, int> chairDictionary)
    {
        if (chairDictionary.Count == 0)
        {
            chairDictionary.Add(ResourcesTags.Stone_2.ToString(), _chairStone_2lv);
            chairDictionary.Add(ResourcesTags.Log_2.ToString(), _chairLog_2lv);
            chairDictionary.Add(ResourcesTags.Neil_2.ToString(), _chairNeil_2lv);
            chairDictionary.Add(ResourcesTags.Cloth_2.ToString(), _chairCloth_2lv);
        }
    }
    [Tooltip("Метод добавления ресурсов для стула 3 уровня в словарь")]
    private void AddResourcesChairDictionary_3lv(IDictionary<string, int> chairDictionary)
    {
        if (chairDictionary.Count == 0)
        {
            chairDictionary.Add(ResourcesTags.Stone_3.ToString(), _chairStone_3lv);
            chairDictionary.Add(ResourcesTags.Log_3.ToString(), _chairLog_3lv);
            chairDictionary.Add(ResourcesTags.Neil_3.ToString(), _chairNeil_3lv);
            chairDictionary.Add(ResourcesTags.Cloth_3.ToString(), _chairCloth_3lv);
        }
    }

    [Tooltip("Метод добавления ресурсов для стола 1 уровня в словарь")]
    private void AddResourcesTableDictionary_1lv(IDictionary<string, int> tableDictionary)
    {
        if (tableDictionary.Count == 0)
        {
            tableDictionary.Add(ResourcesTags.Stone_1.ToString(), _tableStone_1lv);
            tableDictionary.Add(ResourcesTags.Log_1.ToString(), _tableLog_1lv);
            tableDictionary.Add(ResourcesTags.Neil_1.ToString(), _tableNeil_1lv);
            tableDictionary.Add(ResourcesTags.Cloth_1.ToString(), _tableCloth_1lv);
        }
    }

    [Tooltip("Метод добавления ресурсов для стола 2 уровня в словарь")]
    private void AddResourcesTableDictionary_2lv(IDictionary<string, int> tableDictionary)
    {
        if (tableDictionary.Count == 0)
        {
            tableDictionary.Add(ResourcesTags.Stone_2.ToString(), _tableStone_2lv);
            tableDictionary.Add(ResourcesTags.Log_2.ToString(), _tableLog_2lv);
            tableDictionary.Add(ResourcesTags.Neil_2.ToString(), _tableNeil_2lv);
            tableDictionary.Add(ResourcesTags.Cloth_2.ToString(), _tableCloth_2lv);
        }
    }

    [Tooltip("Метод добавления ресурсов для стола 3 уровня в словарь")]
    private void AddResourcesTableDictionary_3lv(IDictionary<string, int> tableDictionary)
    {
        if (tableDictionary.Count == 0)
        {
            tableDictionary.Add(ResourcesTags.Stone_3.ToString(), _tableStone_3lv);
            tableDictionary.Add(ResourcesTags.Log_3.ToString(), _tableLog_3lv);
            tableDictionary.Add(ResourcesTags.Neil_3.ToString(), _tableNeil_3lv);
            tableDictionary.Add(ResourcesTags.Cloth_3.ToString(), _tableCloth_3lv);
        }
    }

    [Tooltip("Метод возвращает словарь со значениеями ресурсов для КАМИНА через эвент")]
    private IDictionary<string, int> GetFireplaceDictionary(int levelObject)
    {

        if (levelObject == 3)
        {
            AddResourcesFireplaceDictionary_3lv(_fireplaceDictionary_3lv);
            return _fireplaceDictionary_3lv;
        }
        else if (levelObject == 2)
        {
            AddResourcesFireplaceDictionary_2lv(_fireplaceDictionary_2lv);
            return _fireplaceDictionary_2lv;
        }

        AddResourcesFireplaceDictionary_1lv(_fireplaceDictionary_1lv);
        return _fireplaceDictionary_1lv;
    }

    [Tooltip("Метод возвращает словарь со значениеями ресурсов для СТУЛА через эвент")]
    private IDictionary<string, int> GetChairDictionary(int levelObject)
    {
        
        if (levelObject == 3)
        {
            AddResourcesChairDictionary_3lv(_chairDictionary_3lv);
            return _chairDictionary_3lv;
        }
        else if (levelObject == 2)
        {
            AddResourcesChairDictionary_2lv(_chairDictionary_2lv);
            return _chairDictionary_2lv;
        }

        AddResourcesChairDictionary_1lv(_chairDictionary_1lv);
        return _chairDictionary_1lv;
    }

    [Tooltip("Метод возвращает словарь со значениеями ресурсов для СТОЛА через эвент")]
    private IDictionary<string, int> GetTableDictionary(int levelObject)
    {

        if (levelObject == 3)
        {
            AddResourcesTableDictionary_3lv(_tableDictionary_3lv);
            return _tableDictionary_3lv;
        }
        else if (levelObject == 2)
        {
            AddResourcesTableDictionary_2lv(_tableDictionary_2lv);
            return _tableDictionary_2lv;
        }

        AddResourcesTableDictionary_1lv(_tableDictionary_1lv);
        return _tableDictionary_1lv;
    }


}
