using Android.App;
using Android.OS;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Droid.Views;
using Core.Repositories;
using Gcm.Client;
using Microsoft.WindowsAzure.MobileServices;
using MLearning.Core.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Android.Widget;
using Android.Graphics;
using Android.Views;
using System.Collections.Generic;
using Android.Support.V4.View;
using DataSource;
using System.Collections.ObjectModel;
using Android.Content.PM;
using Android.Content;
using System.Threading;
using Android.Graphics.Drawables;

namespace MLearning.Droid.Views
{
	[Activity(Label = "View for LOViewModel", ScreenOrientation = ScreenOrientation.Portrait)]
	public class LOView : MvxActivity, VerticalScrollViewPager.ScrollViewListenerPager
	{

		LOViewModel vm; 
		Bitmap bm_user;
		Bitmap bmLike;
		Drawable drBack;

		ProgressDialog _progresD;
		//	LinearLayout layoutPanelScroll;
		RelativeLayout mainLayout;
		RelativeLayout mainLayoutIndice;
		RelativeLayout mainLayoutPages;


		int widthInDp;
		int heightInDp;
		List<FrontContainerView> listFront = new List<FrontContainerView> ();


		List<FrontContainerViewPager> listFrontPager = new List<FrontContainerViewPager>();
		//	VerticalScrollView scrollVertical;
		bool ISLOADED= false;
		int IndiceSection=0;

		List<VerticalScrollViewPager> listaScroll = new List<VerticalScrollViewPager>();
		List<VerticalScrollViewPager> listaScrollIni = new List<VerticalScrollViewPager>();

		ViewPager viewPager;
		ViewPager viewPagerIni;

		async protected  override  void OnCreate(Bundle bundle)
		{
			
			this.Window.AddFlags(WindowManagerFlags.Fullscreen);
			base.OnCreate(bundle);
			var metrics = Resources.DisplayMetrics;
			widthInDp = ((int)metrics.WidthPixels);
			heightInDp = ((int)metrics.HeightPixels);
			Configuration.setWidthPixel (widthInDp);
			Configuration.setHeigthPixel (heightInDp);
			vm = this.ViewModel as LOViewModel;

			int tam = Configuration.getWidth (80);
			bm_user = Configuration.getRoundedShape(Bitmap.CreateScaledBitmap(getBitmapFromAsset ("icons/nouser.png"), tam,tam, true)
				,tam,tam);

			bmLike = Bitmap.CreateScaledBitmap (getBitmapFromAsset ("images/like.png"), Configuration.getWidth (43), Configuration.getWidth (35), true);

			drBack = new BitmapDrawable(Bitmap.CreateScaledBitmap (getBitmapFromAsset ("images/fondocondiagonalm.png"), 640, 1136, true));


			await ini();
			//LoadPagesDataSource ();

			SetContentView (mainLayout);
		} 




		async Task  ini(){

			mainLayout = new RelativeLayout (this);

			_progresD = new ProgressDialog (this);
			_progresD.SetCancelable (false);
			_progresD.SetMessage ("Wait please..");

			mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);	
			mainLayout.SetBackgroundColor(Color.ParseColor("#ffffff"));

			mainLayoutIndice = new RelativeLayout (this);
			mainLayoutIndice.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);	
			mainLayoutIndice.SetBackgroundColor(Color.ParseColor("#ffffff"));

