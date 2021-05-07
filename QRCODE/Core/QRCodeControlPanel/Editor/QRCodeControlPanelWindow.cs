using System.Collections.Generic;
using System.Linq;
using QRCode.Audio;
using QRCode.Editor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace QRCode.ControlPanel
{
    public class QRCodeControlPanelWindow : OdinMenuEditorWindow
    {
        //---<INITIALIZATION>------------------------------------------------------------------------------------------<
        [MenuItem("QRCode/Control Panel")]
        private static void OpenWindow()
        {
            GetWindow<QRCodeControlPanelWindow>().Show();
        }

        //---<CORE>----------------------------------------------------------------------------------------------------<
        protected override OdinMenuTree BuildMenuTree()
        {
            //--<INIT TREE>
            var tree = new OdinMenuTree(true, OdinMenuStyle.TreeViewStyle)
            {
                DefaultMenuStyle = {IconSize = 28f}, 
                Config = {DrawSearchToolbar = true, DrawScrollView = true}
            };

            //--<SETTINGS TREE>
            var settingIcon = QREditorIcons.GetEmoticonASprite(EditorEmoticon.SettingEmoticon);
            tree.Add("  Settings", QRCodeControlPanelSettings.Instance, settingIcon );
            
            //--<SOUND TREE>
            SoundOverview.Instance.UpdateAllSoundOverview();
            var soundIcon = QREditorIcons.GetEmoticonASprite(EditorEmoticon.SoundEmoticon);
            tree.Add("  Sounds", new SoundTable(SoundOverview.Instance.allSounds), soundIcon);
            var soundPath = QRCodeControlPanelUtilities.SoundSystemSettings.soundPath;
            tree.AddAllAssetsAtPath("  Sounds", soundPath, typeof(Sound), true)
                .ForEach(AddDragHandles);
            
            return tree;
        }
        
        private void AddDragHandles(OdinMenuItem menuItem)
        {
            menuItem.OnDrawItem += x => DragAndDropUtilities.DragZone(
                menuItem.Rect, 
                menuItem.Value, 
                false, 
                false);
        }

        protected override void OnBeginDrawEditors()
        {
            var selected = this.MenuTree.Selection.FirstOrDefault();
            var toolBarHeight = MenuTree.Config.SearchToolbarHeight;
            SirenixEditorGUI.BeginHorizontalToolbar(toolBarHeight);
            {
                if (selected != null)
                    GUILayout.Label(selected.Name);
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
    }

    //---<SOUND>-------------------------------------------------------------------------------------------------------<
    #region Sound
    
    public class SoundTable
    {
        [TableList(AlwaysExpanded =  true, IsReadOnly = true, ShowPaging = true), ShowInInspector]
        private readonly List<SoundWrapper> allSounds;

        public Sound this[int index]
        {
            get => allSounds[index].Sound;
        }

        public SoundTable(IEnumerable<Sound> sounds)
        {
            allSounds = sounds.Select(x => new SoundWrapper(x)).ToList();
        }
            
        private class SoundWrapper
        {
            private Sound _sound;

            public Sound Sound
            {
                get => _sound;
            }

            public SoundWrapper(Sound sound)
            {
                this._sound = sound;
            }

            //---Sound------------------------------------
            
            [BoxGroup("@SoundName", order: 0)]
            [ShowInInspector]
            [InlineButton("ApplyName")]
            [InlineButton("SelectObject", "\u2794")]
            public string SoundName
            {
                get => _sound.name;
                set
                {
                    _sound.name = value;
                    EditorUtility.SetDirty(_sound);
                }
            }
            
            [BoxGroup("@SoundName", order: 0)]
            [ShowInInspector]
            [InlineEditor(InlineEditorModes.SmallPreview, InlineEditorObjectFieldModes.Foldout)]
            [InlineButton("SelectClip", "\u2794")]
            [ShowIf("@_sound.GetType() != typeof(MultipleSound)")]
            public AudioClip AudioClip
            {
                get => _sound.audioClip;
                set
                {
                    _sound.audioClip = value;
                    EditorUtility.SetDirty(_sound);
                }
            }

            //---Settings---------------------------------------------------
            
            [FoldoutGroup("Options", order: 2, Expanded = true)]
            [ShowInInspector]
            [EnumPaging]
            public SoundType SoundType
            {
                get => _sound.soundType;
                set
                {
                    _sound.soundType = value;
                    EditorUtility.SetDirty(_sound);
                }
            }

            [FoldoutGroup("Options", order: 2)]
            [ShowInInspector]
            [HideIf("@!this.UseRandomVolume" )]
            [MinMaxSlider(0f, 1f, true)]
            public Vector2 RandomVolume
            {
                get => _sound.rdmVolume;
                set
                {
                    _sound.rdmVolume = value;
                    EditorUtility.SetDirty(_sound);
                }
            }
            
            [FoldoutGroup("Options", order: 2)]
            [HideIf("@this.UseRandomVolume")]
            [ShowInInspector]
            [PropertyRange(0f, 1f)]
            public float Volume
            {
                get => _sound.volume;
                set
                {
                    _sound.volume = value;
                    EditorUtility.SetDirty(_sound);
                }
            }

            [MinMaxSlider(-3f, 3f, true)]
            [ShowInInspector]
            [HideIf("@!this.UseRandomPicth")]
            [FoldoutGroup("Options", order: 2)]
            public Vector2 RandomPitch
            {
                get => _sound.rdmPitch;
                set
                {
                    _sound.rdmPitch = value;
                    EditorUtility.SetDirty(_sound);
                }
            }
            
            [FoldoutGroup("Options", order: 2)]
            [HideIf("@this.UseRandomPicth")]
            [ShowInInspector]
            [PropertyRange(-3f, 3f)]
            public float Pitch
            {
                get => _sound.pitch;
                set
                {
                    _sound.pitch = value;
                    EditorUtility.SetDirty(_sound);
                }
            }
            
            [FoldoutGroup("Options", order: 2)]
            [HideIf("@this.UseRandomPicth")]
            [ShowInInspector]
            [PropertyRange(0f, 1f)]
            public float SpatialBlend
            {
                get => _sound.spatialBlend;
                set
                {
                    _sound.spatialBlend = value;
                    EditorUtility.SetDirty(_sound);
                }
            }

            [FoldoutGroup("Options", order: 2)]
            [ShowInInspector]
            public bool Loop
            {
                get => _sound.loop;
                set
                {
                    _sound.loop = value;
                    EditorUtility.SetDirty(_sound);
                }
            }
            
            [FoldoutGroup("Options"), HorizontalGroup("Options/More")]
            [ShowInInspector]
            public bool UseRandomVolume
            {
                get => _sound.useRandomVolume;
                set
                {
                    _sound.useRandomVolume = value;
                    EditorUtility.SetDirty(_sound);
                }
            }
            
            [FoldoutGroup("Options"), HorizontalGroup("Options/More")]
            [ShowInInspector]
            public bool UseRandomPicth
            {
                get => _sound.useRandomPitch;
                set
                {
                    _sound.useRandomPitch = value;
                    EditorUtility.SetDirty(_sound);
                }
            }

            //---Details------------------------------------------
            
            [FoldoutGroup("@SoundName/Details", order: 2)]
            [ShowInInspector]
            [Sirenix.OdinInspector.ReadOnly]
            private string duration => $"{Duration}s";

            [FoldoutGroup("@SoundName/Details", order: 2)]
            [ShowInInspector]
            [Sirenix.OdinInspector.ReadOnly]
            private string samples => $"{Samples}samples";

            //---Multiple Sound---------------------------------------------
            
            [BoxGroup("@SoundName", order: 1)]
            [ShowInInspector]
            [ShowIf("@_sound.GetType() == typeof(MultipleSound)")]
            public List<AudioClip> AudioClips
            {
                get
                {
                    if (_sound is MultipleSound)
                        return ((MultipleSound) _sound).clips;
                    else return null;
                }
                set
                {
                    if (_sound is MultipleSound)
                        ((MultipleSound) _sound).clips = value;
                    else
                        ((MultipleSound) _sound).clips = null;
                    
                    EditorUtility.SetDirty(_sound);
                }
            }

            //---Properties
            
            private float Duration => AudioClip != null ? AudioClip.length : 0;
            private float Samples => AudioClip != null ? AudioClip.samples : 0;
            
            //---Privates Functions----------------------------------------------------
            
            void ApplyName()
            {
                var path = AssetDatabase.GetAssetPath(_sound);
                AssetDatabase.RenameAsset(path, SoundName);
            }
            void SelectClip() => Selection.activeObject = AudioClip;
            void SelectObject() => Selection.activeObject = _sound;
        }
    }
    
    [GlobalConfig("Assets/QRCODE/Core/Sound System/Resources")]
    public class SoundOverview : GlobalConfig<SoundOverview>
    {
        [Sirenix.OdinInspector.ReadOnly]
        [ListDrawerSettings(ShowPaging =  true), Searchable]
        public Sound[] allSounds;

#if UNITY_EDITOR
        [Button(ButtonSizes.Medium), PropertyOrder(-1)]
        public void UpdateAllSoundOverview()
        {
            allSounds = Resources.FindObjectsOfTypeAll<Sound>();
        }
#endif
    }
    
    #endregion
}
