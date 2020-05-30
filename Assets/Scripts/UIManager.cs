using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text handle;
    [SerializeField]
    private Sprite[] _sprites;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private GameObject _gameOver;
    [SerializeField]
    private Text _restartText;
    private GameManager _gameManager;
    //private Player player;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score)
    {
        handle.text = "Score : " + score.ToString();
    }
    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _sprites[currentLives];
    }
    public void GameOver()
    {
        _gameOver.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOver.SetActive(!_gameOver.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
