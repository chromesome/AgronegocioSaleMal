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

    public Actor CreateNewActor(int id)
    {
        Actor newActor;

        if(actorDictionary.TryGetValue(id, out newActor))
        {
            return Instantiate(newActor);
        }
        else
        {
            throw new System.Exception("No existe el actor");
        }
    }
}
