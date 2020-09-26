using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorFactory : MonoBehaviour
{
    [SerializeField] private List<Actor> actors;

    private Dictionary<int, Actor> actorDictionary;

    private void Awake()
    {
        actorDictionary = new Dictionary<int, Actor>();

        foreach (Actor actor in actors)
        {
            actorDictionary.Add(actor.id, actor);
        }
    }

    public Actor CreateNewActor(int id, float xPos, float yPos)
    {
        Actor newActor;

        if(actorDictionary.TryGetValue(id, out newActor))
        {
            Vector3 position = new Vector3(xPos, yPos, 0f);
            return Instantiate(newActor, position, Quaternion.identity);
        }
        else
        {
            throw new System.Exception("No existe el actor");
        }
    }
}
