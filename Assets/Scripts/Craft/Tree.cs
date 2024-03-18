using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Testando commit
public class Tree : MonoBehaviour
{
    [SerializeField] private float treeHealth;
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private int totalWood;

    [SerializeField] private ParticleSystem leafs;

    private bool isCut;

    public void OnHit()
    {
        treeHealth--;

        anim.SetTrigger("isHit");
        leafs.Play();

        if(treeHealth <= 0)
        {
            //cria o toco e instancia os drops (madeira)
            for (int i = 0; i < totalWood; i++)
            {
                Instantiate(woodPrefab, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f) ,0f), transform.rotation);
            }

            anim.SetTrigger("cut");

            isCut = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
           if(collision.CompareTag("Axe") && !isCut)
           {
            OnHit();
           }
    }

}
