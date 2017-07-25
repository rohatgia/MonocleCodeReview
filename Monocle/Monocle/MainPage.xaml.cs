using Monocle.CustomControls;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;
using XLabs.Ioc;
using Tesseract;

namespace Monocle
{
	public partial class MainPage :  ContentPage, INotifyPropertyChanged

    {

        public bool ColumnVisibility = false;
        public int ResumeCount = 1;
        public event PropertyChangedEventHandler PropertyChanged;
        private GridLength _IsColumnVisible;
        private GridLength _MainViewAdjust;
        private readonly ITesseractApi _tesseract;
        private readonly IDevice _device;
        private readonly IMediaPicker _mediaPicker;

        void OnPropertyChanged(String prop)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        //Custom RadioButton Control from https://github.com/kirtisagar/XamarinFormsRadioButtonXAML
        /*
        MyRadiouGroup.CheckedChanged += MyRadiouGroup_CheckedChanged;	
         
         void MyRadiouGroup_CheckedChanged(object sender, int e)
        {
            var radio = sender as CustomRadioButton;

            if (radio == null || radio.Id == -1) return;

            // Display selected value in Label   
            txtSelected.Text = radio.Text;

        }

        private Dictionary<int, string> myList;
        public Dictionary<int, string> MyList
        {
            get { return myList; }
            set
            {
                myList = value;
                OnPropertyChanged("MyList");
            }
        }

        private void LoadData()
        {
            MyList.Add(0, "Software Engineer");
            MyList.Add(1, "HR");
            MyList.Add(2, "QA Tester");
            MyList.Add(3, "Management");
        }
        */
        private bool _CommentsEnabled;
        public bool CommentsEnabled
        {
            get { return _CommentsEnabled; }
            set { _CommentsEnabled = value; OnPropertyChanged("CommentsEnabled"); }
        }
        private bool _SelectionEnabled;
        public bool SelectionEnabled
        {
            get { return _SelectionEnabled; }
            set { _SelectionEnabled = value; OnPropertyChanged("SelectionEnabled"); }
        }
        public GridLength IsColumnVisible
        {
            get { return _IsColumnVisible; }
            set { _IsColumnVisible = value; OnPropertyChanged("IsColumnVisible"); }
        }
        public GridLength MainViewAdjust
        {
            get { return _MainViewAdjust; }
            set { _MainViewAdjust = value; OnPropertyChanged("MainViewAdjust"); }
        }
        private bool _CategorySelected = false;
        public bool CategorySelected
        {
            get { return _CategorySelected; }
            set { _CategorySelected = value; OnPropertyChanged("CategorySelected"); }
        }
        private String _Counter = "1/100";
        public string Counter
        {
            get { return _Counter; }
            set { _Counter = (value + "/100"); OnPropertyChanged("Counter"); }
        }
        private String _Pane1Text = "";
        public String Pane1Text
        {
            get { return _Pane1Text; }
            set { _Pane1Text = value; OnPropertyChanged("Pane1Text"); }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        int _Value = default(int);
        public int Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                OnPropertyChanged("ValueAs1");
                OnPropertyChanged("ValueAs2");
                OnPropertyChanged("ValueAs3");
                OnPropertyChanged("ValueAs4");
                CategorySelected = true;
            }
        }
        public bool ValueAs1
        {
            get { return Value.Equals(1); }
            set { Value = 1; }
        }
        public bool ValueAs2
        {
            get { return Value.Equals(2); }
            set { Value = 2; }
        }
        public bool ValueAs3
        {
            get { return Value.Equals(3); }
            set { Value = 3; }
        }
        public bool ValueAs4
        {
            get { return Value.Equals(4); }
            set { Value = 4; }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////

        public MainPage()
		{
            ColumnVisibility = true;
            CommentsEnabled = false;
            SelectionEnabled = true;
            Pane1Text = "How would you categorize this resume? What job type is this candidate applying for?";
            //LoadData();
            _tesseract = Resolver.Resolve<ITesseractApi>();
            _device = Resolver.Resolve<IDevice>();
            InitializeComponent();
            /*var pdfload = DependencyService.Get<IPDFLoad>();
            if (pdfload != null)
            {

            }
             */
        }


        async void DisplayConfirmDialog()
            {
                var answer = await DisplayAlert("Confrimations", "Do you know what's going to happen?", "Yes", "No");
                Debug.WriteLine("Answer: " + answer);
            }

        async void DisplayEndOfListDialog(object sender, EventArgs e)
        {
            await DisplayAlert("Congratulations", "You've finished all the things!", "Woohoo");
            
        }

        async Task Recognise(MediaFile result)
        {
            if (result.Source == null)
                return;
            try
            {
                activityIndicator.IsRunning = true;
                if (!_tesseract.Initialized)
                {
                    var initialised = await _tesseract.Init("eng");
                    if (!initialised)
                        return;
                }
                if (!await _tesseract.SetImage(result.Source))
                    return;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Cancel");
            }
            finally
            {
                activityIndicator.IsRunning = false;
            }
            TextLabel.Text = _tesseract.Text;
            var words = _tesseract.Results(PageIteratorLevel.Word);
            var symbols = _tesseract.Results(PageIteratorLevel.Symbol);
            var blocks = _tesseract.Results(PageIteratorLevel.Block);
            var paragraphs = _tesseract.Results(PageIteratorLevel.Paragraph);
            var lines = _tesseract.Results(PageIteratorLevel.Textline);
        }

        public void LoadNext_Tapped(object sender, EventArgs e)
        {
            if (ResumeCount == 1)
            {
                DisplayConfirmDialog();
            }
            else
            {
                //LoadNext();
                /*var pdfload = DependencyService.Get<IPDFLoad>();
                if(pdfload != null)
                {

                }*/
            }
        }

        public void Login_Tapped(object sender, EventArgs e)
        {
            /*var logout = DependencyService.Get<ISSO>();
            if(logout != null)
            {

            }*/
        }

        private void Comments_Tapped(object sender, EventArgs e)
        {
            if (ColumnVisibility && !SelectionEnabled)
            {
                ColumnVisibility = false;
                CommentsEnabled = false;
                SelectionEnabled = false;
                Pane1Text = "";
            }
            else
            {
                ColumnVisibility = true;
                CommentsEnabled = true;
                SelectionEnabled = false;
                Pane1Text = "Enter a comment about this resume. Your entry is saved when you classify a resume.";
            }
            ColumnSetter();
        }

        private void Resumes_Tapped(object sender, EventArgs e)
        {
            if (ColumnVisibility && !CommentsEnabled)
            {
                ColumnVisibility = false;
                CommentsEnabled = false;
                SelectionEnabled = false;
                Pane1Text = "";
            }
            else
            {
                ColumnVisibility = true;
                CommentsEnabled = false;
                SelectionEnabled = true;
                Pane1Text = "How would you categorize this resume? What job type is this candidate applying for?";
            }
            ColumnSetter();
        }

        private void Email_Tapped(object sender, EventArgs e)
        {
            /*var email = DependencyService.Get<IEmail>();
            if(email != null)
            {

            }*/
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        private void ColumnSetter()
        {

            if (ColumnVisibility)
            {
                IsColumnVisible = new GridLength(1, GridUnitType.Star);
                MainViewAdjust = new GridLength(4, GridUnitType.Star);
            }
            else
            {
                IsColumnVisible = new GridLength(0);
                MainViewAdjust = new GridLength(1, GridUnitType.Star);
            }

            this.InitializeComponent();

        }

        
        //////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
