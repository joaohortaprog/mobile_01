using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;

    public List<GameObject> levels;

    [Header("Sections")]
    public List<LevelSectionBase> levelSections;
    public int numberOfSections = 5;


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

        for(int i = 0; i < numberOfSections; i++)
        {
            CreateLevelSection();
        }
    }
    private void CreateLevelSection()
    {
        var section = levelSections[Random.Range(0, levelSections.Count)];
        var spawnedSection = Instantiate(section, container);

        if (_spawnedSections.Count > 0)
        {
            var lastSection = _spawnedSections[_spawnedSections.Count - 1];

            spawnedSection.transform.position = lastSection.sectionFinish.position;
        }

        _spawnedSections.Add(spawnedSection);
    }
    #endregion

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            SpawnNextLevel();
        }
    }
}
