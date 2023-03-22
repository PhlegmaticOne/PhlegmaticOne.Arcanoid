using Libs.Popups.ViewModels.Commands;
using UnityEditor;
using UnityEngine;

namespace Popups.Start.Commands
{
    public class ExitGameCommand : ICommand
    {
        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            #if UNITY_EDITOR
                if(EditorApplication.isPlaying) 
                {
                    EditorApplication.isPlaying = false;
                    return;
                }
            #endif
            Application.Quit();
        }
    }
}