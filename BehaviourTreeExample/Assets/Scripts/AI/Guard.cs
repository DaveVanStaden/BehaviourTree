using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float lowHealthThreshhold;
    [SerializeField] private float healthRestoreRate;
    
    [SerializeField] private float chasingRange;
    [SerializeField] private float attackRange;
    
    [SerializeField] private Transform[] patrolPoints;
    private int patrolPointIndex;
    
    [SerializeField] private Transform weaponLocation;

    [SerializeField] private Transform player;
    
    [SerializeField] private GameObject MaterialStateObject;
    
    public FieldOfView fov;

    private Material material;

    private Node topNode;
    public bool hasWeapon;
    public bool isGettingWeapon;
    public bool isAttacking;
    public bool canAttack;
    private bool blinded;

    private float _currentHealth;
    public float currentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = Mathf.Clamp(value, 0, startingHealth); }
    }
    
    private BTBaseNode tree;
    private NavMeshAgent agent;
    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        material = MaterialStateObject.GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        _currentHealth = startingHealth;
        ConstructBehaviourTree();
        //Create your Behaviour Tree here!
    }
    private void FixedUpdate()
    {
        _currentHealth += Time.deltaTime * healthRestoreRate;
        if (!blinded)
        {
            topNode.Evaluate();
            if (topNode.nodeState == NodeState.FAILURE)
            {
                SetColor(Color.red);
            }
        }
        else
        {
            SetColor(Color.black);
        }
        //tree?.Run();
    }

    public void Smoked()
    {
        StartCoroutine(cannotSee());
    }

    IEnumerator cannotSee()
    {
        blinded = true;
        yield return new WaitForSeconds(4);
        blinded = false;

    }

    public void SetColor(Color color)
    {
        material.color = color;
    }

    private void ConstructBehaviourTree()
    {
        HealthNode healthNode = new HealthNode(this, lowHealthThreshhold);
        ChaseNode chaseNode = new ChaseNode(player, agent, this,hasWeapon);
        RangeNode chasingRangeNode = new RangeNode(chasingRange, player, transform, this);
        RangeNode attackRangeNode = new RangeNode(attackRange, player, transform, this);
        AttackNode attackNode = new AttackNode(agent, this,player);
        CanAttackNode canAttackNode = new CanAttackNode(this);
        PatrolNode patrolNode = new PatrolNode(agent, patrolPoints, this);
        GetWeaponNode weaponNode = new GetWeaponNode(weaponLocation, agent,this);
        IsInFieldOfViewNode fovNode = new IsInFieldOfViewNode(fov, this);
        IsInFieldOfViewPatrolNode patrolFovNode = new IsInFieldOfViewPatrolNode(fov,this);

        Sequence attackSequence = new Sequence(new List<Node> {canAttackNode,attackRangeNode, attackNode});
        Sequence chaseSequence = new Sequence(new List<Node> {canAttackNode,chasingRangeNode, chaseNode});
        Sequence getWeaponSequence = new Sequence(new List<Node> {fovNode, weaponNode});
        Sequence patrolSequence = new Sequence(new List<Node> {patrolFovNode, patrolNode});
        
        topNode = new Selector(new List<Node> {patrolSequence, getWeaponSequence, attackSequence, chaseSequence});
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Handles.color = Color.yellow;
    //    Vector3 endPointLeft = viewTransform.position + (Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;
    //    Vector3 endPointRight = viewTransform.position + (Quaternion.Euler(0, ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;

    //    Handles.DrawWireArc(viewTransform.position, Vector3.up, Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward, ViewAngleInDegrees.Value * 2, SightRange.Value);
    //    Gizmos.DrawLine(viewTransform.position, endPointLeft);
    //    Gizmos.DrawLine(viewTransform.position, endPointRight);

    //}
}
