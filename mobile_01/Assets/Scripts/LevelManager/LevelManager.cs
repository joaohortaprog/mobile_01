using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;

    [Header("Levels")]
    public List<SOLevelSetup> levels;

    [Header("Sections")]
    public List<LevelSectionBase> levelSectionsStart;
    public List<LevelSectionBase> levelSections;
    public List<LevelSectionBase> levelSectionsFinish;


    //public float timeBetweenSections = .3f;


    [SerializeField] private int _index;
    [SerializeField] private SOLevelSetup _currentLevel;

    [SerializeField] private List<LevelSectionBase> _spawnedSections = new List<LevelSectionBase>();


    private void Start()
    {
        //SpawnNextLevel();
        CreateLevelSections();
    }

    private void NextLevel()
    {
        if (levels == null || levels.Count == 0)
        {
            Debug.LogWarning("Nenhum nivel configurado!");
            return;
        }

        _index++;

        if (_index >= levels.Count)
        {
            ResetLevelIndex();
        }

        CreateLevelSections();
    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }

    #region Levels

    private void CreateLevelSections()
    {
        if (levels == null || levels.Count == 0)
        {
            Debug.LogWarning("Nenhum nivel configurado!");
            return;
        }

        CleanSpawnedSections();

        _currentLevel = levels[_index];

        for (int i = 0; i < _currentLevel.numberOfStartSections; i++)
        {
            CreateLevelSection(levelSectionsStart);
        }

        for (int i = 0; i < _currentLevel.numberOfSections; i++)
        {
            CreateLevelSection(levelSections);
        }

        for (int i = 0; i < _currentLevel.numberOfFinishSections; i++)
        {
            CreateLevelSection(levelSectionsFinish);
        }

        ColorManager.Instance.ChangeColorByType(_currentLevel.artType);
    }

    private void CreateLevelSection(List<LevelSectionBase> list)
    {
        if (list == null || list.Count == 0)
        {
            Debug.LogWarning("Lista de secoes vazia!");
            return;
        }

        var section = list[Random.Range(0, list.Count)];
        var spawnedSection = Instantiate(section, container);

        if (_spawnedSections.Count > 0)
        {
            var lastSection = _spawnedSections[_spawnedSections.Count - 1];

            spawnedSection.transform.position = lastSection.sectionFinish.position;
        }

        var artSetup = ArtManager.Instance.GetSetupByType(_currentLevel.artType);

        if (artSetup != null && artSetup.gameObject != null)
        {
            foreach (var p in spawnedSection.GetComponentsInChildren<ArtPiece>())
            {
                p.ChangePiece(artSetup.gameObject);
            }
        }
        else
        {
            Debug.LogWarning("Configuracao de arte nao encontrada!");
        }

        _spawnedSections.Add(spawnedSection);
    }

    private void CleanSpawnedSections()
    {
        for(int i = _spawnedSections.Count -1; i >= 0; i--)
        {
            Destroy(_spawnedSections[i].gameObject);
        }

        _spawnedSections.Clear();
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
        if (Input.GetKeyDown(KeyCode.D))
        {
            CreateLevelSections();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }
    }
}
