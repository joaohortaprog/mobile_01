using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;

    public List<GameObject> levels;

    [Header("Sections")]
    public List<LevelSectionBase> levelSectionsStart;
    public List<LevelSectionBase> levelSections;
    public List<LevelSectionBase> levelSectionsFinish;


    public int numberOfStartSections = 1;
    public int numberOfSections = 5;
    public int numberOfFinishSections = 1;

    public float timeBetweenSections = .3f;


    [SerializeField] private int _index;
    private GameObject _currentLevel;
    public List<LevelSectionBase> _spawnedSections;

    private void Awake()
    {
        //SpawnNextLevel();
        CreateLevelSections();
    }

    private void SpawnNextLevel()
    {
        if(_currentLevel != null)
        {
            Destroy(_currentLevel);
            _index++;

            if(_index >= levels.Count)
            {
                ResetLevelIndex();
            }

        }

        _currentLevel = Instantiate(levels[_index], container);
        _currentLevel.transform.localPosition = Vector3.zero;
    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }

    #region Levels
    private void CreateLevelSections()
    {
        _spawnedSections = new List<LevelSectionBase>();

        //FORMA ALTERNATIVA PARA GERAÇÃO USANDO CO-ROUTINE
        /*for (int i = 0; i < numberOfSections; i++)
        {
            CreateLevelSection(CreateLevelSectionCoroutine());
            //yield return new WaitForSeconds(timeBetweenSections);
        }*/

        for (int i = 0; i < numberOfStartSections; i++)
        {
            CreateLevelSection(levelSectionsStart);
        }

        for (int i = 0; i < numberOfSections; i++)
        {
            CreateLevelSection(levelSections);
        }
        
        for (int i = 0; i < numberOfFinishSections; i++)
        {
            CreateLevelSection(levelSectionsFinish);
        }
    }
    private void CreateLevelSection(List<LevelSectionBase> list)
    {
        var section = list[Random.Range(0, list.Count)];
        var spawnedSection = Instantiate(section, container);

        if (_spawnedSections.Count > 0)
        {
            var lastSection = _spawnedSections[_spawnedSections.Count - 1];

            spawnedSection.transform.position = lastSection.sectionFinish.position;
        }

        _spawnedSections.Add(spawnedSection);
    }

    //FORMA ALTERNATIVA PARA GERAÇÃO USANDO CO-ROUTINE
    /*IEnumerator CreateLevelSectionCoroutine()
    {
        _spawnedSections = new List<LevelSectionBase>();

        for (int i = 0; i < numberOfSections; i++)
        {
            CreateLevelSection(levelSections);
            yield return new WaitForSeconds(timeBetweenSections);
        }
    }*/

    #endregion

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            SpawnNextLevel();
        }
    }
}
