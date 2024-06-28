using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePassVariables: MonoBehaviour
{
    public static bool CenterstageField;
    public static bool CenterstageBots;
    public static bool PowerplayField;
    public static bool PowerplayBots;
    public static bool RelicrecoveryField;
    public static bool RelicrecoveryBots;

    public static void ToggleCenterstageField()
    {
        CenterstageField = !CenterstageField;
    }
    public static void ToggleCenterstageBots()
    {
        CenterstageBots = !CenterstageBots;
    }
    public static void TogglePowerplayField()
    {
        PowerplayField = !PowerplayField;
    }
    public static void TogglePowerplayBots()
    {
        PowerplayBots = !PowerplayBots;
    }
    public static void ToggleRelicrecoveryField() 
    {
        RelicrecoveryField = !RelicrecoveryField;
    }
    public static void ToggleRelicrecoveryBots()
    {
        RelicrecoveryBots = !RelicrecoveryBots;
    }
    public void OnDestroy()
    {
        string toStore = "";
        if (CenterstageField) {toStore += "1";} else{toStore += "0";}
        if (CenterstageBots) {toStore += "1";} else { toStore += "0"; }
        if (PowerplayField) { toStore += "1"; } else { toStore += "0"; }
        if (PowerplayBots) { toStore += "1"; } else { toStore += "0"; }
        if (RelicrecoveryField) { toStore += "1"; } else { toStore += "0"; }
        if (RelicrecoveryBots) { toStore += "1"; } else { toStore += "0"; }
        PlayerPrefs.SetString("Activated", toStore);
    }

}
