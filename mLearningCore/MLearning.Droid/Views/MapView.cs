
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Text;
using Square.Picasso;
using MLearning.Droid.Views;
using Android.Graphics.Drawables;

namespace MLearning.Droid
{
	public class MapView : RelativeLayout
	{
		RelativeLayout mainLayout;
		LinearLayout mapSpace;
		public VerticalScrollView placeSpace;
		public ImageView mapImage;
		public List<LinearLayout> _placesLayout = new List<LinearLayout>();
		public List<MapItemInfo> _placesData = new List<MapItemInfo> ();
		PopupWindow infoPopUp; 
		LinearLayout fullInfo;
		public LinearLayout header;
		public TextView titulo_header;
		public string titulo_map_header;

		public List<PlaceItem> _currentPlaces = new List<PlaceItem>();
		public List<Tuple<int,int>> _positionCurrentPlaces = new List<Tuple<int, int>> ();
		public ListView  listPlaces;

		public LinearLayout placesInfoLayout;
		public LinearLayout placesContainer;

		public String titulo;
		public String descripcion;
		public String mapUrl;

		public List<LinearLayoutLO> _listLinearPlaces = new List<LinearLayoutLO> ();
		public List<ImageIconMap> _listLinearPositonPlaces = new List<ImageIconMap> ();

		int widthInDp;
		int heightInDp;

		Context context;

		VerticalScrollView scrollPlaces;

		public bool placeInfoOpen = false;

		public MapView (Context context) :
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
			//iniNotifList ();
			this.AddView (mainLayout);

		}


		public Bitmap getBitmapFromAsset( String filePath) {
			System.IO.Stream s = context.Assets.Open (filePath);
			Bitmap bitmap = BitmapFactory.DecodeStream (s);

			return bitmap;
		}

		public void setMapImage(String url)
		{
			
			Picasso.With (context).Load (url).Resize(Configuration.WIDTH_PIXEL,Configuration.getHeight(800)).Placeholder(context.Resources.GetDrawable (Resource.Drawable.progress_animation)).CenterCrop().Into (mapImage);
			mapImage.Click += delegate {
			/*	Intent lance = new Intent();
				lance.SetAction(Intent.ActionView);
				String typedata = "image/*";
				lance.SetType(typedata);
				String phuri = url;
				Android.Net.Uri uri = Android.Net.Uri.Parse(phuri);

				lance.SetDataAndType(uri,typedata);
				context.StartActivity(lance);
				*/
				/*
				mapImage.PivotX = 100;
				mapImage.PivotY = 50;
				mapImage.ScaleX = 3;
				mapImage.ScaleY = 3;
				*/
			};



		}

		public void showFocusMap(int position)
		{
			var posXY = _positionCurrentPlaces [position];

			mapImage.PivotX = posXY.Item1;
			mapImage.PivotY = posXY.Item2;
			mapImage.ScaleX = 3;
			mapImage.ScaleY = 3;
		}

		public void ini(){

			Drawable dr = new BitmapDrawable (getBitmapFromAsset("images/1header.png"));
			header = new LinearLayout(context);
			header.LayoutParameters = new LinearLayout.LayoutParams (-1,Configuration.getHeight(125));
			header.Orientation = Orientation.Horizontal;
			header.SetGravity (GravityFlags.Center);
			header.SetBackgroundDrawable (dr);


			titulo_header = new TextView (context);
			titulo_header.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			titulo_header.TextSize = Configuration.getHeight (20);
			titulo_header.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/ArcherMediumPro.otf");
			titulo_header.SetTextColor (Color.White);
			titulo_header.Gravity = GravityFlags.Center;
			//titulo_header.TextAlignment = TextAlignment.Center;

			placesInfoLayout = new LinearLayout (context);
			placesInfoLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			int space = Configuration.getWidth (30);
			placesInfoLayout.SetPadding(space,space,space,space);
			placesInfoLayout.Orientation = Orientation.Vertical;

			mainLayout = new RelativeLayout (context);
			mainLayout.LayoutParameters = new RelativeLayout.LayoutParams (-1,-1);

			mainLayout.AddView (header);

			mapImage = new ImageView (context);
			mapSpace = new LinearLayout (context);
			mapSpace.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(675));
			mapSpace.SetY (Configuration.getHeight (125));
			mapSpace.SetGravity (GravityFlags.CenterHorizontal);
			mapSpace.AddView (mapImage);



