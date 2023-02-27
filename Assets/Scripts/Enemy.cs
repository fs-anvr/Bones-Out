using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{  
    [SerializeField] private float speed = 1.3f;
    [SerializeField] private float angrySpeed = 2.2f;
    [SerializeField] private float positionOfPatrol = 2.0f;
    [SerializeField] private float attackDistance = 5.0f;
    [SerializeField] private Transform point;
    private bool _facingRight = true;

    private Transform skeleton;
    private Transform wolf;
    [SerializeField] private float defaultRange = 5.0f;
    [SerializeField] private float howlRange = 20.0f;
    private float range;

    private bool chill;
    private int angry;
    private bool goBack;

    private Animator _anim;

    private LoadParameters parameters;


    private void Start()
    {
        _anim = this.GetComponent<Animator>();
        skeleton = GameObject.Find("Skeleton").transform;
        wolf = GameObject.Find("Wolf").transform;
        chill = true;
        goBack = false;
        angry = 0;
        range = defaultRange;

        parameters = Resources.Load<LoadParameters>("LoadParameters");
    }


    private void Update()
    {
        StateLogic();
        State();
        isHowl();
    }


    private void StateLogic()
    {
        float skeletonDistance = Vector2.Distance(transform.position, skeleton.position);
        float wolfDistance = Vector2.Distance(transform.position, wolf.position);

        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == 0)
        {
            chill = true;
        }

        if (skeletonDistance < range || wolfDistance < range)
        {
            if (skeletonDistance < wolfDistance)
            {
                angry = 1;
            }
            else
            {
                angry = 2;
            }
            chill = false;
            goBack = false;
        }
        else
        {
            goBack = true;
            angry = 0;
        }
    }

    private void State()
    {
        if (chill)
        {
            Chill();
        }
        else if(angry == 1)
        {
            Angry(skeleton);
        }
        else if (angry == 2)
        {
            Angry(wolf);
        }
        else if (goBack)
        {
            GoBack();
        }
    }


    private void Reverse()
    {
        _facingRight = !_facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }


    private void Chill()
    {
        if ((transform.position.x > point.position.x + positionOfPatrol && _facingRight) || 
            (transform.position.x < point.position.x - positionOfPatrol && !_facingRight))
        {
            Reverse();
        }

        if (_facingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }


    private void Angry(Transform player)
    {
        if ((transform.position.x - player.position.x < 0 && !_facingRight) || 
            (transform.position.x - player.position.x > 0 && _facingRight))
        {
            Reverse();
        }
        transform.position = Vector2.MoveTowards(transform.position, player.position, angrySpeed * Time.deltaTime);


        if (Vector2.Distance(transform.position, player.position) < attackDistance && !_anim.GetBool("Attack"))
        {
            _anim.SetBool("Attack", true);
        }
    }

    public void AttackSoundPlay()
    {
        this.GetComponent<AudioSource>().Play();
    }

    private void ResetAttack()
    {
        _anim.SetBool("Attack", false);
    }


    private void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
    }

    private void isHowl()
    {
        if (parameters.howl && range != howlRange)
        {
            range = howlRange;
            Debug.Log("howlRange active");
        }
        if (!parameters.howl && range != defaultRange && angry == 0)
        {
            range = defaultRange;
            Debug.Log("defaultRange active");
        }
    }
}
