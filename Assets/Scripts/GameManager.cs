using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameManager instance;



    public static PlayerController player;
    public static int scorePlayer = 0;

    private void Awake()
    {



        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();


            scorePlayer = 0;
            player.Attack = 0;
            player.Shield = 0;
            player.Life = 100;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log($"Score: {scorePlayer}, Player: Life {player.Life}, Attack {player.Attack}, Shield {player.Shield}");

    }


    public void AddScore(int _scorePlayer)
    {
        scorePlayer += _scorePlayer;
    }

    public void AddPlayerShield(int _shield)
    {
        player.GetComponent<PlayerController>().AddShield(_shield);
        player.GetComponent<PlayerController>().AddItem(ItemController.ItemType.Escudo);
    }
    public void AddPlayerAttack(int _attack)
    {
        player.GetComponent<PlayerController>().AddAttack(_attack);
        player.GetComponent<PlayerController>().AddItem(ItemController.ItemType.Arma);
    }
    public void AddPlayerLife(int _life)
    {
        player.GetComponent<PlayerController>().AddLife(_life);

        if (_life > 0)
            player.GetComponent<PlayerController>().AddItem(ItemController.ItemType.Cura);
    }



}
