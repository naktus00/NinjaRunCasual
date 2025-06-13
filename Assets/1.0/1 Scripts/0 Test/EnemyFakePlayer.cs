using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFakePlayer : MonoBehaviour
{
    private FakePlayerComponents _comps;

    [Space(5), Header("Costume")]
    [SerializeField] private int weaponIndex;
    [SerializeField] private int weaponColorIndex;
    [SerializeField] private int skinIndex;

    [Space(5), Header("Features")]
    //[SerializeField] private float _health;
    [SerializeField] private NinjaWarriorStatus _status;

    [Space(5), Header("Fake Player")]
    [SerializeField] private string fpName;

    [Space(5), Header("Other")]
    [SerializeField] private float ragDollForce;

    [HideInInspector] public NinjaWarriorStatus status { get { return _status; } }
    [HideInInspector] public Transform[] hitEffectPoints { get { return _comps.hitEffectPoints; } }
    //[HideInInspector] public float health { get { return _health; } }

    private void Awake()
    {
        _comps = this.gameObject.GetComponent<FakePlayerComponents>();
    }

    private void Start()
    {
        NinjaWarriorStatus.Initialize(ref _status);

        GameManager.Instance.onFightEnded += (b) =>
        {
            if (b == true)
                return;

            StartCoroutine(IEOnFightEnded());
        };
    }

    private void AffectHealth(float amount)
    {
        _status.health += amount;
        _status.health = Mathf.Clamp(_status.health, 0f, NinjaWarriorStatus.maxHealth);
    }
    private IEnumerator IEKilledByPlayer(Vector3 targetDirection, float force, float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);

        _comps.coll.enabled = false;
        _comps.rb.isKinematic = true;
        _comps.animator.enabled = false;
        RagdollController ragdoll = GetComponent<RagdollController>();
        ragdoll.RagdollON();
        Rigidbody hipRb = ragdoll.ragdollBits[0].GetComponent<Rigidbody>();
        Vector3 dir = targetDirection.normalized;
        hipRb.AddForce(dir * force, ForceMode.Impulse);
        StartCoroutine(AddtionalRequirement.DoSlowmotion(0.05f, 2f));

        yield return new WaitForSeconds(3f);

        GameManager.Instance.InvokeFightEnded(true);
    }
    private IEnumerator IEOnFightEnded()
    {
        //transform.Rotate((-1f) * transform.forward);

        yield return new WaitForSeconds(0.75f);

        SelectEndingDance();
        _comps.animator.SetBool("dance", true);

    }
    private void SelectEndingDance()
    {
        int n = UnityEngine.Random.Range(0, 3);

        switch (n)
        {
            case 0:
                _comps.animator.SetFloat("selectDance", 0f);
                break;
            case 1:
                _comps.animator.SetFloat("selectDance", 0.5f);
                break;
            case 2:
                _comps.animator.SetFloat("selectDance", 1f);
                break;
        }
    }

    public void Death()
    {
        _comps.coll.enabled = false;
        _comps.rb.isKinematic = true;
        _comps.animator.enabled = false;
        //comps.levelCanvasParent.gameObject.SetActive(false);

        GetComponent<RagdollController>().RagdollON();

        Vector3 backwardV3 = transform.forward * (-1f);
        Vector3 directionV3 = (backwardV3 + Vector3.up).normalized;
        //comps.hip.GetComponent<Rigidbody>().AddForce(directionV3 * ragDollForce, ForceMode.Impulse);
    }
    public void WhenDamageTaken(float enemyDamage)
    {
        float damageTaken = (-1f) * enemyDamage * (1 - _status.defenceRatio);
        AffectHealth(damageTaken);
    }
    public int RandomHitAnim()
    {
        int n = UnityEngine.Random.Range(0, 4);

        string animKey = $"atk{n + 1}";
        _comps.animator.SetTrigger(animKey);

        return n;
    }
    public void KilledByPlayer(Vector3 targetDirection, float force, float waitingTime)
    {
        StartCoroutine(IEKilledByPlayer(targetDirection, force, waitingTime));
    }
    
}
