﻿using System.Collections.ObjectModel;

namespace Engine.Models
{
    public class Project : BaseNotificationClass
    {
        private bool _isComplete;
        private bool _isDirty;

        public string Name { get; private set; }
        public Language OutputLanguage { get; private set; }

        public ObservableCollection<Method> Methods { get; }

        public bool IsDirty
        {
            get { return _isDirty; }
            private set
            {
                _isDirty = value;

                NotifyPropertyChanged("IsDirty");
            }
        }

        public bool IsComplete
        {
            get { return _isComplete; }
            private set
            {
                _isComplete = value;
                
                NotifyPropertyChanged("IsComplete");
            }
        }

        public Project(string name, Language outputLanguage)
        {
            Name = name;
            OutputLanguage = outputLanguage;

            Methods = new ObservableCollection<Method>();

            IsDirty = false;
            IsComplete = false;
        }

        public void AddMethod(MethodAction methodAction, string name)
        {
            //TODO: Prevent duplicate method names.

            Method method = new Method(methodAction, name);

            Methods.Add(method);

            foreach(Method existingMethod in Methods)
            {
                existingMethod.AddChainableMethods(method);
            }

            IsDirty = true;
            IsComplete = false;
        }
    }
}