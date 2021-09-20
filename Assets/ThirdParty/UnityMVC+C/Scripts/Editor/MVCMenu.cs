﻿using UnityEditor;
using UnityMVC.Component;
using UnityMVC.Controller;
using UnityMVC.Editor;
using UnityMVC.View;

public class MVCMenu : EditorWindow
{
    //[MenuItem("Unity MVC+C/Tools/Update Partials", priority = 200)]
    private static void UpdatePartials()
    {
        var controllers = MVCToolsMenu.GetPaths(ScriptType.Controller, typeof(Controller));
        var views = MVCToolsMenu.GetPaths(ScriptType.View, typeof(View));
        var components = MVCToolsMenu.GetPaths(ScriptType.MVCComponent, typeof(MVCComponent));
        var componentGroups = MVCToolsMenu.GetPaths(ScriptType.MVCComponentGroup, typeof(MVCComponentGroup));

        foreach (var item in controllers)
        {
            MVCCodeGenerator.UpdatePartial(item.nameSpace, item.type, item.name, item.baseType, item.path, item.view);
        }
        foreach (var item in views)
        {
            MVCCodeGenerator.UpdatePartial(item.nameSpace, item.type, item.name, item.baseType, item.path, item.view);
        }
        foreach (var item in components)
        {
            MVCCodeGenerator.UpdatePartial(item.nameSpace, item.type, item.name, item.baseType, item.path, item.view);
        }
        foreach (var item in componentGroups)
        {
            MVCCodeGenerator.UpdatePartial(item.nameSpace, item.type, item.name, item.baseType, item.path, item.view);
        }
    }

    [MenuItem("Unity MVC+C/Update Folder Structure", priority = 300)]
    private static void CreateFolderStructure()
    {
        MVCFolderStructure.SetupProjectFolder();
    }
}