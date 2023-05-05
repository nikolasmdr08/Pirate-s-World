using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    public GameObject auxCamera;
    private LifeManager playerLifeManager;
    private LifeManager enemyLifeManager;
    private ShipMove playerReference;

    [Header("Spawners")]
    public Transform SpawnPlayer;
    public Transform SpawnEnemy;
    public float respawnTime =5f;

    [Header("Control Islas")]
    GameObject[] islas;
    public int islasDisponibles = 0;
    public int islasPlayer = 0;
    public int islasEnemy = 0;

    [Header("HUD")]
    public float tiempoRestante = 179f;
    public Text cuentaRegresivaText;
    public Text countIslandText;
    public Text statusText;

    public GameObject panelEndGame;
    public Text resultTxt;
    string state = "";

    private void Start()
    {
        player = GameObject.Find("ShipPrefab");
        playerLifeManager = player.GetComponent<LifeManager>();
        playerReference = player.GetComponent<ShipMove>();
        enemy = GameObject.Find("EnemyController");
        enemyLifeManager = enemy.GetComponent<LifeManager>();

        auxCamera.SetActive(false);

        islas = GameObject.FindGameObjectsWithTag("Respawn");

    }

    private void Update()
    {
        RespawnController();
        TimeController();
        CountIslands();
        HUDController();

        //if (tiempoRestante <= 0f || islasPlayer >= 10 || islasEnemy >= 10)
        if (tiempoRestante <= 0f)
        {
            Debug.Log("Juego Terminado");
            Time.timeScale = 0;

            if(tiempoRestante <= 0)
            {
                cuentaRegresivaText.text = "0:00"; // correccion de tiempo negativo
            }

            if(islasPlayer > islasEnemy)
            {
                state = "Ganaste";
            }
            else if(islasPlayer < islasEnemy)
            {
                state = "Perdiste";
            }
            else
            {
                state = "Empate";
            }

            panelEndGame.SetActive(true);
            resultTxt.text = state;

        }
    }

    private void HUDController()
    {
        int hp = playerLifeManager.ActualLifePoints();
        float speed = playerReference.GetSpeedPoints();
        int atk = playerReference.GetAtackPoints();
        statusText.text = "HP: "+ hp +"\nVelocidad: "+ speed + "\nDefenza: 1\nAtaque: "+ atk;
    }

    private void CountIslands()
    {
        islasDisponibles = GameObject.FindGameObjectsWithTag("IslaEmpty").Length;
        islasPlayer = GameObject.FindGameObjectsWithTag("IslaPlayer").Length;
        islasEnemy = GameObject.FindGameObjectsWithTag("IslaEnemy").Length;

        countIslandText.text = "Islas Disponibles: " + islasDisponibles + "\nIslas del Jugador: " + islasPlayer + "\nIslas del Enemigo: " + islasEnemy;
    }

    private void TimeController()
    {
        tiempoRestante -= Time.deltaTime;
        int minutos = Mathf.FloorToInt(tiempoRestante / 60f);
        int segundos = Mathf.RoundToInt(tiempoRestante % 60f);
        cuentaRegresivaText.text =  minutos.ToString("00") + ":" + segundos.ToString("00");
    }

    private void RespawnController()
    {
        if (!playerLifeManager.IsAlive())
        {
            Invoke("RespawnPlayer", respawnTime);
            auxCamera.SetActive(true);
            auxCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z-10);
            player.SetActive(false);
        }
        if (!enemyLifeManager.IsAlive())
        {
            Invoke("RespawnEnemy", respawnTime);
            enemy.SetActive(false);
        }
    }

    private void RespawnPlayer()
    {
        player.transform.position = SpawnPlayer.position;
        playerLifeManager.ReceiveHealing(10); // le devuelvo la vida 
        auxCamera.SetActive(false);
        player.SetActive(true);
    }

    private void RespawnEnemy()
    {
        enemy.transform.position = SpawnEnemy.position;
        enemyLifeManager.ReceiveHealing(10); // le devuelvo la vida 
        enemy.SetActive(true);
    }
}