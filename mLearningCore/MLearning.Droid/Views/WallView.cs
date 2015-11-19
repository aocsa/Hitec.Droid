
using System;
using System.Collections.Generic;
using Android.Content;
using Android.Util;
using Android.Widget;
using Android.Graphics;

using Android.Graphics.Drawables;
using Square.Picasso;
using MLearning.Droid.Views;
using MLearning.Core.ViewModels;
using Android.Text;

namespace MLearning.Droid
{
	public class WallView: RelativeLayout
	{
		MainViewModel vm;
		public VerticalScrollView _scrollSpace;
		public LinearLayout header;
		public List<UnidadItem> _listUnidades = new List<UnidadItem>();
		ListView  _listViewUnidades;
		public List<ImageView> _listIconMap = new List<ImageView> ();

		public LinearLayout _spaceUnidades;
		public LinearLayout _mapSpace;

		public List<LinearLayout> _listLinearUnidades = new List<LinearLayout> ();


		RelativeLayout _mainLayout;
		RelativeLayout _fondo2;
		public TextView _txtCursoN;
		public TextView _txtUnidadN;

		LinearLayout linearGradiente;

		LinearLayout commentLayout;
		ListView commentList;
		List<CommentDataRow> _dataTemplateItem = new List<CommentDataRow> ();
		LinearLayout infoCursoUnidad;

		LinearLayout _workspace;

		public int currentLOImageIndex = 0;

		//section_1
		RelativeLayout _contentRLayout_S1;
		TextView _txtTitle_S1;
		TextView _txtAuthor_S1;
		ImageView _imAuthor_S1;
		TextView _txtChapter_S1;

		LinearLayout _itemsLayout_S1;
		List<ImageView> _imItem_S1;
		List<TextView> _txtItem_S1;

		List<ImageLOView> _ListLOImages_S2;

		public void setFooterBackground(Drawable background)
		{
			linearGradiente.SetBackgroundDrawable (background);
		}


		private CommentDataRow[] _listItems;
		public CommentDataRow[] ListItems{
			set{_listItems = value;
				for (int i = 0; i < _listItems.Length; i++) {
					_dataTemplateItem.Add (new CommentDataRow (){name = _listItems[i].name, im_profile = _listItems[i].im_profile, date = _listItems[i].date, comment = _listItems[i].comment });
					commentList.Adapter = new CommentAdapter (context, _dataTemplateItem);
					commentList.SetBackgroundColor (Color.White);
					commentList.DividerHeight = 0;
					commentList.Clickable = false;
					commentList.ChoiceMode = ChoiceMode.None;
				}

			}

		}


		string _title;
		public string Title{
			set{_title = value;
				_txtTitle_S1.Text = _title;}
		}

		string _author;
		public string Author{
			set{_author = value;
				_txtAuthor_S1.Text = _author;}
		}

		string _chapter;
		public string Chapter{
			set{_chapter = value;
				_txtChapter_S1.Text = _chapter;}
		}

		Bitmap _imageAuthor;
		public Bitmap ImageAuthor {
			set{_imageAuthor = value;
				_imAuthor_S1.SetImageBitmap (_imageAuthor);
				//_imageAuthor.Recycle ();
				_imageAuthor = null;
				}
		
		}

		string _info1;
		public string Info1{
			set{_info1 = value;
				_txtInfo1_S3.Text = _info1;}
		}

		string _info2;
		public string Info2{
			set{_info2 = value;
				_txtInfo2_S3.Text = _info2;}
		}
		string _info3;
		public string Info3{
			set{_info3 = value;
				_txtInfo3_S3.Text = _info3;}
		}


		public List<ImageLOView> ListImages{
			set{ _ListLOImages_S2 = value;
				_images_S2.RemoveAllViews ();	

				for (int i = 0; i < _ListLOImages_S2.Count; i++) {

					_images_S2.AddView (_ListLOImages_S2[i]);
					_ListLOImages_S2 [i].Click += imLoClick;

				}
			}

		}

