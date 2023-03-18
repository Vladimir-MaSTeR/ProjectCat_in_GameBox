using System;
using System.Collections.Generic;
using UnityEngine;

public class EventsResources
{
    //"Изменить Кол-во Ткани <Ур предмета, колл ресурса, (0 = убавить. 1 прибавить)>"
    public static Action<int, int, int> onClouthInBucket; // ТКАНЬ | Первый ар. - Ур предмета.
                                                          //         Второй ар. - колл ресурса.
                                                          //         Третий ар. - прибавить или убавить ресурс (0 = убавить. 1 прибавить)

    //" Изменить Кол-во дерева <Ур предмета, колл ресурса, (0 = убавить. 1 прибавить)>"
    public static Action<int, int, int> onLogInBucket;   // ДЕРЕВО | Первый ар. - Ур предмета.
                                                         //          Второй ар. - колл ресурса.
                                                         //          Третий ар. - прибавить или убавить ресурс (0 = убавить. 1 прибавить)   

    //Изменить Кол-во каманя<Ур предмета, колл ресурса, (0 = убавить. 1 прибавить)>"
    public static Action<int, int, int> onStoneInBucket;  // КАМЕНЬ | Первый ар. - Ур предмета.
                                                          //          Второй ар. - колл ресурса.
                                                          //          Третий ар. - прибавить или убавить ресурс (0 = убавить. 1 прибавить)  

    [Tooltip(" Изменить Кол-во гвоздей <Ур предмета, колл ресурса, (0 = убавить. 1 прибавить)>")]
    public static Action<int, int, int> onNeilInBucket;  // ГВОЗДИ | Первый ар. - Ур предмета.
                                                         //          Второй ар. - колл ресурса.
                                                         //          Третий ар. - прибавить или убавить ресурс (0 = убавить. 1 прибавить)

    public static Action<bool> onHoldBucket;


    public static Func<int, int> onGetCurentClouth; // возвращает подписчику текущее колличество ткани
    public static Func<int, int> onGetCurentLog;    // возвращает подписчику текущее колличество дерева
    public static Func<int, int> onGetCurentStone;  // возвращает подписчику текущее колличество камня
    public static Func<int, int> onGetCurentNeil;   // возвращает подписчику текущее колличество гвоздей


//---------------------------------------------------------------------------------------------------------------------------------
    public static Func<int> onGetComfortCurrentValue; // возвращает подписчику текущее колличество Уюта
    public static Action<int, bool> onAddOrDeductComfortValue; // Изменяет Кол-во Уюта
                                                               //   Первый ар. - кол-во ресурса.
                                                               //   Второй ар. - прибавить или убавить ресурс.(true = прибавить. false убавить)
    public static Action<int> onUpdateComfortValue; // эвент вызывается для обновления текста уюта

    public static Func<int> onGetSparkCurrentValue;   // возвращает подписчику текущее колличество Искорок
    public static Action<int, bool> onAddOrDeductSparkValue; // Изменяет Кол-во Искорки
                                                             //   Первый ар. - кол-во ресурса.
                                                             //   Второй ар. - прибавить или убавить ресурс.(true = прибавить. false убавить)
    public static Action<int> onUpdateSparkValue; // эвент вызывается для обновления текста искорки

    //---------------------------------------------------------------------------------------------------------------------------------

    [Tooltip("Эвент передает уровень нужных ресурсов а получает словарь со значениеями ресурсов для КАМИНА")]
    public static Func<int, IDictionary<string, int>> onGetFireplaceDictionary;

    [Tooltip("Эвент передает уровень нужных ресурсов а получает словарь со значениеями ресурсов для СТУЛА")]
    public static Func<int, IDictionary<string, int>> onGetChairDictionary;

    [Tooltip("Эвент передает уровень нужных ресурсов а получает словарь со значениеями ресурсов для СТОЛА")]
    public static Func<int, IDictionary<string, int>> onGetTableDictionary;

    [Tooltip("возвращает подписчику словарь объектов дома и их уровень в доме")]
    public static Func<int, IDictionary<string, int>> onGetLvObjHome;

    [Tooltip("возвращает подписчику словарь условий для выполнения КВЕСТА")]
    //public static Func<int, IDictionary<string, int>> onGetQuestsDictionary;


    public static Action onUpdateQuest;
    
    [Tooltip("Эвент завершения квеста  по апгрейду камина, запуская эвент нужно отправить уровень камина")]
    public static Action onEndFireplaceQuest;

    [Tooltip("Эвент завершения квеста  по апгрейду стула, запуская эвент нужно отправить уровень стула")]
    public static Action onEndChairQuest;

    [Tooltip("Эвент завершения квеста  по апгрейду стола, запуская эвент нужно отправить уровень стола")]
    public static Action onEndTableQuest;

    [Tooltip("Эвент запускает активацию готовности камина по апгрейду ")]
    public static Action onAnimationReadyUpFireplace;

    [Tooltip("Эвент запускает активацию готовности стула по апгрейду")]
    public static Action onAnimationReadyUpChair;

    [Tooltip("Эвент запускает активацию готовности  стола по апгрейду")]
    public static Action onAnimationReadyUpTable;


    [Tooltip("Эвент завершения главного квеста")]
    public static Action onEndMainQuest;

    [Tooltip("Эвент активания квеста  по апгрейду камина")]
    public static Action<int> onFireplaceQuest;
    [Tooltip("Эвент активания квеста  по апгрейду стула")]
    public static Action<int> onChairQuest;
    [Tooltip("Эвент активания квеста  по апгрейду стола")]
    public static Action<int> onTableQuest;

    [Tooltip("Эвент для заказа спавна предмета")]
    public static Action<string> onSpawnItem;



}