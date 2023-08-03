using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace UnityToolbarExtender.Examples
{
    internal static class ToolbarStyles
    {
        public static readonly GUIStyle CommandButtonStyle;

        static ToolbarStyles()
        {
            CommandButtonStyle = new GUIStyle("Command")
            {
                fontSize = 16,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Bold
            };
        }
    }

    [InitializeOnLoad]
    public class SceneSwitchLeftButton
    {
        static SceneSwitchLeftButton()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }

        private static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();

            if (GUILayout.Button(new GUIContent("GO!", "BaseGo"), ToolbarStyles.CommandButtonStyle))
            {
                //SceneHelper.OpenLastLaunchScene();
                SceneHelper.StartScene("Base");
                
            }
            
            if (GUILayout.Button(new GUIContent("BA", "Base"), ToolbarStyles.CommandButtonStyle))
            {
                //SceneHelper.OpenLastLaunchScene();
                SceneHelper.OpenScene("Base");
            }
            
            if (GUILayout.Button(new GUIContent("ME", "Menu"), ToolbarStyles.CommandButtonStyle))
            {
                //SceneHelper.OpenLastLaunchScene();
                SceneHelper.OpenScene("Menu");
            }

            if (GUILayout.Button(new GUIContent("TL", "Training level"), ToolbarStyles.CommandButtonStyle))
            {
                //SceneHelper.OpenLastLaunchScene();
                SceneHelper.OpenScene("TrainingLevel");
            }            

            if (GUILayout.Button(new GUIContent("1", "Level 1"), ToolbarStyles.CommandButtonStyle))
            {
                //SceneHelper.OpenLastLaunchScene();
                SceneHelper.OpenScene("Level1");
            }

            if (GUILayout.Button(new GUIContent("2", "Level 2"), ToolbarStyles.CommandButtonStyle))
            {
                //SceneHelper.OpenLastLaunchScene();
                SceneHelper.OpenScene("Level2");
            }

            if (GUILayout.Button(new GUIContent("3", "Level 3"), ToolbarStyles.CommandButtonStyle))
            {
                //SceneHelper.OpenLastLaunchScene();
                SceneHelper.OpenScene("Level3");
            }

            if (GUILayout.Button(new GUIContent("4", "Level 4"), ToolbarStyles.CommandButtonStyle))
            {
                //SceneHelper.OpenLastLaunchScene();
                SceneHelper.OpenScene("Level4");
            }

            if (GUILayout.Button(new GUIContent("5", "Level 5"), ToolbarStyles.CommandButtonStyle))
            {
                //SceneHelper.OpenLastLaunchScene();
                SceneHelper.OpenScene("Level5");
            }       
            
            if (GUILayout.Button(new GUIContent("6", "Level 6"), ToolbarStyles.CommandButtonStyle))
            {
                //SceneHelper.OpenLastLaunchScene();
                SceneHelper.OpenScene("Level6");
            }       
            
            if (GUILayout.Button(new GUIContent("7", "Level 7"), ToolbarStyles.CommandButtonStyle))
            {
                //SceneHelper.OpenLastLaunchScene();
                SceneHelper.OpenScene("Level7");
            }      
            
            if (GUILayout.Button(new GUIContent("8", "Level 8"), ToolbarStyles.CommandButtonStyle))
            {
                //SceneHelper.OpenLastLaunchScene();
                SceneHelper.OpenScene("Level8");
            }
        }
    }


    static class SceneHelper
    {
        private static string _sceneToOpen;

        public static void StartScene(string sceneName)
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }

            _sceneToOpen = sceneName;
            EditorApplication.update += OnUpdate;
        }

        public static void OpenScene(string sceneName)
        {
            var guids = AssetDatabase.FindAssets("t:scene " + sceneName, null);
            if (guids.Length == 0)
            {
                Debug.LogWarning("Couldn't find scene file");
            }
            else
            {
                var scenePath = AssetDatabase.GUIDToAssetPath(guids[0]);
                EditorSceneManager.OpenScene(scenePath);
            }
        }


        static void OnUpdate()
        {
            if (_sceneToOpen == null ||
                EditorApplication.isPlaying || EditorApplication.isPaused ||
                EditorApplication.isCompiling || EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return;
            }

            EditorApplication.update -= OnUpdate;

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                // need to get scene via search because the path to the scene
                // file contains the package version so it'll change over time
                var guids = AssetDatabase.FindAssets("t:scene " + _sceneToOpen, null);
                if (guids.Length == 0)
                {
                    Debug.LogWarning("Couldn't find scene file");
                }
                else
                {
                    var scenePath = AssetDatabase.GUIDToAssetPath(guids[0]);
                    EditorSceneManager.OpenScene(scenePath);
                    EditorApplication.isPlaying = true;
                }
            }

            _sceneToOpen = null;
        }
    }
}