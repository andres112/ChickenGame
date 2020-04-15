using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
 public class DifficultyClasses
 {
     [System.Serializable]
     public struct classData{
         public GameObject[] sections;
     }
     public classData[] classes;
 }
