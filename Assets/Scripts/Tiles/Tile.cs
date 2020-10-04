using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IDestructible, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int level;
    public float resistance = 100f;
    [SerializeField] float maxResistance = 100f;
    [SerializeField] List<Sprite> tileStateSprites;
    [SerializeField] List<float> spawnPosition;

    int x;
    int y;

    public int X
    {
        get => x;
        set
        {
            x = value;
            SortingLayer();
        }
    }

    public int Y
    {
        get => y;
        set
        {
            y = value;
            SortingLayer();
        }
    }

    Actor actor;
    Fire fire;

    List<ActionItem> tileActions;

    public Actor Actor {
        get => actor;

        set
        {
            actor = value;
            if(actor != null)
            {
                actor.transform.parent = this.transform;
                actor.transform.position = spawnPoint.transform.position;
                actor.X = x;
                actor.Y = y;
            }
        }
    }

    public Fire Fire
    {
        get => fire;

        set
        {
            fire = value;
            if(fire != null)
            {
                fire.transform.parent = this.transform;
                fire.transform.position = spawnPoint.transform.position;
                fire.X = x;
                fire.Y = y;
            }
        }
    }

    public List<Tile> neighbours;
    public Transform spawnPoint;

    [SerializeField] bool selectable = true;

    SpriteRenderer sprTile;
    bool selected = false;

    void Awake()
    {
        SetupActions();
    }

    public void SetupActions()
    {
        tileActions = new List<ActionItem>();
        if (actor == null)
        {
            if (level > 2 && level < 6)
            {
                tileActions.Add(new ActionItem(0, "Granja", "ActionBuildFarm", 60));
                tileActions.Add(new ActionItem(1, "Fábrica", "ActionBuildFactory", 100));
            }
            else if (level >= 6)
            {
                tileActions.Add(new ActionItem(2, "Fuego", "ActionFire", 0));
            }
        }
        
    }

    private void Start()
    {
        sprTile = this.GetComponent<SpriteRenderer>();
    }

    internal void Unselect()
    {
        selected = false;
        sprTile.color = Color.white;
    }

    //DEBUG
    internal string GetDetails()
    {
        String tileDetails = "";
        // text stream
        tileDetails += "Tile selected: " + this.name;
        tileDetails += "\nActor " + Actor;
        tileDetails += "\nneighbors------";
        foreach (Tile item in neighbours)
        {
            tileDetails += "\n" + item.name;
        }

        return tileDetails;
    }

    internal List<ActionItem> GetActions()
    {
        if(Fire != null)
        {
            return Fire.GetActions();
        }
        else if (Actor != null)
        {
            return Actor.GetActions();
        }
        else
        {
            return tileActions;
        }
    }

    public float GetMaxHealth()
    {
        return maxResistance;
    }

    public float GetCurrentHealth()
    {
        return resistance;
    }

    public float ReceiveDamage(float damage)
    {
        Debug.Log(this.name + " received damage = " + damage);
        if(level < 9)
        {
            Tree tree = Actor as Tree;
            if(tree != null)
            {
                damage = tree.ReceiveDamage(damage);
            }
            else if(IsOnFire())
            {
                Fire.Consume();
            }

            // take remaining damage
            resistance -= damage;

            if(resistance <= 0)
            {
                float remainingDamage = Mathf.Abs(resistance - damage);
                DemoteTile();

                resistance -= remainingDamage;
            }
        }

        // Tile receives all damage
        return 0;
    }

    private void DemoteTile()
    {
        Debug.Log("Downgrade " + this.name);
        level -= 1;
        resistance = maxResistance;
        this.GetComponent<SpriteRenderer>().sprite = tileStateSprites[level];
        SetupActions();

        if(level == 5 && IsOnFire())
        {
            Destroy(Fire.gameObject);
            Fire = null;
        }

        if(level < 3)
        {
            Destroy(Actor.gameObject);
            Actor = null;
        }

        ReajustSpawnPoint();
    }

    private void ReajustSpawnPoint()
    {
        // Modo cabeza mal
        Transform spawnTransform = spawnPoint.transform;
        Vector3 newSpawnPosition = new Vector3(spawnTransform.localPosition.x, spawnPosition[level]);

        spawnTransform.localPosition = newSpawnPosition;

        if(Actor != null)
        {
            Actor.transform.localPosition = newSpawnPosition;
        }

        if(Fire != null)
        {
            Fire.transform.localPosition = newSpawnPosition;
        }
    }

    public bool IsOnFire()
    {
        return this.fire != null ? true : false;
    }

    public void TrySetFire(Fire fire)
    {
        Debug.Log("Try to set fire " + this.name);
        // Si es agua no hagas nada
        if(level < 9)
        {
            bool setFire = true;

            float fireChance = resistance + level - RiskModifier();
            float rndNumber = UnityEngine.Random.Range(0, 99);

            setFire = fireChance < rndNumber ? true : false;

            if (setFire)
            {
                this.Fire = Instantiate(fire);
            }
        }
    }

    private float RiskModifier()
    {
        float riskModifier = 0f;

        foreach (Tile tile in neighbours)
        {
            if (tile.IsOnFire())
            {
                riskModifier += 1f;
            }
        }

        return riskModifier;
    }

    private void SortingLayer()
    {
        SpriteRenderer sprRenderer = GetComponent<SpriteRenderer>();
        if (sprRenderer != null)
        {
            sprRenderer.sortingOrder = y == 0 ? y : y + 3;
        }
    }

    private void OnMouseExit()
    {
        if (!selected)
        {
            sprTile.color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        #region DEBUG
        Debug.Log("OnMouseDown " + this.name);
        Debug.Log("Actor " + Actor);
        Debug.Log("neighbors------");
        foreach (Tile item in neighbours)
        {
            Debug.Log(item.name);
        }
        #endregion

        if (this != GameManager.instance.SelectedTile)
        {
            if (selectable)
            {
                selected = true;
                sprTile.color = Color.red;
                GameManager.instance.SelectedTile = this;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selectable && !selected)
        {
            sprTile.color = Color.gray;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!selected)
        {
            sprTile.color = Color.white;
        }
    }
}
