using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyPack : MonoBehaviour
{
    [Header("Enemies Setup")]
    public int numberOfRows;
    public int numberOfColumns;

    public float offsetX;
    public float offsetY;

    public GameObject Enemy;

    private Enemies[,] _enemies2DArray;
    
    [Header("Enemies parameters")]
    public float speedHorizontal;
    public float speedVertical;
    public float timeBetweenMovement;
    private float _timer = 0;
    
    private float Orientation = 1; //1 = right, -1 = left

    private bool _hasMovedDown = false;

    // Start is called before the first frame update
    void Start()
    {
        _enemies2DArray = new Enemies[numberOfColumns,numberOfRows];
        
        for (int i = 0; i < numberOfRows; i++)
        {
            for (int j = 0; j < numberOfColumns; j++)
            {
                GameObject newEnemy = GameObject.Instantiate(Enemy, new Vector2(transform.localPosition.x + offsetX * j, transform.localPosition.y - offsetY * i), quaternion.identity, this.transform);
                newEnemy.name = "Enemy " + j + " " + i;
                _enemies2DArray[j, i] = newEnemy.GetComponent<Enemies>();
                Debug.Log(_enemies2DArray[j,i].name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= timeBetweenMovement)
        {
            _timer = 0;
            Move();
            Debug.Log("Timer down");
            Debug.Log(_timer);
        }
    }

    private void Move()
    {
        transform.position = new Vector2(transform.position.x + speedHorizontal * Orientation, transform.position.y);
        
        if (!_hasMovedDown) return;
        
        _hasMovedDown = false;
    }

    public void MoveDown()
    {
        if (_hasMovedDown) return;
        
        transform.position = new Vector2(transform.position.x, transform.position.y - speedVertical);
        _hasMovedDown = true;
        ChangeDirection();
    }

    private void ChangeDirection()
    {
        if (Orientation == 1)
        {
            Orientation = -1;
        }
        else
        {
            Orientation = 1;
        }
    }
}