			mainLayoutPages = new RelativeLayout (this);
			mainLayoutPages.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);	
			mainLayoutPages.SetBackgroundColor(Color.ParseColor("#ffffff"));
			viewPager = new ViewPager (this);
			viewPagerIni = new ViewPager (this);


			/*	layoutPanelScroll = new LinearLayout (this);
			layoutPanelScroll.LayoutParameters = new LinearLayout.LayoutParams (-1,-2);	
			layoutPanelScroll.SetBackgroundColor(Color.ParseColor("#ffffff"));
			layoutPanelScroll.Orientation = Orientation.Vertical;
*/
			/*	scrollVertical = new VerticalScrollView (this);
			scrollVertical.setOnScrollViewListener (this); 
			scrollVertical.LayoutParameters = new ViewGroup.LayoutParams (-1, -1);

			scrollVertical.SetX (0); scrollVertical.SetY (0);					


			scrollVertical.AddView (layoutPanelScroll);*/
			//mainLayoutIndice.AddView (scrollVertical);
			mainLayoutIndice.SetX (0); mainLayoutIndice.SetY (0);
			mainLayout.AddView (mainLayoutIndice);
			//mainLayout.AddView (scrollVertical);

			//var vm = this.ViewModel as LOViewModel;

			await vm.InitLoad();
			loadLOsInCircle(0);

			viewPagerIni.SetOnPageChangeListener (new MyPageChangeListener (this,listFront));
			viewPager.SetOnPageChangeListener (new MyPageChangeListenerPager (this, listFrontPager));
			vm.PropertyChanged += Vm_PropertyChanged;


		}

		void Vm_PropertyChanged (object sender, PropertyChangedEventArgs e)
		{

			//var vm = this.ViewModel as LOViewModel;
			if (e.PropertyName == "IsWaiting") {
				_progresD.Show ();
			}

			if (e.PropertyName == "LOsInCircle")
			if (vm.LOsInCircle != null) {
			}
			vm.LOsInCircle.CollectionChanged+= Vm_LOsInCircle_CollectionChanged;
		}

		void Vm_LOsInCircle_CollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{

			loadLOsInCircle (e.NewStartingIndex);		

		}

		void loadLOsInCircle(int index){


			//var vm = this.ViewModel as LOViewModel;
			if (vm.LOsInCircle != null) {		


				for (int i = 0; i < vm.LOsInCircle.Count; i++) {
					VerticalScrollViewPager scrollPager = new VerticalScrollViewPager (this);
					scrollPager.setOnScrollViewListener (this); 
					LinearLayout linearScroll = new LinearLayout (this);
					linearScroll.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
					linearScroll.Orientation = Orientation.Vertical;

					//	if(Configuration.IndiceActual==i){
					FrontContainerView front = new FrontContainerView (this);
					front.Tag = "indice";
					front.Author = vm.LOsInCircle [i].lo.name + " " + vm.LOsInCircle [i].lo.lastname;
					front.Chapter = vm.LOsInCircle [i].lo.description;
					front.NameLO = vm.LOsInCircle [i].lo.title;
					front.Like = "10";
					front.ImageChapter = vm.LOsInCircle [i].lo.url_background;

					listFront.Add (front);
					listFront [i].setBack (drBack,bmLike);


					/*
					if (vm.LOsInCircle [i].background_bytes != null) {
						Bitmap bm = BitmapFactory.DecodeByteArray (vm.LOsInCircle [i].background_bytes, 0, vm.LOsInCircle [i].background_bytes.Length);

						front.ImageChapterBitmap = bm;
						bm = null;
					}
					*/

					vm.LOsInCircle[i].PropertyChanged += (s1, e1) =>
					{
						if (e1.PropertyName == "background_bytes")
						{
							/*
							Bitmap bm = BitmapFactory.DecodeByteArray (vm.LOsInCircle [i].background_bytes, 0, vm.LOsInCircle [i].background_bytes.Length);
							front.ImageChapterBitmap = bm;
							bm = null;
							*/
							front.ImageChapter = vm.LOsInCircle [i].lo.url_background;

						}
					};								

					linearScroll.AddView (front);

					if (vm.LOsInCircle [i].stack.IsLoaded) {				
						var s_list = vm.LOsInCircle [i].stack.StacksList;
						int indice = 0;
						for (int j = 0; j < s_list.Count; j++) {				


							for (int k = 0; k < s_list [j].PagesList.Count; k++) {

								ChapterContainerView section = new ChapterContainerView (this);								
								section.Author = vm.LOsInCircle [i].lo.name + " " + vm.LOsInCircle [i].lo.lastname;					
								section.Title = s_list [j].PagesList [k].page.title;
								section.Container = s_list [j].PagesList [k].page.description;							
								section.ColorText = Configuration.ListaColores [indice % 6];
								section.setDefaultProfileUserBitmap (bm_user);


								section.Image = s_list [j].PagesList [k].page.url_img;
								/*
								if (s_list [j].PagesList [k].cover_bytes != null) {
									Bitmap bm = BitmapFactory.DecodeByteArray (s_list [j].PagesList [k].cover_bytes, 0, s_list [j].PagesList[k].cover_bytes.Length);
									section.ImageBitmap = bm;
									bm = null;
								}
								*/

								section.Indice = indice;								
								section.Click += delegate {
									

									IndiceSection = section.Indice; 										



									mainLayoutIndice.Visibility = ViewStates.Invisible;		



									if (ISLOADED == false) {		
									LoadPagesDataSource();
									} else {

									viewPager.CurrentItem= IndiceSection;
									mainLayoutPages.Visibility = ViewStates.Visible;
									
									mainLayoutIndice.Visibility = ViewStates.Invisible;


										
									}

								};
								linearScroll.AddView (section);
								indice++;
							}

						}
					} else {

						vm.LOsInCircle [i].stack.PropertyChanged+= (s3, e3) => {

							var s_list = vm.LOsInCircle [i].stack.StacksList;
							int indice = 0;
							for (int j = 0; j < s_list.Count; j++) {
								for (int k = 0; k < s_list [j].PagesList.Count; k++) {
									ChapterContainerView section = new ChapterContainerView (this);								
									section.Author = vm.LOsInCircle [i].lo.name + " " + vm.LOsInCircle [i].lo.lastname;					
									section.Title = s_list [j].PagesList [k].page.title;
									section.Container = s_list [j].PagesList [k].page.description;							
									section.ColorText = Configuration.ListaColores [indice % 6];
									section.setDefaultProfileUserBitmap (bm_user);

									section.Image = s_list [j].PagesList [k].page.url_img;
									/*
									if (s_list [j].PagesList [k].cover_bytes != null) {
										Bitmap bm = BitmapFactory.DecodeByteArray (s_list [j].PagesList [k].cover_bytes, 0, s_list [j].PagesList[k].cover_bytes.Length);
										section.ImageBitmap = bm;
										bm = null;
									}
									*/
									section.Indice = indice;								
									section.Click += delegate {




										IndiceSection = section.Indice; 										
										mainLayoutIndice.Visibility = ViewStates.Invisible;		


										if (ISLOADED == false) {	
										

										LoadPagesDataSource();


										} else {
											viewPager.CurrentItem= IndiceSection;
											mainLayoutPages.Visibility = ViewStates.Visible;
											mainLayoutIndice.Visibility = ViewStates.Invisible;
										}


									};
									linearScroll.AddView (section);
									indice++;
								}

							}
						};

					}

					scrollPager.VerticalScrollBarEnabled = false;
					scrollPager.AddView (linearScroll);

					listaScrollIni.Add (scrollPager);

				}
				mainLayoutIndice.RemoveAllViews ();
				//_progresD.Hide ();
				mainLayoutIndice.AddView (viewPagerIni);
				mainLayoutIndice.SetX (0);
				mainLayoutIndice.SetY (0);
				//mainLayout.AddView (mainLayoutIndice);
				LOViewAdapter adapter = new LOViewAdapter (this, listaScrollIni);
				viewPagerIni.Adapter = adapter;
				//viewPager.CurrentItem = IndiceSection;
			}

		}



		void LoadPagesDataSource()
		{


			//LOViewModel vm = ViewModel as LOViewModel;
			//var styles = new StyleConstants();
			//vm.IsLoading.Execute(null);
			bool is_main = true;

			for (int i = 0; i < vm.LOsInCircle.Count; i++)
			{
				var s_list = vm.LOsInCircle[i].stack.StacksList;
				//if (Configuration.IndiceActual == i) {
				int indice = 0;
				for (int j = 0; j < s_list.Count; j++) {						



					for (int k = 0; k < s_list [j].PagesList.Count; k++) {
						VerticalScrollViewPager scrollPager = new VerticalScrollViewPager (this);
						scrollPager.setOnScrollViewListener (this); 
						LinearLayout linearScroll = new LinearLayout (this);
						linearScroll.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
						linearScroll.Orientation = Orientation.Vertical;
						//LOPageSource page = new LOPageSource();
						var content = s_list [j].PagesList [k].content;
						FrontContainerViewPager front = new FrontContainerViewPager (this);
						front.Tag = "pager";

						//front.ImageChapter = vm.LOsInCircle [i].lo.url_background;
						front.ImageChapter = s_list [j].PagesList[k].page.url_img;
						/*
						Bitmap bm = BitmapFactory.DecodeByteArray (s_list [j].PagesList [k].cover_bytes, 0, s_list [j].PagesList [k].cover_bytes.Length);
						front.ImageChapterBitmap = bm;
						bm = null;
						*/

						front.Title = s_list [j].PagesList [k].page.title;
						front.Description =  s_list [j].PagesList [k].page.description;
						var slides = s_list [j].PagesList [k].content.lopage.loslide;
						front.setBack (drBack);

						//revisar si slides == null;

						//vm.OpenPageCommand.Execute (s_list [j].PagesList [k]);
						//var slides2 = s_list [j].PagesList [k].content.lopage.loslide.
						linearScroll.AddView (front);
						listFrontPager.Add (front);
						//listFrontPager [i].setBack (drBack);
						//vm.OpenPageCommand.Execute(s_list[j].PagesList[k]);
						var currentpage = s_list [j].PagesList [k];
						//var pagerr = s_list[j].PagesList[k].content.lopage.loslide

						for (int m = 1; m < slides.Count; m++) {
							LOSlideSource slidesource = new LOSlideSource(this);

							var _id_ = vm.LOsInCircle [i].lo.color_id;
							is_main = !is_main;
							//Console.WriteLine ("TIPOOOOOOOOOO = " + slides [m].lotype);	

							slidesource.ColorS = Configuration.ListaColores [indice % 6];

							slidesource.Type = slides[m].lotype;
							if (slides[m].lotitle != null) slidesource.Title = slides[m].lotitle;
							if (slides[m].loparagraph != null) slidesource.Paragraph = slides[m].loparagraph;
							if (slides[m].loimage != null) slidesource.ImageUrl = slides[m].loimage;
							if (slides[m].lotext != null) slidesource.Paragraph = slides[m].lotext;
							if (slides[m].loauthor != null) slidesource.Author = slides[m].loauthor;
							if (slides[m].lovideo != null) slidesource.VideoUrl = slides[m].lovideo;
							//if (slides[m].image_bytes != null) slidesource.ImageBytes = slides[m].image_bytes; 


							var c_slide = slides[m];
							/*
							c_slide.PropertyChanged+=(s,e)=>{
								if (e.PropertyName == "image_bytes" && c_slide.image_bytes != null)
									slidesource.ImageBytes = c_slide.image_bytes; 
							};
							*/

							if (c_slide.loitemize != null){
								slidesource.Itemize = new ObservableCollection<LOItemSource>();
								var items = c_slide.loitemize.loitem;
								for (int n = 0; n < items.Count; n++){ 
									LOItemSource item = new LOItemSource();
									if (items[n].loimage != null) item.ImageUrl = items[n].loimage;
									if (items[n].lotext != null) item.Text = items[n].lotext;
									//imagebytes
									//if (items[n].image_bytes != null) item.ImageBytes = items[n].image_bytes; 

									var c_item_ize = items[n];
									/*
									c_item_ize.PropertyChanged += (s1, e1) =>{
										if (e1.PropertyName == "image_bytes" && c_item_ize.image_bytes != null)
											item.ImageBytes = c_item_ize.image_bytes; 

										
									};
									*/
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


					//}

				}

				//}

			}
			mainLayoutPages.RemoveAllViews ();
			//_progresD.Hide ();
			mainLayoutPages.AddView (viewPager);
			mainLayoutPages.SetX (0);
			mainLayoutPages.SetY (0);
			mainLayout.AddView (mainLayoutPages);
			LOViewAdapter adapter = new LOViewAdapter (this, listaScroll);
			viewPager.Adapter = adapter;
			viewPager.CurrentItem = IndiceSection;

		}
		/*
		public void  OnScrollChanged(VerticalScrollView scrollView, int l, int t, int oldl, int oldt) {

			listFront[0].Imagen.SetY (scrollView.ScrollY / 2);
		//	listFront[0].Imagen.ScaleX =scrollView.ScrollY / 10;
		//	listFront[0].Imagen.ScaleY =scrollView.ScrollY / 10;

		}*/

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


		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s =this.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;

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


		public override void OnBackPressed ()
		{
			if (mainLayoutIndice.Visibility == ViewStates.Visible) {
				base.OnBackPressed ();
			}
			ISLOADED = true;
			mainLayoutIndice.Visibility = ViewStates.Visible;
			mainLayoutPages.Visibility = ViewStates.Invisible;


		}

	}
}