			placeSpace = new VerticalScrollView (context);
			placeSpace.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(345));
			placeSpace.SetY (Configuration.getHeight (800));
			placeSpace.SetBackgroundColor (Color.White);

			placesContainer = new LinearLayout (context);
			placesContainer.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(345));
			placesContainer.Orientation = Orientation.Vertical;


			mainLayout.AddView (mapSpace);
			mainLayout.AddView (placeSpace);

			scrollPlaces = new VerticalScrollView (context);
			scrollPlaces.LayoutParameters = new VerticalScrollView.LayoutParams (-1,Configuration.getHeight(1011));
			scrollPlaces.AddView (placesInfoLayout);
			scrollPlaces.SetY (Configuration.getHeight (125));


			//mainLayout.AddView (placesInfoLayout);

			//iniPlancesList ();

		}

		public void showPLaceInfo(int position)
		{
			mainLayout.AddView(scrollPlaces);

			titulo_header.Text = _currentPlaces [position].titulo;

			scrollPlaces.SetBackgroundColor (Color.White);
			placeInfoOpen = true;
			placesInfoLayout.RemoveAllViews ();
			int space = Configuration.getWidth (15);
			var extraInfo = _placesData [position].placeExtraInfo;
			for (int i = 0; i < extraInfo.Count; i++) {
				TextView detalle = new TextView (context);
				detalle.TextSize = Configuration.getHeight (15);
				detalle.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/ArcherMediumPro.otf");


				detalle.Text = extraInfo [i].detalle;

				String url = extraInfo [i].url;
				ImageView image = new ImageView (context);
				if(extraInfo [i].detalle!=null)
				{
					image.SetPadding (0, space, 0, space);
				}

				Picasso.With (context).Load (url).Placeholder(context.Resources.GetDrawable (Resource.Drawable.progress_animation)).Resize(Configuration.getWidth(640),Configuration.getHeight(640)).CenterInside().Into (image);
				placesInfoLayout.AddView (detalle);
				placesInfoLayout.AddView (image);

			}
			placesInfoLayout.SetBackgroundColor (Color.White);
		}

		public void hidePlaceInfo()
		{
			mainLayout.RemoveView (scrollPlaces);
			//placesInfoLayout.RemoveAllViews ();
			//placesInfoLayout.SetBackgroundColor (Color.Transparent);
		}

		public void iniPlancesList()
		{
			//_currentPlaces.Clear ();
			_listLinearPlaces.Clear();
			placeSpace.RemoveAllViews ();
			placesContainer.RemoveAllViews ();

			VerticalScrollView listScrollPlaces = new VerticalScrollView (context);
			listScrollPlaces.LayoutParameters = new VerticalScrollView.LayoutParams (-1, Configuration.getHeight (345));

			LinearLayout listSpaceLayout = new LinearLayout(context);
			listSpaceLayout.LayoutParameters = new LinearLayout.LayoutParams (-1, -2);
			listSpaceLayout.Orientation = Orientation.Vertical;

			for (int i = 0; i < _currentPlaces.Count; i++) {

				var item = _currentPlaces [i];

				LinearLayoutLO linearItem = new LinearLayoutLO (context);
				linearItem.index = i;
				TextView txtName = new TextView (context);
				ImageView imgIcon = new ImageView (context);

				txtName.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (400), -1);
				txtName.Gravity = GravityFlags.CenterVertical;

				txtName.Text = item.titulo;
				//txtName.SetTextColor (Color.ParseColor ("#ffffff"));
				txtName.Typeface =  Typeface.CreateFromAsset(context.Assets, "fonts/HelveticaNeue.ttf");
				txtName.TextSize = Configuration.getHeight (15);
				//imgIcon.SetImageBitmap (Bitmap.CreateScaledBitmap (getBitmapFromAsset (item.Asset), Configuration.getWidth (30), Configuration.getWidth (30), true));

				int H = 80;
				int W = 120;

				linearItem.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight (H));
				//linearItem.SetBackgroundDrawable (background_row);
				linearItem.Orientation = Orientation.Horizontal;
				linearItem.SetGravity (Android.Views.GravityFlags.CenterVertical);
				//linearItem.AddView (imgIcon);


				RelativeLayout imageLayout = new RelativeLayout (context);
				imageLayout.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (W), Configuration.getHeight (H));
				ImageView iconImage = new ImageView (context);
				Picasso.With (context).Load (item.pathIcon).Resize(Configuration.getWidth(W),Configuration.getHeight(H)).CenterCrop().Into (iconImage);
				imageLayout.AddView (iconImage);

				LinearLayout gradiente = new LinearLayout (context);
				gradiente.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (W), Configuration.getHeight (H));

				imageLayout.AddView (gradiente);

				ImageIconMap icon = new ImageIconMap (context);
				icon.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (W), Configuration.getHeight (H));
				//icon.SetBackgroundResource (Resource.Drawable.mapIcon);
				//icon.indexCurso = indexCurso;
				//icon.indexUnidad = indexUnidad;
				//icon.SetBackgroundResource
				icon.index = i;
				icon.SetImageBitmap(Bitmap.CreateScaledBitmap (getBitmapFromAsset("icons/map2.jpg"), Configuration.getWidth (W), Configuration.getWidth (H), true));
				RelativeLayout iconLayout = new RelativeLayout (context);
				//iconLayout.SetGravity (GravityFlags.Center);
				iconLayout.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (W), Configuration.getHeight (H));
				//iconLayout.SetBackgroundColor (Color.White);
				//iconLayout.SetGravity (GravityFlags.End);
				//iconLayout.SetX(Configuration.getWidth(580));

				LinearLayout gradiente2 = new LinearLayout (context);
				gradiente2.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth (W), Configuration.getHeight (H));

				iconLayout.AddView (icon);
				iconLayout.AddView (gradiente2);

				linearItem.AddView (imageLayout);
				linearItem.AddView (txtName);
				linearItem.AddView (iconLayout);
				int space = Configuration.getWidth (30);
				//linearItem.SetPadding (space,0,space,0);
				//imgIcon.SetPadding (Configuration.getWidth(68), 0, 0, 0);
				txtName.SetPadding (Configuration.getWidth(10), 0, 0, 0);

				if (i % 2 == 0) {
				gradiente.SetBackgroundResource (Resource.Drawable.gradiente2);
				gradiente2.SetBackgroundResource (Resource.Drawable.gradiente22);
				linearItem.SetBackgroundColor (Color.ParseColor ("#F0AE11"));
				txtName.SetTextColor (Color.White);
				} else {
				gradiente.SetBackgroundResource (Resource.Drawable.gradiente1);
				gradiente2.SetBackgroundResource (Resource.Drawable.gradiente11);
				txtName.SetTextColor (Color.ParseColor("#F0AE11"));
				}

				_listLinearPlaces.Add (linearItem);
				_listLinearPositonPlaces.Add (icon);
				listSpaceLayout.AddView (linearItem);

			}

			/*
			listPlaces = new ListView (context);
			listPlaces.LayoutParameters = new LinearLayout.LayoutParams (-1, Configuration.getHeight(345));

			listPlaces.Adapter = new PlaceAdapter (context, _currentPlaces);
			listPlaces.DividerHeight = 0;

			placesContainer.AddView (listPlaces);
*/
			placesContainer.AddView (listSpaceLayout);
			placeSpace.AddView(placesContainer);

			titulo_header.Text = titulo_map_header;
			header.AddView (titulo_header);



		}


		public void updatePlaces()
		{
			for (int i = 0; i < _placesLayout.Count; i++) 
			{
				_placesLayout [i].Click += showPopOut;
				mainLayout.AddView (_placesLayout [i]);
			}
		}

		private void showPopOut (object sender, EventArgs e)
		{
			LinearLayout popOutLayout = new LinearLayout (context);
			popOutLayout.LayoutParameters = new LinearLayout.LayoutParams (Configuration.getWidth(500),Configuration.getWidth(500));
			popOutLayout.SetBackgroundColor (Color.White);


			//infoPopUp.ContentView = popOutLayout;
			infoPopUp.ShowAsDropDown(popOutLayout);
		}

	}
}
	