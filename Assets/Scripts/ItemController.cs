using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    public enum ItemType { NoType, Arma, Escudo, Item, Cura, Daño };

    [SerializeField] private bool rotate;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private ItemType itemType;
    [SerializeField] private bool active;
    [SerializeField] private int value;

    public bool Active
    {
        set { gameObject.SetActive(value); active = value; }
        get { return active; }
    }


    public ItemType Item
    {
        get { return itemType; }
    }



    [SerializeField] private int valor;
    [SerializeField] private string Nombre;

    // Start is called before the first frame update
    void Start()
    {
        Active = active;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && itemType != ItemType.NoType)
        {
            Collect();
        }
    }

    public void Collect()
    {


        if (collectSound) AudioSource.PlayClipAtPoint(collectSound, transform.position);


        switch (itemType)
        {
            case ItemType.NoType:
                break;
            case ItemType.Arma:
                GameManager.instance.AddPlayerAttack(value);
                break;
            case ItemType.Cura:
                GameManager.instance.AddPlayerLife(value);
                break;
            case ItemType.Daño:
                GameManager.instance.AddPlayerLife(value);
                break;
            case ItemType.Escudo:
                GameManager.instance.AddPlayerShield(value);
                break;
            case ItemType.Item:
                GameManager.instance.AddScore(valor * 2);
                break;
        }

        GameManager.instance.AddScore(valor);
        gameObject.SetActive(false);

    }
}
