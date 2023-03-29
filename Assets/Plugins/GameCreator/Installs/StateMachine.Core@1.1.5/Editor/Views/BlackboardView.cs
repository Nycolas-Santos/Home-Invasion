using System;
using GameCreator.Editor.Common;
using GameCreator.Runtime.Common;
using NinjutsuGames.StateMachine.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace NinjutsuGames.StateMachine.Editor
{
    public class BlackboardView : PinnedElementView
    {
        protected BaseGraphView graphView;

        private new const string title = "Blackboard";
        private const string USS_PATH = EditorPaths.VARIABLES + "StyleSheets/RuntimeGlobalList";
        private const string NAME_LIST = "GC-RuntimeGlobal-List-Head";
        private const string CLASS_LIST_ELEMENT = "gc-runtime-global-list-element";
        private readonly string exposedParameterViewStyle = "GraphProcessorStyles/ExposedParameterView";
        private RunnerNameListTool fieldList;

        // private List<Rect> blackboardLayouts = new List<Rect>();

        public BlackboardView()
        {
            var style = Resources.Load<StyleSheet>(exposedParameterViewStyle);
            if (style != null)
                styleSheets.Add(style);
        }

        public static Action OnListChanged;

        protected virtual void UpdateParameterList()
        {
            fieldList.Refresh();
            OnListChanged?.Invoke();
        }

        protected override void Initialize(BaseGraphView graphView)
        {
            this.graphView = graphView;
            base.title = title;
            scrollable = true;
            
            var serializedObject = new SerializedObject(this.graphView.graph);
            var listProperty = serializedObject.FindProperty("m_NameList");
            fieldList = new RunnerNameListTool(listProperty);
            switch (EditorApplication.isPlaying)
            {
                case true:
                    var graphEditor = UnityEditor.Editor.CreateEditor(this.graphView.graph);
                    content.Add(graphEditor.CreateInspectorGUI());
                    // PaintRuntime();
                    // content.Add(new NameListView(listProperty.FindPropertyRelative("m_Runtime")));
                    break;

                case false:
                    content.Add(fieldList);
                    fieldList.EventChangeSize += _ => { UpdateParameterList(); };
                    break;
            }

            // content.Add(new PropertyTool(listProperty));
            SetPosition(new Rect(0, 20, 350, 350));
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            graphView.onExposedParameterListChanged += UpdateParameterList;
            graphView.initialized += UpdateParameterList;
            Undo.undoRedoPerformed += UpdateParameterList;

            UpdateParameterList();
        }

        private void OnPlayModeStateChanged(PlayModeStateChange mode)
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            graphView.ClosePinned<BlackboardView>(this);
        }
        
        protected void PaintRuntime()
        {
            var variables = graphView.graph;
            if (variables == null) return;

            variables.Unregister(RuntimeOnChange);
            variables.Register(RuntimeOnChange);

            RuntimeOnChange(string.Empty);
        }

        private void RuntimeOnChange(string variableName)
        {
            this.content.Clear();
            this.content.styleSheets.Clear();

            StyleSheet[] sheets = StyleSheetUtils.Load(USS_PATH);
            foreach (StyleSheet styleSheet in sheets) this.content.styleSheets.Add(styleSheet);

            VisualElement content = new VisualElement
            {
                name = NAME_LIST
            };

            var variables = graphView.graph;
            if (variables == null) return;

            string[] names = variables.Names;
            foreach (string id in names)
            {
                Image image = new Image
                {
                    image = variables.Icon(id)
                };

                Label title = new Label(variables.Title(id));
                title.style.color = ColorTheme.Get(ColorTheme.Type.TextNormal);

                VisualElement element = new VisualElement();
                element.AddToClassList(CLASS_LIST_ELEMENT);

                element.Add(image);
                element.Add(title);

                content.Add(element);
            }

            this.content.Add(content);
        }
    }
}