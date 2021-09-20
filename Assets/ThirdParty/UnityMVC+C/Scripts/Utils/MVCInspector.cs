using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityMVC.Component;

namespace UnityMVC.Editor
{

    public class MVCDataDependencies
    {
        public MVCInspectorData controllers;
        public MVCInspectorData views;
        public MVCInspectorData mvcComponents;
        public MVCInspectorData mvcComponentGroups;
        public MVCInspectorData unityComponents;
    }

    public class MVCInspectorData
    {
        public Type src;
        public List<MVCInspectorDataTypeResult> results = new List<MVCInspectorDataTypeResult>();
    }

    public class MVCInspectorDataTypeResult
    {
        public Type type;
        public List<FieldInfo> dependenciesRoot;
    }
    public class MVCInspector
    {
        public static MVCDataDependencies GetDependenciesList()
        {
            MVCDataDependencies data = new MVCDataDependencies();

            data.controllers = GetDependencies(typeof(Controller.Controller));
            data.views = GetDependencies(typeof(View.View));
            data.unityComponents = GetDependencies(typeof(UnityComponent.UnityComponent));
            data.mvcComponentGroups = GetDependencies(typeof(MVCComponentGroup));

            var mvcComponents = GetDependencies(typeof(MVCComponent));
            //mvcComponents.results = mvcComponents.results.Where(x => !(x is MVCComponentGroup)).ToList();
            data.unityComponents = mvcComponents;

            return data;
        }
        
        public static MVCInspectorData GetDependencies(Type requiredType)
        {
            MVCInspectorData data = new MVCInspectorData();
            data.src = requiredType;

            List<Type> controllers = Assembly.GetAssembly(requiredType).GetTypes().ToList();
            
            List<Type> filteredTypes = controllers.Where(x =>
                x.IsClass &&
                !x.IsAbstract &&
                x.IsSubclassOf(requiredType) &&
                !x.Name.Contains("Template")).ToList();

            foreach (Type type in filteredTypes)
            {
                MVCInspectorDataTypeResult result = new MVCInspectorDataTypeResult();
                result.type = type;
                result.dependenciesRoot = GetFields(type, requiredType);
                data.results.Add(result);
            }
            return data;
        }

        private static List<FieldInfo> GetFields(Type target, Type srcType)
        {
            List<FieldInfo> dependencies = new List<FieldInfo>();
            List<Type> types = Assembly.GetAssembly(srcType).GetTypes().ToList();


            List<Type> filteredTypes = types.Where(x =>
                x.IsClass &&
                !x.IsAbstract &&
                !x.Name.Contains("Template") &&
                x.Namespace != target.Namespace &&
                !(x.Name.Contains($"{target.Name}Events") && x.Namespace.Contains($"MVC.Events")) &&
                !(x.Name.Contains($"{target.Name}Model") && x.Namespace.Contains($"MVC.Model")) &&
                x != target).ToList();

            List<FieldInfo> fields = target.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).ToList();

            foreach (FieldInfo field in fields)
            {
                if (filteredTypes.Contains(field.FieldType))
                {
                    if (IsMVC(field.FieldType))
                    {
                        dependencies.Add(field);
                    }
                }
            }

            return dependencies;
        }

        private static bool IsMVC(Type type)
        {
            bool isController = type.BaseType == typeof(Controller.Controller);
            bool isView = type.BaseType == typeof(View.View);
            bool isMvcComponent = type.BaseType == typeof(MVCComponent);
            bool isUnityComponent = type.BaseType == typeof(UnityComponent.UnityComponent);

            return isController || isView || isMvcComponent || isUnityComponent;
        }
    }
    
}