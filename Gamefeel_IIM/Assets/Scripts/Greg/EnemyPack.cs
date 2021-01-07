using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyPack : MonoBehaviour
{
    [Header("Enemies Setup")]
    public int numberOfRows;
    public int numberOfColumns;

    public float offsetX;
    public float offsetY;

    public GameObject Enemy;

    private List<Enemies[]> _enemies2DArray;
    
    [Header("Enemies parameters")]
    public float speedHorizontal;
    public float speedVertical;
    public float delayBeforeGoingDown = 0.4f;
    public float timeBetweenMovement;
    public Vector2 enemyScale;
    
    
    
    private float _timer = 0;
    
    private int _orientation = 1; //1 = right, -1 = left

    private bool _hasMovedDown = false;
    private bool stopHorizontalMovement = false;
    public int minMoveToGoDownAgain = 2;
    private int currentMoveDownCount = 2;
    
    private int _randomColumnShooting = 0;
    private float _randomShootingTimer = 0;

    private bool _canShootTimerGoDown = false;
    private int numberOfEnemies = 0;
    
    public float minimumTimeToShootAgain = 1;
    public float maximumTimeToShootAgain = 5;
    

    // Start is called before the first frame update
    void Start()
    {
        _enemies2DArray = new List<Enemies[]>();
   
        for (int i = 0; i < numberOfColumns; i++)
        {
            Enemies[] enemiesColumn = new Enemies[numberOfRows];
            for (int j = 0; j < numberOfRows; j++)
            {
                GameObject newEnemy = GameObject.Instantiate(Enemy, new Vector2(transform.localPosition.x + offsetX * i, transform.localPosition.y - offsetY * j), Quaternion.identity, this.transform);
                newEnemy.transform.localScale = enemyScale;
                newEnemy.name = "Enemy " + i + " " + j;
                enemiesColumn[j] = newEnemy.GetComponent<Enemies>();
                enemiesColumn[j].SetIds(i, j);
                enemiesColumn[j].ChangeDirection(_orientation);
            }
            _enemies2DArray.Add(enemiesColumn);
        }

        numberOfEnemies = numberOfColumns * numberOfRows;
        
        SetNewShootingTimer();
        currentMoveDownCount = minMoveToGoDownAgain;
    }

    public void RemoveColumn(int columnId, int rowID)
    {
        _enemies2DArray[columnId][rowID] = null;
        numberOfEnemies -= 1;
        CheckIfWin();
        
        if (CheckIfStillEnemiesOnColumn(columnId)) return;
        
        for (int i = columnId+1; i < _enemies2DArray.Count; i++)
        {
            for (int j = 0; j < _enemies2DArray[i].Length; j++)
            {
                if (_enemies2DArray[i][j] != null)
                {
                    _enemies2DArray[i][j].ReduceColumnNumber();
                }
            }
        }
        _enemies2DArray.Remove(_enemies2DArray[columnId]);
    }

    private void CheckIfWin()
    {
        if (numberOfEnemies > 0) return;
        
        GameManager.Instance.winGame();
    }

    private bool CheckIfStillEnemiesOnColumn(int columnId)
    {
        for (int i = 0; i < _enemies2DArray[columnId].Length; i++)
        {
            if (_enemies2DArray[columnId][i] != null)
            {
                return true; 
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopHorizontalMovement) return;
        
        _timer += Time.deltaTime;
        if(_timer >= timeBetweenMovement)
        {
            _timer = 0;
            Move();
            Debug.Log("Timer down");
            Debug.Log(_timer);
        }


        if (!_canShootTimerGoDown) return;
        _randomShootingTimer -= Time.deltaTime;

        if (_randomShootingTimer <= 0)
        {
            _canShootTimerGoDown = false;
            Shoot();
        }
    }

    public void Shoot()
    {
        _randomColumnShooting = Random.Range(0, _enemies2DArray.Count);
        
        Enemies enemyShooting = null;
        for (int i = 0; i < _enemies2DArray[_randomColumnShooting].Length; i++)
        {
            if (_enemies2DArray[_randomColumnShooting][i] != null)
            {
                enemyShooting = _enemies2DArray[_randomColumnShooting][i];
            }
        }

        if (enemyShooting != null)
        { 
            enemyShooting.StartShoot();
        }

        SetNewShootingTimer();
    }

    public void SetNewShootingTimer()
    {
        _randomShootingTimer = Random.Range(minimumTimeToShootAgain, maximumTimeToShootAgain);
        _canShootTimerGoDown = true;
    }

    #region Move
    private void Move()
    {
        transform.position = new Vector2(transform.position.x + speedHorizontal * _orientation, transform.position.y);
        ResidualEffectAllEnnemies();
        if (!_hasMovedDown) return;

        currentMoveDownCount -= 1;
        if (currentMoveDownCount > 0) return;
        _hasMovedDown = false;
        currentMoveDownCount = minMoveToGoDownAgain;
    }

    public void MoveDown()
    {
        if (_hasMovedDown) return;
        _hasMovedDown = true;
        stopHorizontalMovement = true;
        StartCoroutine(MoveDownCoroutine());
    }
    
    private IEnumerator MoveDownCoroutine()
    {
        yield return new WaitForSeconds(delayBeforeGoingDown);
        
        transform.position = new Vector2(transform.position.x, transform.position.y - speedVertical);
        ResidualEffectAllEnnemies();
        ChangeDirection();
        stopHorizontalMovement = false;
    }

    private void ChangeDirection()
    {
        if (_orientation == 1)
        {
            _orientation = -1;
        }
        else
        {
            _orientation = 1;
        }
        ChangeDirectionForAllEnemiesLeft(_orientation);
    }

    private void ChangeDirectionForAllEnemiesLeft(int direction)
    {
        for (int i = 0; i < _enemies2DArray.Count; i++)
        {
            for (int j = 0; j < _enemies2DArray[i].Length; j++)
            {
                if (_enemies2DArray[i][j] != null)
                {
                    _enemies2DArray[i][j].ChangeDirection(direction);
                }
            }
        }
    }


    private void ResidualEffectAllEnnemies()
    {
        for (int i = 0; i < _enemies2DArray.Count; i++)
        {
            for (int j = 0; j < _enemies2DArray[i].Length; j++)
            {
                if (_enemies2DArray[i][j] != null)
                {
                    StartCoroutine(_enemies2DArray[i][j].EnnemyResidual(_enemies2DArray[i][j].transform.position));
                }
            }
        }
    }
    #endregion
}
