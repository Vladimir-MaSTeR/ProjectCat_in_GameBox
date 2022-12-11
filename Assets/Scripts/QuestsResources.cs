using System.Collections.Generic;
using UnityEngine;

public class QuestsResources : MonoBehaviour
{
    [Header("Камин 1ур")]
    [SerializeField] private int _fireplaceStone_1lv = 10;
    [SerializeField] private int _fireplaceLog_1lv = 3;
    [SerializeField] private int _fireplaceNeil_1lv = -1; // - 1 значит что ресурс не требуется для починки предмета.
    [SerializeField] private int _fireplaceCloth_1lv = -1;

    [Header("Камин 2ур")]
    [SerializeField] private int _fireplaceStone_2lv = 12;
    [SerializeField] private int _fireplaceLog_2lv = 5;
    [SerializeField] private int _fireplaceNeil_2lv = 3; // - 1 значит что ресурс не требуется для починки предмета.
    [SerializeField] private int _fireplaceCloth_2lv = -1;

    [Header("Стул 1ур")]
    [SerializeField] private int _chairStone_1lv = -1;
    [SerializeField] private int _chairLog_1lv = 8;
    [SerializeField] private int _chairNeil_1lv = 4;
    [SerializeField] private int _chairCloth_1lv = -1;

    [Header("Стол 1ур")]
    [SerializeField] private int _tableStone_1lv = -1;
    [SerializeField] private int _tableLog_1lv = 8;
    [SerializeField] private int _tableNeil_1lv = 4;
    [SerializeField] private int _tableCloth_1lv = -1;


    [Tooltip("Словарь для камина")]
    private IDictionary<string, int> _fireplaceDictionary_1lv = new Dictionary<string, int>();
    private IDictionary<string, int> _fireplaceDictionary_2lv = new Dictionary<string, int>();
    [Tooltip("Словарь для стула")]
    private IDictionary<string, int> _chairDictionary_1lv = new Dictionary<string, int>();
    [Tooltip("Словарь для стола")]
    private IDictionary<string, int> _tableDictionary_1lv = new Dictionary<string, int>();

    private void Start()
    {
        AddResourcesFireplaceDictionary_1lv(_fireplaceDictionary_1lv);
        AddResourcesChairDictionary_1lv(_chairDictionary_1lv);
        AddResourcesTableDictionary_1lv(_tableDictionary_1lv);
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
        fireplaceDictionary.Add(ResourcesTags.Stone_1.ToString(), _fireplaceStone_1lv);
        fireplaceDictionary.Add(ResourcesTags.Log_1.ToString(), _fireplaceLog_1lv);
        fireplaceDictionary.Add(ResourcesTags.Neil_1.ToString(), _fireplaceNeil_1lv);
        fireplaceDictionary.Add(ResourcesTags.Cloth_1.ToString(), _fireplaceCloth_1lv);
    }

    [Tooltip("Метод добавления ресурсов для камина 2 уровня в словарь")]
    private void AddResourcesFireplaceDictionary_2lv(IDictionary<string, int> fireplaceDictionary)
    {
        fireplaceDictionary.Add(ResourcesTags.Stone_2.ToString(), _fireplaceStone_2lv);
        fireplaceDictionary.Add(ResourcesTags.Log_2.ToString(), _fireplaceLog_2lv);
        fireplaceDictionary.Add(ResourcesTags.Neil_2.ToString(), _fireplaceNeil_2lv);
        fireplaceDictionary.Add(ResourcesTags.Cloth_2.ToString(), _fireplaceCloth_2lv);
    }

    [Tooltip("Метод добавления ресурсов для стула 1 уровня в словарь")]
    private void AddResourcesChairDictionary_1lv(IDictionary<string, int> chairDictionary)
    {
        chairDictionary.Add(ResourcesTags.Stone_1.ToString(), _chairStone_1lv);
        chairDictionary.Add(ResourcesTags.Log_1.ToString(), _chairLog_1lv);
        chairDictionary.Add(ResourcesTags.Neil_1.ToString(), _chairNeil_1lv);
        chairDictionary.Add(ResourcesTags.Cloth_1.ToString(), _chairCloth_1lv);
    }

    [Tooltip("Метод добавления ресурсов для стола 1 уровня в словарь")]
    private void AddResourcesTableDictionary_1lv(IDictionary<string, int> tableDictionary)
    {
        tableDictionary.Add(ResourcesTags.Stone_1.ToString(), _tableStone_1lv);
        tableDictionary.Add(ResourcesTags.Log_1.ToString(), _tableLog_1lv);
        tableDictionary.Add(ResourcesTags.Neil_1.ToString(), _tableNeil_1lv);
        tableDictionary.Add(ResourcesTags.Cloth_1.ToString(), _tableCloth_1lv);
    }

    [Tooltip("Метод возвращает словарь со значениеями ресурсов для КАМИНА через эвент")]
    private IDictionary<string, int> GetFireplaceDictionary(int levelObject)
    {
        if (levelObject == 2)
        {
            return _fireplaceDictionary_2lv;
        }
        else if (levelObject == 3)
        {
          //  return _fireplaceDictionary_3lv;
        }

        return _fireplaceDictionary_1lv;
    }

    [Tooltip("Метод возвращает словарь со значениеями ресурсов для СТУЛА через эвент")]
    private IDictionary<string, int> GetChairDictionary(int levelObject)
    {
        if (levelObject == 3)
        {
            //   return _fireplaceDictionary_2lv;
        }
        else if (levelObject == 2)
        {
            //  return _fireplaceDictionary_3lv;
        }

        return _chairDictionary_1lv;
    }

    [Tooltip("Метод возвращает словарь со значениеями ресурсов для СТОЛА через эвент")]
    private IDictionary<string, int> GetTableDictionary(int levelObject)
    {
        if (levelObject == 3)
        {
            //   return _fireplaceDictionary_2lv;
        }
        else if (levelObject == 2)
        {
            //  return _fireplaceDictionary_3lv;
        }

        return _tableDictionary_1lv;
    }

}
