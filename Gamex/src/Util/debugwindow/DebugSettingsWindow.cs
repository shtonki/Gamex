using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Configuration;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Gamex.src.Util.Settingsx;

namespace Gamex.src.Util.DebugWindow
{
    public partial class DebugSettingsWindow : UserControl
    {
        private Dictionary<TreeNode, SettingInfo> NodeToInfoTable; 

        public DebugSettingsWindow()
        {
            InitializeComponent();

            InitializeTree();
        }

        private void InitializeTree()
        {
            NodeToInfoTable = new Dictionary<TreeNode, SettingInfo>();
            SettingsTreeView.Nodes.Add(BuildNode(Settings.Tree, null));
            SettingsTreeView.Nodes[0].Expand();
        }

        /// <summary>
        /// Gets the PropertyInfo of properties with the SettingInfo attribute from the Type 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private PropertyInfo[] PropertyInfoFromType(Type type)
        {
            return type.GetProperties()
                .Where(p => p.IsDefined(typeof (SettingInfo)))
                .ToArray();
        }

        private SettingInfo InfoFromProperty(PropertyInfo propertyInfo)
        {
            var info = propertyInfo.GetCustomAttributes(typeof(SettingInfo), false);

            if (info.Length < 1)
            {
                // property has no information
                return null;
            }
            if (info.Length > 1)
            {
                // property has too much infomation
                throw new GameBrokenException();
            }
            if (!(info[0] is SettingInfo))
            {
                // something is seriously fucked
                throw new GameBrokenException();
            }

            return (SettingInfo)info[0];
        }

        /// <summary>
        /// Takes an object and the SettingInfo object for said object and generates a TreeNode from it and all it's children
        /// Fills out some shit in the SettingsInfo 
        /// </summary>
        /// <param name="root">The root object</param>
        /// <param name="infox">SettingInfo of the setting</param>
        /// <returns></returns>
        private TreeNode BuildNode(object root, SettingInfo infox)
        {
            
            var rootType = root.GetType();
            var rootProperties = PropertyInfoFromType(rootType);

            string title;
            if (infox == null)
            {
                title = "Settings";
            }
            else
            {
                title = infox.SettingName;
            }
            var rootNode = new TreeNode(title);

            foreach (var propertyInfo in rootProperties)
            {
                var settingInfo = InfoFromProperty(propertyInfo);
                var propertyType = propertyInfo.PropertyType;
                settingInfo.SettingType = propertyType;
                settingInfo.SettingName = propertyInfo.Name;
                
                settingInfo.SetValueFunctionHandle = (value) => propertyInfo.SetValue(root, value);
                settingInfo.GetValueFunctionHandle = () => propertyInfo.GetValue(root);
                
                rootNode.Nodes.Add(BuildNode(settingInfo.GetValueFunctionHandle(), settingInfo));
            }

            NodeToInfoTable[rootNode] = infox;
            return rootNode;
        }

        private Control ControlFromSetting(SettingInfo info, System.Drawing.Size size)
        {
            Panel p = new Panel();
            p.Size = size;

            Label SettingNameLabel = new Label();
            SettingNameLabel.Text = info.SettingName;
            SettingNameLabel.Size = new System.Drawing.Size(size.Width, 20);
            p.Controls.Add(SettingNameLabel);
            
            var CommentLabel = new Label();
            CommentLabel.Text = info.Comment;
            CommentLabel.Size = new System.Drawing.Size(size.Width, 20);
            CommentLabel.Location = new Point(0, SettingNameLabel.Bounds.Bottom);
            p.Controls.Add(CommentLabel);


            var ValueLabel = new Label();
            ValueLabel.Text = info.Value.ToString();
            ValueLabel.Size = new System.Drawing.Size(size.Width, 20);
            ValueLabel.Location = new Point(0, CommentLabel.Bounds.Bottom);
            p.Controls.Add(ValueLabel);

            var InputBox = new TextBox();
            InputBox.Size = new System.Drawing.Size(size.Width, 20);
            InputBox.Location = new Point(0, ValueLabel.Bounds.Bottom);
            InputBox.KeyDown += (sender, args) =>
            {
                if (args.KeyCode == Keys.Enter)
                {
                    SetSetting(info, InputBox.Text);
                    InputBox.Text = "";
                    ValueLabel.Text = info.Value.ToString();
                }
            };
            p.Controls.Add(InputBox);

            return p;
        }

        private void SetSetting(SettingInfo info, string raw)
        {
            object o;

            if (info.SettingType == typeof (bool))
            {
                bool b;
                if (!Boolean.TryParse(raw, out b)) { return; }
                o = b;
            }
            else if (info.SettingType == typeof (int))
            {
                int i;
                if (!Int32.TryParse(raw, out i)) { return; }
                o = i;
            }
            else
            {
                return;
            }

            info.Value = o;
        }

        private void HandleSelectChanged(object sender, TreeViewEventArgs e)
        {
            HandleNodeSelected(e.Node);
        }

        private void HandleNodeSelected(TreeNode node)
        {
            // if we're a leaf node, i.e. we're an actual setting
            if (node.LastNode == null)
            {
                SettingInfoPanel.Controls.Clear();

                var settingInfo = NodeToInfoTable[node];
                var infoPanel = ControlFromSetting(settingInfo, SettingInfoPanel.ClientSize);
                SettingInfoPanel.Controls.Add(infoPanel);
            }
        }
    }
}
