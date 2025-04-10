using Landfall.Haste;
using Landfall.Modding;
using System.Reflection;
using UnityEngine;

namespace FCR;

[LandfallPlugin]
public class Program
{
    static Program()
    {
        On.MainCamera.AddFovImpulse += MainCamera_AddFovImpulse;
        On.CameraMovement.Update += CameraMovement_Update;
    }

    private static void MainCamera_AddFovImpulse(On.MainCamera.orig_AddFovImpulse orig, MainCamera self, float force)
    {
        return;
    }

    private static void CameraMovement_Update(On.CameraMovement.orig_Update orig, CameraMovement self)
    {
        orig(self);
        if (Camera.main == null)
        {
            return;
        }

        Type type = typeof(CameraMovement);
        FieldInfo field = type.GetField("baseFOV", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        Camera.main.fieldOfView = (float)field.GetValue(self);
    }
}