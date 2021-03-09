using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global 
{
    public static bool Debug = true;
    public static int RoundCount = 0;
    public static int DefaultBloodValue = 100;
    public static int currentBloodValue = 100;

    public static Vector3 lastEveryEventPosition = new Vector3(-1.28f, -1, 0);
    public static Vector3 lastEveryEnemyEventPosition = new Vector3(-1.28f, 1, 0);

    public static  float everyEventOffset = 1.28f;
    public static int target = 0;
    public static int EnemyTarget = 0;

    public static List<EveryEvent> AllEventlist = new List<EveryEvent>();
    public static List<EveryEvent> AllEnemyEventlist = new List<EveryEvent>();

    public static Vector3 allEventVirtualPosition = new Vector3(0, 0, 0);
    public static Vector3 allEnemyEventVirtualPosition = new Vector3(0, 0, 0);

    public static bool Clicked = false;
    public static bool Clicked_Enemy = false;
    public static bool StartSlide = false;

    public static bool NextRound = false;
    public static bool CurrentRoundCompleted = false;

    public static List<Collider2D> EveryEventGameObjectsList = new List<Collider2D>();
    public static List<Collider2D> EnemyEveryEventGameObjectsList = new List<Collider2D>();

    public static int[] Players = { 0, 1 };
    public static int[] Races = { 1, 2, 3, 4, 5, 6, 7 };
    public static int[] Skills = { 1, 2, 3, 4, 5, 6, 7 };

}
