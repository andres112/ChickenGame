using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionGenerator : MonoBehaviour
{
    public GameObject theCheckpointSection;
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
        platformWidth = theCheckpointSection.GetComponent<BoxCollider2D>().size.x * theCheckpointSection.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        bool levelchange = false;
        if(transform.position.x < generationPoint.position.x){
            if(sectionCounter >= nextLevelChange){
                constructionLevel++;
                sectionsPerLevel = sectionsPerLevel + sectionsIncrement;
                nextLevelChange = nextLevelChange + sectionsPerLevel;
                levelchange = true;
            }
            if (levelchange){
                float oldPlatformWidth = platformWidth;
                platformWidth = theCheckpointSection.GetComponent<BoxCollider2D>().size.x * theCheckpointSection.transform.localScale.x;
                transform.position = new Vector3(transform.position.x + oldPlatformWidth/2 +platformWidth/2, transform.position.y, transform.position.z);
                Instantiate(theCheckpointSection, transform.position, transform.rotation);
            }
            else{
                int levelMax = constructionLevel;
                if (levelMax >= theSections.classes.Length){
                    levelMax = theSections.classes.Length;
                }
                int level = Random.Range(0,levelMax);
                int index =  Random.Range(0,theSections.classes[level].sections.Length);
                float oldPlatformWidth = platformWidth;
                platformWidth = theSections.classes[level].sections[index].GetComponent<BoxCollider2D>().size.x * theSections.classes[level].sections[index].transform.localScale.x;
                transform.position = new Vector3(transform.position.x + oldPlatformWidth/2 +platformWidth/2, transform.position.y, transform.position.z);
                 Instantiate(theSections.classes[level].sections[index], transform.position, transform.rotation);
                sectionCounter++;
            }
            
        }
    }

    public void resetPrivateVariables()
    {
        StartCoroutine("ResetPrivateVariablesCo");
    }

    public void ResetPrivateVariablesCo()
    {
        constructionLevel = LevelScript.levelValue + 1;
        nextLevelChange = sectionsPerLevel;
        sectionCounter = 0;
        platformWidth = theCheckpointSection.GetComponent<BoxCollider2D>().size.x * theCheckpointSection.transform.localScale.x;

    }
}
