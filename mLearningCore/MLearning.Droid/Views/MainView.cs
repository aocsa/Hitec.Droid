using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;
using Gcm.Client;

using MLearning.Core.ViewModels;
using System.ComponentModel;
using Android.Widget;
using System.Collections.Generic;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Graphics;
using System;
using Android.Support.V4.Widget;

using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using System.Collections.ObjectModel;
using TaskView;
using Android.Content.PM;
using DataSource;
using Android.Support.V4.View;
using Android.Content;

namespace MLearning.Droid.Views
{

	[Activity(Label = "View for FirstViewModel", ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainView : MvxActionBarActivity, VerticalScrollViewPager.ScrollViewListenerPager
	{

		ObservableCollection<MLearning.Core.ViewModels.MainViewModel.page_collection_wrapper> s_list;
		private SupportToolbar mToolbar;
		private MyActionBarDrawerToggle mDrawerToggle;
		private DrawerLayout mDrawerLayout;

		private LinearLayout mLeftDrawer;
		private LinearLayout mRightDrawer;

		private List<ChatDataRow> mItemsChat;
		private ListView mListViewChat;
		private LOContainerView _foro;

		TextView title_view;
		TextView title_list;
		TextView info1;
		TextView info2;
		List<ImageLOView> list = new List<ImageLOView> ();

		//private SupportToolbar mToolbar;

		RelativeLayout mainLayout;
		TextView txtUserName;
		TextView txtSchoolName;
		TextView txtCurse;
		TextView txtUserRol;
		TextView txtPorcentaje;
		TextView txtCurseTitle;
		TextView txtTaskTitle;
		TextView txtPendiente;


		ImageView imgUser;
		ImageView imgSchool;
		ImageView imgNotificacion;
		ImageView imgChat;
		ImageView imgCurse;
		ImageView imgTask;

		ProgressBar progressBar;
		LinearLayout linearTxtValorBarra;
		TextView txtValorBarra;
		LinearLayout linearCurse;
		LinearLayout linearTask;
		LinearLayout linearUserData;
		LinearLayout linearBarraCurso;
		LinearLayout linearSchool;
		LinearLayout linearListCurso;
		LinearLayout linearListTask;

		LinearLayout linearinfo1;

		List<ImageView> listRutas = new List<ImageView> ();
		HorizontalScrollView scrollRutas;

		LinearLayout linearListTaskTop;
		LinearLayout linearListRutas;
		LinearLayout linearListTaskBotton;

		LinearLayout linearList;
		LinearLayout linearPendiente;
		LinearLayout linearUser;

		ProgressDialog _dialogDownload;


		RelativeLayout main_ContentView;
		TaskView task;
		int widthInDp;
		int heightInDp;
		int PositionLO =0;

		List<CursoItem> _currentCursos = new List<CursoItem>();
		ListView  listCursos;

		List<TaskItem> _currentTask = new List<TaskItem>();
		ListView  listTasks;

		List<TaskItem> _tasksTop = new List<TaskItem>();
		ListView  _listTasksTop;

		List<TaskItem> _tasksBotton = new List<TaskItem>();
		ListView  _listTasksBotton;

		List<Drawable> headersDR = new List<Drawable> ();

		FrameLayout frameLayout;

		List<FrontContainerViewPager> listFrontPager = new List<FrontContainerViewPager>();
		List<VerticalScrollViewPager> listaScroll = new List<VerticalScrollViewPager>();
		ViewPager viewPager;
		Drawable drBack;

		MainViewModel vm;
		frontView frontView;
		WallView lo;
		FrontContainerViewPager lector;
		public int _currentCurso;
		public int _currentUnidad;
		public bool _lectorOpen;

		protected override void OnCreate(Bundle bundle)
		{
			this.Window.AddFlags(WindowManagerFlags.Fullscreen);
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.MainView);

			_currentCurso = -1;
			_lectorOpen = false;

			headersDR.Add (new BitmapDrawable (getBitmapFromAsset("images/header1.png")));
			headersDR.Add (new BitmapDrawable (getBitmapFromAsset("images/header2.png")));
			headersDR.Add (new BitmapDrawable (getBitmapFromAsset("images/header3.png")));
			headersDR.Add (new BitmapDrawable (getBitmapFromAsset("images/header4.png")));

			drBack = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("images/fondocondiagonalm.png"), 640, 1136, true));
			vm = this.ViewModel as MainViewModel;

			lo = new WallView(this);
			frontView = new frontView (this);
			lector = new FrontContainerViewPager (this);


			frontView._listLinearItem [0].Click += delegate {showRutas ();};
			frontView._listLinearItem [1].Click += delegate {showServicios ();};
			frontView._listLinearItem [2].Click += delegate {showGuiaSilvestre ();};
			frontView._listLinearItem [3].Click += delegate {showCifras ();};


			LinearLayout linearMainLayout = FindViewById<LinearLayout>(Resource.Id.left_drawer);

			var metrics = Resources.DisplayMetrics;
			widthInDp = ((int)metrics.WidthPixels);
			heightInDp = ((int)metrics.HeightPixels);
			Configuration.setWidthPixel (widthInDp);
			Configuration.setHeigthPixel (heightInDp);

			task = new TaskView (this);


			initRutas ();
			initLinearInfo ();
			iniMenu ();

			mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
			SetSupportActionBar(mToolbar);
			mToolbar.SetNavigationIcon (Resource.Drawable.transparent);

			mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			mLeftDrawer = FindViewById<LinearLayout>(Resource.Id.left_drawer);
			mRightDrawer = FindViewById<LinearLayout>(Resource.Id.right_drawer);

			mLeftDrawer.Tag = 0;
			mRightDrawer.Tag = 1;

			frameLayout = FindViewById<FrameLayout> (Resource.Id.content_frame);

			main_ContentView = new RelativeLayout (this);
			main_ContentView.LayoutParameters = new RelativeLayout.LayoutParams (-1, -1);

			LOContainerView LOContainer = new LOContainerView (this);

			lo.OpenChat.Click += imBtn_Chat_Click;
			lo.OpenUnits.Click += imBtn_Units_Click;

			main_ContentView.AddView (lo);
			lo.getWorkSpaceLayout.AddView (frontView);

			frameLayout.AddView (main_ContentView);

			RelativeLayout RL = FindViewById<RelativeLayout> (Resource.Id.main_view_relativeLayoutCL);

			Drawable dr = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/nubeactivity.png"), 768, 1024, true));
			RL.SetBackgroundDrawable (dr);

			//seting up chat view content

			title_view = FindViewById<TextView> (Resource.Id.chat_view_title);


			info1= FindViewById<TextView> (Resource.Id.chat_view_info1);
			info2 = FindViewById<TextView> (Resource.Id.chat_view_info2);
			title_list = FindViewById<TextView> (Resource.Id.chat_list_title);

			mListViewChat = FindViewById<ListView> (Resource.Id.chat_list_view);

			title_view.SetX (Configuration.getWidth(74));
			title_view.SetY (Configuration.getHeight (202));

			title_view.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			title_view.SetTypeface (null, TypefaceStyle.Bold);

			info1.SetX (Configuration.getWidth (76));
			info1.SetY (Configuration.getHeight (250));
			info1.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			info2.SetX (Configuration.getWidth (76));
			info2.SetY (Configuration.getHeight (285));
			info2.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			title_list.SetX (Configuration.getWidth (76));
			title_list.SetY (Configuration.getHeight (391));

			title_list.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			title_list.SetTypeface (null, TypefaceStyle.Bold);

			mListViewChat.SetX (0);
			mListViewChat.SetY (Configuration.getHeight (440));


			linearMainLayout.AddView (mainLayout);

			vm.PropertyChanged += new PropertyChangedEventHandler(logout_propertyChanged);

			RegisterWithGCM();

			mDrawerToggle = new MyActionBarDrawerToggle(
				this,							//Host Activity
				mDrawerLayout,					//DrawerLayout
				Resource.String.openDrawer,		//Opened Message
				Resource.String.closeDrawer		//Closed Message
			);

			mDrawerLayout.SetDrawerListener(mDrawerToggle);
			SupportActionBar.SetHomeButtonEnabled (true);
			SupportActionBar.SetDisplayShowTitleEnabled(false);

			mDrawerToggle.SyncState();

			if (bundle != null)
			{
				if (bundle.GetString("DrawerState") == "Opened")
				{
					SupportActionBar.SetTitle(Resource.String.openDrawer);
				}

				else
				{
					SupportActionBar.SetTitle(Resource.String.closeDrawer);
				}
			}
			else
			{
				SupportActionBar.SetTitle(Resource.String.closeDrawer);
			}


			initListCursos ();
			initListTaskTop ();
			initListTaskBotton ();

			viewPager = new ViewPager (this);

			viewPager.SetOnPageChangeListener (new MyPageChangeListenerPager (this, listFrontPager));


		}


