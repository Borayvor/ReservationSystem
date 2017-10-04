using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// By Ivaylo Kenov -> https://github.com/ivaylokenov/ASP.NET-MVC-Lambda-Expression-Helpers
/// </summary>
namespace ReservationSystem.Extensions
{
  public static class MvcExtensions
  {
    private const string ControllerSuffix = "Controller";

    private static ConcurrentDictionary<MethodInfo, string> actionNameInfo = new ConcurrentDictionary<MethodInfo, string>();
    private static ConcurrentDictionary<Type, string> routeAreaInfo = new ConcurrentDictionary<Type, string>();

    public static string GetActionName(this LambdaExpression actionExpression)
    {
      var methodCallExpression = actionExpression.Body as MethodCallExpression;
      if (methodCallExpression?.Object == null)
      {
        throw new ArgumentOutOfRangeException(
            nameof(actionExpression),
            "Expected instance method call expression but received other type of expression instead.");
      }

      var actionMethod = methodCallExpression.Method;
      string result = string.Empty;

      if (actionNameInfo.TryGetValue(actionMethod, out result))
      {
        return result;
      }

      var actionNameAttribute = actionMethod.GetCustomAttribute<ActionNameAttribute>();
      result = actionNameAttribute?.Name ?? actionMethod.Name;
      actionNameInfo.TryAdd(actionMethod, result);

      return result;
    }

    public static string GetControllerName(this Type controllerType)
    {
      string typeName = controllerType.Name;
      return typeName.Substring(0, typeName.Length - ControllerSuffix.Length);
    }

    public static string GetAreaName(this Type type)
    {
      if (routeAreaInfo.TryGetValue(type, out string result))
      {
        return result;
      }

      var routeAreaAttribute = type.GetCustomAttribute<AreaAttribute>();
      if (routeAreaAttribute != null)
      {
        routeAreaInfo.TryAdd(type, routeAreaAttribute.RouteValue);
        return routeAreaAttribute.RouteValue;
      }

      string[] namespaceParts = (type.Namespace ?? string.Empty).ToLowerInvariant().Split('.');
      int areaIndex = GetAreaIndex(namespaceParts);
      result = areaIndex < 0 ? string.Empty : namespaceParts[areaIndex + 1];
      routeAreaInfo.TryAdd(type, result);
      return result;
    }

    private static int GetAreaIndex(IReadOnlyList<string> namespaceParts)
    {
      for (int i = 0; i < namespaceParts.Count; i++)
      {
        if (namespaceParts[i] == "areas")
        {
          return i;
        }
      }

      return -1;
    }
  }
}
