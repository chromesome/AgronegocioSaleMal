using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorFactoryTest : MonoBehaviour
{
    [SerializeField] private ActorFactory _actorFactory;

    private void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            _actorFactory.CreateNewActor(0);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            _actorFactory.CreateNewActor(1);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            _actorFactory.CreateNewActor(2);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            _actorFactory.CreateNewActor(3);
        }
    }
}