		#region menu

		private void iniMenu(){
			mainLayout = new RelativeLayout (this);

			_foro = new LOContainerView (this);

			_dialogDownload = new ProgressDialog (this);
			_dialogDownload.SetCancelable (false);
			_dialogDownload.SetMessage ("Downloading...");

			txtUserName = new TextView (this);
			txtCurse = new TextView (this);
			txtSchoolName = new TextView (this);
			txtUserRol = new TextView (this);
			txtPorcentaje = new TextView (this);
			txtCurseTitle = new TextView (this);
			txtTaskTitle = new TextView (this);
			txtPendiente = new TextView (this);
			txtValorBarra = new TextView (this);

			imgChat = new ImageView (this);
			imgUser = new ImageView (this);
			imgSchool = new ImageView (this);
			imgNotificacion = new ImageView (this);
			imgCurse = new ImageView (this);
			imgTask = new ImageView (this);

			linearBarraCurso = new LinearLayout (this);
			linearCurse= new LinearLayout (this);
			linearSchool= new LinearLayout (this);
			linearTask= new LinearLayout (this);
			linearUserData= new LinearLayout (this);
			linearUser = new LinearLayout (this);
			linearListCurso = new LinearLayout (this);
			linearListTask = new LinearLayout (this);



			linearListTaskTop = new LinearLayout(this);
			linearListRutas = new LinearLayout (this);
			linearListTaskBotton = new LinearLayout(this);

			linearList = new LinearLayout (this);
			linearPendiente = new LinearLayout (this);

			linearTxtValorBarra = new LinearLayout (this);

			listCursos = new ListView (this);
			listTasks = new ListView (this);

			scrollRutas = new HorizontalScrollView (this);
			scrollRutas.LayoutParameters = new HorizontalScrollView.LayoutParams (-1,-2);





			_listTasksTop = new ListView (this);
			_listTasksBotton = new ListView (this);

			mItemsChat = new List<ChatDataRow> ();

			mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);
			Drawable d = new BitmapDrawable (getBitmapFromAsset ("icons/fondo.png"));
			mainLayout.SetBackgroundDrawable (d);
			d = null;

			linearBarraCurso.LayoutParameters = new LinearLayout.LayoutParams (-1,LinearLayout.LayoutParams.WrapContent);
			linearCurse.LayoutParameters = new LinearLayout.LayoutParams (-1,Configuration.getHeight(50));
			linearTask.LayoutParameters = new LinearLayout.LayoutParams (-1,Configuration.getHeight(50));
			linearSchool.LayoutParameters = new LinearLayout.LayoutParams (-1,LinearLayout.LayoutParams.WrapContent);
			linearUserData.LayoutParameters = new LinearLayout.LayoutParams (-1,LinearLayout.LayoutParams.WrapContent);
			linearUser.LayoutParameters = new LinearLayout.LayoutParams (-1, LinearLayout.LayoutParams.WrapContent);
			linearListCurso.LayoutParameters = new LinearLayout.LayoutParams (-1,Configuration.getHeight(250));

			linearListTask.LayoutParameters = new LinearLayout.LayoutParams (-1,-2);

			linearListTaskTop.LayoutParameters = new LinearLayout.LayoutParams (-1,-2);
			linearListRutas.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			linearListTaskBotton.LayoutParameters = new LinearLayout.LayoutParams (-1,-2);


