using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionGenerator : MonoBehaviour
{
    public DifficultyClasses theSections;
    public Transform generationPoint;
    public int sectionsPerLevel;
    public int sectionsIncrement;
    private int constructionLevel;
    private int nextLevelChange;
    private int sectionCounter;

    private float platformWidth;


    // Start is called before the first frame update
    void Start()
    {
        constructionLevel = 1;
        nextLevelChange = sectionsPerLevel;
        sectionCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < generationPoint.position.x){
            if(sectionCounter >= nextLevelChange){
                constructionLevel++;
                sectionsPerLevel = sectionsPerLevel + sectionsIncrement;
                nextLevelChange = nextLevelChange + sectionsPerLevel;
            }
            int levelMax = constructionLevel;
            if (levelMax >= theSections.classes.Length){
                levelMax = theSections.classes.Length;
            }
            int level = Random.Range(0,levelMax);
            int index =  Random.Range(0,theSections.classes[level].sections.Length);
            platformWidth = theSections.classes[level].sections[index].GetComponent<BoxCollider2D>().size.x * theSections.classes[level].sections[index].transform.localScale.x;
            transform.position = new Vector3(transform.position.x +platformWidth, transform.position.y, transform.position.z);
            Instantiate(theSections.classes[level].sections[index], transform.position, transform.rotation);
            sectionCounter++;
        }
    }
}
