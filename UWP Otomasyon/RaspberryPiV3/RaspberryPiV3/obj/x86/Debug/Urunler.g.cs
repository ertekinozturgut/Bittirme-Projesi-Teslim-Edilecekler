﻿#pragma checksum "C:\Users\Ertekin\source\repos\RaspberryPiV3\RaspberryPiV3\Urunler.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F45A25EDC73B8EAF983A6747C393927D"
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
    partial class Urunler : 
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
        private class Urunler_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IUrunler_Bindings
        {
            private global::RaspberryPiV3.Urunler dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.ComboBox obj9;
            private global::Windows.UI.Xaml.Controls.ComboBox obj10;

            public Urunler_obj1_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 9: // Urunler.xaml line 61
                        this.obj9 = (global::Windows.UI.Xaml.Controls.ComboBox)target;
                        break;
                    case 10: // Urunler.xaml line 62
                        this.obj10 = (global::Windows.UI.Xaml.Controls.ComboBox)target;
                        break;
                    default:
                        break;
                }
            }

            // IUrunler_Bindings

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
                    this.dataRoot = (global::RaspberryPiV3.Urunler)newDataRoot;
                    return true;
                }
                return false;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::RaspberryPiV3.Urunler obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_Depolar(obj.Depolar, phase);
                        this.Update_Bolgeler(obj.Bolgeler, phase);
                    }
                }
            }
            private void Update_Depolar(global::System.Collections.Generic.List<global::System.String> obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Urunler.xaml line 61
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj9, obj, null);
                }
            }
            private void Update_Bolgeler(global::System.Collections.Generic.List<global::System.String> obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Urunler.xaml line 62
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
            case 1: // Urunler.xaml line 1
                {
                    this.sayfaurunler = (global::Windows.UI.Xaml.Controls.Page)(target);
                }
                break;
            case 2: // Urunler.xaml line 16
                {
                    this.urunlergridlayout = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3: // Urunler.xaml line 94
                {
                    this.txtarabad = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 4: // Urunler.xaml line 95
                {
                    this.txtarabadurum = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5: // Urunler.xaml line 96
                {
                    this.txtarabasicaklik = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6: // Urunler.xaml line 97
                {
                    this.txtarabanem = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 7: // Urunler.xaml line 59
                {
                    this.dtpsonktarih = (global::Telerik.UI.Xaml.Controls.Input.RadDatePicker)(target);
                }
                break;
            case 8: // Urunler.xaml line 60
                {
                    this.txturunad = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 9: // Urunler.xaml line 61
                {
                    this.cmbdepoid = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 10: // Urunler.xaml line 62
                {
                    this.cmbbolgeid = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 11: // Urunler.xaml line 63
                {
                    this.txtidealsicaklik = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 12: // Urunler.xaml line 64
                {
                    this.txtstokadedi = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 13: // Urunler.xaml line 65
                {
                    this.btnkayit = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnkayit).Click += this.btnkayit_Click;
                }
                break;
            case 14: // Urunler.xaml line 70
                {
                    this.btnsifirla = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnsifirla).Click += this.btnsifirla_Click;
                }
                break;
            case 15: // Urunler.xaml line 75
                {
                    this.datagridurunler = (global::Telerik.UI.Xaml.Controls.Grid.RadDataGrid)(target);
                }
                break;
            case 16: // Urunler.xaml line 89
                {
                    this.txtidealnem = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 17: // Urunler.xaml line 90
                {
                    this.txtmaxsicaklik = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 18: // Urunler.xaml line 91
                {
                    this.txtmaxnem = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 19: // Urunler.xaml line 28
                {
                    this.btnanasayfa = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnanasayfa).Click += this.btnanasayfa_Click;
                }
                break;
            case 20: // Urunler.xaml line 29
                {
                    this.btndepo = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btndepo).Click += this.btndepo_Click;
                }
                break;
            case 21: // Urunler.xaml line 30
                {
                    this.btnbolgeler = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnbolgeler).Click += this.btnbolgeler_Click;
                }
                break;
            case 22: // Urunler.xaml line 31
                {
                    this.btnurunler = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnurunler).Click += this.btnurunler_Click;
                }
                break;
            case 23: // Urunler.xaml line 32
                {
                    this.btnarabalar = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnarabalar).Click += this.btnarabalar_Click;
                }
                break;
            case 24: // Urunler.xaml line 33
                {
                    this.btnkullanicilar = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnkullanicilar).Click += this.btnkullanicilar_Click;
                }
                break;
            case 25: // Urunler.xaml line 34
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
            case 1: // Urunler.xaml line 1
                {                    
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    Urunler_obj1_Bindings bindings = new Urunler_obj1_Bindings();
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

