using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotLaserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManage;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isSpeedActive = false;
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private float _speedMultiplier = 2.0f;

    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private int _score;
    private int _addScore = 10;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManage = GameObject.FindObjectOfType<SpawnManager>();
        if (_spawnManage == null) Debug.Log("SpawnManager is null");

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null) Debug.Log("UIManager is null");

        //_shield = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject;
        //if (_spawnManage == null) Debug.Log("Shield is null");
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);

        //float verticalInput = Input.GetAxis("Vertical");
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput);
        
        transform.Translate(direction * _speed * Time.deltaTime);

        //if (transform.position.y >= 0)
        //{
        //    transform.position = new Vector3(transform.position.x, 0, 0);
        //}
        //else if (transform.position.y <= -3.8f)
        //{
        //    transform.position = new Vector3(transform.position.x, -3.8f, 0);
        //}
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }        
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        Vector3 offset = new Vector3(0, 1.06f, 0);

        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotLaserPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
        }
    }

    public void Damage()
    {
        if (_isShieldActive)
        {
            GameObject shield = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject;
            shield.SetActive(false);
            _isShieldActive = false;
            return;
        }

        _lives--;
        _uiManager.UpdateLives(_lives);
        if (_lives < 1)
        {
            _uiManager.GameOver();
            Destroy(gameObject);
            if (_spawnManage != null) _spawnManage.OnPlayerDeath();
        }
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isTripleShotActive = false;
    }
    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isSpeedActive = false;
        _speed /= _speedMultiplier;
    }
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public void SpeedActive()
    {
        _isSpeedActive = true;
        _speed *= _speedMultiplier;

        StartCoroutine(SpeedPowerDownRoutine());
    }
    public void ShieldActive()
    {
        _isShieldActive = true;
        _shield.SetActive(true);
    }

    public void AddScore(int point)
    {
        _score += point;
        _uiManager.UpdateScore(_score);
    }
    
    public int GetScore()
    {
        return _score;
    }
}
