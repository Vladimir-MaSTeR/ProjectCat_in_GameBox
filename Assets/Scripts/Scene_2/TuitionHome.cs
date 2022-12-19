using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuitionHome : MonoBehaviour
{

    [SerializeField] private int _LvTuition = 0;

    [SerializeField] GameObject _canvasTuitionPart1;
    [SerializeField] GameObject _canvasTuitionPart2;
    [SerializeField] GameObject _fireplacePointer;
    [SerializeField] GameObject _canvasTuitionPart3;
    [SerializeField] GameObject _objFireplace;

    void Start()
    {
        LoadResouces();

        if (_LvTuition > 2)
        {
           _dead();
        }
        ChekTuition();

    }

    // Update is called once per frame
    void Update()
    {
        ChekTuition();

        if (_LvTuition != 2)
        {
            if (_objFireplace.GetComponent<ClickRepair>().LvObj == 1)
            {
                _LvTuition = 2;
///                SaveResources();
            }
        }

    }

    private void ChekTuition()
    {
        if (_LvTuition > 2)
        {
            SaveResources();
            Invoke("_dead", 1.5f);
        }

        if (_LvTuition == 0) /// Первая часть обучения до перехода на доску
        {
            _canvasTuitionPart1.SetActive(true);
            _canvasTuitionPart2.SetActive(false);
            _canvasTuitionPart3.SetActive(false);



        }
        if (_LvTuition == 1) /// Вторая часть обучения ремонт камина
        {
            _canvasTuitionPart2.SetActive(true);
            _canvasTuitionPart1.SetActive(false);
            _canvasTuitionPart3.SetActive(false);

            _fireplacePointer.SetActive(true);


        }
        if (_LvTuition == 2)  /// Заверщение обучения 
        {
            _canvasTuitionPart3.SetActive(true);
            _canvasTuitionPart1.SetActive(false);
            _canvasTuitionPart2.SetActive(false);

            _fireplacePointer.SetActive(false);


        }

    }

    private void _dead()
    {
        Destroy(transform.gameObject); 
    }

    public void setLvTuition(int _lvT)
    {
        _LvTuition = _lvT;
        SaveResources();
        Debug.Log("Прогресс обучения " + _LvTuition);
    }


    private void SaveResources()
    {
        PlayerPrefs.SetInt("TuitionHome", _LvTuition);



        PlayerPrefs.Save();
    }

    private void LoadResouces()
    {

        {
            if (PlayerPrefs.HasKey("TuitionHome"))
            {
                _LvTuition = PlayerPrefs.GetInt("TuitionHome");
            }



        }
    }

}
