using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Syncfusion.Android.DataForm;
using System.Collections.Generic;
using System.ComponentModel;
using Android.Graphics;

namespace DataFormTouchGesture
{
    [Activity(Label = "DataFormTouchGesture", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var linearLayout =new LinearLayout(this);
            var dataform = new SfDataForm(this);
            dataform.DataObject =
               new Employees()
               {
                   ContactID = 1001,
                   EmployeeID = 1709,
                   Title = "Software"
               };

            dataform.IsReadOnly = true;
            dataform.LayoutManager = new DataFormLayoutManagerExt(dataform);
            linearLayout.AddView(dataform);
            // Set our view from the "main" layout resource
            SetContentView(linearLayout);

        }
    }
    public class DataFormLayoutManagerExt : DataFormLayoutManager
    {
        float initialY;
        float currentY;
        bool IsPerformOnTouch;
        public DataFormLayoutManagerExt(SfDataForm dataForm) : base(dataForm)
        {

        }
        protected override DataFormItemView CreateDataFormItemView(int rowIndex, int columnIndex)
        {
            var view = base.CreateDataFormItemView(rowIndex, columnIndex);
            return view;
        }
        protected override View GenerateViewForLabel(DataFormItem dataFormItem)
        {
            var view = base.GenerateViewForLabel(dataFormItem);
            view.Touch += OnLabelTouch;
            return view;
        }

        // Label/Image click event raised 
        // Event raised three times.  
        private void OnLabelTouch(object sender, View.TouchEventArgs e)
        {
            // Based on condition you can achieve your requirement. 

            if (e.Event.Action == MotionEventActions.Down)
            {
                initialY = e.Event.RawY;
                this.IsPerformOnTouch = true;
            }
            else if (e.Event.Action == MotionEventActions.Move)
            {
                currentY = e.Event.RawY;
                // Due to Scrolling operation, we have changed the IsPerform flag is false; 
                if (Math.Abs(currentY - initialY) > 20)
                    this.IsPerformOnTouch = false;
            }
            if (e.Event.Action == MotionEventActions.Up)
            {
                if (IsPerformOnTouch)// Based on the flag, we can do perform here. 
                {
                    // your code here  
                }
            }
        }

        protected override void OnEditorCreated(DataFormItem dataFormItem, View editor)
        {
            base.OnEditorCreated(dataFormItem, editor);
            editor.Enabled = true;
            editor.Focusable = false;
            editor.Touch += OnEditorTouch;
        }

        // Editor event raised three times.  
        private void OnEditorTouch(object sender, View.TouchEventArgs e)
        {
            // Based on condition you can achieve your requirement. 

            if (e.Event.Action == MotionEventActions.Down)
            {
                initialY = e.Event.RawY;
                this.IsPerformOnTouch = true;
            }
            else if (e.Event.Action == MotionEventActions.Move)
            {
                currentY = e.Event.RawY;
                // Due to Scrolling operation, we have changed the IsPerformOnTouch flag is false; 
                if (Math.Abs(currentY - initialY) > 20)
                    this.IsPerformOnTouch = false;
            }
            if (e.Event.Action == MotionEventActions.Up)
            {
                if (IsPerformOnTouch)// Based on the flag, we can do perform here. 
                {
                    // your code here  
                }
            }
        }
    }
}

