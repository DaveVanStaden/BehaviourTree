using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class Rogue : MonoBehaviour
{
    
    [SerializeField] private Transform player;
    [SerializeField] private FieldOfView fov;
    [SerializeField] private float followDistance;
    [SerializeField] private Guard enemy;
    [SerializeField] private GameObject smokeBomb;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject MaterialStateObject;
    
    private BTBaseNode tree;
    private NavMeshAgent agent;
    private Animator animator;
    
    private Material material;

    public float throwForce;
    public float throwUpwardForce;
    
    private Node topNode;
    public bool smokeEnemy;

    private float shootTime;
    private float maxShootTime = 4f;
    
    private void Awake()
    {
        shootTime = maxShootTime;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        material = MaterialStateObject.GetComponent<MeshRenderer>().material;
    }
    public void SetColor(Color color)
    {
        material.color = color;
    }
    private void Start()
    {
        ConstructBehaviourTree();
    }

    private void FixedUpdate()
    {
        topNode.Evaluate();
        if (topNode.nodeState == NodeState.FAILURE)
        {
            SetColor(Color.red);
        }
    }
    private void ConstructBehaviourTree()
    {
        FollowPlayerNode followNode = new FollowPlayerNode(this, player, agent, followDistance);
        SeeEnemyNode seeEnemyNode = new SeeEnemyNode(this, fov);
        HasWeaponNode hasWeaponNode = new HasWeaponNode(enemy, this);
        SmokeNode smokeNode = new SmokeNode(this, enemy.gameObject.transform);

        Sequence followSequence = new Sequence(new List<Node> {followNode});
        Sequence smokeSequence = new Sequence(new List<Node> {seeEnemyNode,hasWeaponNode,smokeNode});

        topNode = new Selector(new List<Node> {smokeSequence, followSequence});
    }

    public void InstantiateSmokeBomb()
    {
        shootTime += Time.deltaTime;
        if (shootTime > maxShootTime)
        {
            GameObject projectile = Instantiate(smokeBomb, throwPoint.position, this.gameObject.transform.rotation);

            Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();

            Vector3 forceToAdd = transform.forward * throwForce + transform.up * throwUpwardForce;
        
            projectileRB.AddForce(forceToAdd,ForceMode.Impulse);
        
            enemy.Smoked();
            shootTime = 0;
        }
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