			linearList.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			linearPendiente.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (30), Configuration.getWidth (30));
			linearTxtValorBarra.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);

			linearBarraCurso.Orientation = Orientation.Vertical;
			linearBarraCurso.SetGravity (GravityFlags.Center);

			linearTxtValorBarra.Orientation = Orientation.Vertical;
			linearTxtValorBarra.SetGravity (GravityFlags.Center);
			txtValorBarra.Gravity = GravityFlags.Center;

			linearCurse.Orientation = Orientation.Horizontal;
			linearCurse.SetGravity (GravityFlags.CenterVertical);

			linearTask.Orientation = Orientation.Horizontal;
			linearTask.SetGravity (GravityFlags.CenterVertical);

			linearSchool.Orientation = Orientation.Horizontal;

			linearUserData.Orientation = Orientation.Vertical;
			linearUserData.SetGravity (GravityFlags.Center);

			linearUser.Orientation = Orientation.Vertical;
			linearUser.SetGravity (GravityFlags.CenterHorizontal);

			linearListCurso.Orientation = Orientation.Vertical;

			linearListTask.Orientation = Orientation.Vertical;

			linearListTaskTop.Orientation = Orientation.Vertical;
			linearListRutas.Orientation = Orientation.Horizontal;
			linearListTaskBotton.Orientation = Orientation.Vertical;


			linearListRutas.SetGravity (GravityFlags.Center);

			linearList.Orientation = Orientation.Vertical;

			linearPendiente.Orientation = Orientation.Horizontal;
			linearPendiente.SetGravity (GravityFlags.Center);
			//linearList.SetGravity (GravityFlags.CenterVertical);

			progressBar = new ProgressBar (this,null,Android.Resource.Attribute.ProgressBarStyleHorizontal);
			progressBar.LayoutParameters = new ViewGroup.LayoutParams (Configuration.getWidth (274), Configuration.getHeight (32));
			progressBar.ProgressDrawable = Resources.GetDrawable (Resource.Drawable.progressBackground);
			progressBar.Progress = 60;
			txtValorBarra.Text = "60%";
			//progressBar.text
			txtValorBarra.SetY(13);

			txtCurse.Text = "Cursos del 2015";
			txtCurse.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");


			//	txtUserName.Text ="David Spencer";
			txtUserName.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			txtUserRol.Text ="PerÃº";
			txtUserRol.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			txtSchoolName.Text ="Colegio Sagrados Corazones";
			txtSchoolName.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			txtPorcentaje.Text = "60%";
			txtPorcentaje.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			txtCurseTitle.Text = "CURSOS";
			txtCurseTitle.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			txtTaskTitle.Text = "TAREAS";	
			txtTaskTitle.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");

			txtPendiente.Text = "1";
			txtPendiente.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
			txtPendiente.SetY (-10);


			txtPendiente.SetTextSize (Android.Util.ComplexUnitType.Px, Configuration.getHeight (30));
			txtUserName.SetTextSize (Android.Util.ComplexUnitType.Px, Configuration.getHeight (35));
			txtUserRol.SetTextSize (Android.Util.ComplexUnitType.Px, Configuration.getHeight (30));


			txtCurseTitle.SetPadding (Configuration.getWidth (48), 0, 0, 0);
			txtTaskTitle.SetPadding (Configuration.getWidth (48), 0, 0, 0);

			txtCurse.SetTextColor (Color.ParseColor ("#ffffff"));
			txtUserName.SetTextColor (Color.ParseColor ("#ffffff"));
			txtUserRol.SetTextColor (Color.ParseColor ("#999999"));
			txtSchoolName.SetTextColor (Color.ParseColor ("#ffffff"));
			txtPorcentaje.SetTextColor (Color.ParseColor ("#ffffff"));
			txtPendiente.SetTextColor (Color.ParseColor ("#ffffff"));
			txtTaskTitle.SetTextColor (Color.ParseColor ("#ffffff"));
			txtCurseTitle.SetTextColor (Color.ParseColor ("#ffffff"));
			txtValorBarra.SetTextColor (Color.ParseColor ("#ffffff"));

			txtUserName.Gravity = GravityFlags.CenterHorizontal;
			txtUserRol.Gravity = GravityFlags.CenterHorizontal;
			txtCurse.Gravity = GravityFlags.CenterHorizontal;


			imgChat.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/chat.png"),Configuration.getWidth (45), Configuration.getWidth (40),true));
			imgUser.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/user.png"),Configuration.getWidth (154), Configuration.getHeight (154),true));
			imgSchool.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/colegio.png"),Configuration.getWidth (29), Configuration.getHeight (29),true));
			imgNotificacion.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/notif.png"),Configuration.getWidth (35), Configuration.getWidth (40),true));
			imgCurse.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/curso.png"),Configuration.getWidth (23), Configuration.getHeight (28),true));
			imgTask.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/vertareas.png"),Configuration.getWidth (23), Configuration.getHeight (28),true));

			Drawable drPendiente = new BitmapDrawable (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/pendiente.png"), Configuration.getWidth(30), Configuration.getWidth(30), true));
			linearPendiente.SetBackgroundDrawable (drPendiente);
			drPendiente = null;

			imgCurse.SetPadding (Configuration.getWidth (68), 0, 0, 0);
			imgTask.SetPadding(Configuration.getWidth(68),0,0,0);


			linearCurse.SetBackgroundColor(Color.ParseColor("#0d1216"));
			linearTask.SetBackgroundColor (Color.ParseColor ("#0d1216"));

			//linearBarraCurso.AddView (txtCurse);
			//linearBarraCurso.AddView (progressBar);

			//linearTxtValorBarra.AddView (txtValorBarra);

			linearCurse.AddView (imgCurse);
			//linearCurse.AddView (txtCurseTitle);
			linearTask.AddView (imgTask);
			linearTask.AddView (txtTaskTitle);
			linearPendiente.AddView (txtPendiente);



			imgSchool.SetPadding (Configuration.getWidth(68),0,0,0);
			txtSchoolName.SetPadding (Configuration.getWidth(40),0,0,0);
			linearSchool.AddView (imgSchool);
			linearSchool.AddView (txtSchoolName);

			linearUser.AddView (txtUserName);
			linearUser.AddView (txtUserRol);

			linearUserData.AddView (imgUser);
			linearUserData.AddView (linearUser);

			linearListCurso.AddView (listCursos);

			linearListTask.AddView (listTasks);


			for (int i = 0; i < listRutas.Count; i++) 
			{
				linearListRutas.AddView (listRutas [i]);
			}
			scrollRutas.AddView (linearListRutas);
			scrollRutas.SetPadding (Configuration.getWidth (45), 0, Configuration.getWidth (45), 0);


			linearListTaskTop.AddView (_listTasksTop);
			//linearListRutas.AddView (scrollRutas);
			linearListTaskBotton.AddView (_listTasksBotton);

			linearList.AddView (linearListTaskTop);
			linearList.AddView (scrollRutas);
			linearList.AddView (linearinfo1);
			//linearList.AddView (linearCurse);
			//linearList.AddView (linearListCurso);
			//linearList.AddView (linearTask);
			//linearList.AddView (linearListTask);
			linearList.AddView (linearListTaskBotton);



			imgChat.SetX (Configuration.getWidth(59)); imgChat.SetY (Configuration.getHeight(145));
			imgChat.Click += delegate {
				mDrawerLayout.CloseDrawer (mLeftDrawer);
				mDrawerLayout.OpenDrawer (mRightDrawer);
			};

			imgNotificacion.SetX (Configuration.getWidth(59));  imgNotificacion.SetY (Configuration.getHeight(233)); 
			imgNotificacion.Click += delegate {
				mDrawerLayout.CloseDrawer (mLeftDrawer);
				main_ContentView.RemoveAllViews ();
				main_ContentView.AddView(new NotificationView(this));
			};


			linearPendiente.SetX (Configuration.getWidth(94));  linearPendiente.SetY (Configuration.getHeight(217)); 

			linearUserData.SetX (0); linearUserData.SetY (Configuration.getHeight(100));
			linearBarraCurso.SetX (0); linearBarraCurso.SetY (Configuration.getHeight(412));
			linearTxtValorBarra.SetX (0); linearTxtValorBarra.SetY (Configuration.getHeight(443));
			linearSchool.SetX (0); linearSchool.SetY (Configuration.getHeight(532));
			linearList.SetX (0); linearList.SetY (Configuration.getHeight(360));

			Bitmap bm;
			txtUserName.Text = vm.UserFirstName + " "+ vm.UserLastName;

			if (vm.UserImage != null) {
				bm = BitmapFactory.DecodeByteArray (vm.UserImage, 0, vm.UserImage.Length);

				Bitmap newbm = Configuration.GetRoundedCornerBitmap (Bitmap.CreateScaledBitmap (bm,Configuration.getWidth (154), Configuration.getHeight (154),true));
				imgUser.SetImageBitmap (newbm);

				newbm = null;
			}
			bm = null;

			vm.PropertyChanged += Vm_PropertyChanged;

			//mainLayout.AddView (imgChat);
			//mainLayout.AddView (imgNotificacion);
			//mainLayout.AddView (linearPendiente);
			mainLayout.AddView (linearUserData);
			mainLayout.AddView (linearBarraCurso);
			mainLayout.AddView (linearTxtValorBarra);
			//mainLayout.AddView (linearSchool);
			mainLayout.AddView (linearList);

			imgChat = null;
			imgNotificacion = null;
			imgCurse = null;
			imgTask = null;
			imgSchool = null;
			imgUser = null;
		}

		#endregion


		private void initListCursos(){		
			//	resetMLOs(0); 
			populateCircleScroll(0);

		}

		private void iniPeoples (){
			populatePeopleScroll(0);
		}

		void Vm_PropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			//var vm = this.ViewModel as MainViewModel;

			string property = e.PropertyName;
			switch(property){
			case "UserFirstName":
				txtUserName.Text = vm.UserFirstName + " " + vm.UserLastName;
				break;
			case "UserLastName":
				txtUserName.Text = vm.UserFirstName + " " + vm.UserLastName;
				break;
			case "CirclesList":
				//populateCircleScroll (0);
				(ViewModel as MainViewModel).CirclesList.CollectionChanged+= CirclesList_CollectionChanged;
				break;
			case "UserImage":
				if (vm.UserImage != null) {

					Bitmap bm = BitmapFactory.DecodeByteArray (vm.UserImage, 0, vm.UserImage.Length);

					Bitmap newbm = Configuration.GetRoundedCornerBitmap (Bitmap.CreateScaledBitmap (bm,Configuration.getWidth (154), Configuration.getHeight (154),true));
					imgUser.SetImageBitmap (newbm);
					bm = null;
					newbm = null;
				}
				break;
			case "LearningOjectsList":
				resetMLOs ();
				(ViewModel as MainViewModel).LearningOjectsList.CollectionChanged += _learningObjectsList_CollectionChanged;
				break;

			case  "LOsInCircle":
				if (vm.LOsInCircle != null) {
				}
				populateLoInCircle (0);
				vm.LOsInCircle.CollectionChanged += Vm_LOsInCircle_CollectionChanged;
				break;


			case "UsersList":
				populatePeopleScroll(0);
				(ViewModel as MainViewModel).UsersList.CollectionChanged+=  UsersList_CollectionChanged;
				break;

			case "PendingQuizzesList":
				resetPendingQuizzes();
				break;
			case "CompletedQuizzesList":
				loadCompleteQuizzes();
				//(ViewModel as MainViewModel).CompletedQuizzesList.CollectionChanged+= CompletedQuizzesList_CollectionChanged;

				break;

			case "PostsList":
				//resetComments();
				break;
			default:

				break;

			}
		}

		void Vm_LOsInCircle_CollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			//populateLoInCircle (e.NewStartingIndex);
			//Console.WriteLine ("E : " + e.NewStartingIndex);
		}

		void populateLoInCircle (int index){
			
			if (vm.LOsInCircle != null) {
				//Console.WriteLine ("ENTREEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
				for (int i = 0; i < vm.LOsInCircle.Count; i++) {
					//Console.WriteLine ("CONTANDOOOOOOOOOOOOOOO");
					if (vm.LOsInCircle [i].stack.IsLoaded) {				
						s_list = vm.LOsInCircle [i].stack.StacksList;



					}
				}
			}

			Console.WriteLine ("____________________>FIN");
			//var s_list = vm.LOsInCircle [index].stack.StacksList;


/*
			for (int i = 0; i < s_list.Count; i++) {
				lo._listUnidades.Add(new UnidadItem{ 
					Title = s_list[index].PagesList[i].page.title, 
					Description = s_list[index].PagesList[i].page.description });
			}
			lo.initUnidades ();
*/

		}

		void CirclesList_CollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			populateCircleScroll (e.NewStartingIndex);
		}


		void populateCircleScroll(int index){
			_currentCursos = new List<CursoItem> ();

			Console.WriteLine ("POPULATE_CIRCLE_SCROLL");

			if (vm.CirclesList != null)
			{
				for (int i = 0; i < vm.CirclesList.Count; i++)
				{
					var newinfo = new CursoItem()
					{
						CursoName = vm.CirclesList[i].name,
						Index =  i					
					};
					_currentCursos.Add(newinfo);
				}

				listCursos.Adapter = new CursoAdapter(this, _currentCursos);
				listCursos.ItemClick+= ListCursos_ItemClick;
			}
		}

		void CompletedQuizzesList_CollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			loadCompleteQuizzes ();
		}

		void populatePeopleScroll(int indice){
			mItemsChat = new List<ChatDataRow> ();

			if (vm.UsersList!= null)
			{
				for (int i = 0; i < vm.UsersList.Count; i++)
				{
					var newinfo = new ChatDataRow()
					{
						name = vm.UsersList[i].user.name + " " + vm.UsersList[i].user.lastname,
						state = vm.UsersList[i].user.is_online,
						index = i,
						imageProfile = vm.UsersList[i].user.image_url

					};
					mItemsChat.Add(newinfo);
				}

				mListViewChat.Adapter = new ChatListViewAdapter(this, mItemsChat);
				mListViewChat.DividerHeight = 0;

			}

		}



		void resetMLOs(){

			main_ContentView.RemoveAllViews ();
			main_ContentView.AddView (lo);
			mDrawerLayout.CloseDrawer (mLeftDrawer);


			if (vm.LearningOjectsList != null) {

				List<ImageLOView> list = new List<ImageLOView> ();

				for (int i = 0; i < vm.LearningOjectsList.Count; i++) {
					ImageLOView imgLO = new ImageLOView (this);

					imgLO.Title = vm.LearningOjectsList [i].lo.title;
					imgLO.Author = vm.LearningOjectsList [i].lo.name + " " + vm.LearningOjectsList [i].lo.lastname;

					imgLO.Url = vm.LearningOjectsList [i].lo.url_cover;
					imgLO.sBackgoundUrl = vm.LearningOjectsList [i].lo.url_background;
					//imgLO.ImagenUsuario = getBitmapFromAsset ("icons/imgautor.png");
					imgLO.Chapter = "Flora y Fauna";
					imgLO.index = i;


					list.Add (imgLO);

				}

				for (int i = 0; i < list.Count; i++) {
					list [i].Click += Lo_ImagenLO_Click;
				}

				lo.ListImages = list;

			}

		}

		private void imLoClick(object sender, EventArgs eventArgs)
		{
			Console.WriteLine (":::::::::::::::::"+s_list [lo.currentLOImageIndex].PagesList [0].page.title);

			//vm.OpenLOCommand.Execute(vm.LearningOjectsList[lo.currentLOImageIndex]);
			//var s_list = vm.LOsInCircle [0].stack.StacksList;
			//int a = 10;

		}

		void Lo_OpenComments_Click (object sender, EventArgs e)
		{
			lo.getWorkSpaceLayout.RemoveAllViews ();
			lo.getWorkSpaceLayout.AddView (_foro);
			LoadForo ();
		}


		void Lo_OpenChat_Click (object sender, EventArgs e)
		{
			mDrawerLayout.CloseDrawer (mLeftDrawer);
			mDrawerLayout.CloseDrawer (mRightDrawer);
			mDrawerLayout.OpenDrawer (mRightDrawer);
		}

		void Lo_OpenTasks_Click (object sender, EventArgs e)
		{
			LoadQuiz ();
			mDrawerLayout.CloseDrawer (mLeftDrawer);	
		}


		void _learningObjectsList_CollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{

			resetMLOs();
		}

		void UsersList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			populatePeopleScroll(e.NewStartingIndex);
		}


		public void initListTaskTop()
		{
			TaskItem home = new TaskItem ();
			TaskItem fotos = new TaskItem ();
			TaskItem rutas = new TaskItem ();

			home.Name = "Home";
			fotos.Name = "Fotos";
			rutas.Name = "Rutas";

			home.Asset = "icons/iconhome.png";
			fotos.Asset = "icons/iconcamara.png";
			rutas.Asset = "icons/iconrutas.png";

			_tasksTop.Add (home);
			_tasksTop.Add (fotos);
			_tasksTop.Add (rutas);

			_listTasksTop.Adapter = new TaskAdapter (this, _tasksTop);
			_listTasksTop.ItemClick += _listTasksItemTop_Click;




		}

		void _listTasksItemTop_Click (object sender, AdapterView.ItemClickEventArgs e)
		{
			
			if (e.Position == 0)//home
			{				
				showHome ();
			}
			else if(e.Position == 1)//fotos
			{
				showHome ();
				mDrawerLayout.CloseDrawer (mLeftDrawer);
			}
			else if(e.Position == 2)//rutas
			{
				showRutas ();
			}

		}

		void _listTaskItemBotton_Click (object sender, AdapterView.ItemClickEventArgs e)
		{
			if (e.Position == 0)//servicios
			{
				showServicios ();
			}
			else if(e.Position == 1)//silvestre
			{
				showGuiaSilvestre ();
			}
			else if(e.Position == 2)//otros
			{
				showCifras ();
			}
			else if(e.Position == 3)//otros
			{
				LogOut();
			}
		}

		public void initRutas()
		{

			ImageView ruta1 = new ImageView (this);
			ImageView ruta2 = new ImageView (this);
			ImageView ruta3 = new ImageView (this);

			int space = Configuration.getWidth (15);
			int w = Configuration.getWidth (123);
			int h = Configuration.getWidth (136);

			ruta1.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/rutamachupicchu.png"), w, h, true));
			ruta2.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/rutasallkantay.png"), w, h, true));
			ruta3.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/rutasmenores.png"), w, h, true));

			ruta1.SetPadding (space, 0, space, 0);
			ruta2.SetPadding (space, 0, space, 0);
			ruta3.SetPadding (space, 0, space, 0);

			listRutas.Add (ruta1);
			listRutas.Add (ruta2);
			listRutas.Add (ruta3);

			/*
			ruta1.Click += delegate {showRutas();};
			ruta2.Click += delegate {showRutas();};
			ruta3.Click += delegate {showRutas();};
			*/

		}

		public void initListTaskBotton()
		{
			TaskItem guiaServicios = new TaskItem ();
			TaskItem guiaSilvestre = new TaskItem ();
			TaskItem caminoCifras = new TaskItem ();
			TaskItem salir = new TaskItem ();

			guiaServicios.Name = "Guia de servicios";
			guiaSilvestre.Name = "Guia de vida silvestre";
			caminoCifras.Name = "Camino inca en cifras";
			salir.Name = "Salir";

			guiaServicios.Asset = "icons/iconservicios.png";
			guiaSilvestre.Asset = "icons/iconvidasilvestre.png";
			caminoCifras.Asset = "icons/iconcifras.png";
			salir.Asset = "icons/iconsalir.png";

			_tasksBotton.Add (guiaServicios);
			_tasksBotton.Add (guiaSilvestre);
			_tasksBotton.Add (caminoCifras);
			_tasksBotton.Add (salir);

			_listTasksBotton.Adapter = new TaskAdapter (this, _tasksBotton);
			_listTasksBotton.ItemClick += _listTaskItemBotton_Click;


		}



		public void LogOut()
		{
			vm.LogoutCommand.Execute(null);
		}

		public void showHome()
		{
			_currentCurso = -1;
			try{
			lo.getWorkSpaceLayout.SetBackgroundColor (Color.Transparent);
			lo.getWorkSpaceLayout.RemoveAllViews ();
			lo.getWorkSpaceLayout.AddView (frontView);
			mDrawerLayout.CloseDrawer (mLeftDrawer);
			}catch{
			}
		}

		public void showCurso(int index)
		{
			try{
				_currentCurso = index;
				
				lo.getWorkSpaceLayout.SetBackgroundColor (Color.Transparent);
				lo.getWorkSpaceLayout.RemoveAllViews ();

				if (vm.CirclesList.Count >= index || vm.CirclesList[index]==null) {
					vm.SelectCircleCommand.Execute (vm.CirclesList [index]);
				}else{
					return;
				}
						
				vm.OpenLOCommand.Execute(vm.LearningOjectsList[index]);
				mDrawerLayout.CloseDrawer (mLeftDrawer);
				resetMLOs ();
				lo._spaceUnidades.RemoveAllViews();
			}catch{
				
			}
		}

		public void showRutas()			{showCurso (0);lo.header.SetBackgroundDrawable (headersDR[0]);lo._contentScrollView_S2.SetBackgroundColor (Color.ParseColor ("#FFBF00"));}
		public void showServicios()		{showCurso (1);lo.header.SetBackgroundDrawable (headersDR[1]);lo._contentScrollView_S2.SetBackgroundColor (Color.ParseColor ("#00FFFF"));}
		public void showGuiaSilvestre()	{showCurso (2);lo.header.SetBackgroundDrawable (headersDR[2]);lo._contentScrollView_S2.SetBackgroundColor (Color.ParseColor ("#74DF00"));}
		public void showCifras()		{showCurso (3);lo.header.SetBackgroundDrawable (headersDR[3]);lo._contentScrollView_S2.SetBackgroundColor (Color.ParseColor ("#8258FA"));}




		public void initLinearInfo()
		{
			linearinfo1 = new LinearLayout (this);
			linearinfo1.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			linearinfo1.Orientation = Orientation.Horizontal;
			linearinfo1.SetGravity (GravityFlags.CenterHorizontal);

			List<String> items = new List<string> ();
			List<ImageView> icons = new List<ImageView> ();
			List<String> iconsPath = new List<string> ();

			items.Add ("I was here");
			items.Add ("Your faves");
			items.Add ("Guides");

			iconsPath.Add ("icons/iconiwashere.png");
			iconsPath.Add ("icons/iconlike.png");
			iconsPath.Add ("icons/iconmap.png");

			int w = Configuration.getWidth(30);
			int h = Configuration.getWidth(30);
			int spacing = Configuration.getWidth(15);
			int titleSpace = Configuration.getWidth (10);


			for (int i = 0; i < items.Count; i++) 
			{

				icons.Add (new ImageView (this));
				icons [i].SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset (iconsPath[i]), w, h, true));

				TextView title = new TextView (this);
				title.Text = items [i];
				title.SetTextColor (Color.ParseColor ("#ffffff"));
				title.Typeface =  Typeface.CreateFromAsset(this.Assets, "fonts/HelveticaNeue.ttf");
				title.SetPadding (titleSpace, 0, 0, 0);
				title.SetTextSize (Android.Util.ComplexUnitType.Px, Configuration.getHeight (25));


				LinearLayout itemLayoutmp = new LinearLayout (this);
				itemLayoutmp.LayoutParameters = new LinearLayout.LayoutParams (-2, -2);
				itemLayoutmp.Orientation = Orientation.Horizontal;
				itemLayoutmp.SetGravity (GravityFlags.CenterVertical);
				itemLayoutmp.SetPadding(spacing, 0, spacing, 0);

				itemLayoutmp.AddView (icons [i]);
				itemLayoutmp.AddView (title);



				linearinfo1.AddView (itemLayoutmp);
				//linearinfo1.AddView (icons[i]);
				//linearinfo1.AddView (title);



			}

			int padding = Configuration.getWidth (45);

			linearinfo1.SetPadding (padding, 0, padding, 0);

		}
		public void initListTasks (){
			TaskItem item1 = new TaskItem();
			TaskItem item2 = new TaskItem();
			TaskItem item3 = new TaskItem ();

			item1.Name = "Ver Tareas";
			item1.Asset = "icons/tareas.png";
			item2.Name = "Hacer una pregunta";
			item2.Asset = "icons/pregunta.png";

			item3.Name = "Salir";
			item3.Asset = "icons/salir.png";

			_currentTask.Add (item1);
			_currentTask.Add (item2);
			_currentTask.Add (item3);

			listTasks.Adapter = new TaskAdapter (this, _currentTask);
			listTasks.ItemClick+= ListTasks_ItemClick;

		}


		void ListTasks_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{


			Console.WriteLine ("POSITION!!! = "+ e.Position);



			if (e.Position == 0) {
				LoadQuiz ();
			}
			if (e.Position == 2) {
				//salir command
				vm.LogoutCommand.Execute(null);
			}


			mDrawerLayout.CloseDrawer (mLeftDrawer);

		}

		void LoadForo(){
			lo.getWorkSpaceLayout.RemoveAllViews();
			lo.getWorkSpaceLayout.AddView (_foro);

			_foro.Author=vm.LearningOjectsList[lo.currentLOImageIndex].lo.name +" "+vm.LearningOjectsList[lo.currentLOImageIndex].lo.lastname;
			_foro.NameLO = vm.LearningOjectsList [lo.currentLOImageIndex].lo.title;
			_foro.Chapter = vm.LearningOjectsList [lo.currentLOImageIndex].lo.description;
			_foro.CoverUrl = vm.LearningOjectsList [lo.currentLOImageIndex].lo.url_background;


			int i = 0;
			List<CommentDataRow> _currentComments = new List<CommentDataRow> ();
			foreach (var quiz in vm.PostsList) 
			{
				var newinfo = new CommentDataRow ();
				newinfo.comment = vm.PostsList [i].post.text;
				newinfo.date = vm.PostsList [i].post.created_at.ToString ();
				newinfo.im_profile = vm.PostsList [i].post.image_url;
				newinfo.name = vm.PostsList [i].post.name;

				_currentComments.Add(newinfo);
				i++;
			}

			_foro.ListaComentarios = _currentComments;


		}

		void LoadQuiz(){
			//main_ContentView.RemoveAllViews ();
			//main_ContentView.AddView (task);

			//var vm = ViewModel as MainViewModel;
			lo.getWorkSpaceLayout.RemoveAllViews();
			lo.getWorkSpaceLayout.AddView (task);


			task.Author=vm.LearningOjectsList[lo.currentLOImageIndex].lo.name +" "+vm.LearningOjectsList[lo.currentLOImageIndex].lo.lastname;
			task.NameLO = vm.LearningOjectsList [lo.currentLOImageIndex].lo.title;
			task.Chapter = vm.LearningOjectsList [lo.currentLOImageIndex].lo.description;
			task.CoverUrl = vm.LearningOjectsList [lo.currentLOImageIndex].lo.url_background;


			loadCompleteQuizzes ();
			resetPendingQuizzes ();
		}

		void loadCompleteQuizzes(){
			String icon = "icons/tareacompleta.png";

			List<TaskViewItem> _currentTask = new List<TaskViewItem> ();
			//var vm = ViewModel as MainViewModel;


			int i = 0;
			foreach (var quiz in vm.CompletedQuizzesList) 
			{
				var newinfo = new TaskViewItem()
				{
					Icon = icon,
					Tarea= vm.CompletedQuizzesList[i].content,
					Index = i

				};
				_currentTask.Add(newinfo);
				i++;
			}
			task.ListaTareasCompletas = _currentTask;
		}

		void resetPendingQuizzes(){
			String icon = "icons/tareaincompleta.png";
			List<TaskViewItem> _currentTask = new List<TaskViewItem> ();
			//var vm = ViewModel as MainViewModel;

			int i = 0;
			foreach (var quiz in vm.PendingQuizzesList) 
			{
				var newinfo = new TaskViewItem()
				{
					//CursoName = vm.LearningOjectsList[i].lo.title,
					//Index =  i					
					Icon = icon,
					Tarea= vm.PendingQuizzesList[i].content,
					Index = i

				};
				_currentTask.Add(newinfo);
				i++;
			}
			task.ListaTareasIncompletas = _currentTask;
		}

		void ListCursos_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{


			var circle = _currentCursos [e.Position];

			//var vm = this.ViewModel as MainViewModel;
			//vm.SelectCircleCommand.Execute(vm.CirclesList[circle.Index]);

			vm.SelectCircleCommand.Execute(vm.CirclesList[circle.Index]);
			PositionLO = e.Position;


		}

		void imBtn_Units_Click(object sender, EventArgs e)
		{
			lo.getWorkSpaceLayout.RemoveAllViews ();
		}

		void imBtn_Comments_Click(object sender, EventArgs e)
		{
			//vm.LogoutCommand.Execute (null);

		}

		void imBtn_Chat_Click(object sender, EventArgs e)
		{


			mDrawerLayout.CloseDrawer (mLeftDrawer);
			mDrawerLayout.CloseDrawer (mRightDrawer);
			mDrawerLayout.OpenDrawer (mRightDrawer);
		}


		void Lo_ImagenLO_Click (object sender, EventArgs e)
		{
			
			var imView = sender as ImageLOView;
			_currentUnidad = imView.index;

			Console.WriteLine ("::::::::::::::::::::::::::: " + imView.index + " :: " +vm.LOsInCircle.Count);

			if (vm.LOsInCircle.Count <= imView.index 
				|| vm.LOsInCircle [imView.index].stack == null
				|| vm.LOsInCircle [imView.index].stack.StacksList == null) {
				
				var myHandler = new Handler ();
				myHandler.Post(()=>{
					Toast.MakeText (this, "Downloading...", ToastLength.Short).Show();
				});
				return;
			} else {
				Console.WriteLine ("IN");
				try{
					s_list = vm.LOsInCircle [imView.index].stack.StacksList;
				}catch{
				}
				Console.WriteLine ("OUT");

				lo._listUnidades.Clear ();

				for (int j = 0; j < s_list.Count; j++) {
					lo._listUnidades.Add (new UnidadItem { 
						Title = s_list [j].PagesList [0].page.title, 
						Description = s_list [j].PagesList [0].page.description,
						ImageUrl = s_list[j].PagesList[0].page.url_img
							
					});
					Console.WriteLine (lo._listUnidades[j].Title);
					Console.WriteLine (lo._listUnidades[j].Description);
					Console.WriteLine (lo._listUnidades[j].ImageUrl);
				}

				lo.initUnidades (_currentCurso,_currentUnidad);
				//lo._txtCursoN.Text = vm.CirclesList[_currentCurso].name;
				//lo._txtUnidadN.Text = vm.LOsInCircle[imView.index].lo.title;

				if (_currentCurso == 0) {

					for (int i = 0; i < lo._listLinearUnidades.Count; i++) {

						Console.WriteLine ("ADD LECTOR : " + i);
						lo._listLinearUnidades [i].Click += delegate {
							LoadPagesDataSource (i);
						};
					}
				}

			}

		}

		
		/*
		void resetComments(){
			
		}

       
		*/

		//toolbar codes requisites

		public override bool OnOptionsItemSelected (IMenuItem item)
		{		
			switch (item.ItemId)
			{

			case Android.Resource.Id.Home:
				//The hamburger icon was clicked which means the drawer toggle will handle the event
				//all we need to do is ensure the right drawer is closed so the don't overlap
				mDrawerLayout.CloseDrawer (mRightDrawer);
				mDrawerToggle.OnOptionsItemSelected(item);
				return true;


			default:
				return base.OnOptionsItemSelected (item);
			}
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.action_menu, menu);



			return base.OnCreateOptionsMenu (menu);
		}

		protected override void OnSaveInstanceState (Bundle outState)
		{
			if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
			{
				outState.PutString("DrawerState", "Opened");
			}

			else
			{
				outState.PutString("DrawerState", "Closed");
			}

			base.OnSaveInstanceState (outState);
		}

		protected override void OnPostCreate (Bundle savedInstanceState)
		{
			base.OnPostCreate (savedInstanceState);
			mDrawerToggle.SyncState();
		}

		public override void OnConfigurationChanged (Android.Content.Res.Configuration newConfig)
		{
			base.OnConfigurationChanged (newConfig);
			mDrawerToggle.OnConfigurationChanged(newConfig);
		}
		private void RegisterWithGCM()
		{


			if (!GcmClient.IsRegistered(this))
			{
				GcmClient.CheckDevice(this);
				GcmClient.CheckManifest(this);

				// Register for push notifications
				System.Diagnostics.Debug.WriteLine("Registering...");


				GcmClient.Register(this, Core.Configuration.Constants.SenderID);

			}





		}


		void LoadPagesDataSource (int pageIndex)
		{

			_lectorOpen = true;
			bool is_main = true;

			for (int i = 0; i < vm.LOsInCircle.Count; i++)
			{
				var s_listp = vm.LOsInCircle[i].stack.StacksList;
				int indice = 0;

				for (int j = 0; j < s_listp.Count; j++) 
				{						

						for (int k = 0; k < s_listp [j].PagesList.Count; k++) 
						{
							VerticalScrollViewPager scrollPager = new VerticalScrollViewPager (this);
							scrollPager.setOnScrollViewListener (this); 
							LinearLayout linearScroll = new LinearLayout (this);
							linearScroll.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
							linearScroll.Orientation = Orientation.Vertical;

							var content = s_listp [j].PagesList [k].content;
							FrontContainerViewPager front = new FrontContainerViewPager (this);
							front.Tag = "pager";


							front.ImageChapter = s_listp [j].PagesList[k].page.url_img;


							front.Title = s_listp [j].PagesList [k].page.title;
							front.Description =  s_listp [j].PagesList [k].page.description;
							var slides = s_listp [j].PagesList [k].content.lopage.loslide;
							front.setBack (drBack);


							linearScroll.AddView (front);
							listFrontPager.Add (front);

							var currentpage = s_listp [j].PagesList [k];
			

							for (int m = 1; m < slides.Count; m++) 
							{
								LOSlideSource slidesource = new LOSlideSource(this);

								var _id_ = vm.LOsInCircle [i].lo.color_id;
								is_main = !is_main;
								

								slidesource.ColorS = Configuration.ListaColores [indice % 6];

								slidesource.Type = slides[m].lotype;
								if (slides[m].lotitle != null) slidesource.Title = slides[m].lotitle;
								if (slides[m].loparagraph != null) slidesource.Paragraph = slides[m].loparagraph;
								if (slides[m].loimage != null) slidesource.ImageUrl = slides[m].loimage;
								if (slides[m].lotext != null) slidesource.Paragraph = slides[m].lotext;
								if (slides[m].loauthor != null) slidesource.Author = slides[m].loauthor;
								if (slides[m].lovideo != null) slidesource.VideoUrl = slides[m].lovideo;

								var c_slide = slides[m];


								if (c_slide.loitemize != null)
								{
									slidesource.Itemize = new ObservableCollection<LOItemSource>();
									var items = c_slide.loitemize.loitem;

									for (int n = 0; n < items.Count; n++)
									{ 
										LOItemSource item = new LOItemSource();
										if (items[n].loimage != null) item.ImageUrl = items[n].loimage;
										if (items[n].lotext != null) item.Text = items[n].lotext;


										var c_item_ize = items[n];

										slidesource.Itemize.Add(item);
									}
								}

								linearScroll.AddView (slidesource.getViewSlide());

							} 
						scrollPager.VerticalScrollBarEnabled = false;
						scrollPager.AddView (linearScroll);
						listaScroll.Add (scrollPager);
						indice++;
					}




				}



			}
			lo.getWorkSpaceLayout.RemoveAllViews ();
			lo.getWorkSpaceLayout.SetBackgroundColor (Color.White);
			lo.getWorkSpaceLayout.AddView (viewPager);
			//_progresD.Hide ();

			List<int> numUn = new List<int>();
			numUn.Add (0);
			numUn.Add (4);
			numUn.Add (9);

			LOViewAdapter adapter = new LOViewAdapter (this, listaScroll);
			viewPager.Adapter = adapter;
			viewPager.SetCurrentItem (numUn[_currentUnidad], false);
			//viewPager.CurrentItem = IndiceSection;

		}


		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s = this.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);
		
			return bitmap;
		}



		void logout_propertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "IsLoggingOut" && (sender as MainViewModel).IsLoggingOut)
			{
				GcmClient.UnRegister(this);
			}
		}


		protected override void OnPause ()
		{
			base.OnPause ();
			_dialogDownload.Hide ();
		}


		public void OnScrollChangedPager(VerticalScrollViewPager scrollView, int l, int t, int oldl, int oldt) {
			var view=(LinearLayout)scrollView.GetChildAt (0);

			if (view.GetChildAt (0).Tag.Equals("indice")) {
				var pagerrr =  (FrontContainerView)view.GetChildAt (0);
				pagerrr.Imagen.SetY (scrollView.ScrollY / 2);	
			}if (view.GetChildAt (0).Tag.Equals("pager")) {
				var pagerrr =  (FrontContainerViewPager)view.GetChildAt (0);
				pagerrr.Imagen.SetY (scrollView.ScrollY / 2);	
			}


			//Console.WriteLine("SCROLLEOOO LOS PAGERRRRRRRRRRRRR "+ scrollView.ScrollY);

		}


		public class MyPageChangeListener : Java.Lang.Object, ViewPager.IOnPageChangeListener
		{
			Context _context;
			List<FrontContainerView> listFront;
			//ScrollViewHorizontal scroll;
			public MyPageChangeListener (Context context, List<FrontContainerView> listFront)
			{
				_context = context;	
				this.listFront = listFront;

			}

			#region IOnPageChangeListener implementation
			public void OnPageScrollStateChanged (int p0)
			{
				Console.WriteLine (p0);
			}

			public void OnPageScrolled (int p0, float p1, int p2)
			{

				Console.WriteLine ("p0 = " + p0 + " p1 = " + p1 + " p2 = " + p2);
				listFront [p0].Imagen.SetX (p2 / 2);		
				//if(p0+1<listFront.Count){
				//	listFront [p0 + 1].Imagen.SetX (p2/2);
				//}

			}

			public void OnPageSelected (int position)
			{
				//	Toast.MakeText (_context, "Changed to page " + position, ToastLength.Short).Show ();
			}
			#endregion
		}

		public override void OnBackPressed ()
		{

			if (_lectorOpen) {
				_lectorOpen = false;
				showRutas ();
			} else if (_currentCurso == 0) {
				showHome ();
			} else if (_currentCurso == 1) {
				showHome ();
			} else if (_currentCurso == 2) {
				showHome ();
			} else if (_currentCurso == 3) {
				showHome ();
			}


		}



		public class MyPageChangeListenerPager : Java.Lang.Object, ViewPager.IOnPageChangeListener
		{
			Context _context;
			List<FrontContainerViewPager> listFront;
			//ScrollViewHorizontal scroll;
			public MyPageChangeListenerPager (Context context, List<FrontContainerViewPager> listFront)
			{
				_context = context;	
				this.listFront = listFront;

			}

			#region IOnPageChangeListener implementation
			public void OnPageScrollStateChanged (int p0)
			{
				Console.WriteLine (p0);
			}

			public void OnPageScrolled (int p0, float p1, int p2)
			{

				Console.WriteLine ("p0 = " + p0 + " p1 = " + p1 + " p2 = " + p2);
				listFront [p0].Imagen.SetX (p2 / 2);		
				//if(p0+1<listFront.Count){
				//	listFront [p0 + 1].Imagen.SetX (p2/2);
				//}

			}

			public void OnPageSelected (int position)
			{
				//	Toast.MakeText (_context, "Changed to page " + position, ToastLength.Short).Show ();
			}
			#endregion
		}


	
	}
}