		public LinearLayout getMapSpaceLayout{
			get{ return _mapSpace;}
		}

		public LinearLayout getWorkSpaceLayout{
			get{ return _workspace;}
		}

		public ImageView OpenUnits{
			get{return _imItems_S4 [0]; }
		}

		public ImageView OpenComments{
			get{return _imItems_S4 [1]; }
		}

		public ImageView OpenLO{
			get{return _imItems_S4 [2]; }
		}

		public ImageView OpenChat{
			get{return _imItems_S4 [3]; }
		}

		public ImageView OpenTasks{
			get{return _imItems_S4 [4]; }
		}
		//section_2


		public HorizontalScrollView _contentScrollView_S2;
		LinearLayout _images_S2;


		private void imLoClick(object sender, EventArgs eventArgs)
		{
			var textFormat = Android.Util.ComplexUnitType.Px;

			var imView = sender as ImageLOView;
			currentLOImageIndex = imView.index;
		
			var test = new ImageView (context);
			test.DrawingCacheEnabled = true;
			test.LayoutParameters = new LinearLayout.LayoutParams (-1, -1);

			Picasso.With (context).Load (imView.sBackgoundUrl).Resize(Configuration.getWidth(640),Configuration.getWidth(640)).CenterCrop().Into (test);
			_fondo2.SetVerticalGravity (Android.Views.GravityFlags.Start);
			_fondo2.RemoveAllViews();

			infoCursoUnidad.RemoveAllViews ();
			infoCursoUnidad.AddView (_txtCursoN);
			infoCursoUnidad.AddView (_txtUnidadN);

			_fondo2.AddView(test);
			//_txtCursoN.Text = "PROBANDO";
			//_txtUnidadN.Text = "PROBANDO";
			_txtCursoN.SetTextSize (textFormat,Configuration.getHeight(60));
			_txtUnidadN.SetTextSize (textFormat,Configuration.getHeight(50));

			_txtCursoN.SetTextColor (Color.ParseColor("#ffffff"));
			_txtCursoN.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/ArcherMediumPro.otf");

			_txtUnidadN.SetTextColor (Color.ParseColor("#ffffff"));
			_txtUnidadN.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/ArcherMediumPro.otf");
			_txtCursoN.Gravity = Android.Views.GravityFlags.Right;
			_txtUnidadN.Gravity = Android.Views.GravityFlags.Right;
			//_txtCursoN.TextAlignment = Android.Views.TextAlignment.Gravity;
				
				
			_fondo2.AddView (infoCursoUnidad);
			//infoCursoUnidad.SetX (Configuration.getWidth (300));
			infoCursoUnidad.SetY (Configuration.getWidth (420));

			//actualizar titulo, nombreAuthor, capitulo, imAuthor
		}

		//section_3

		LinearLayout _contentLLayout_S3;
		TextView _txtInfo1_S3;
		TextView _txtInfo2_S3;
		TextView _txtInfo3_S3;

		//section_4

		LinearLayout _contentLLayout_S4;
		List<ImageView> _imItems_S4;


		int widthInDp;
		int heightInDp;


		Context context;

		public WallView (Context context) :
		base (context)
		{
			this.context = context;
			Initialize ();
		}

		void Initialize ()
		{
			var metrics = Resources.DisplayMetrics;
			widthInDp = ((int)metrics.WidthPixels);
			heightInDp = ((int)metrics.HeightPixels);
			Configuration.setWidthPixel (widthInDp);
			Configuration.setHeigthPixel (heightInDp);


			ini ();


			this.AddView (_mainLayout);
		}




		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s = context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}

		public void ini(){


			_txtCursoN = new TextView (context);
			_txtCursoN.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			_txtUnidadN = new TextView (context);
			_mainLayout = new RelativeLayout (context);

			linearGradiente = new LinearLayout (context);
			linearGradiente.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (310));
			linearGradiente.SetBackgroundResource (Resource.Drawable.gradiente);


