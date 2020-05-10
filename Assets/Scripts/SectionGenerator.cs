using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionGenerator : MonoBehaviour
{
    public GameObject thePlainSection;
    public GameObject theGoalSection;
    public GameObject theCheckpointSection;
    public DifficultyClasses theSections;
    public Transform generationPoint;
    public int sectionsPerLevel;
    public int sectionsIncrement;
    public int max_level;
    private int constructionLevel;
    private int nextLevelChange;
    private int sectionCounter;
    private bool goalSet;

    private float platformWidth;


    // Start is called before the first frame update
    void Start()
    {
        constructionLevel = 0;
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
                nextLevelChange = sectionsPerLevel * (1 + constructionLevel) + (sectionsIncrement * constructionLevel);
                levelchange = true;
            }
            if (levelchange){
                float oldPlatformWidth = platformWidth;
                platformWidth = theCheckpointSection.GetComponent<BoxCollider2D>().size.x * theCheckpointSection.transform.localScale.x;
                transform.position = new Vector3(transform.position.x + oldPlatformWidth/2 +platformWidth/2, transform.position.y, transform.position.z);
                Instantiate(theCheckpointSection, transform.position, transform.rotation);
            }
            else{
                if (constructionLevel == max_level)
                {
                    if (!goalSet)
                    {
                        float oldPlatformWidth = platformWidth;
                        platformWidth = theGoalSection.GetComponent<BoxCollider2D>().size.x * theGoalSection.transform.localScale.x;
                        transform.position = new Vector3(transform.position.x + oldPlatformWidth / 2 + platformWidth / 2, transform.position.y, transform.position.z);
                        Instantiate(theGoalSection, transform.position, transform.rotation);
                        goalSet = true;
                    }
                    else
                    {
                        float oldPlatformWidth = platformWidth;
                        platformWidth = thePlainSection.GetComponent<BoxCollider2D>().size.x * thePlainSection.transform.localScale.x;
                        transform.position = new Vector3(transform.position.x + oldPlatformWidth / 2 + platformWidth / 2, transform.position.y, transform.position.z);
                        Instantiate(thePlainSection, transform.position, transform.rotation);
                    }

                }
                else
                {
                    int levelMax = constructionLevel;
                    if (levelMax >= theSections.classes.Length)
                    {
                        levelMax = theSections.classes.Length;
                    }
                    int level = Random.Range(0, levelMax);
                    int index = Random.Range(0, theSections.classes[level].sections.Length);
                    float oldPlatformWidth = platformWidth;
                    platformWidth = theSections.classes[level].sections[index].GetComponent<BoxCollider2D>().size.x * theSections.classes[level].sections[index].transform.localScale.x;
                    transform.position = new Vector3(transform.position.x + oldPlatformWidth / 2 + platformWidth / 2, transform.position.y, transform.position.z);
                    Instantiate(theSections.classes[level].sections[index], transform.position, transform.rotation);
                    sectionCounter++;
                }
                
            }
            
        }
    }

    public void resetPrivateVariables()
    {
        StartCoroutine("ResetPrivateVariablesCo");
    }

    public void ResetPrivateVariablesCo()
    {
        constructionLevel = LevelScript.levelValue;
        nextLevelChange = sectionsPerLevel * (1 + constructionLevel) + (sectionsIncrement* constructionLevel);
        if (constructionLevel == 0)
        {
            sectionCounter = 0;
        }
        else {
            sectionCounter = (sectionsPerLevel * constructionLevel) + sectionsIncrement * (constructionLevel - 1);
        }
        
        platformWidth = theCheckpointSection.GetComponent<BoxCollider2D>().size.x * theCheckpointSection.transform.localScale.x;
        goalSet = false;
    }
}
