using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private List<GameObject> _object;
    private Animator[] _objectAnim;
    private Animator _skeletonAnim;
    private LoadParameters parameters;
    private bool inTrigger;
    private bool recovered;
    private float timer;

    [SerializeField] private bool possibilityDeactivation = true;

    [SerializeField] private string activatedBool = "Activated";


    void Start()
    {
        _anim = GetComponent<Animator>();
        _skeletonAnim = GameObject.Find("Skeleton").GetComponent<Animator>();
        _objectAnim = new Animator[_object.Count];
        for (int i = 0; i < _object.Count; ++i)
        {
            _objectAnim[i] = _object[i].GetComponent<Animator>();             
        }
        inTrigger = false;
        recovered = _anim.GetBool("Recovered");
        parameters = Resources.Load<LoadParameters>("LoadParameters");
        timer = 0;
    }


    void Update()
    {
        ActivateLever();
        RecoveryLever();
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }


    private void RecoveryLever()
	{
        if (inTrigger && !recovered)
        {
			if (Input.GetButtonDown("Submit") && parameters.currentCharacterName == "Skeleton" && _skeletonAnim.GetBool("Arms"))
			{
                bool legs = _skeletonAnim.GetBool("Legs");
                bool arms = _skeletonAnim.GetBool("Arms");
                if (legs || arms)
                {
                    if (legs)
                    {
                        _skeletonAnim.SetBool("Legs", false);
                    }
                    else
                    {
                        _skeletonAnim.SetBool("Arms", false);
                    }
                    _anim.SetBool("Recovered", true);
                    recovered = true;
                }
			}
        }
	}

    private void ActivateLever()
    {
        if (inTrigger && recovered)
        {
            if (Input.GetButtonDown("Submit") && parameters.currentCharacterName == "Skeleton" && _skeletonAnim.GetBool("Arms") && timer <= 0)
			{
                if (!_anim.GetBool("Activated") || possibilityDeactivation)
                {
                    for (int i = 0; i < _object.Count; ++i)
                    {
                        if (_object[i].GetComponent<Spikes>() != null)
                        {
                            _object[i].GetComponent<Spikes>().ChangeActive();
                        }
                        else
                        {
                            _objectAnim[i].SetBool(activatedBool, !_objectAnim[i].GetBool(activatedBool));
                        }
                    }
                    this.GetComponent<AudioSource>().Play();
                    _anim.SetBool("Activated", !_anim.GetBool("Activated"));
                }
			}
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Skeleton")
        {
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Skeleton")
        {
            inTrigger = false;
        }
    }
}
