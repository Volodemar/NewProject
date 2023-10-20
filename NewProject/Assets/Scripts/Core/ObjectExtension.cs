using System.Collections.Generic;
using UnityEngine;

public static class ObjectExtension 
{
     private static List<Object> savedObjects = new List<Object>();
     
     public static void DontDestroyOnLoad(this Object obj){
         savedObjects.Add(obj);
         Object.DontDestroyOnLoad(obj);
     }
     
     public static void Destory(this Object obj){
         savedObjects.Remove(obj);
         Destory(obj);
     }
     
     public static List<Object> GetSavedObjects(){ 
         return new List<Object>(savedObjects); 
     }
}