			var textFormat = Android.Util.ComplexUnitType.Px;

			_mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1, -1);

			_scrollSpace = new VerticalScrollView (context);
			_scrollSpace.LayoutParameters = new VerticalScrollView.LayoutParams (-1, Configuration.getHeight(1015));
			_scrollSpace.SetY (Configuration.getHeight (125));
			//_scrollSpace.SetBackgroundColor (Color.ParseColor ("#FF0000"));
			_mainLayout.AddView (_scrollSpace);

			_mapSpace = new LinearLayout (context);

			_mapSpace.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (1015));
			_mapSpace.SetY(Configuration.getHeight (125));
			_mainLayout.AddView (_mapSpace);


			LinearLayout _mainSpace = new LinearLayout (context);
			_mainSpace.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			_mainSpace.Orientation = Orientation.Vertical;

			_scrollSpace.AddView (_mainSpace);

			_fondo2 = new RelativeLayout (context);
			_fondo2.LayoutParameters = new RelativeLayout.LayoutParams (-1, Configuration.getWidth (640));
			_fondo2.SetY (Configuration.getHeight (0));

			Drawable dr1 = new BitmapDrawable (getBitmapFromAsset("icons/fondoselec.png"));
			_fondo2.SetBackgroundDrawable (dr1);
			dr1 = null;

			_mainSpace.AddView (_fondo2);

			infoCursoUnidad = new LinearLayout (context);
			infoCursoUnidad.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(250));
			infoCursoUnidad.Orientation = Orientation.Vertical;
			infoCursoUnidad.SetGravity(Android.Views.GravityFlags.Right);
			infoCursoUnidad.SetPadding (Configuration.getWidth(30), Configuration.getWidth (25), Configuration.getWidth(30), Configuration.getWidth (25));
			infoCursoUnidad.SetBackgroundColor (Color.ParseColor ("#40000000"));

			TextView _txtCurso = new TextView (context);
			_txtCurso.Text = "LAS RUTAS";
			_txtCurso.SetY (-100);

			//_mainSpace.AddView (_txtCurso);


			//section1-----------------------------------------------
			_contentRLayout_S1 = new RelativeLayout(context);
			_txtTitle_S1 = new TextView (context);
			_txtAuthor_S1 = new TextView (context);
			_imAuthor_S1 = new ImageView (context);
			_txtChapter_S1 = new TextView (context);

			_itemsLayout_S1 = new LinearLayout (context);//not used
			_imItem_S1 = new List<ImageView> ();
			_txtItem_S1 = new List<TextView>();





			//_mainLayout.AddView (_txtTitle_S1);
			//_mainLayout.AddView (_txtAuthor_S1);
			//_mainLayout.AddView (_imAuthor_S1);

			//_mainLayout.AddView (_txtChapter_S1);

			//_mainSpace.AddView (_txtChapter_S1);






			_contentRLayout_S1.LayoutParameters = new RelativeLayout.LayoutParams (-1, Configuration.getHeight (480));


			LinearLayout _linearTitle = new LinearLayout (context);
			_linearTitle.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			_linearTitle.SetGravity (Android.Views.GravityFlags.Center);
			_linearTitle.AddView (_txtTitle_S1);
			_linearTitle.SetY (Configuration.getHeight (60));

			linearGradiente.SetX (0); linearGradiente.SetY (Configuration.getHeight(860));
			//_mainLayout.AddView (linearGradiente);
			//_mainLayout.AddView (_linearTitle);

			//_txtTitle_S1.SetX (Configuration.getWidth (245));_txtTitle_S1.SetY (Configuration.getHeight (60));

			//Bitmap newbm = Configuration.getRoundedShape(Bitmap.CreateScaledBitmap( getBitmapFromAsset("icons/imgautor.png"), Configuration.getWidth(170), Configuration.getWidth(170), true),Configuration.getWidth(170),Configuration.getHeight(170));
		
			//_imAuthor_S1.SetImageBitmap (newbm);
		//	newbm.Recycle ();
			//newbm = null;

			//_imAuthor_S1.SetX (Configuration.getWidth (240));_imAuthor_S1.SetY (Configuration.getHeight (189));

			LinearLayout _linearAuthor = new LinearLayout (context);
			_linearAuthor.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			_linearAuthor.SetGravity (Android.Views.GravityFlags.Center);
			_linearAuthor.AddView (_txtAuthor_S1);
			_linearAuthor.SetY (Configuration.getHeight (378));
			//_mainLayout.AddView (_linearAuthor);

			//_txtAuthor_S1.SetX (Configuration.getWidth (228));_txtAuthor_S1.SetY (Configuration.getHeight (378));

			LinearLayout _linearChapter = new LinearLayout (context);
			_linearChapter.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			_linearChapter.SetGravity (Android.Views.GravityFlags.Center);
			//_linearChapter.AddView (_txtChapter_S1);
			_linearChapter.SetY (Configuration.getHeight (502));
			//_mainLayout.AddView (_linearChapter);


			//_txtChapter_S1.SetX (Configuration.getWidth (191));_txtChapter_S1.SetY (Configuration.getHeight (502));






			_txtTitle_S1.Text = "Camino Inca";
			_txtTitle_S1.SetTextColor (Color.White);
			_txtTitle_S1.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			_txtTitle_S1.SetTextSize (textFormat,Configuration.getHeight(30));

			_txtAuthor_S1.Text = "David Spencer";
			_txtAuthor_S1.SetTextColor (Color.White);
			_txtAuthor_S1.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			_txtAuthor_S1.SetTextSize (textFormat,Configuration.getHeight(30));
			//_txtAuthor_S1.SetBackgroundColor (Color.ParseColor ("#60000000"));
			_txtAuthor_S1.SetShadowLayer (50.8f, 0.0f, 0.0f, Color.ParseColor ("#000000"));



			_txtChapter_S1.Text = "FLORA Y FAUNA";
			_txtChapter_S1.SetTextColor (Color.White);
			_txtChapter_S1.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			_txtChapter_S1.SetTextSize (textFormat,Configuration.getHeight(35));


			List<string> item_path = new List<string> ();
			item_path.Add ("icons/icona.png");
			item_path.Add ("icons/iconb.png");
			item_path.Add ("icons/iconc.png");
			item_path.Add ("icons/icond.png");
			item_path.Add ("icons/icone.png");
			item_path.Add ("icons/iconf.png");
			item_path.Add ("icons/icong.png");


			int inixItemIM = Configuration.getWidth (33);
			int crecIM = Configuration.getWidth (90);

			int inixItemTXT = Configuration.getWidth (42);
			int crecTXT = Configuration.getWidth (90);
			int inixLinea = Configuration.getWidth (93);

			for (int i = 0; i < item_path.Count; i++) {
				
				_imItem_S1.Add(new ImageView(context));
				_imItem_S1[i].SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset(item_path[i]), Configuration.getWidth (30), Configuration.getWidth (30), true));
				//_mainLayout.AddView (_imItem_S1 [i]);
				_imItem_S1 [i].SetX (inixItemIM+(i*crecIM));_imItem_S1 [i].SetY (Configuration.getHeight(602));


				if (i != item_path.Count - 1) {
					ImageView linea = new ImageView (context);
					linea.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset ("icons/lineatareas.png"), 1, Configuration.getHeight (68), true));
					//_mainLayout.AddView (linea);
					linea.SetX (inixLinea + (i * crecIM));
					linea.SetY (Configuration.getHeight (605));
					linea = null;
				}



				_txtItem_S1.Add (new TextView (context));
				_txtItem_S1 [i].Text = "0";
				_txtItem_S1 [i].SetTextColor (Color.ParseColor ("#2E9AFE"));
				_txtItem_S1[i].Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
				_txtItem_S1[i].SetTextSize (textFormat,Configuration.getHeight(30));
				//_mainLayout.AddView (_txtItem_S1 [i]);
				_txtItem_S1 [i].SetX (inixItemTXT+(i*crecTXT));_txtItem_S1 [i].SetY (Configuration.getHeight(640));
				_imItem_S1 [i] = null;

			}
			_imItem_S1 = null;


			//-------------------------------------------------------


			//section2------------------------------------------------

			_contentScrollView_S2 = new HorizontalScrollView (context);
			_contentScrollView_S2.LayoutParameters = new HorizontalScrollView.LayoutParams (-1, Configuration.getWidth(160));
			_contentScrollView_S2.HorizontalScrollBarEnabled = false;

			_images_S2 = new LinearLayout (context);
			_images_S2.Orientation = Orientation.Horizontal;
			_images_S2.LayoutParameters = new LinearLayout.LayoutParams(-2,-1);

			_contentScrollView_S2.SetX (0);


			_contentScrollView_S2.AddView (_images_S2);

			//----------------------------------------------------------

			//section3------------------------------------------------

			_contentLLayout_S3 = new LinearLayout (context);
			_contentLLayout_S3.Orientation = Orientation.Vertical;
			_contentLLayout_S3.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (160));
			_contentLLayout_S3.SetX (0);_contentLLayout_S3.SetY (Configuration.getHeight(875));
			_contentLLayout_S3.SetGravity (Android.Views.GravityFlags.Center);


			_txtInfo1_S3 = new TextView (context);
			_txtInfo2_S3 = new TextView (context);
			_txtInfo3_S3 = new TextView (context);

			_txtInfo1_S3.Text = "Duración: 05 dias / 04 noches ";
			_txtInfo2_S3.Text = "Distancia: 65km";
			_txtInfo3_S3.Text = "Punto mas elevado: 4,6386 msnm (Salkantay)";

			_txtInfo1_S3.Gravity = Android.Views.GravityFlags.CenterHorizontal;
			_txtInfo2_S3.Gravity = Android.Views.GravityFlags.CenterHorizontal;
			_txtInfo3_S3.Gravity = Android.Views.GravityFlags.CenterHorizontal;


			_txtInfo1_S3.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			_txtInfo2_S3.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
			_txtInfo3_S3.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");

			_txtInfo1_S3.SetTextSize (textFormat,Configuration.getHeight(30));
			_txtInfo2_S3.SetTextSize (textFormat,Configuration.getHeight(30));
			_txtInfo3_S3.SetTextSize (textFormat,Configuration.getHeight(30));

			_txtInfo1_S3.SetTextColor (Color.White);
			_txtInfo2_S3.SetTextColor (Color.White);
			_txtInfo3_S3.SetTextColor (Color.White);


			_contentLLayout_S3.AddView (_txtInfo1_S3);
			_contentLLayout_S3.AddView (_txtInfo2_S3);
			_contentLLayout_S3.AddView (_txtInfo3_S3);

			//Drawable dr3 = new BitmapDrawable (getBitmapFromAsset("icons/fondonotif.png"));
			//_contentLLayout_S3.SetBackgroundDrawable(dr3);
				//_contentLLayout_S3.SetBackgroundColor(Color.ParseColor("#80000000"));
			//_mainLayout.AddView (_contentLLayout_S3);

			//_mainLayout.AddView (_contentScrollView_S2);

			_mainSpace.AddView (_contentScrollView_S2);
			//----------------------------------------------------------
			/*
			_listUnidades.Add(new UnidadItem{ Title = "Dia 1", Description = "Piscacucho-Wayllabamba" });
			_listUnidades.Add(new UnidadItem{ Title = "Dia 2", Description = "Wayllabamba-Pacaymayo" });
			_listUnidades.Add(new UnidadItem{ Title = "Dia 3", Description = "Pacaymayo-Wiñay Wayna" });
			_listUnidades.Add(new UnidadItem{ Title = "Dia 4", Description = "WIñay Wayna-Machu PIcchu"});
		*/

			/*
			_listViewUnidades = new ListView(context);
			_listViewUnidades.Adapter = new UnidadAdapter (context, _listUnidades);

			_mainSpace.AddView (_listViewUnidades);
			*/

			_spaceUnidades = new LinearLayout (context);
			_spaceUnidades.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			_spaceUnidades.Orientation = Orientation.Vertical;
			_spaceUnidades.SetBackgroundColor (Color.White);
			_mainSpace.AddView (_spaceUnidades);

			//section4------------------------------------------------
			_imItems_S4 = new List<ImageView>();


			List<string> botton_icon_path = new List<string> ();
			botton_icon_path.Add ("icons/btnhome.png");
			botton_icon_path.Add ("icons/btncomentariosazul.png");
			botton_icon_path.Add ("icons/btncontenido.png");
			botton_icon_path.Add ("icons/btnchatazul.png");
			botton_icon_path.Add ("icons/btnmap.png");

			_imItems_S4.Add (new ImageView (context));
			_imItems_S4[0].SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset(botton_icon_path[0]), Configuration.getWidth (40), Configuration.getWidth (40), true));
			//_mainLayout.AddView (_imItems_S4[0]);
			_imItems_S4[0].SetX (Configuration.getWidth(58));_imItems_S4[0].SetY (Configuration.getHeight(1069));
			_imItems_S4 [0].Visibility = Android.Views.ViewStates.Invisible;

			_imItems_S4.Add (new ImageView (context));
			_imItems_S4[1].SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset(botton_icon_path[1]), Configuration.getWidth (78), Configuration.getWidth (55), true));
			//_mainLayout.AddView (_imItems_S4[1]);
			_imItems_S4[1].SetX (Configuration.getWidth(169));_imItems_S4[1].SetY (Configuration.getHeight(1069));
			_imItems_S4 [1].Visibility = Android.Views.ViewStates.Invisible;




			_imItems_S4.Add (new ImageView (context));
			_imItems_S4[2].SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset(botton_icon_path[2]), Configuration.getWidth (80), Configuration.getWidth (80), true));
			//_mainLayout.AddView (_imItems_S4 [2]);
			_imItems_S4[2].SetX (Configuration.getWidth(297));_imItems_S4[2].SetY (Configuration.getHeight(1050));
			_imItems_S4 [2].Visibility = Android.Views.ViewStates.Invisible;



			_imItems_S4.Add (new ImageView (context));
			_imItems_S4[3].SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset(botton_icon_path[3]), Configuration.getWidth (30), Configuration.getWidth (51), true));
			//_mainLayout.AddView (_imItems_S4[3]);
			_imItems_S4[3].SetX (Configuration.getWidth(421));_imItems_S4[3].SetY (Configuration.getHeight(1069));
			_imItems_S4 [3].Visibility = Android.Views.ViewStates.Invisible;


			_imItems_S4.Add (new ImageView (context));
			_imItems_S4[4].SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset(botton_icon_path[4]), Configuration.getWidth (30), Configuration.getWidth (40), true));
			//_mainLayout.AddView (_imItems_S4 [4]);
			_imItems_S4[4].SetX (Configuration.getWidth(540));_imItems_S4[4].SetY (Configuration.getHeight(1069));
			_imItems_S4 [4].Visibility = Android.Views.ViewStates.Invisible;

			//----------------------------------------------------------

			Drawable dr = new BitmapDrawable (getBitmapFromAsset("images/header1.png"));
			header = new LinearLayout(context);
			header.LayoutParameters = new LinearLayout.LayoutParams (-1,Configuration.getHeight(125));
			header.Orientation = Orientation.Vertical;

			header.SetBackgroundDrawable (dr);


			//_mainLayout.SetBackgroundDrawable (dr);
			_mainLayout.AddView(header);
			dr = null;





		
			_workspace = new LinearLayout (context);
			_workspace.LayoutParameters = new LinearLayout.LayoutParams (-1, -1);
			//_workspace.SetBackgroundColor (Color.ParseColor ("#ffffff"));
			//_workspace.SetY (Configuration.getHeight (110));

			_mainLayout.AddView (_workspace);
			//_mainLayout.SetBackgroundColor (Color.ParseColor ("#ffffff"));
			//_workspace.AddView (_foro);
			//_workspace.Visibility = Android.Views.ViewStates.Invisible;


		}


		public void initUnidades(int indexCurso, int indexUnidad)
		{
			var textFormat = Android.Util.ComplexUnitType.Px;
			_spaceUnidades.RemoveAllViews ();
			_listLinearUnidades.Clear ();
			_listIconMap.Clear ();
			int numUnidades = _listUnidades.Count;
			for (int i = 0; i < numUnidades; i++) 
			{
				LinearLayout linearUnidad = new LinearLayout (context);
				linearUnidad.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
				linearUnidad.Orientation = Orientation.Horizontal;
				linearUnidad.SetGravity (Android.Views.GravityFlags.CenterVertical);

				IconImageMap icon = new IconImageMap (context);
				icon.indexCurso = indexCurso;
				icon.indexUnidad = indexUnidad;
				icon.indexLO = i;

				icon.SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/iconmap.png"), Configuration.getWidth (60), Configuration.getWidth (80), true));
				icon.SetX (Configuration.getWidth (60));

				if (indexCurso == 0) {
					linearUnidad.AddView (icon);
					_listIconMap.Add (icon);
				}

				LinearLayout linearContenido = new LinearLayout (context);
				linearContenido.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth(460), -2);
				linearContenido.Orientation = Orientation.Vertical;
				linearContenido.SetGravity (Android.Views.GravityFlags.Center);
				linearContenido.SetX(Configuration.getWidth (100));

				TextView titleUnidad = new TextView(context);
				titleUnidad.Text = _listUnidades [i].Title;
				titleUnidad.SetTextColor(Color.ParseColor (Configuration.ListaColores [i % 6]));
				titleUnidad.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
				titleUnidad.SetTextSize (textFormat,Configuration.getHeight(40));

				TextView descriptionUnidad = new TextView(context);
				descriptionUnidad.TextFormatted = Html.FromHtml (_listUnidades [i].Description);
				//descriptionUnidad.Text = _listUnidades [i].Description;
				descriptionUnidad.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
				descriptionUnidad.SetTextSize (textFormat,Configuration.getHeight(30));
				//descriptionUnidad.SetTextIsSelectable (true);

				linearContenido.AddView (titleUnidad);
				linearContenido.AddView (descriptionUnidad);


				if (indexCurso == 2) {
					linearContenido.RemoveViewAt (1);
					ImageView imgUnidad = new ImageView (context);
					Picasso.With (context).Load (_listUnidades[i].ImageUrl).Resize(Configuration.getWidth(500),Configuration.getHeight(400)).CenterInside().Into (imgUnidad);
					linearContenido.AddView (imgUnidad);
				}

				linearUnidad.AddView (linearContenido);

				_listLinearUnidades.Add (linearUnidad);
				LinearLayout separationLinear = new LinearLayout (context);
				separationLinear.LayoutParameters = new LinearLayout.LayoutParams (-1, 5);
				separationLinear.SetBackgroundColor (Color.ParseColor ("#D8D8D8"));
				separationLinear.Orientation = Orientation.Horizontal;
				linearUnidad.SetPadding (0, Configuration.getWidth (25), 0, Configuration.getWidth (25));

				//linearUnidad.AddView (separationLinear);
				_spaceUnidades.AddView (linearUnidad);
				_spaceUnidades.AddView (separationLinear);
			}
		}
	}
}

