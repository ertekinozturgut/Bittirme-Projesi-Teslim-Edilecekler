﻿#pragma checksum "C:\Users\Ertekin\source\repos\RaspberryPiV3\RaspberryPiV3\Bolgeler.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "286E537842EB98BFE265BB0514633CAD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RaspberryPiV3
{
    partial class Bolgeler : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(global::Windows.UI.Xaml.Controls.ItemsControl obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.ItemsSource = value;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class Bolgeler_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IBolgeler_Bindings
        {
            private global::RaspberryPiV3.Bolgeler dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.ComboBox obj10;

            public Bolgeler_obj1_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 10: // Bolgeler.xaml line 68
                        this.obj10 = (global::Windows.UI.Xaml.Controls.ComboBox)target;
                        break;
                    default:
                        break;
                }
            }

            // IBolgeler_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::RaspberryPiV3.Bolgeler)newDataRoot;
                    return true;
                }
                return false;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::RaspberryPiV3.Bolgeler obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_depolar(obj.depolar, phase);
                    }
                }
            }
            private void Update_depolar(global::System.Collections.Generic.List<global::System.String> obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Bolgeler.xaml line 68
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj10, obj, null);
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Bolgeler.xaml line 72
                {
                    this.txtarabad = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 3: // Bolgeler.xaml line 73
                {
                    this.txtarabadurum = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 4: // Bolgeler.xaml line 74
                {
                    this.txtarabasicaklik = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5: // Bolgeler.xaml line 75
                {
                    this.txtarabanem = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6: // Bolgeler.xaml line 43
                {
                    this.datagridbolgeler = (global::Telerik.UI.Xaml.Controls.Grid.RadDataGrid)(target);
                }
                break;
            case 7: // Bolgeler.xaml line 52
                {
                    this.txtkadi = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 8: // Bolgeler.xaml line 57
                {
                    global::Windows.UI.Xaml.Controls.Button element8 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element8).Click += this.btnkayit_Click;
                }
                break;
            case 9: // Bolgeler.xaml line 62
                {
                    this.btnsifirla = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnsifirla).Click += this.btnsifirla_Click;
                }
                break;
            case 10: // Bolgeler.xaml line 68
                {
                    this.cmbbolgeid = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 11: // Bolgeler.xaml line 23
                {
                    this.btnanasayfa = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnanasayfa).Click += this.btnanasayfa_Click;
                }
                break;
            case 12: // Bolgeler.xaml line 24
                {
                    this.btndepo = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btndepo).Click += this.btndepo_Click;
                }
                break;
            case 13: // Bolgeler.xaml line 25
                {
                    this.btnbolgeler = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnbolgeler).Click += this.btnbolgeler_Click;
                }
                break;
            case 14: // Bolgeler.xaml line 26
                {
                    this.btnurunler = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnurunler).Click += this.btnurunler_Click;
                }
                break;
            case 15: // Bolgeler.xaml line 27
                {
                    this.btnarabalar = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnarabalar).Click += this.btnarabalar_Click;
                }
                break;
            case 16: // Bolgeler.xaml line 28
                {
                    this.btnkullanicilar = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnkullanicilar).Click += this.btnkullanicilar_Click;
                }
                break;
            case 17: // Bolgeler.xaml line 29
                {
                    this.btniletisim = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btniletisim).Click += this.btniletisim_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1: // Bolgeler.xaml line 1
                {                    
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    Bolgeler_obj1_Bindings bindings = new Bolgeler_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            }
            return returnValue;
        }
    }
}

