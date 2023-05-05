using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour
{
    List<string> dialogues;
    public int currentDialogueIndex;
    public int actualMision = 0;
    public bool dialogoActivo = false;
    public bool enMision = false;

    public GameObject referenciaPlayer;
    public GameObject referenciaPunto;
    public GameObject referenciaIsla;
    public GameObject referenciaEnemigo;

    public GameObject referenciaPanel;
    public Text msj;


    void Update()
    {
        if (dialogoActivo)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentDialogueIndex < dialogues.Count)
                {
                    msj.text = dialogues[currentDialogueIndex];
                    currentDialogueIndex++;
                }
                else
                {
                    dialogoActivo = false;
                    enMision = true;
                    Debug.Log("Fin del diálogo");
                }
            }
        }
        else
        {
            if (enMision)
            {
                switch (actualMision)
                {
                    case 0:
                        //mision objetivo 1
                        referenciaPanel.SetActive(false);
                        referenciaPlayer.SetActive(true);
                        referenciaPunto.SetActive(true);
                        break;
                    case 1:
                        //mision objetivo 2
                        referenciaPanel.SetActive(false);
                        referenciaIsla.SetActive(true);
                        break;
                    case 2:
                        //mision objetivo 3
                        referenciaPanel.SetActive(false);
                        referenciaEnemigo.SetActive(true);
                        break;
                    case 3:
                        referenciaPanel.SetActive(false);
                        SceneManager.LoadScene("Game");
                        break;
                }
            }
            else
            {
                switch (actualMision)
                {
                    case 0:
                        referenciaPanel.SetActive(true);
                        dialogues = new List<string> { "Bienvenido Capitan", "Soy el nuevo asistente que contrato", "Como? no me recuerda?", "emmm, pero dijo que era indispensable para el barco...", "Se siente forzado que me acepte igualmente, pero estoy listo", "Al menos sabe navegar no?", "...", "...", "bueno... yo lo ayudare, tengo que ganarme el pan.", "El bardo solo avanza hacia delante [W]", "y gira gracias al timon [A-D]", "Probemos dirigirnos al punto marcado." };
                        dialogoActivo = true;
                        msj.text = dialogues[currentDialogueIndex];
                        currentDialogueIndex = 1;
                        break;
                    case 1:
                        referenciaPanel.SetActive(true);
                        referenciaPunto.SetActive(false);
                        dialogues = new List<string> { "Bien, hemos avanzado.", "Seguramente notaste la brujula. no es asi?", "La brujula es muy util para encontrar islas para capturar", "Para capturar debes permanecer unos instantes en la isla.", "Sigue la brujula y captura una isla." };
                        dialogoActivo = true;
                        currentDialogueIndex = 0;
                        msj.text = dialogues[currentDialogueIndex];
                        currentDialogueIndex++;
                        break;
                    case 2:
                        referenciaIsla.SetActive(false);
                        referenciaPanel.SetActive(true);
                        dialogues = new List<string> { "¡¡¡Capitan!!!", "Un enemigo intenta tomar nuestra isla", "Debemos defenderla", "En serio? no recuerda como atacar?", "...", "El barco solo dispara hacia los lados.", "Debemos acercarnos y posicionarnos perpendicularmente y disparar [Espacio]" };
                        dialogoActivo = true;
                        currentDialogueIndex = 0;
                        msj.text = dialogues[currentDialogueIndex];
                        currentDialogueIndex++;
                        break;
                }
            }

        }

        //control avance mision
        if (referenciaPunto.activeSelf)
        {
            IslandController isla = referenciaPunto.GetComponent<IslandController>();
            if (isla.actualLider != "")
            {
                actualMision++;
                enMision = false;
            }
        }

        if (referenciaIsla.activeSelf)
        {
            IslandController isla = referenciaIsla.GetComponent<IslandController>();
            if (isla.actualLider != "")
            {
                actualMision++;
                enMision = false;
            }
        }
        if (referenciaEnemigo.activeSelf)
        {
            LifeManager lifeEnemy = GameObject.Find("EnemyController").GetComponent<LifeManager>();
            if (!lifeEnemy.IsAlive())
            {
                referenciaEnemigo.SetActive(false);
                actualMision++;
                enMision = false;
                referenciaPanel.SetActive(true);
                referenciaIsla.SetActive(false);
                dialogues = new List<string> { "Uff! eso fue facil", "Pero no crea capitan que siempre sera tan sencilllo", "A partid de ahora deberemos competir con otros para dominar las islas.", "Empecemos" };
                dialogoActivo = true;
                currentDialogueIndex = 0;
                msj.text = dialogues[currentDialogueIndex];
                currentDialogueIndex++; ;
            }
        }


    }


}
