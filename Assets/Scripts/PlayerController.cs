using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float velocidadPlayer = 1000f;
    [SerializeField] private float fuerzaSalto = 500f;
    [SerializeField] private float velocidadGiro = 10f;
    [SerializeField] private Animator animaPlayer;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private AudioClip walkSound;

    private bool[] KeysSelected;
    Dictionary<string, GameObject> ListItems;

    [SerializeField] private int lifePlayer;
    [SerializeField] private int shieldPlayer;
    [SerializeField] private int attackPlayer;

    private GameObject menuItems;


    private AudioSource audioPlayer;

    private bool isGrounded = true;
    private bool isRotate = false;

    private float giroPlayer = 0f;

    //[SerializeField] private Animator animaPlayer = new Animator();


    private Rigidbody rbPlayer;

    // Start is called before the first frame update
    void Start()
    {
        ListItems = new Dictionary<string, GameObject>();
        KeysSelected = new bool[3];

        audioPlayer = GetComponent<AudioSource>();
        rbPlayer = GetComponent<Rigidbody>();

        animaPlayer.SetBool("IsIdle", true);
        animaPlayer.SetBool("IsRun", false);
        animaPlayer.SetBool("IsBack", false);
        animaPlayer.SetBool("IsJump", false);

    }


    // Update is called once per frame
    void Update()
    {
        IsGrounded();
    }

    private void FixedUpdate()
    {
        Mover();
        MoverItems();
        ControlAnimacion();
    }

    private void Mover()
    {
        float ejeVertical = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.A) && isGrounded)
        {
            giroPlayer -= Time.deltaTime * velocidadGiro * 10;
            transform.rotation = Quaternion.Euler(0, giroPlayer, 0);
            isRotate = true;
        }
        else if (Input.GetKey(KeyCode.D) && isGrounded)
        {
            giroPlayer += Time.deltaTime * velocidadGiro * 10;
            transform.rotation = Quaternion.Euler(0, giroPlayer, 0);
            isRotate = true;
        }
        else
        {
            isRotate = false;
        }

        if (ejeVertical != 0 && isGrounded)
        {
            rbPlayer.AddRelativeForce(Vector3.forward * velocidadPlayer * ejeVertical, ForceMode.Force);
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.PlayOneShot(walkSound, 0.5f);
            }
        }




        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rbPlayer.AddRelativeForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }
    private void ControlAnimacion()
    {

        float ejeVertical = Input.GetAxis("Vertical");

        animaPlayer.SetBool("IsRun", ejeVertical > 0 && isGrounded);
        animaPlayer.SetBool("IsBack", ejeVertical < 0 || isRotate);
        animaPlayer.SetBool("IsJump", !isGrounded);


        animaPlayer.SetBool("IsIdle", ejeVertical == 0 && isGrounded && !isRotate);


        Debug.Log($"EjeVertical {ejeVertical}; IsIdle {animaPlayer.GetBool("IsIdle")} ; IsJump {animaPlayer.GetBool("IsJump")}; IsRun {animaPlayer.GetBool("IsRun")} ; IsBack {animaPlayer.GetBool("IsBack")}; isRotate {isRotate}; isGround {isGrounded}");
    }
    private void IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 0.05f, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }


    }
    public void AddShield(int _shield)
    {
        shieldPlayer += _shield;
    }
    public void AddAttack(int _attack)
    {
        attackPlayer += _attack;
    }
    public void AddLife(int _life)
    {
        lifePlayer += _life;
    }

    public int Shield { get { return shieldPlayer; } set { shieldPlayer = value; } }
    public int Attack { get { return attackPlayer; } set { attackPlayer = value; } }
    public int Life { get { return lifePlayer; } set { lifePlayer = value; } }


    private void MoverItems()
    {

        if (menuItems != null)
        {


            if (Input.GetKey(KeyCode.Alpha1))
            {

                if (ListItems.ContainsKey("Escudo"))
                {
                    KeysSelected[0] = true;
                    ListItems["Escudo"].transform.localScale = new Vector3(1, 1, 1);
                }
                if (ListItems.ContainsKey("Cura"))
                {
                    KeysSelected[1] = false;
                    ListItems["Cura"].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                if (ListItems.ContainsKey("Espada"))
                {
                    KeysSelected[2] = false;
                    ListItems["Espada"].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {




                if (ListItems.ContainsKey("Escudo"))
                {
                    KeysSelected[0] = false;
                    ListItems["Escudo"].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                if (ListItems.ContainsKey("Cura"))
                {
                    KeysSelected[1] = true;
                    ListItems["Cura"].transform.localScale = new Vector3(1, 1, 1);
                }
                if (ListItems.ContainsKey("Espada"))
                {
                    KeysSelected[2] = false;
                    ListItems["Espada"].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
            }
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                if (ListItems.ContainsKey("Escudo"))
                {
                    KeysSelected[0] = false;
                    ListItems["Escudo"].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                if (ListItems.ContainsKey("Cura"))
                {
                    KeysSelected[1] = false;
                    ListItems["Cura"].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                if (ListItems.ContainsKey("Espada"))
                {
                    KeysSelected[2] = true;
                    ListItems["Espada"].transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else if (Input.GetKey(KeyCode.Alpha0))
            {

                KeysSelected[0] = false;
                KeysSelected[1] = false;
                KeysSelected[2] = false;

                if (ListItems.ContainsKey("Escudo"))
                    ListItems["Escudo"].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                if (ListItems.ContainsKey("Cura"))
                    ListItems["Cura"].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                if (ListItems.ContainsKey("Espada"))
                    ListItems["Espada"].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }

            if (Input.GetKey(KeyCode.Delete))
            {

                if (KeysSelected[0] && ListItems.ContainsKey("Escudo"))
                    DeleteItem("Escudo");
                else if (KeysSelected[1] && ListItems.ContainsKey("Cura"))
                    DeleteItem("Cura");
                else if (KeysSelected[2] && ListItems.ContainsKey("Espada"))
                    DeleteItem("Espada");

                KeysSelected[0] = false;
                KeysSelected[1] = false;
                KeysSelected[2] = false;

            }



        }


    }


    public void AddItem(ItemController.ItemType _item)
    {

        foreach (Transform child in gameObject.transform)
        {
            if (child.name.Equals("IconosMap"))
            {

                menuItems = child.gameObject;

                foreach (Transform iconObject in child.gameObject.transform)
                {

                    switch (_item)
                    {
                        case ItemController.ItemType.Escudo:
                            if (iconObject.name.Equals("Escudo_icon")
                               && !iconObject.gameObject.active)
                            {
                                iconObject.gameObject.SetActive(true);
                                ListItems.Add("Escudo", iconObject.gameObject);
                            };

                            break;

                        case ItemController.ItemType.Arma:

                            if (iconObject.name.Equals("Espada_Icon")
                                && !iconObject.gameObject.active)
                            {
                                iconObject.gameObject.SetActive(true);
                                ListItems.Add("Espada", iconObject.gameObject);
                            };

                            break;

                        case ItemController.ItemType.Cura:
                            if (iconObject.name.Equals("Cura_Icon")
                               && !iconObject.gameObject.active)
                            {
                                iconObject.gameObject.SetActive(true);
                                ListItems.Add("Cura", iconObject.gameObject);
                            };
                            break;

                    };



                }
            }
        }

    }

    public void DeleteItem(string _key)
    {
        ItemController itemObj = ListItems[_key].GetComponent<ItemController>();
        itemObj.Active = false;

        ListItems.Remove(_key);

    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Vector3 direction = gameObject.transform.TransformDirection(Vector3.down) * 0.05f;
        Gizmos.DrawRay(gameObject.transform.position, direction);

    }
}
