using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckBooks : MonoBehaviour
{
    [SerializeField] private List<GameObject> books = new List<GameObject>();
    [SerializeField] private List<GameObject> tempList = new List<GameObject>();
    [SerializeField] private GameObject _necro;
    public bool gameIsEnd = false;  

    void Update()
    {
        BooksCheck();   
    }
    void BooksCheck()
    {
        if (tempList.SequenceEqual(books))
        {
            foreach (GameObject book in tempList)
            {
                Destroy(book);
            }
            _necro.SetActive(true);
            gameIsEnd = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (books.Contains(other.gameObject))
        {
            tempList.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(books.Contains(other.gameObject))
        {
            tempList.Clear();
        }
    }
}
