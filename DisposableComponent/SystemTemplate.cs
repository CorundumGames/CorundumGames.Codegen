//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:6.0.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CorundumGames.Codegen.DisposableComponent {
    using DesperateDevs.Utils;
    using Entitas;
    using System;
    
    
    internal partial class SystemTemplate : SystemTemplateBase {
        
        
        #line 84 "DisposableComponent\SystemTemplate.tt"

    private readonly string _componentName;
    private readonly string _contextName;

    public string SystemName => $"DisposeOf{_contextName.RemoveContextSuffix()}{_componentName.RemoveComponentSuffix()}System";

    internal SystemTemplate(string componentName, string contextName)
    {
        _componentName = componentName ?? throw new ArgumentNullException(nameof(componentName));
        _contextName = contextName ?? throw new ArgumentNullException(nameof(contextName));
    }

        #line default
        #line hidden
        
        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 3 "DisposableComponent\SystemTemplate.tt"
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 3 "DisposableComponent\SystemTemplate.tt"
 /* So Rider highlights the templated parts as C# */ 
            
            #line default
            #line hidden
            
            #line 6 "DisposableComponent\SystemTemplate.tt"
            this.Write("\r\n");
            
            #line default
            #line hidden
            
            #line 7 "DisposableComponent\SystemTemplate.tt"

    var contextName = _contextName.RemoveContextSuffix();
    var entityName = contextName.AddEntitySuffix();
    var matcherName = contextName.AddMatcherSuffix();
    var systemName = SystemName;

            
            #line default
            #line hidden
            
            #line 13 "DisposableComponent\SystemTemplate.tt"
            this.Write("\r\npublic sealed class ");
            
            #line default
            #line hidden
            
            #line 14 "DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( systemName ));
            
            #line default
            #line hidden
            
            #line 14 "DisposableComponent\SystemTemplate.tt"
            this.Write(" : Entitas.IInitializeSystem, Entitas.ITearDownSystem\r\n{\r\n    private static read" +
                    "only Entitas.GroupChanged<");
            
            #line default
            #line hidden
            
            #line 16 "DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( entityName ));
            
            #line default
            #line hidden
            
            #line 16 "DisposableComponent\SystemTemplate.tt"
            this.Write("> _OnEntityRemoved = OnEntityRemoved;\r\n    private static readonly Entitas.GroupU" +
                    "pdated<");
            
            #line default
            #line hidden
            
            #line 17 "DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( entityName ));
            
            #line default
            #line hidden
            
            #line 17 "DisposableComponent\SystemTemplate.tt"
            this.Write("> _OnEntityUpdated = OnEntityUpdated;\r\n    private static readonly Entitas.Contex" +
                    "tEntityChanged _OnEntityWillBeDestroyed = OnEntityWillBeDestroyed;\r\n    private " +
                    "readonly Entitas.IContext<");
            
            #line default
            #line hidden
            
            #line 19 "DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( entityName ));
            
            #line default
            #line hidden
            
            #line 19 "DisposableComponent\SystemTemplate.tt"
            this.Write("> _context;\r\n    private readonly Entitas.IGroup<");
            
            #line default
            #line hidden
            
            #line 20 "DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( entityName ));
            
            #line default
            #line hidden
            
            #line 20 "DisposableComponent\SystemTemplate.tt"
            this.Write("> _group;\r\n\r\n    public ");
            
            #line default
            #line hidden
            
            #line 22 "DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( systemName ));
            
            #line default
            #line hidden
            
            #line 22 "DisposableComponent\SystemTemplate.tt"
            this.Write("(Contexts contexts)\r\n    {\r\n        _context = contexts.");
            
            #line default
            #line hidden
            
            #line 24 "DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( contextName.LowercaseFirst() ));
            
            #line default
            #line hidden
            
            #line 24 "DisposableComponent\SystemTemplate.tt"
            this.Write(";\r\n        _group = _context.GetGroup(");
            
            #line default
            #line hidden
            
            #line 25 "DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( matcherName ));
            
            #line default
            #line hidden
            
            #line 25 "DisposableComponent\SystemTemplate.tt"
            this.Write(".");
            
            #line default
            #line hidden
            
            #line 25 "DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _componentName.RemoveComponentSuffix() ));
            
            #line default
            #line hidden
            
            #line 25 "DisposableComponent\SystemTemplate.tt"
            this.Write(@");
    }

    public void Initialize()
    {
        _group.OnEntityRemoved += _OnEntityRemoved;
        _group.OnEntityUpdated += _OnEntityUpdated;
        _context.OnEntityWillBeDestroyed += _OnEntityWillBeDestroyed;
    }

    public void TearDown()
    {
        using var _ = UnityEngine.Pool.ListPool<");
            
            #line default
            #line hidden
            
            #line 37 "DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( entityName ));
            
            #line default
            #line hidden
            
            #line 37 "DisposableComponent\SystemTemplate.tt"
            this.Write(">.Get(out var buffer);\r\n        foreach (var e in _group.GetEntities(buffer))\r\n  " +
                    "      {\r\n            e.");
            
            #line default
            #line hidden
            
            #line 40 "DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _componentName.RemoveComponentSuffix().LowercaseFirst() ));
            
            #line default
            #line hidden
            
            #line 40 "DisposableComponent\SystemTemplate.tt"
            this.Write(@".Dispose();
        }

        _group.OnEntityRemoved -= _OnEntityRemoved;
        _group.OnEntityUpdated -= _OnEntityUpdated;
        _context.OnEntityWillBeDestroyed -= _OnEntityWillBeDestroyed;
    }

    private static void OnEntityRemoved(
        Entitas.IGroup<");
            
            #line default
            #line hidden
            
            #line 49 "DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( entityName ));
            
            #line default
            #line hidden
            
            #line 49 "DisposableComponent\SystemTemplate.tt"
            this.Write("> group,\r\n        ");
            
            #line default
            #line hidden
            
            #line 50 "DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( entityName ));
            
            #line default
            #line hidden
            
            #line 50 "DisposableComponent\SystemTemplate.tt"
            this.Write(@" entity,
        int index,
        Entitas.IComponent component
    )
    {
        if (component is System.IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    private static void OnEntityUpdated(
        Entitas.IGroup<");
            
            #line default
            #line hidden
            
            #line 62 "DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( entityName ));
            
            #line default
            #line hidden
            
            #line 62 "DisposableComponent\SystemTemplate.tt"
            this.Write("> group,\r\n        ");
            
            #line default
            #line hidden
            
            #line 63 "DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( entityName ));
            
            #line default
            #line hidden
            
            #line 63 "DisposableComponent\SystemTemplate.tt"
            this.Write(@" entity,
        int index,
        Entitas.IComponent previousComponent,
        Entitas.IComponent newComponent
    )
    {
        if (previousComponent is System.IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    private static void OnEntityWillBeDestroyed(Entitas.IContext context, Entitas.IEntity entity)
    {
        if (entity is ");
            
            #line default
            #line hidden
            
            #line 77 "DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( entityName ));
            
            #line default
            #line hidden
            
            #line 77 "DisposableComponent\SystemTemplate.tt"
            this.Write(" e && e.has");
            
            #line default
            #line hidden
            
            #line 77 "DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _componentName.RemoveComponentSuffix() ));
            
            #line default
            #line hidden
            
            #line 77 "DisposableComponent\SystemTemplate.tt"
            this.Write(")\r\n        {\r\n            e.");
            
            #line default
            #line hidden
            
            #line 79 "DisposableComponent\SystemTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( _componentName.RemoveComponentSuffix().LowercaseFirst() ));
            
            #line default
            #line hidden
            
            #line 79 "DisposableComponent\SystemTemplate.tt"
            this.Write(".Dispose();\r\n        }\r\n    }\r\n}\r\n\r\n");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
        }
    }
    
    public class SystemTemplateBase {
        
        private global::System.Text.StringBuilder builder;
        
        private global::System.Collections.Generic.IDictionary<string, object> session;
        
        private global::System.CodeDom.Compiler.CompilerErrorCollection errors;
        
        private string currentIndent = string.Empty;
        
        private global::System.Collections.Generic.Stack<int> indents;
        
        private ToStringInstanceHelper _toStringHelper = new ToStringInstanceHelper();
        
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session {
            get {
                return this.session;
            }
            set {
                this.session = value;
            }
        }
        
        public global::System.Text.StringBuilder GenerationEnvironment {
            get {
                if ((this.builder == null)) {
                    this.builder = new global::System.Text.StringBuilder();
                }
                return this.builder;
            }
            set {
                this.builder = value;
            }
        }
        
        protected global::System.CodeDom.Compiler.CompilerErrorCollection Errors {
            get {
                if ((this.errors == null)) {
                    this.errors = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errors;
            }
        }
        
        public string CurrentIndent {
            get {
                return this.currentIndent;
            }
        }
        
        private global::System.Collections.Generic.Stack<int> Indents {
            get {
                if ((this.indents == null)) {
                    this.indents = new global::System.Collections.Generic.Stack<int>();
                }
                return this.indents;
            }
        }
        
        public ToStringInstanceHelper ToStringHelper {
            get {
                return this._toStringHelper;
            }
        }
        
        public void Error(string message) {
            this.Errors.Add(new global::System.CodeDom.Compiler.CompilerError(null, -1, -1, null, message));
        }
        
        public void Warning(string message) {
            global::System.CodeDom.Compiler.CompilerError val = new global::System.CodeDom.Compiler.CompilerError(null, -1, -1, null, message);
            val.IsWarning = true;
            this.Errors.Add(val);
        }
        
        public string PopIndent() {
            if ((this.Indents.Count == 0)) {
                return string.Empty;
            }
            int lastPos = (this.currentIndent.Length - this.Indents.Pop());
            string last = this.currentIndent.Substring(lastPos);
            this.currentIndent = this.currentIndent.Substring(0, lastPos);
            return last;
        }
        
        public void PushIndent(string indent) {
            this.Indents.Push(indent.Length);
            this.currentIndent = (this.currentIndent + indent);
        }
        
        public void ClearIndent() {
            this.currentIndent = string.Empty;
            this.Indents.Clear();
        }
        
        public void Write(string textToAppend) {
            this.GenerationEnvironment.Append(textToAppend);
        }
        
        public void Write(string format, params object[] args) {
            this.GenerationEnvironment.AppendFormat(format, args);
        }
        
        public void WriteLine(string textToAppend) {
            this.GenerationEnvironment.Append(this.currentIndent);
            this.GenerationEnvironment.AppendLine(textToAppend);
        }
        
        public void WriteLine(string format, params object[] args) {
            this.GenerationEnvironment.Append(this.currentIndent);
            this.GenerationEnvironment.AppendFormat(format, args);
            this.GenerationEnvironment.AppendLine();
        }
        
        public class ToStringInstanceHelper {
            
            private global::System.IFormatProvider formatProvider = global::System.Globalization.CultureInfo.InvariantCulture;
            
            public global::System.IFormatProvider FormatProvider {
                get {
                    return this.formatProvider;
                }
                set {
                    if ((value != null)) {
                        this.formatProvider = value;
                    }
                }
            }
            
            public string ToStringWithCulture(object objectToConvert) {
                if ((objectToConvert == null)) {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                global::System.Type type = objectToConvert.GetType();
                global::System.Type iConvertibleType = typeof(global::System.IConvertible);
                if (iConvertibleType.IsAssignableFrom(type)) {
                    return ((global::System.IConvertible)(objectToConvert)).ToString(this.formatProvider);
                }
                global::System.Reflection.MethodInfo methInfo = type.GetMethod("ToString", new global::System.Type[] {
                            iConvertibleType});
                if ((methInfo != null)) {
                    return ((string)(methInfo.Invoke(objectToConvert, new object[] {
                                this.formatProvider})));
                }
                return objectToConvert.ToString();
            }
        }
    }
}
