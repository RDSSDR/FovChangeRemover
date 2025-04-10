using Landfall.Haste;
using Landfall.Modding;
using System.Reflection;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Localization;
using Zorro.Settings;

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

        /*Type type = typeof(CameraMovement);
        FieldInfo field = type.GetField("baseFOV", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        Camera.main.fieldOfView = (float)field.GetValue(self);*/
        Camera.main.fieldOfView = GameHandler.Instance.SettingsHandler.GetSetting<FovSetting>().Value;
    }
}

[HasteSetting]
public class FovSetting : FloatSetting, IExposedSetting
{
    public override void ApplyValue() => Debug.Log($"Mod apply value {Value}");
    protected override float GetDefaultValue() => 120;
    protected override float2 GetMinMaxValue() => new(0, 200);
    public LocalizedString GetDisplayName() => new UnlocalizedString("FOV");
    public string GetCategory() => SettingCategory.Graphics;
}